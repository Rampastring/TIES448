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
        void AddParam(string name, object value);
        object GetParam(string name);
    }

    class Env : IEnv
    {
        public Env() { }

        private List<Function> functions;

        private Dictionary<string, object> parameters = new Dictionary<string, object>();

        public void AddParam(string name, object value)
        {
            // TODO handle parameters with same names
            parameters.Add(name, value);
        }

        public object GetParam(string name)
        {
            if (parameters.TryGetValue(name, out object value))
                return value;

            throw new KeyNotFoundException("Parameter not found: " + name);
        }

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