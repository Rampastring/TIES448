using System;
using System.Collections.Generic;
using Antlr4.Runtime;
using Antlr4.Runtime.Tree;

namespace Hassembler
{
    /// <summary>
    /// Interface for the environment.
    /// Used for avoiding a circular reference (Env -> Function -> ExprNode -> Env).
    /// </summary>
    interface IEnv
    {
        Result GetReferenceValue(string functionName);
    }

    class Env : IEnv
    {
        public Env() { }

        private List<Function> functions;

        public void SetFunctions(List<Function> functionList)
        {
            functions = functionList;
        }

        public Result GetReferenceValue(string functionName)
        {
            if (functions == null)
                throw new Exception("Env: Function list not initialized");

            Function funk = functions.Find(f => f.Name == functionName);
            if (funk == null) throw new Exception
                ($"No function named {functionName} found in environment");
            else return funk.StartNode.GetValue();
        } 

    }
}