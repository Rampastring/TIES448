using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;

namespace HassemblerTests
{
    [TestClass]
    public class InterpreterTests
    {
        Hassembler.Hassembler hassembler = new Hassembler.Hassembler();

        /// <summary>
        /// Tests basic arithmetic.
        /// Input: 1+2*3
        /// <returns>
        /// 7
        /// </returns>
        /// </summary>
        //[TestMethod]
        //public void Arith()
        //{
        //    hassembler.ParseCode("1+2*3");
        //    Assert.Fail();
        // currently the interpreter does not handle basic arithmetics outside functions 
        // }

    /// <summary>
    /// Tests basic arithmetic function.
    /// Source code: f = 1+2*3
    /// Input: f
    /// <returns>
    /// f = 7
    /// </returns>
    /// </summary>
    [TestMethod]
        public void ArithFun()
        {
            hassembler.ParseCode("f = 1+2*3");
            Assert.AreEqual("f = 7", hassembler.CallFunction("f", new List<object>()));
        }

        /// <summary>
        /// Tests booleans
        /// Source code: f = 2 == 2
        /// Input: f
        /// <returns>
        /// f = True
        /// </returns>
        /// </summary>
        [TestMethod]
        public void BoolTest()
        {
            hassembler.ParseCode("f = 2 == 2");
            Assert.AreEqual("f = True", hassembler.CallFunction("f", new List<object>()));
        }
        /// <summary>
        /// Tests the getter for all functions
        /// Source code: 
        /// a=1+1+1*3
        /// f=2+4
        /// <returns>
        /// a f
        /// </returns> 
        /// </summary>
        [TestMethod]
        public void GetFuncs()
        {
            hassembler.ParseCode("a=1+1+1*3\r\nf=2+4");
            string str = "";
            foreach (Hassembler.Function f in hassembler.GetFunctions)
                str += f.Name + ' ';
            Assert.AreEqual("a f ", str);
        }

        /// <summary>
        /// Test function call with parameters
        /// Source code:
        /// "fabulous a b = a + b
        ///  y=4 + fabulous 2 3
        ///  "
        ///  Input: y
        ///  <returns>
        ///  
        ///  </returns>
        /// </summary>
        [TestMethod]
        public void FunCallWithParameters()
        {
            hassembler.ParseCode("fabulous a b = a + b\ny=4 + fabulous 2 3\n");
            Assert.AreEqual("y = 9", hassembler.CallFunction("y", new List<object>()));
        }
        /// <summary>
        /// Tests function references to other functions
        /// Input: 
        /// a=3
        /// f=a+4
        /// <returns>
        /// Found functions: a f
        /// </returns> 
        /// </summary>
        [TestMethod]
        public void FuncWithRef()
        {
            hassembler.ParseCode("a=3\r\nf=a+4");
            string str = "";
            foreach (Hassembler.Function f in hassembler.GetFunctions)
                str += f.Name + ' ';
            Assert.AreEqual("a f ", str);
        }

        /// <summary>
        /// Tests recursive function with fibonacci (giving end condition)
        /// Source code: 
        /// "f x y end = if y < end then f y (x+y) end else y"
        /// Input: 
        /// f 1 1 10
        /// <returns>
        /// f = 13
        /// </returns> 
        /// </summary>
        [TestMethod]
        public void Recursion()
        {
            hassembler.ParseCode("f x y end = if y < end then f y (x+y) end else y");
            Assert.AreEqual("f = 13", hassembler.CallFunction("f", new List<object>() { 1,1,10 } ));
        }

        /// <summary>
        /// Tests function call to undefined function 
        /// Source code: 
        /// "g = 1 + 3"
        /// Input: 
        /// f
        /// <returns>
        /// "Function not found"
        /// </returns> 
        /// </summary>
        [TestMethod]
        public void FuncNotDefined()
        {
            hassembler.ParseCode("g = 1 + 3");
            Assert.AreEqual("Function not found: f", hassembler.CallFunction("f", new List<object>() { 1, 1, 10 }));
        }

        /// <summary>
        /// Tests that syntax errors produce an exception
        /// Source code: 
        /// "f = 1 ++ 2"
        /// <returns>
        /// 
        /// </returns> 
        /// </summary>
        [TestMethod]
        public void SyntaxErrorTest()
        {
            Assert.ThrowsException<Hassembler.HassemblerException>(
                () => { hassembler.ParseCode("f = 1 ++ 2"); });
        }



    }
}
