using System;
using System.Text;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using BFH.EADN.Common.Types.Contracts;

namespace BFH.EADN.CommonTests
{
    /// <summary>
    /// Tests for types
    /// </summary>
    [TestClass]
    public class Types
    {
        [TestMethod]
        public void Contracts()
        {
            //Base
            TestHelper.TestHelper.TestProperties<ServiceFault>();
            
            //Management
            TestHelper.TestHelper.TestProperties<Answer>();
            TestHelper.TestHelper.TestProperties<Question>();
            TestHelper.TestHelper.TestProperties<Quiz>();
            TestHelper.TestHelper.TestProperties<Topic>();
            TestHelper.TestHelper.TestProperties<User>();

            //Play
            TestHelper.TestHelper.TestProperties<PlayQuestion>();
            TestHelper.TestHelper.TestProperties<QuestionAnswerState>();
        }
    }
}
