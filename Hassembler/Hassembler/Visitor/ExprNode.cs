using System;
using System.Collections.Generic;

namespace Hassembler
{
    /// <summary>
    /// Base class for an expression node.
    /// </summary>
    public abstract class ExprNode
    {
        protected NodeContext Context {get;}   

        public ExprNode(NodeContext context)
        {
            Context = context;
        }

        public ExprNode Parent => Context.Parent;
        protected IEnv Env => Context.Env;

        public abstract Result GetValue();
    }

    /// <summary>
    /// Base class for an expression node that acts as a
    /// parent to other nodes.
    /// </summary>
    abstract class ParentNode : ExprNode
    {
        public ParentNode(NodeContext context) : base(context)
        {
        }

        public ExprNode Right { get; set; }
        public ExprNode Left { get; set; }
    }

    class ITENode : ExprNode
    {
        public ITENode(NodeContext context) : base(context)
        {
        }

        public ExprNode Condition { get; set; }
        public ExprNode ThenExpr { get; set; }
        public ExprNode ElseExpr { get; set; }

        public override Result GetValue() =>
            Condition.GetValue().GetResult<bool>() 
            ? ThenExpr.GetValue() : ElseExpr.GetValue();
    }

    /// <summary>
    /// An expression node that contains parentheses.
    /// TODO rename
    /// </summary>
    class ParNode : ExprNode
    {
        public ParNode(NodeContext context) : base(context)
        {
        }

        public ExprNode Follower { get; set; }

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


        public int Value { get; private set; }

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

        public bool Value { get; private set; }

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

        public string FunctionName { get; private set; }

        /// <summary>
        /// The number of parameters of the function reference node.
        /// Given when the function reference node is created based on the parse tree.
        /// </summary>
        public int ParamLimit { get; }

        public bool IsParamListSaturated => parameterNodes.Count == ParamLimit;

        public void AddParameter(ExprNode paramNode)
        {
            if (IsParamListSaturated)
                throw new InvalidOperationException("Parameter limit exceeded!");

            parameterNodes.Add(paramNode);
        }

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


        public string ParameterName { get; private set; }
        public Function Function { get; private set; }

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


        public SumOperation Operation { get; private set; }

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


        public MultOperation Operation { get; private set; }

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

    class CompNode : ParentNode
    {
        public CompOperation Operation {get; private set;}

        public CompNode(NodeContext context, CompOperation operation) : base(context)
        {
            Operation = operation;
        }


        public override Result GetValue()
        {
            switch (Operation)
            {
                case CompOperation.Less:
                    return Left.GetValue() < Right.GetValue();
                
                case CompOperation.Greater:
                    return Left.GetValue() > Right.GetValue();
                
                case CompOperation.LessEqual:
                    return Left.GetValue() <= Right.GetValue();
            
                case CompOperation.GreaterEqual:
                    return Left.GetValue() >= Right.GetValue();
                
                case CompOperation.Equal:
                    return Left.GetValue() == Right.GetValue();

                default:
                case CompOperation.NotEqual:
                    return Left.GetValue() != Right.GetValue();
            }
        }
    }
}
