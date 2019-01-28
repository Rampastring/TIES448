using System;
using Antlr4.Runtime;
using Antlr4.Runtime.Tree;

namespace Hassembler
{
    class Program
    {
        static void Main(string[] args)
        {
            // string testiohjelma = "a=1+1+1*3\r\nf=2+4\r\ng=2*(4+5)\r\nh=2*4+5\r\nr=f+a\r\nbn=2+r\r\n";
            // string testiohjelma = "a=0-4\r\nb=2 + if a then 4+4 else 2 + 3";
            // string testiohjelma = "fabulous a b = a + b\ny=4 + fabulous 2 3\n";
            string testiohjelma = "fabulous a = if a > 0 then (a + fabulous (a-1)) else 0";

            ICharStream charStream = CharStreams.fromstring(testiohjelma);
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

                string[] parts = name.Split(' ', StringSplitOptions.RemoveEmptyEntries);
                int givenParamCount = parts.Length - 1;

                if (givenParamCount < 0)
                {
                    Console.WriteLine("No function name given!");
                    continue;
                }

                Function f = visitor.Functions.Find(fnc => fnc.Name == parts[0]);

                if (f == null)
                {
                    Console.WriteLine($"Function {name} not found!");
                    continue;
                }

                if (f.ParamCount > givenParamCount)
                {
                    Console.WriteLine("Not enough parameters given, expected " + f.ParamCount);
                    continue;
                }
                else if (f.ParamCount < givenParamCount)
                {
                    Console.WriteLine("Too many parameters given, expected " + f.ParamCount);
                    continue;
                }

                var functionParams = f.Parameters;

                for (int i = 1; i < parts.Length; i++)
                {
                    if (!int.TryParse(parts[i], out int paramValue))
                    {
                        Console.WriteLine("Failed to convert parameter #" + (i - 1) + " to int!");
                    }

                    visitor.Env.AddParam(functionParams[i - 1].Name, paramValue);
                }

                Console.WriteLine($"{name} = {f.StartNode.GetValue()}");
                visitor.Env.CleanupParams();
            }
        }
    }
}
