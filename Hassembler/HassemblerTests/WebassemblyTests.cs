using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;

namespace HassemblerTests
{
    /// <summary>
    /// Tests the WebAssembly compiler.
    /// Includes all tests from <see cref="InterpreterTests"/>
    /// in a way that tests the WebAssembly compiler instead of the interpreter.
    /// Tests that are heavily interpreter-specific (such as tests that
    /// test parameter delivery) have been cut out.
    /// </summary>
    [TestClass]
    public class WebassemblyTests
    {
        Hassembler.Hassembler hassembler = new Hassembler.Hassembler();

        /// <summary>
        /// Tests basic arithmetic function (addition and multiplication).
        /// Source code: f = 1+2*3
        /// </summary>
        [TestMethod]
        public void Arith()
        {   
            string expectedOutput = 
            "(module\n" +
            "  (func $f (result i32)\n" +
            "    (i32.add\n" +
            "      (i32.const 1)\n" +
            "      (i32.mul\n" +
            "        (i32.const 2)\n" +
            "        (i32.const 3)\n" +
            "      )\n" +
            "    )\n" +
            "  )\n" +
            "  (export \"f\" (func $f))\n" +
            ")";

            hassembler.ParseCode("f = 1+2*3");
            Assert.AreEqual(expectedOutput, hassembler.GetInWebAssemblyTextFormat());
        }

        /// <summary>
        /// Tests basic arithmetic function (division and subtraction)
        /// Source code: f = (10-1)/3
        /// </summary>
        [TestMethod]
        public void Arith2()
        {
            hassembler.ParseCode("f = (10-1)/3");

            string expectedOutput =
             "(module\n" +
             "  (func $f (result i32)\n" +
             "    (i32.div_s\n" +
             "      (i32.sub\n" +
             "        (i32.const 10)\n" +
             "        (i32.const 1)\n" +
             "      )\n" +
             "      (i32.const 3)\n" +
             "    )\n" +
             "  )\n" +
             "  (export \"f\" (func $f))\n" +
             ")";

            Assert.AreEqual(expectedOutput, hassembler.GetInWebAssemblyTextFormat());
        }


        /// <summary>
        /// Tests booleans
        /// Source code: f = 2 == 2
        /// </summary>
        [TestMethod]
        public void BoolTest()
        {
            hassembler.ParseCode("f = 2 == 2");

            string expectedOutput =
             "(module\n" +
             "  (func $f (result i32)\n" +
             "    (i32.eq\n" +
             "      (i32.const 2)\n" +
             "      (i32.const 2)\n" +
             "    )\n" +
             "  )\n" +
             "  (export \"f\" (func $f))\n" +
             ")";

            Assert.AreEqual(expectedOutput, hassembler.GetInWebAssemblyTextFormat());
        }

        /// <summary>
        /// Tests condition statements (if then else)
        /// Source code: f = if 1 < 3 then 4+4 else 2 + 3
        /// </summary>
        [TestMethod]
        public void ITETest()
        {
            string expectedOutput =
             "(module\n" +
             "  (func $f (result i32)\n" +
             "    (if (result i32)\n" +
             "      (i32.lt_s\n" +
             "        (i32.const 1)\n" +
             "        (i32.const 3)\n" +
             "      )\n" +
             "      (then\n" +
             "        (i32.add\n" +
             "          (i32.const 4)\n" +
             "          (i32.const 4)\n" +
             "        )\n" +
             "      )\n" +
             "      (else\n" +
             "        (i32.add\n" +
             "          (i32.const 2)\n" +
             "          (i32.const 3)\n" +
             "        )\n" +
             "      )\n" +
             "    )\n" +
             "  )\n" +
             "  (export \"f\" (func $f))\n" +
             ")";

            hassembler.ParseCode("f = if 1 < 3 then 4+4 else 2 + 3");
            Assert.AreEqual(expectedOutput, hassembler.GetInWebAssemblyTextFormat());
        }


