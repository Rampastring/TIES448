using System;
using System.Collections.Generic;

namespace Hassembler
{
    /// <summary>
    /// Base class for an expression node.
    /// </summary>
    public abstract class ExprNode
    {
        protected const string WasmIntFormat = "i32";
        private const int InitialWasmLineDepth = 2;
        private const int WasmBlockDepth = 2;

        public ExprNode(NodeContext context)
        {
            Context = context;
        }

        /// <summary>
        /// The context of the node.
        /// </summary>
        public NodeContext Context { get; }

        /// <summary>
        /// Returns the parent of the node, or null if the node has no parent.
        /// </summary>
        public ExprNode Parent => Context.Parent;

        /// <summary>
        /// Returns the environment associated with this node.
        /// </summary>
        protected IEnv Env => Context.Env;


        /// <summary>
        /// Adds a child node to this node.
        /// Throws a <see cref="ASTException"/> if this node does not accept more children.
        /// </summary>
        /// <param name="context">The parser context.</param>
        /// <param name="child">The child to add.</param>
        public virtual void AddChildNode(ExprNode child)
        {
            throw new ASTException("This node type does not accept children!");
        }

        /// <summary>
        /// Checks if this node can accept a child node.
        /// </summary>
        public virtual bool CanAcceptChildNode() => false;

        /// <summary>
        /// Statically checks that the type of this
        /// expression and sub-expressions make sense.
        /// </summary>
        public virtual void TypeCheck() { }

        /// <summary>
        /// Checks that the type of this node's result matches the given type.
        /// Throws a <see cref="TypeError"/> if the type does not match.
        /// </summary>
        /// <param name="expectedType">The expected type.</param>
        /// <param name="message">The error message of the thrown exception.</param>
        public void CheckNodeType(Type expectedType, string message)
        {
            Type nodeType = GetExpectedResultType();
            if (nodeType != expectedType && nodeType != typeof(object))
            {
                throw new TypeError(expectedType, nodeType, Context, message);
            }
        }

        /// <summary>
        /// Returns the expected type of the node.
        /// Type of Object means any type.
        /// </summary>
        public virtual Type GetExpectedResultType() => typeof(object);

        /// <summary>
        /// Resolves and returns the value of the node.
        /// </summary>
        public abstract Result GetValue();

        /// <summary>
        /// Generates and returns the WebAssembly (WAT) representation of the node.
        /// </summary>
        public abstract string ToWebAssembly();

        /// <summary>
        /// Defines how much this node is indented and indents children 
        /// when generating WebAssembly code.
        /// </summary>
        public virtual int WasmIndentDepth => 1;

        protected string GetWebAsmLine(string contents)
        {
            string indent = "";
            int depth = GetDepth();
            for (int i = 0; i < depth; i++)
                indent += " ";
            return indent + contents + Environment.NewLine;
        }

        private int GetDepth()
        {
            return InitialWasmLineDepth + GetExpressionDepth() * WasmBlockDepth;
        }

        private int GetExpressionDepth()
        {
            int depth = WasmIndentDepth;
            ExprNode parent = Parent;
            while (parent != null)
            {
                depth += parent.WasmIndentDepth;
                parent = parent.Parent;
            }
            return depth;
        }
    }

    /// <summary>
    /// Base class for an expression node that has a left and right child.
    /// </summary>
    abstract class ParentNode : ExprNode
    {
        public ParentNode(NodeContext context) : base(context)
        {
        }

        public ExprNode Right { get; private set; }
        public ExprNode Left { get; private set; }

        public override void AddChildNode(ExprNode child)
        {
            if (Left == null)
                Left = child;
            else if (Right == null)
                Right = child;
            else
                throw new ASTException("ParentNode: No unfilled child node!");
        }

        public override bool CanAcceptChildNode() => Left == null || Right == null;
    }

    /// <summary>
    /// An expression node for an if then else structured expression.
    /// </summary>
    class ITENode : ExprNode
    {
        public ITENode(NodeContext context) : base(context)
        {
        }

        public ExprNode Condition { get; private set; }
        public ExprNode ThenExpr { get; private set; }
        public ExprNode ElseExpr { get; private set; }

        public override void AddChildNode(ExprNode child)
        {
            if (Condition == null)
                Condition = child;
            else if (ThenExpr == null)
                ThenExpr = child;
            else if (ElseExpr == null)
                ElseExpr = child;
            else
                throw new VisitException(child.Context, "ITENode.AddChildNode: No free child node left!");
        }

        public override bool CanAcceptChildNode() => Condition == null || ThenExpr == null || ElseExpr == null;

        public override void TypeCheck()
        {
            Condition.CheckNodeType(typeof(bool), "Condition must be a Bool");
            Condition.TypeCheck();
            ThenExpr.TypeCheck();
            ElseExpr.TypeCheck();

            if (ThenExpr.GetExpectedResultType() != typeof(object) &&
                ElseExpr.GetExpectedResultType() != typeof(object) &&
                ThenExpr.GetExpectedResultType() != ElseExpr.GetExpectedResultType())
            {
                throw new TypeError(Context, "Types of 'then' and 'else' expressions need to match");
            }
        }

