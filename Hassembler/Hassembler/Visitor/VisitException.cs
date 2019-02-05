using System;

namespace Hassembler
{
    public abstract class HassemblerException : Exception
    {
        protected int Line { get; }
        protected int Column { get; }
        
        public HassemblerException(int line, int column, string message) : base(message)
        {
            this.Line = line;
            this.Column = column;
        }

        public HassemblerException(HaskellmmParser.ExprContext context, string message) : this(context.Start.Line, context.Start.Column, message) { }

        public override string ToString()
        {
            return "An error has occured in line: " + Line.ToString() + ", column: " + Column.ToString() + Environment.NewLine +
            Message;
        }
    }

    class VisitException : HassemblerException
    {
        public VisitException(HaskellmmParser.ExprContext context, string message) : base(context, message) { }

        public VisitException(int line, int column, string message) : base(line, column, message) { }

        public VisitException(NodeContext context, string message) : base(context.Line, context.Column, message) { }
    }

    class SyntaxError : HassemblerException
    {
        private string UnknownToken { get; }

        public SyntaxError(string unknownToken, int line, int column, string message) : base(line, column, message)
        {
            this.UnknownToken = unknownToken;
        }

        public override string ToString()
        {
            return "Syntax error: Unknown token: " + UnknownToken.ToString() + Environment.NewLine + 
            "In line: " + Line.ToString() + ", column: " + Column.ToString() + Environment.NewLine +
            Message;
        } 
    }

    class TypeError : HassemblerException
    {
        private Type Expected {get;}

        private Type Actual {get;}

        public TypeError(Type expected, Type actual, int line, int column, string message) : base(line, column, message)
        {
            this.Expected = expected;
            this.Actual = actual;
        }

        public override string ToString()
        {
            return "Could not match expected type: " + Expected.ToString() + " with the actual type: " + Actual.ToString() + Environment.NewLine +
            "In line: " + Line.ToString() + ", column: " + Column.ToString() + Environment.NewLine +
            Message;
        } 

    }

    class ASTException : Exception
    {
        public ASTException(string message) : base(message)
        {

        }
    }
}
