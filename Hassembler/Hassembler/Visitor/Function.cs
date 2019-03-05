using System.Collections.Generic;
using System.Text;

namespace Hassembler
{
    /// <summary>
    /// A Haskellmm function.
    /// </summary>
    public class Function
    {
        public Function(string name)
        {
            Name = name;
        }

        /// <summary>
        /// The name of the function.
        /// </summary>
        public string Name { get; private set; }

        /// <summary>
        /// The root node of the function's expression tree.
        /// </summary>
        public ExprNode StartNode { get; set; }

        private List<Parameter> parameters = new List<Parameter>();

        /// <summary>
        /// Adds a parameter to the function.
        /// </summary>
        /// <param name="parameter">The parameter to add.</param>
        public void AddParameter(Parameter parameter)
        {
            parameters.Add(parameter);
        }

        public int ParamCount => parameters.Count;

        public Parameter GetParameter(int i) => parameters[i];

        /// <summary>
        /// Returns a copy of this function's parameter list.
        /// (A copy so you can't edit the original param list)
        /// </summary>
        public List<Parameter> Parameters => new List<Parameter>(parameters);

        /// <summary>
        /// Generates and returns the WebAssembly representation of this function.
        /// </summary>
        public string ToWebAssembly()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append($"  (func ${Name} ");
            foreach (Parameter parameter in Parameters)
            {
                sb.Append($"(param ${parameter.Name} i32) ");
            }
            sb.Append("(result i32)\n");
            sb.Append(StartNode.ToWebAssembly() + '\n');
            sb.Append("  )\n");
            sb.Append($"  (export \"{Name}\" (func ${Name}))");
            return sb.ToString();
        }
    }

    public struct Parameter
    {
        public Parameter(string name)
        {
            Name = name;
        }

        public string Name { get; private set; }
    }
}
