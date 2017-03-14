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
            int intResult = Common.Common.GetConfigValue<int>("year");
            string stringResult = Common.Common.GetConfigValue<string>("message");
            bool boolResult  = Common.Common.GetConfigValue<bool>("isTrue");
            Assert.AreEqual(1950, intResult);
            Assert.AreEqual("this is a text", stringResult);
            Assert.AreEqual(true, boolResult);
        }

        [TestMethod]
        public void CreateServiceFault()
        {
            Exception ex = new Exception("This is a text execption");
            string reason = "this is a reason";
            ServiceFault sf = Common.Common.CreateServiceFault(ex, reason);
            Assert.IsNotNull(sf);
            Assert.AreEqual(ex.Message, sf.Message);
            Assert.AreEqual(reason, sf.Reason);
        }
    }
}
