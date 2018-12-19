using System;
using Antlr4.Runtime;
using Antlr4.Runtime.Tree;

namespace Hassembler
{
    class Program
    {
        static void Main(string[] args)
        {
            string testifunktio = "a=1+1+1*3\nf=2+4\ng=2*(4+5)\nh=2*4+5\nr=f+a\nbNM=2+r\n";

            ICharStream charStream = CharStreams.fromstring(testifunktio);
            HaskellmmLexer lexer = new HaskellmmLexer(charStream);
            HaskellmmParser parser = new HaskellmmParser(new CommonTokenStream(lexer));
            IParseTree tree = parser.prog();
            Console.WriteLine(tree.ToStringTree().Replace("\\n", Environment.NewLine));
            var visitor = new EvalVisitor();
            VisitorResult result = visitor.Visit(tree);
            Console.WriteLine(result);

            Console.WriteLine();
            Console.WriteLine("Found functions: ");
            visitor.Functions.ForEach(f => Console.WriteLine(f.Name));
            
            while (true)
            {
                Console.WriteLine();
                Console.Write("Input function name to evaluate it: > ");
                string name = Console.ReadLine();
                Console.WriteLine();

                Function f = visitor.Functions.Find(fnc => fnc.Name == name);

                if (f == null)
                {
                    Console.WriteLine($"Function {name} not found!");
                    continue;
                }

                Console.WriteLine($"{name} = {f.StartNode.GetValue()}");
            }
        }
    }
}
