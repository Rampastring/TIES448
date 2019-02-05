using System;

namespace Hassembler
{
    public struct NodeContext
    {
        public ExprNode Parent {get; private set;}
        public IEnv Env {get; private set;}

        public int Line {get; private set;}
        public int Column {get; private set;}

        public NodeContext(ExprNode parent, IEnv env, int line, int column)
        {
            Parent = parent;
            Env = env ?? throw new ArgumentNullException("env");
            Line = line;
            Column = column;
        }
    } 
}