        /// <summary>
        /// Tests LOTS of parentheses
        /// Source code: f = ((2+(2*30))/(2*1))
        /// </summary>
        [TestMethod]
        public void ParenTest()
        {
            string expectedOutput =
                          "(module\n" +
                          "  (func $f (result i32)\n" +
                          "    (i32.div_s\n" +
                          "      (i32.add\n" +
                          "        (i32.const 2)\n" +
                          "        (i32.mul\n" +
                          "          (i32.const 2)\n" +
                          "          (i32.const 30)\n" +
                          "        )\n" +
                          "      )\n" +
                          "      (i32.mul\n" +
                          "        (i32.const 2)\n" +
                          "        (i32.const 1)\n" +
                          "      )\n" +
                          "    )\n" +
                          "  )\n" +
                          "  (export \"f\" (func $f))\n" +
                          ")";

            hassembler.ParseCode("f = ((2+(2*30))/(2*1))");
            Assert.AreEqual(expectedOutput, hassembler.GetInWebAssemblyTextFormat());
        }

        /// <summary>
        /// Test multiple functions and parameters
        /// Source code:
        /// "fabulous a b = a + b
        /// y=4 + fabulous 2 3"
        /// </summary>
        [TestMethod]
        public void FunCallWithParameters()
        {
            string expectedOutput =
                          "(module\n" +
                          "  (func $fabulous (param $a i32) (param $b i32) (result i32)\n" +
                          "    (i32.add\n" +
                          "      (get_local $a)\n" +
                          "      (get_local $b)\n" +
                          "    )\n" +
                          "  )\n" +
                          "  (export \"fabulous\" (func $fabulous))\n" +
                          "\n" +
                          "  (func $y (result i32)\n" +
                          "    (i32.add\n" +
                          "      (i32.const 4)\n" +
                          "      (call $fabulous\n" +
                          "        (i32.const 2)\n" +
                          "        (i32.const 3)\n" +
                          "      )\n" +
                          "    )\n" +
                          "  )\n" +
                          "  (export \"y\" (func $y))\n" +
                          ")";

            hassembler.ParseCode("fabulous a b = a + b\ny=4 + fabulous 2 3\n");
            Assert.AreEqual(expectedOutput, hassembler.GetInWebAssemblyTextFormat());
        }

        /// <summary>
        /// Tests function references to other functions
        /// Source code: 
        /// a=3
        /// f=a+9
        /// </summary>
        [TestMethod]
        public void FunWithRef()
        {
            string expectedOutput =
                          "(module\n" +
                          "  (func $a (result i32)\n" +
                          "    (i32.const 3)\n" +
                          "  )\n" +
                          "  (export \"a\" (func $a))\n" +
                          "\n" +
                          "  (func $f (result i32)\n" +
                          "    (i32.add\n" +
                          "      (call $a)\n" +
                          "      (i32.const 9)\n" +
                          "    )\n" +
                          "  )\n" +
                          "  (export \"f\" (func $f))\n" +
                          ")";

            hassembler.ParseCode("a=3\r\nf=a+9");
            Assert.AreEqual(expectedOutput, hassembler.GetInWebAssemblyTextFormat());
        }
    

        /// <summary>
        /// Tests recursive function with fibonacci (giving end condition)
        /// Source code: 
        /// "f x y end = if y < end then f y (x+y) end else y"
        /// </summary>
        [TestMethod]
        public void Recursion()
        {
            string expectedOutput =
                          "(module\n" +
                          "  (func $f (param $x i32) (param $y i32) (param $end i32) (result i32)\n" +
                          "    (if (result i32)\n" +
                          "      (i32.lt_s\n" +
                          "        (get_local $y)\n" +
                          "        (get_local $end)\n" +
                          "      )\n" +
                          "      (then\n" +
                          "        (call $f\n" +
                          "          (get_local $y)\n" +
                          "          (i32.add\n" +
                          "            (get_local $x)\n" +
                          "            (get_local $y)\n" +
                          "          )\n" +
                          "          (get_local $end)\n" +
                          "        )\n" +
                          "      )\n" +
                          "      (else\n" +
                          "        (get_local $y)\n" +
                          "      )\n" +
                          "    )\n" +
                          "  )\n" +
                          "  (export \"f\" (func $f))\n" +
                          ")";

            hassembler.ParseCode("f x y end = if y < end then f y (x+y) end else y");
            Assert.AreEqual(expectedOutput, hassembler.GetInWebAssemblyTextFormat());
        }
    }
}
