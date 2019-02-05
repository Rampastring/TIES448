using System.Collections.Generic;
using Antlr4.Runtime;
using Antlr4.Runtime.Misc;

namespace Hassembler
{
    /// <summary>
    /// The main visitor class.
    /// </summary>
    class EvalVisitor : HaskellmmBaseVisitor<VisitorResult>
    {
        public EvalVisitor()
        {
            Env = new Env();

            // Note: we're passing the function list as reference -
            // if the function list is modified in Env, the changes wil
            // also apply here
            Env.SetFunctions(Functions);
        }

        public List<Function> Functions = new List<Function>();

        public Env Env { get; private set; }

        private Function currentFunction;
        private ExprNode currentNode;


        /// <summary>
        /// Creates and returns a node context based on a parser context.
        /// </summary>
        /// <param name="context">The parser context.</param>
        /// <returns>The created node context.</returns>
        private NodeContext CreateNodeContext(ParserRuleContext context) =>
            new NodeContext(currentNode, Env, context.Start.Line, context.Start.Column);

        /// <summary>
        /// The visit subroutine for a function definition.
        /// Entering this means that we've begun parsing a new function.
        /// </summary>
        public override VisitorResult VisitF_defi([NotNull] HaskellmmParser.F_defiContext context)
        {
            int eqIndex = -1;
            for (int i = 0; i < context.ChildCount; i++)
            {
                if (context.GetChild(i).GetText() == "=")
                {
                    eqIndex = i;
                    break;
                }
            }

            if (eqIndex == -1)
                throw new VisitException(context.Start.Line, context.Start.Column ,"'=' expected in function definition");

            string functionName = context.GetChild(0).GetText();

            currentFunction = new Function(functionName);

            for (int i = 1; i < eqIndex; i++)
            {
                currentFunction.AddParameter(new Parameter(context.GetChild(i).GetText()));
            }

            if (Functions.Find(f => f.Name == functionName) != null)
            {
                throw new VisitException(context.Start.Line, context.Start.Column,
                    "Multiple definitions found for function " + functionName);
            }
                
            Functions.Add(currentFunction);

            currentNode = null;

            return base.VisitF_defi(context);
        }

        /// <summary>
        /// Adds a node that references a parameter of the currently visited function.
        /// </summary>
        /// <param name="context">The parser context.</param>
        /// <param name="paramName">The name of the parameter to reference.</param>
        private void AddParamReferenceNode(ParserRuleContext context, string paramName)
        {
            var nodeContext = CreateNodeContext(context);
            var node = new ParameterReferenceNode(nodeContext, paramName, currentFunction);
            AddExprNode(node);
            currentNode = FindEarliestParentWithUnfilledChildren(currentNode);
        }

        /// <summary>
        /// The visit subroutine for function call parameters.
        /// </summary>
        public override VisitorResult VisitRefVar([NotNull] HaskellmmParser.RefVarContext context)
        {
            string refName = context.GetText();
            AddParamReferenceNode(context, refName);

            return base.VisitRefVar(context);
        }

        /// <summary>
        /// The visit subroutine for references.
        /// A reference can point either to a function (when it's a function call)
        /// or a parameter of the currently visited function.
        /// </summary>
        public override VisitorResult VisitRefExp([NotNull] HaskellmmParser.RefExpContext context)
        {
            if (context.children[0].ChildCount == 0)
                throw new VisitException(context, "RefExps are expected to contain grandchildren.");

            var child = context.children[0];
            string refName = child.GetChild(0).GetText();

            if (currentFunction.Parameters.FindIndex(p => p.Name == refName) > -1)
            {
                AddParamReferenceNode(context, refName);
            }
            else
            {
                NodeContext nodeContext = CreateNodeContext(context);
                int parameterCount = child.ChildCount - 1;
                ExprNode node = new FunctionReferenceNode(nodeContext, refName, parameterCount);
                AddExprNode(node);
                if (parameterCount == 0)
                    currentNode = FindEarliestParentWithUnfilledChildren(currentNode);
            }

            return base.VisitRefExp(context);
        }

        /// <summary>
        /// The visit subroutine for if-then-else expressions.
        /// </summary>
        public override VisitorResult VisitIte_defi([NotNull] HaskellmmParser.Ite_defiContext context)
        {
            string s = context.GetText();
            ITENode iteNode = new ITENode(CreateNodeContext(context));
            AddExprNode(iteNode);

            return base.VisitIte_defi(context);
        }

        /// <summary>
        /// The visit subroutine for integer constants.
        /// </summary>
        public override VisitorResult VisitIntVar([NotNull] HaskellmmParser.IntVarContext context)
        {
            string s = context.GetText();

            int value = int.Parse(s);

            IntNode node = new IntNode(CreateNodeContext(context), value);
            AddExprNode(node);
            // An integer constant has no parents, so find the next node to fill
            // by traversing the tree upwards
            currentNode = FindEarliestParentWithUnfilledChildren(currentNode);

            return base.VisitIntVar(context);
        }

