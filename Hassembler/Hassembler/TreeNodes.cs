using System;
using Antlr4.Runtime;
using Antlr4.Runtime.Tree;

namespace Hassembler
{
    abstract class Node
    {
        // Each node has two children and one parent
        // Parent of root node and children of leaf nodes are null;
        protected Node Left {get; set;}
        protected Node Right {get; set;}
        protected Node Parent {get; set;}

        protected int CodeLineNumber {get; set;}
        protected int CodeColumnNumber {get; set;}

        public override String ToString() => "Oispa lanka";

        public virtual Result Solve()
        {
            throw new NotImplementedException();
        }
    }

    class Prog_node : Node
    {

    }
    class F_defi_node : Node
    {

    }

    abstract class Expr_node : Node
    {

    }

    class Add_expr_node : Expr_node
    {
        public override Result Solve()
        {
            return new Result(Left.Solve().GetResult<int>() + Right.Solve().GetResult<int>());
        }
    }

    class Subs_expr_node : Expr_node
    {
        public override Result Solve()
        {
            return new Result(Left.Solve().GetResult<int>() - Right.Solve().GetResult<int>());
        }
    }

    class Int_node : Expr_node
    {
        private Result res_value;

        public Int_node(int value) => res_value = new Result(value);
        public override Result Solve()
        {
            return res_value;
        }

    }

    class F_name_node : Node
    {
        private String Name {get; set;}
    }
}