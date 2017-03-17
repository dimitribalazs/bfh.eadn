﻿using System;
using System.Text;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using BFH.EADN.Common.Types.Contracts;

namespace BFH.EADN.CommonTests
{
    /// <summary>
    /// Summary description for Types
    /// </summary>
    [TestClass]
    public class Types
    {
        public Types()
        {
            //
            // TODO: Add constructor logic here
            //
        }

        private TestContext testContextInstance;

        /// <summary>
        ///Gets or sets the test context which provides
        ///information about and functionality for the current test run.
        ///</summary>
        public TestContext TestContext
        {
            get
            {
                return testContextInstance;
            }
            set
            {
                testContextInstance = value;
            }
        }

        #region Additional test attributes
        //
        // You can use the following additional attributes as you write your tests:
        //
        // Use ClassInitialize to run code before running the first test in the class
        // [ClassInitialize()]
        // public static void MyClassInitialize(TestContext testContext) { }
        //
        // Use ClassCleanup to run code after all tests in a class have run
        // [ClassCleanup()]
        // public static void MyClassCleanup() { }
        //
        // Use TestInitialize to run code before running each test 
        // [TestInitialize()]
        // public void MyTestInitialize() { }
        //
        // Use TestCleanup to run code after each test has run
        // [TestCleanup()]
        // public void MyTestCleanup() { }
        //
        #endregion

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
