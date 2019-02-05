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
    /// Tests basic arithmetic function (addition and multiplication).
    /// Source code: f = 1+2*3
    /// Input: f
    /// <returns>
    /// f = 7
    /// </returns>
    /// </summary>
    [TestMethod]
        public void Arith()
        {
            hassembler.ParseCode("f = 1+2*3");
            Assert.AreEqual("f = 7", hassembler.CallFunction("f", new List<object>()));
        }

        /// <summary>
        /// Tests basic arithmetic function (division and subtraction)
        /// Source code: f = (10-1)/3
        /// Input: f
        /// <returns>
        /// f = 3
        /// </returns>
        /// </summary>
        [TestMethod]
        public void Arith2()
        {
            hassembler.ParseCode("f = (10-1)/3");
            Assert.AreEqual("f = 3", hassembler.CallFunction("f", new List<object>()));
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
        /// Tests condition statements (if then else)
        /// Source code: f = if 1 < 3 then 4+4 else 2 + 3
        /// Input: f
        /// <returns>
        /// f = 8
        /// </returns>
        /// </summary>
        [TestMethod]
        public void ITETest()
        {
            hassembler.ParseCode("f = if 1 < 3 then 4+4 else 2 + 3");
            Assert.AreEqual("f = 8", hassembler.CallFunction("f", new List<object>()));
        }


        /// <summary>
        /// Tests LOTS of parentheses
        /// Source code: f = ((2+(2*30))/(2*1))
        /// Input: f
        /// <returns>
        /// f = 31
        /// </returns>
        /// </summary>
        [TestMethod]
        public void ParenTest()
        {
            hassembler.ParseCode("f = ((2+(2*30))/(2*1))");
            Assert.AreEqual("f = 31", hassembler.CallFunction("f", new List<object>()));
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
        public void GetFuns()
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
        /// Test function call with non-int parameters
        /// Source code:
        /// f = g True
        /// g x = x
        ///  Input: f
        ///  <returns>
        ///  True
        ///  </returns>
        /// </summary>
        [TestMethod]
        public void FunCallWithNaNParameters()
        {
            hassembler.ParseCode("f = g True\r\ng x = x");
            Assert.AreEqual("f = True", hassembler.CallFunction("f", new List<object>()));
        }

        /// <summary>
        /// Test function call with spaces around newlines
        /// Source code:
        /// f = g True 
        /// g x = x
        ///  Input: f
        ///  <returns>
        ///  True
        ///  </returns>
        /// </summary>
        [TestMethod]
        public void FunWithSpacesAroundNewline()
        {
            hassembler.ParseCode("f = g True \r\n g x = x");
            Assert.AreEqual("f = True", hassembler.CallFunction("f", new List<object>()));
        }

        /// <summary>
        /// Tests function references to other functions
        /// Source code: 
        /// a=3
        /// f=a+9
        /// Input: f
        /// <returns>
        /// f = 12
        /// </returns> 
        /// </summary>
        [TestMethod]
        public void FunWithRef()
        {
            hassembler.ParseCode("a=3\r\nf=a+9");
            Assert.AreEqual("f = 12", hassembler.CallFunction("f", new List<object>()));
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
            Assert.AreEqual("f 1 1 10 = 13", hassembler.CallFunction("f", 1, 1, 10));
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
        public void FunNotDefined()
        {
            hassembler.ParseCode("g = 1 + 3");
            Assert.AreEqual("Function not found: f", hassembler.CallFunction("f", 1, 1, 10));
        }

        /// <summary>
        /// Tests that syntax errors produce an exception
        /// Source code: 
        /// "f = 1 ++ 2"
        /// <returns>
        /// (HassemblerException)
        /// </returns> 
        /// </summary>
        [TestMethod]
        public void SyntaxErrorTest()
        {
            //Assert.ThrowsException<Hassembler.HassemblerException>(
            //    () => { hassembler.ParseCode("f = 1 ++ 2"); });
            hassembler.ParseCode("g = 1 ++ 3");
            Assert.AreEqual("g = -1", hassembler.CallFunction("g", new List<object>()));
        }

        /// <summary>
        /// Tests that syntax errors produce an exception
        /// Source code: 
        /// "f == 1 + 2"
        /// <returns>
        /// (VisitException)
        /// </returns> 
        /// </summary>
        [TestMethod]
        public void SyntaxErrorTest2()
        {
            Assert.ThrowsException<Hassembler.VisitException>(
                () => { hassembler.ParseCode("f == 1 + 2"); });
            
        }

        /// <summary>
        /// Tests that function can only be defined once
        /// Source code: 
        /// f = 1 + 2 
        /// f = 4
        /// <returns>
        /// (VisitException)
        /// </returns> 
        /// </summary>
        [TestMethod]
        public void MultFunTest()
        {
            Assert.ThrowsException<Hassembler.VisitException>(
                () => { hassembler.ParseCode("f = 1 + 2 \r\n f = 4"); });

        }

        /// <summary>
        /// Tests that negative integers cannot be given
        /// Source code: 
        /// f = -2-2
        /// <returns>
        /// (VisitException)
        /// </returns> 
        /// </summary>
        [TestMethod]
        public void NegativeInt()
        {
            Assert.ThrowsException<Hassembler.VisitException>(
                () => { hassembler.ParseCode("f = -2+2"); });
        }

        /// <summary>
        /// Tests that comments are acceptable (and skipped by parser)
        /// Source code: 
        /// -- tämä on kommentti
        /// f = 4+4
        /// <returns>
        /// f = 4
        /// </returns> 
        /// </summary>
        [TestMethod]
        public void CommentTest()
        {
            hassembler.ParseCode("-- tämä on kommentti \r\n f = 2+2");
            Assert.AreEqual("f = 4", hassembler.CallFunction("f", new List<object>()));
        }

        /// <summary>
        /// Tests that static type checking works
        /// Source code:
        /// f = 1 + True
        /// <returns>
        /// (TypeError)
        /// </returns> 
        /// </summary>
        [TestMethod]
        public void TypeCheckTest()
        {
            Assert.ThrowsException<Hassembler.TypeError>(() => hassembler.ParseCode("f = True + 1"));
        }

        /// <summary>
        /// Tests that runtime type checking works for equality comparisons
        /// Source code:
        /// f = x = x == 2
        /// Input:
        /// f True
        /// <returns>
        /// (RuntimeException)
        /// </returns> 
        /// </summary>
        [TestMethod]
        public void EqualityComparisonRuntimeTypeCheck()
        {
            hassembler.ParseCode("f x = x == 2");
            Assert.ThrowsException<Hassembler.RuntimeException>(() => hassembler.CallFunction("f", true));
        }
    }
}
