using System;
using System.Collections.Generic;

namespace Hassembler
{
    /// <summary>
    /// Base class for an expression node.
    /// </summary>
    public abstract class ExprNode
    {
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
        /// Resolves and returns the value of the node.
        /// </summary>
        public abstract Result GetValue();
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

        public override Result GetValue() =>
            Condition.GetValue().GetResult<bool>() 
            ? ThenExpr.GetValue() : ElseExpr.GetValue();
    }

    /// <summary>
    /// An expression node that contains parentheses.
    /// TODO rename
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

        public override Result GetValue()
        {
            return Follower.GetValue();
        }
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

        public override Result GetValue() => new Result(Value);
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

        public override Result GetValue() => new Result(Value);
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

        public override Result GetValue()
        {
            List<object> parameters = new List<object>();
            parameterNodes.ForEach(p => parameters.Add(p.GetValue().GetResult<int>())); // TODO handle non-int types
            return Env.GetReferenceValue(FunctionName, parameters);
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

        public override Result GetValue()
        {
            switch (Operation)
            {
                case MultOperation.Multiply:
                    return Right.GetValue() * Left.GetValue();
                default:
                case MultOperation.Divide:
                    return Right.GetValue() / Left.GetValue();
            }
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
    }
}
