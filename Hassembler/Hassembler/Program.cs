using System;
using Antlr4.Runtime;
using Antlr4.Runtime.Tree;

namespace Hassembler
{
    class Program
    {
        static void Main(string[] args)
        {
            String testifunktio = "f=1+1*3\n";
            
            ICharStream testi = CharStreams.fromstring(testifunktio);
            HaskellmmLexer lexer = new HaskellmmLexer (testi);
            HaskellmmParser parser = new HaskellmmParser(new CommonTokenStream(lexer));
            IParseTree tree = parser.prog();
            Console.WriteLine(tree.ToStringTree());
            Console.WriteLine(tree.GetChild(2).ToStringTree());
        }
    }
}