        /// <summary>
        /// The visit subroutine for boolean constants.
        /// </summary>
        public override VisitorResult VisitBoolVar([NotNull] HaskellmmParser.BoolVarContext context)
        {
            string s = context.GetText();

            BoolNode node;

            if (s == "True")
                node = new BoolNode(CreateNodeContext(context), true);
            else if (s == "False")
                node = new BoolNode(CreateNodeContext(context), false);
            else
                throw new VisitException(context.Start.Line, context.Start.Column, "Bool was neither True nor False");

            AddExprNode(node);

            currentNode = FindEarliestParentWithUnfilledChildren(currentNode);

            return base.VisitBoolVar(context);
        }

        /// <summary>
        /// The visit subroutine for sum and subtract operations.
        /// </summary>
        public override VisitorResult VisitAddExp([NotNull] HaskellmmParser.AddExpContext context)
        {
            if (context.children.Count < 3)
                throw new VisitException(context.Start.Line, context.Start.Column, "Expected expr (+/-) expr");

            SumOperation operation;
            switch (context.children[1].GetText())
            {
                case "+":
                    operation = SumOperation.Sum;
                    break;
                case "-":
                    operation = SumOperation.Substract;
                    break;
                default:
                    throw new VisitException(context.Start.Line, context.Start.Column, "Operator was neither + or -.");
            }

            AddExprNode(new SumNode(CreateNodeContext(context), operation));

            return base.VisitAddExp(context);
        }

        /// <summary>
        /// The visit subroutine for multiplication and division operators.
        /// </summary>
        public override VisitorResult VisitMultExp([NotNull] HaskellmmParser.MultExpContext context)
        {
            if (context.children.Count < 3)
                throw new VisitException(context.Start.Line, context.Start.Column , "Expected expr (+/-) expr");

            MultOperation operation;
            switch (context.children[1].GetText())
            {
                case "*":
                    operation = MultOperation.Multiply;
                    break;
                case "/":
                    operation = MultOperation.Divide;
                    break;
                default:
                    throw new VisitException(context.Start.Line, context.Start.Column, "Operator was neither * or /");
            }

            AddExprNode(new MultNode(CreateNodeContext(context), operation));
            return base.VisitMultExp(context);
        }

        /// <summary>
        /// The visit subroutine for comparison operators.
        /// </summary>
        public override VisitorResult VisitCompExp([NotNull] HaskellmmParser.CompExpContext context)
        {
            if (context.children.Count < 3)
                throw new VisitException(context.Start.Line, context.Start.Column , "Expected expr compare expr");

            CompOperation operation;
            switch (context.children[1].GetText())
            {
                case "<":
                    operation = CompOperation.Less;
                    break;
                case ">":
                    operation = CompOperation.Greater;
                    break;
                case "<=":
                    operation = CompOperation.LessOrEqual;
                    break;
                case ">=":
                    operation = CompOperation.GreaterOrEqual;
                    break;
                case "==":
                    operation = CompOperation.Equal;
                    break;
                case "!=":
                    operation = CompOperation.NotEqual;
                    break;
                default:
                    throw new VisitException(context.Start.Line, context.Start.Column, "Operator was not comparative operator");
            }

            AddExprNode(new CompNode(CreateNodeContext(context), operation));
            return base.VisitCompExp(context);
            
        }

        /// <summary>
        /// The visit subroutine for parentheses.
        /// </summary>
        public override VisitorResult VisitParenExp([NotNull] HaskellmmParser.ParenExpContext context)
        {
            string s = context.GetText();

            AddExprNode(new ParenthesesNode(CreateNodeContext(context)));

            return base.VisitParenExp(context);
        }

        /// <summary>
        /// The visitor routine for the start of our program.
        /// </summary>
        public override VisitorResult VisitProg([NotNull] HaskellmmParser.ProgContext context)
        {
            return base.VisitProg(context);
        }

        /// <summary>
        /// Adds a new expression node as a child to the current expression node
        /// or as the starting node of the current function.
        /// </summary>
        /// <param name="exprNode">The expression node.</param>
        private void AddExprNode(ExprNode exprNode)
        {
            if (currentFunction.StartNode == null)
            {
                // If we get here, exprNode is meant to be the starting node
                // of the current function

                if (currentNode != null)
                {
                    throw new ASTException("Found starting node for current function, " +
                        "but current node is not null!");
                }

                currentFunction.StartNode = exprNode;
            }
            else if (currentNode != null)
            {
                // exprNode is meant to be a child node of another expression node

                currentNode.AddChildNode(null, exprNode);
            }
            else
                throw new ASTException("No parent node exists!");

            currentNode = exprNode;
        }

        /// <summary>
        /// Traverses the tree upwards from the given node and returns
        /// the first parent node that has unfilled children.
        /// The parent node can be either an operation (with right / left children)
        /// or a node with parentheses.
        /// Returns null if no parent node with unfilled children is found.
        /// </summary>
        /// <param name="node">The node from which to start our search.</param>
        private ExprNode FindEarliestParentWithUnfilledChildren(ExprNode node)
        {
            while (true)
            {
                if (node == null)
                    throw new ASTException("how?");

                if (node.Parent == null)
                    return null;

                ExprNode parent = node.Parent;

                if (parent.CanAcceptChildNode())
                    return parent;

                node = parent;
            }
        }
    }
}
