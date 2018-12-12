namespace Hassembler
{
    /// <summary>
    /// A Haskellmm function.
    /// </summary>
    class Function
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
    }
}
