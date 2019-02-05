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
    public interface IEnv
    {
        /// <summary>
        /// Calls a function.
        /// </summary>
        /// <param name="functionName">The function to call.</param>
        /// <param name="paramList">A list of parameters to call the function with.
        /// You can use null for none.</param>
        Result GetFunctionValue(string functionName, List<object> paramList = null);

        /// <summary>
        /// Adds a parameter to the environment.
        /// </summary>
        /// <param name="name">The name of the parameter.</param>
        /// <param name="value">The value of the parameter.</param>
        void AddParam(string name, object value);

        /// <summary>
        /// Returns the value of a parameter.
        /// </summary>
        /// <param name="name">The name of the parameter.</param>
        object GetParam(string name);
    }

    /// <summary>
    /// Visitor / interpreter environment.
    /// Contains functions found from the code and function parameters
    /// for the interpreter.
    /// </summary>
    class Env : IEnv
    {
        public Env() { }

        private List<Function> functions;

        private Dictionary<string, object> parameters = new Dictionary<string, object>();

        public void AddParam(string name, object value)
        {
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

        /// <summary>
        /// Calls a function.
        /// </summary>
        /// <param name="functionName">The name of the function.</param>
        /// <param name="parameterList">The parameters given to the function. Use null for none.</param>
        public Result GetFunctionValue(string functionName, List<object> parameterList = null)
        {
            if (functions == null)
                throw new RuntimeException("Env: Function list not initialized");

            var oldParams = parameters;
            parameters = new Dictionary<string, object>();

            Function funk = functions.Find(f => f.Name == functionName);
            if (funk == null)
            {
                throw new RuntimeException($"No function named {functionName} found in environment");
            }

            if (parameterList == null)
            {
                if (funk.ParamCount > 0)
                    throw new RuntimeException("No parameters provided when calling function " + funk.Name);
            }

            if (parameterList.Count != funk.ParamCount)
            {
                throw new RuntimeException($"Unmatching parameter " +
                    $"count when calling function {funk.Name}: expected " +
                    $"{funk.ParamCount}, got {parameterList.Count}");
            }

            for (int i = 0; i < parameterList.Count; i++)
            {
                parameters.Add(funk.GetParameter(i).Name, parameterList[i]);
            }

            Result result = funk.StartNode.GetValue();
            parameters = oldParams;
            return result;
        }

        public void CleanupParams()
        {
            parameters.Clear();
        }
    }
}