        public override Result GetValue() =>
            Condition.GetValue().GetResult<bool>() 
            ? ThenExpr.GetValue() : ElseExpr.GetValue();

        public override string ToWebAssembly()
        {
            return $"(\n{ThenExpr.ToWebAssembly()}\n{ElseExpr.ToWebAssembly()}"
            + $"\n{Condition.ToWebAssembly()}\nselect\n)";
        }
    }

    /// <summary>
    /// An expression node that contains parentheses.
    /// </summary>
    class ParenthesesNode : ExprNode
    {
        public ParenthesesNode(NodeContext context) : base(context)
        {
        }

        public ExprNode Follower { get; set; }


        public override void AddChildNode(ExprNode child)
        {
            if (Follower == null)
                Follower = child;
            else
                throw new VisitException(child.Context, "ParenthesesNode.AddChildNode: a child already exists!");
        }

        public override bool CanAcceptChildNode() => Follower == null;

        public override void TypeCheck()
        {
            Follower.TypeCheck();
        }

        public override Result GetValue()
        {
            return Follower.GetValue();
        }

        public override string ToWebAssembly() => Follower.ToWebAssembly();
    }

    /// <summary>
    /// An expression node that is an integer.
    /// </summary>
    class IntNode : ExprNode
    {
        public IntNode(NodeContext context, int value) : base(context)
        {
            Value = value;
        }


        public int Value { get; }

        public override Type GetExpectedResultType() => typeof(int);

        public override Result GetValue() => new Result(Value);

        public override int WasmIndentDepth => 0;

        public override string ToWebAssembly() => GetWebAsmLine($"{WasmIntFormat}.const {Value}");
    }

    /// <summary>
    /// An expression node that is a boolean.
    /// </summary>
    class BoolNode : ExprNode
    {
        public BoolNode(NodeContext context, bool value) : base(context)
        {
            Value = value;
        }

        public bool Value { get; }

        public override Type GetExpectedResultType() => typeof(bool);

        public override Result GetValue() => new Result(Value);

        public override string ToWebAssembly()
        {
            int i = Value ? 1 : 0;
            return $"{WasmIntFormat}.const {i}";
        }
    }

    /// <summary>
    /// An expression node that refers to a function.
    /// </summary>
    class FunctionReferenceNode : ExprNode
    {
        public FunctionReferenceNode(NodeContext context, string functionName, int paramCount) : base(context)
        {
            if (string.IsNullOrEmpty(functionName))
                throw new ArgumentNullException("functionName");

            FunctionName = functionName;
            ParamLimit = paramCount;
            parameterNodes = new List<ExprNode>(paramCount);
        }


        /// <summary>
        /// A list of parameter values to be delivered to the function,
        /// </summary>
        private List<ExprNode> parameterNodes;

        /// <summary>
        /// The name of the function.
        /// </summary>
        public string FunctionName { get; }

        /// <summary>
        /// The number of parameters of the function reference node.
        /// Given when the function reference node is created based on the parse tree.
        /// </summary>
        public int ParamLimit { get; }

        /// <summary>
        /// Checks whether the function reference node has
        /// all its parameter nodes assigned.
        /// </summary>
        public bool IsParamListSaturated => parameterNodes.Count == ParamLimit;


        public override void AddChildNode(ExprNode child)
        {
            if (IsParamListSaturated)
            {
                throw new VisitException(child.Context,
                    "FunctionReferenceNode.AddChildNode: Parameter limit exceeded!");
            }
            
            parameterNodes.Add(child);
        }

        public override bool CanAcceptChildNode() => !IsParamListSaturated;

        public override void TypeCheck()
        {
            parameterNodes.ForEach(p => p.TypeCheck());
        }

        public override Result GetValue()
        {
            List<object> parameters = new List<object>();
            parameterNodes.ForEach(p => parameters.Add(p.GetValue().GetResult()));
            return Env.GetFunctionValue(FunctionName, parameters);
        }

        public override string ToWebAssembly()
        {
            throw new NotImplementedException("Cannot convert function calls to WebAssembly yet");
        }
    }

    /// <summary>
    /// An expression node that refers to a parameter of a function.
    /// </summary>
    class ParameterReferenceNode : ExprNode
    {
        public ParameterReferenceNode(NodeContext context, string paramName, Function func) : base(context)
        {
            ParameterName = paramName;
            Function = func;
        }

        public string ParameterName { get; }
        public Function Function { get; }

        public override Result GetValue()
        {
            return new Result(Env.GetParam(ParameterName));
        }

        public override int WasmIndentDepth => 0;

        public override string ToWebAssembly()
        {
            return $"get_local ${ParameterName}";
        }
    }

    /// <summary>
    /// An expression node that is a sum or subtract operation.
    /// </summary>
    class SumNode : ParentNode
    {
        public SumNode(NodeContext context, SumOperation operation) : base(context)
        {
            Operation = operation;
        }


