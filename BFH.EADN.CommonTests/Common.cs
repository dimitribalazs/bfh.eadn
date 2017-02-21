using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Reflection;
using BFH.EADN.Common.Types.Contracts;

namespace BFH.EADN.CommonTests
{
    [TestClass]
    public class CommonTest
    {
        [TestMethod]
        [ExpectedException(typeof(KeyNotFoundException))]
        public void GetConfigValueFailedException()
        {
            object result = Common.Common.GetConfigValue<int>(null);
        }


        [TestMethod]
        public void GetConfigValueSuccess()
        {
            var intResult = Common.Common.GetConfigValue<int>("year");
            var stringResult = Common.Common.GetConfigValue<string>("message");

            Assert.AreEqual(1950, intResult);
            Assert.AreEqual("this is a text", stringResult);
        }
    }
}
