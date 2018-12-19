namespace Hassembler
{
    /// <summary>
    /// Base class for an expression node.
    /// </summary>
    abstract class ExprNode
    {
        public ExprNode(ExprNode parent)
        {
            Parent = parent;
        }

        public ExprNode Parent { get; private set; }
        
        public abstract Result GetValue();
    }

    /// <summary>
    /// Base class for an expression node that acts as a
    /// parent to other nodes.
    /// </summary>
    abstract class ParentNode : ExprNode
    {
        public ParentNode(ExprNode parent) : base(parent)
        {
        }

        public ExprNode Right { get; set; }
        public ExprNode Left { get; set; }
    }

    /// <summary>
    /// An expression node that contains parentheses.
    /// TODO rename
    /// </summary>
    class ParNode : ExprNode
    {
        public ParNode(ExprNode parent) : base(parent)
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
        public IntNode(ExprNode parent, int value) : base(parent)
        {
            Value = value;
        }

        public int Value { get; private set; }

        public override Result GetValue() => new Result(Value);
    }

    /// <summary>
    /// An expression node that is a sum or subtract operation.
    /// </summary>
    class SumNode : ParentNode
    {
        public SumNode(ExprNode parent, SumOperation operation) : base(parent)
        {
            Operation = operation;
        }

        public SumOperation Operation { get; private set; }

        public override Result GetValue()
        {
            switch (Operation)
            {
                case SumOperation.Sum:
                    return Right.GetValue() + Left.GetValue();
                default:
                case SumOperation.Substract:
                    return Right.GetValue() - Left.GetValue();
            }
        }
    }

    /// <summary>
    /// An expression node that is a multiplication or division operation.
    /// </summary>
    class MultNode : ParentNode
    {
        public MultNode(ExprNode parent, MultOperation operation) : base(parent)
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
}
