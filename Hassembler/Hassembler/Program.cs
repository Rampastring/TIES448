﻿using System;
using System.Collections.Generic;
using Antlr4.Runtime;
using Antlr4.Runtime.Tree;

namespace Hassembler
{
    class Program
    {
        static void Main(string[] args)
        {
            //string testiohjelma = "a=1+1+1*3\r\nf=2+4\r\ng=2*(4+5)\r\nh=2*4+5\r\nr=f+a\r\nbn=2+r\r\n";
            // string testiohjelma = "a=0-4\r\nb=2 + if a then 4+4 else 2 + 3";
            // string testiohjelma = "fabulous a b = a + b\ny=4 + fabulous 2 3\n";
            // string testiohjelma = "fabulous a = if a > 0 then (a + fabulous (a-1)) else 0";
            string testiohjelma = "f = 2 == 2\ng = True == True\nh = False == False\n" +
                "i = True == False\nj = True != False\nk = True != True\n";
            
            Console.WriteLine("Haskell-- (Haskell-minus-minus) interpreter");

            Hassembler hassembler = new Hassembler();
            hassembler.ParseCode(testiohjelma);

            Console.WriteLine();
            Console.WriteLine("Found functions: ");
            hassembler.GetFunctions.ForEach(f => Console.WriteLine(f.Name));
            
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

                List<object> parameters = new List<object>();

                for (int i = 1; i < parts.Length; i++)
                {
                    if (!int.TryParse(parts[i], out int paramValue))
                    {
                        Console.WriteLine("Failed to convert parameter #" + (i - 1) + " to int!");
                    }

                    parameters.Add(paramValue);
                }

                Console.WriteLine(hassembler.CallFunction(parts[0], parameters));
            }
        }
    }
}
