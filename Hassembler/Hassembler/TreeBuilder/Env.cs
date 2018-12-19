using System;
using System.Collections.Generic;
using Antlr4.Runtime;
using Antlr4.Runtime.Tree;

namespace Hassembler
{
    class Env
    {
        private List<Function> functions;

        public Result GetReferenceValue(string functionName)
        {
            Function funk = functions.Find(f => f.Name == functionName);
            if (funk == null) throw new Exception
                ($"No function named {functionName} found in environment");
            else return funk.StartNode.GetValue();
        } 

    }
}