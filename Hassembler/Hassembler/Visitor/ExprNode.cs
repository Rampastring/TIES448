﻿using System;
using System.Collections.Generic;
using System.Text;

namespace Hassembler
{
    /// <summary>
    /// Base class for an expression node.
    /// </summary>
    public abstract class ExprNode
    {
        protected const string WasmIntFormat = "i32";
        private const int WasmBlockDepth = 2;
        private const int WasmInitialDepth = 2;

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

        #region Type checking

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

        #endregion

        /// <summary>
        /// Resolves and returns the value of the node.
        /// </summary>
        public abstract Result GetValue();

        #region WebAssembly Text Format Compilation

        /// <summary>
        /// Generates and returns the WebAssembly (WAT) representation of the node.
        /// Automatically adds start and end parentheses and indentation.
        /// </summary>
        /// <param name="depthOffset">Indentation offset.</param>
        public virtual string ToWebAssembly(int depthOffset = 0)
        {
            this.depthOffset = depthOffset;
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < GetExpressionDepth(depthOffset) * WasmBlockDepth; i++)
            {
                sb.Append(' ');
            }
            string indent = sb.ToString();
            sb.Append('(');
            sb.Append(GetWebAssemblyContent());
            if (IndentWasmBeforeLastParenthesis)
                sb.Append(indent);
            sb.Append(')');

            return sb.ToString();
        }

        /// <summary>
        /// If overriden in a derived class and set to true, the last parenthesis
        /// of this node is automatically indented when generating WebAssembly code.
        /// </summary>
        protected virtual bool IndentWasmBeforeLastParenthesis => false;

        /// <summary>
        /// How many layers of depth this node adds to the WebAssembly code.
        /// Used for indentation.
        /// </summary>
        protected virtual int WasmIndentDepth => 1;

        private int TotalWasmIndentDepth => WasmIndentDepth + depthOffset;

        /// <summary>
        /// Stores the depth offset given to ToWebAssembly
        /// so it can be used for indenting children correctly.
        /// </summary>
        private int depthOffset = 0;

        /// <summary>
        /// Calculates the indentation of the WebAssembly code for this node.
        /// </summary>
        /// <param name="depthOffset">Indentation offset.</param>
        private int GetExpressionDepth(int depthOffset = 0)
        {
            int depth = WasmInitialDepth + depthOffset;
            ExprNode parent = Parent;
            while (parent != null)
            {
                depth += parent.TotalWasmIndentDepth;
                parent = parent.Parent;
            }
            return depth;
        }

        /// <summary>
        /// Generates and returns the indendation spaces for this node.
        /// </summary>
        /// <param name="depthOffset">Indentation offset.</param>
        protected string GetIndent(int depthOffset = 0)
        {
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < (GetExpressionDepth(depthOffset)) * WasmBlockDepth; i++)
            {
                sb.Append(' ');
            }
            return sb.ToString();
        }

        /// <summary>
        /// Generates and returns the actual WebAssembly code for this node,
        /// not including indentation or start and end parentheses.
        /// </summary>
        protected abstract string GetWebAssemblyContent();

        #endregion
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

        protected override int WasmIndentDepth => 2;

        public override Result GetValue() =>
            Condition.GetValue().GetResult<bool>() 
            ? ThenExpr.GetValue() : ElseExpr.GetValue();

        protected override string GetWebAssemblyContent()
        {
            return $"if (result {WasmIntFormat})" + '\n' +
                Condition.ToWebAssembly(-1) + '\n' +
                $"{GetIndent(1)}(then\n{ThenExpr.ToWebAssembly()}\n{GetIndent(1)})\n" +
                $"{GetIndent(1)}(else\n{ElseExpr.ToWebAssembly()}\n{GetIndent(1)})\n";
        }

        protected override bool IndentWasmBeforeLastParenthesis => true;
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

        protected override int WasmIndentDepth => 0;

        public override string ToWebAssembly(int depthOffset = 0) => Follower.ToWebAssembly();

        protected override string GetWebAssemblyContent() => Follower.ToWebAssembly();
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

        protected override string GetWebAssemblyContent() => ($"{WasmIntFormat}.const {Value}");
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

        protected override string GetWebAssemblyContent()
        {
            return $"{WasmIntFormat}.const {(Value ? 1 : 0)}";
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

        protected override bool IndentWasmBeforeLastParenthesis => parameterNodes.Count > 0;

        protected override string GetWebAssemblyContent()
        {
            StringBuilder sb = new StringBuilder("call $" + FunctionName);
            if (parameterNodes.Count > 0)
            {
                sb.Append('\n');
                foreach (ExprNode child in parameterNodes)
                {
                    sb.Append(child.ToWebAssembly() + '\n');
                }
            }
            return sb.ToString();
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

        protected override string GetWebAssemblyContent()
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

        protected override string GetWebAssemblyContent()
        {
            string op = Operation == SumOperation.Sum ? "add" : "sub";
            return ($"{WasmIntFormat}.{op}") + '\n' +
                Left.ToWebAssembly() + '\n' +
                Right.ToWebAssembly() + '\n';
        }

        protected override bool IndentWasmBeforeLastParenthesis => true;
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

        protected override string GetWebAssemblyContent()
        {
            string op = Operation == MultOperation.Multiply ? "mul" : "div_s";
            return ($"{WasmIntFormat}.{op}") + '\n' +
                Left.ToWebAssembly() + '\n' +
                Right.ToWebAssembly() + '\n';
        }

        protected override bool IndentWasmBeforeLastParenthesis => true;
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

        protected override string GetWebAssemblyContent()
        {
            string c_fun = "";
            switch (Operation)
            {
                case CompOperation.Less:
                    c_fun = $"{WasmIntFormat}.lt_s";
                    break;
                case CompOperation.Greater:
                    c_fun = $"{WasmIntFormat}.gt_s";
                    break;
                case CompOperation.LessOrEqual:
                    c_fun = $"{WasmIntFormat}.le_s";
                    break;
                case CompOperation.GreaterOrEqual:
                    c_fun = $"{WasmIntFormat}.ge_s";
                    break;
                case CompOperation.Equal:
                    c_fun = $"{WasmIntFormat}.eq";
                    break;
                case CompOperation.NotEqual:
                    c_fun = $"{WasmIntFormat}.ne";
                    break;
                default:
                    throw new VisitException(Context, "CompNode.ToWebAssembly: Unknown operation type!");
            }
            return c_fun + '\n' +
                Left.ToWebAssembly() + '\n' +
                Right.ToWebAssembly() + '\n';
        }

        protected override bool IndentWasmBeforeLastParenthesis => true;
    }
}
