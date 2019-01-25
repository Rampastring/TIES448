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
        Result GetReferenceValue(string functionName, List<object> paramList = null);
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
        /// <returns></returns>
        public Result GetReferenceValue(string functionName, List<object> parameterList = null)
        {
            if (functions == null)
                throw new Exception("Env: Function list not initialized");

            var oldParams = parameters;
            parameters = new Dictionary<string, object>();

            Function funk = functions.Find(f => f.Name == functionName);
            if (funk == null)
            {
                throw new Exception($"No function named {functionName} found in environment");
            }
            else
            {
                if (parameterList == null)
                {
                    if (funk.ParamCount > 0)
                        throw new InvalidOperationException("No parameters províded when calling function " + funk.Name);
                }
                else
                {
                    if (parameterList.Count != funk.ParamCount)
                    {
                        throw new InvalidOperationException($"Unmatching parameter " +
                            $"count when calling function {funk.Name}: expected " +
                            $"{funk.ParamCount}, got {parameterList.Count}");
                    }

                    for (int i = 0; i < parameterList.Count; i++)
                    {
                        parameters.Add(funk.GetParameter(i).Name, parameterList[i]);
                    }
                }

                Result result = funk.StartNode.GetValue();
                parameters = oldParams;
                return result;
            }
        } 

        public void CleanupParams()
        {
            parameters.Clear();
        }

    }
}