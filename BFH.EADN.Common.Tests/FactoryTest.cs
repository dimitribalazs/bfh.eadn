// <copyright file="FactoryTest.cs">Copyright ©  2016</copyright>
using System;
using BFH.EADN.Common;
using Microsoft.Pex.Framework;
using Microsoft.Pex.Framework.Validation;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace BFH.EADN.Common.Tests
{
    /// <summary>This class contains parameterized unit tests for Factory</summary>
    [PexClass(typeof(Factory))]
    [PexAllowedExceptionFromTypeUnderTest(typeof(InvalidOperationException))]
    [PexAllowedExceptionFromTypeUnderTest(typeof(ArgumentException), AcceptExceptionSubtypes = true)]
    [TestClass]
    public partial class FactoryTest
    {
        /// <summary>Test stub for CreateInstance()</summary>
        [PexGenericArguments(typeof(object))]
        [PexMethod]
        public T CreateInstanceTest<T>()
            where T : class
        {
            T result = Factory.CreateInstance<T>();
            return result;
            // TODO: add assertions to method FactoryTest.CreateInstanceTest()
        }
    }
}