        public SumOperation Operation { get; }

        public override void TypeCheck()
        {
            Left.CheckNodeType(typeof(int), "Left side of a sum or subtract expression is not an int");
            Right.CheckNodeType(typeof(int), "Right side of a sum or subtract expression is not an int");
            Left.TypeCheck();
            Right.TypeCheck();
            base.TypeCheck();
        }

        public override Type GetExpectedResultType() => typeof(int);

        public override Result GetValue()
        {
            switch (Operation)
            {
                case SumOperation.Sum:
                    return Left.GetValue() + Right.GetValue();
                default:
                case SumOperation.Substract:
                    return Left.GetValue() - Right.GetValue();
            }
        }

        public override string ToWebAssembly()
        {
            string op = Operation == SumOperation.Sum ? "add" : "sub";
            return GetWebAsmLine("(") +
                Left.ToWebAssembly() +
                Right.ToWebAssembly() +
                GetWebAsmLine($"{WasmIntFormat}.{op}") + 
                GetWebAsmLine(")");
        }
    }

    /// <summary>
    /// An expression node that is a multiplication or division operation.
    /// </summary>
    class MultNode : ParentNode
    {
        public MultNode(NodeContext context, MultOperation operation) : base(context)
        {
            Operation = operation;
        }

        public MultOperation Operation { get; }

        public override void TypeCheck()
        {
            Left.CheckNodeType(typeof(int), "Left side of a multiplication or division expression is not an int");
            Right.CheckNodeType(typeof(int), "Right side of a multiplication or division expression is not an int");
            Left.TypeCheck();
            Right.TypeCheck();
            base.TypeCheck();
        }

        public override Type GetExpectedResultType() => typeof(int);

        public override Result GetValue()
        {
            switch (Operation)
            {
                case MultOperation.Multiply:
                    return Left.GetValue() * Right.GetValue();
                default:
                case MultOperation.Divide:
                    return Left.GetValue() / Right.GetValue();
            }
        }

        public override string ToWebAssembly()
        {
            string op = Operation == MultOperation.Multiply ? "mul" : "div_s";
            return $"(\n{Left.ToWebAssembly()}\n{Right.ToWebAssembly()}\n{WasmIntFormat}.{op}\n)";
        }
    }
    /// <summary>
    /// An expression node that is a comparative operation.
    /// </summary>

    class CompNode : ParentNode
    {
        public CompNode(NodeContext context, CompOperation operation) : base(context)
        {
            Operation = operation;
        }

        public CompOperation Operation { get; }

        public override void TypeCheck()
        {
            switch (Operation)
            {
                case CompOperation.Less:
                case CompOperation.Greater:
                case CompOperation.LessOrEqual:
                case CompOperation.GreaterOrEqual:
                    Left.CheckNodeType(typeof(int), "Left side of a integer comparison expression is not an int");
                    Right.CheckNodeType(typeof(int), "Right side of a integer comparison expression is not an int");
                    break;
                case CompOperation.Equal:
                case CompOperation.NotEqual:
                    Type leftType = Left.GetValue().GetResultType();
                    Type rightType = Right.GetValue().GetResultType();
                    if (leftType != rightType && leftType != typeof(object) && rightType != typeof(object))
                        throw new VisitException(Context, "Types for equality or inequality comparison operator do not match");
                    break;
            }

            Left.TypeCheck();
            Right.TypeCheck();
        }

        public override Type GetExpectedResultType() => typeof(bool);

        public override Result GetValue()
        {
            switch (Operation)
            {
                case CompOperation.Less:
                    return Left.GetValue() < Right.GetValue();
                
                case CompOperation.Greater:
                    return Left.GetValue() > Right.GetValue();
                
                case CompOperation.LessOrEqual:
                    return Left.GetValue() <= Right.GetValue();
            
                case CompOperation.GreaterOrEqual:
                    return Left.GetValue() >= Right.GetValue();
                
                case CompOperation.Equal:
                    return Left.GetValue() == Right.GetValue();
                case CompOperation.NotEqual:
                    return Left.GetValue() != Right.GetValue();
                default:
                    throw new VisitException(Context, "CompNode.GetValue: Unknown operation type!");
            }
        }

        public override string ToWebAssembly()
        {
            string c_fun = "";
            switch (Operation)
            {
                case CompOperation.Less:
                    c_fun = "lt_s";
                    break;
                case CompOperation.Greater:
                    c_fun = "gt_s";
                    break;
                case CompOperation.LessOrEqual:
                    c_fun = "le_s";
                    break;
                case CompOperation.GreaterOrEqual:
                    c_fun = "ge_s";
                    break;
                case CompOperation.Equal:
                    c_fun = "eq";
                    break;
                case CompOperation.NotEqual:
                    c_fun = "ne";
                    break;
                default:
                    throw new VisitException(Context, "CompNode.ToWebAssembly: Unknown operation type!");
            }
            return $"(\n{Left.ToWebAssembly()}\n{Right.ToWebAssembly()}\n{c_fun}\n)";
        }
    }
}
