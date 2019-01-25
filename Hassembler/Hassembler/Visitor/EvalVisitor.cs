using System.Collections.Generic;
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


        public override VisitorResult VisitAddExp([NotNull] HaskellmmParser.AddExpContext context)
        {
            if (context.children.Count < 3)
                throw new VisitException(context.Start.Line, context.Start.Column ,"Expected expr (+/-) expr");

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
                    throw new VisitException(context.Start.Line, context.Start.Column , "Operator was neither + or -.");
            }

            AddExprNode(new SumNode(currentNode, Env, operation));

            return base.VisitAddExp(context);
        }

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
                throw new VisitException(context.Start.Line, context.Start.Column ,
                    "Multiple definitions found for function " + functionName);

            Functions.Add(currentFunction);

            currentNode = null;

            return base.VisitF_defi(context);
        }

        /// <summary>
        /// The visit subroutine for an expression that references a function.
        /// </summary>
        public override VisitorResult VisitRefVar([NotNull] HaskellmmParser.RefVarContext context)
        {
            string s = context.GetText();
            ExprNode node;
            if (currentFunction.Parameters.FindIndex(p => p.Name == s) > -1)
            {
                node = new ParameterReferenceNode(currentNode, Env, s, currentFunction);
                AddExprNode(node);
                currentNode = FindEarliestParentWithUnfilledChildren(currentNode);
            }
            else
            {
                node = new FunctionReferenceNode(currentNode, Env, s);
                AddExprNode(node);
            }
            
            return base.VisitRefVar(context);
        }

        public override VisitorResult VisitIte_defi([NotNull] HaskellmmParser.Ite_defiContext context)
        {
            string s = context.GetText();

            ITENode iteNode = new ITENode(currentNode, Env);
            AddExprNode(iteNode);

            return base.VisitIte_defi(context);
        }

        public override VisitorResult VisitIntVar([NotNull] HaskellmmParser.IntVarContext context)
        {
            string s = context.GetText();

            int value = int.Parse(s);

            IntNode node = new IntNode(currentNode, Env, value);
            AddExprNode(node);
            // An integer constant has no parents, so find the next node to fill
            // by traversing the tree upwards
            currentNode = FindEarliestParentWithUnfilledChildren(currentNode);

            return base.VisitIntVar(context);
        }

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
                    throw new VisitException(context.Start.Line, context.Start.Column ,"Operator was neither * or /");
            }

            AddExprNode(new MultNode(currentNode, Env, operation));
            return base.VisitMultExp(context);
        }

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
                    operation = CompOperation.LessEqual;
                    break;
                case ">=":
                    operation = CompOperation.GreaterEqual;
                    break;
                case "==":
                    operation = CompOperation.Equal;
                    break;
                case "!=":
                    operation = CompOperation.NotEqual;
                    break;
                default:
                    throw new VisitException(context.Start.Line, context.Start.Column ,"Operator was not comparative operator");
            }

            AddExprNode(new CompNode(currentNode, Env, operation));
            return base.VisitCompExp(context);
            
        }

        public override VisitorResult VisitParenExp([NotNull] HaskellmmParser.ParenExpContext context)
        {
            string s = context.GetText();

            AddExprNode(new ParNode(currentNode, Env));

            return base.VisitParenExp(context);
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

                // Figure out whether the parent is a parent node with
                // right and left children or a node with parentheses
                // or an if-then-else node

                if (currentNode is ParentNode parentNode)
                {
                    // The parent mode is a node with right and left children

                    if (parentNode.Left == null)
                        parentNode.Left = exprNode;
                    else if (parentNode.Right == null)
                        parentNode.Right = exprNode;
                    else
                        throw new ASTException("ParentNode: No unfilled child node!");
                }
                else if (currentNode is ParNode parNode)
                {
                    // A node with parentheses
                    parNode.Follower = exprNode;
                }
                else if (currentNode is ITENode iteNode)
                {
                    // if-then-else
                    if (iteNode.Condition == null)
                        iteNode.Condition = exprNode;
                    else if (iteNode.ThenExpr == null)
                        iteNode.ThenExpr = exprNode;
                    else if (iteNode.ElseExpr == null)
                        iteNode.ElseExpr = exprNode;
                    else
                        throw new ASTException("ITENode: No unfilled child node!");
                }
                else if (currentNode is FunctionReferenceNode frNode)
                {
                    frNode.AddParameter(exprNode);
                }
                else
                {
                    throw new ASTException("Unknown parent node type");
                }
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

                if (parent is ParentNode parentNode)
                {
                    if (parentNode.Left == null || parentNode.Right == null)
                        return parentNode;
                }

                if (parent is ITENode iteNode)
                {
                    // TODO improve class design
                    if (iteNode.Condition == null ||
                        iteNode.ThenExpr == null ||
                        iteNode.ElseExpr == null)
                        return iteNode;
                }

                if (parent is FunctionReferenceNode frNode)
                {
                    // Functions can always accept more parameters
                    return frNode;
                }

                node = parent;
            }
        }

        /// <summary>
        /// The visitor routine for the start of our program.
        /// </summary>
        public override VisitorResult VisitProg([NotNull] HaskellmmParser.ProgContext context)
        {
            return base.VisitProg(context);
        }
    }
}
