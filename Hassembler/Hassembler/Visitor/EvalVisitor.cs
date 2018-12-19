﻿using System.Collections.Generic;
using Antlr4.Runtime.Misc;

namespace Hassembler
{
    /// <summary>
    /// The main visitor class.
    /// </summary>
    class EvalVisitor : HaskellmmBaseVisitor<VisitorResult>
    {
        public List<Function> Functions = new List<Function>();
        Function currentFunction;
        ExprNode currentNode;

        public override VisitorResult VisitAddExp([NotNull] HaskellmmParser.AddExpContext context)
        {
            if (context.children.Count < 3)
                throw new VisitException("Expected expr (+/-) expr");

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
                    throw new VisitException("ei");
            }

            AddExprNode(new SumNode(currentNode, operation));

            return base.VisitAddExp(context);
        }

        public override VisitorResult VisitF_defi([NotNull] HaskellmmParser.F_defiContext context)
        {
            return base.VisitF_defi(context);
        }

        /// <summary>
        /// The visit subroutine for a function name.
        /// Entering this means that we've begun parsing a new function.
        /// </summary>
        public override VisitorResult VisitF_name([NotNull] HaskellmmParser.F_nameContext context)
        {
            string s = context.GetText();
            currentFunction = new Function(s);
            Functions.Add(currentFunction);
            currentNode = null;
            return base.VisitF_name(context);
        }

        public override VisitorResult VisitIntVar([NotNull] HaskellmmParser.IntVarContext context)
        {
            string s = context.GetText();

            int value = int.Parse(s);

            IntNode node = new IntNode(currentNode, value);
            AddExprNode(node);
            // An integer constant has no parents, so find the next node to fill
            // by traversing the tree upwards
            currentNode = FindEarliestParentWithUnfilledChildren(currentNode);

            return base.VisitIntVar(context);
        }

        public override VisitorResult VisitMultExp([NotNull] HaskellmmParser.MultExpContext context)
        {
            if (context.children.Count < 3)
                throw new VisitException("Expected expr (+/-) expr");

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
                    throw new VisitException("ei");
            }

            AddExprNode(new MultNode(currentNode, operation));
            return base.VisitMultExp(context);
        }

        public override VisitorResult VisitParenExp([NotNull] HaskellmmParser.ParenExpContext context)
        {
            string s = context.GetText();

            AddExprNode(new ParNode(currentNode));

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
                    throw new VisitException("Found starting node for current function, " +
                        "but current node is not null!");
                }

                currentFunction.StartNode = exprNode;
            }
            else if (currentNode != null)
            {
                // exprNode is meant to be a child node of another expression node

                // Figure out whether the parent is a parent node with
                // right and left children or a node with parentheses

                ParentNode node = currentNode as ParentNode;
                if (node == null)
                {
                    // The parent node has no right or left children so
                    // it must be a node with parentheses

                    if (!(currentNode is ParNode parNode)) // Yay, C# pattern matching
                    {
                        throw new VisitException("Unknown parent node type");
                    }
                    else
                    {
                        parNode.Follower = exprNode;
                    }
                }
                else
                {
                    // The parent mode is a node with right and left children

                    if (node.Left == null)
                        node.Left = exprNode;
                    else if (node.Right == null)
                        node.Right = exprNode;
                }
            }

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
        private ParentNode FindEarliestParentWithUnfilledChildren(ExprNode node)
        {
            while (true)
            {
                if (node == null)
                    throw new VisitException("how?");

                if (node.Parent == null)
                    return null;

                ExprNode parent = node.Parent;
                ParentNode nodeAsParentNode = parent as ParentNode;

                if (nodeAsParentNode != null)
                {
                    if (nodeAsParentNode.Left == null || nodeAsParentNode.Right == null)
                        return nodeAsParentNode;
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