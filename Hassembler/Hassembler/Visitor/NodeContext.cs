using System;

namespace Hassembler
{
    /// <summary>
    /// The context for a node.
    /// </summary>
    public struct NodeContext
    {
        /// <summary>
        /// The parent of the node. Null for none.
        /// </summary>
        public ExprNode Parent { get; }

        /// <summary>
        /// The interpreter / compiler environment.
        /// </summary>
        public IEnv Env { get; }

        /// <summary>
        /// The start line of the node.
        /// </summary>
        public int Line { get; }

        /// <summary>
        /// The start column of the node.
        /// </summary>
        public int Column { get; }


        public NodeContext(ExprNode parent, IEnv env, int line, int column)
        {
            Parent = parent;
            Env = env ?? throw new ArgumentNullException("env");
            Line = line;
            Column = column;
        }
    } 
}