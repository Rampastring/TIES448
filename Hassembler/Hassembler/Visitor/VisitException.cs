using System;

namespace Hassembler
{
    class VisitException : Exception
    {
        public VisitException(string message) : base(message)
        {
        }
    }

    class SyntaxError : Exception
    {
        private int Line { get; }
        private int Column { get; }
        private string F_name { get; }

        public SyntaxError(int line, int column, string f_name ,string message) : base (message)
        {
            this.Line = line;
            this.Column = column;
            this.F_name = f_name;
        }
    }
}
