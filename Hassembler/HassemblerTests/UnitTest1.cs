using Microsoft.VisualStudio.TestTools.UnitTesting;
using Hassembler;
using System.Collections.Generic;

namespace HassemblerTests
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestMethod1()
        {
            var hassembler = new Hassembler.Hassembler();
            hassembler.ParseCode("f = 2 == 2");
            Assert.AreEqual("f = True", hassembler.CallFunction("f", new List<object>()));
        }
    }
}
