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

        [TestMethod]
        public void TestCaseProperty()
        {
            TestProperties(new Answer());
        }
        public void TestProperties<T>(T instance) where T : class
        {
            foreach (PropertyInfo property in instance.GetType().GetProperties())
            {

                if (property.PropertyType == typeof(string))
                {
                    string test = GetString();
                    string result = TestProperty(property, test);
                    Assert.AreEqual(test, result);
                }
                else if (property.PropertyType == typeof(int))
                {

                    int testInt = GetInteger();
                    int resultInt = TestProperty(property, testInt);
                    Assert.AreEqual(testInt, resultInt);
                }
                else if (property.PropertyType == typeof(bool))
                {

                    bool testBool = GetBool();
                    bool resultBool = TestProperty(property, testBool);
                    Assert.AreEqual(testBool, resultBool);
                }
                else if (property.PropertyType == typeof(Guid))
                {
                    Guid testGuid = Guid.NewGuid();
                    Guid resultGuid = TestProperty(property, testGuid);
                    Assert.AreEqual(testGuid, resultGuid);
                }
            }
        }

        public T TestProperty<T>(PropertyInfo property, T value)
        {
            property.SetValue(property.GetType(), value);
            return (T)property.GetValue(property);
        }

        public string GetString()
        {
            return "ABCDFGHIJKLJKLJasdfas";
        }

        public int GetInteger()
        {
            Random rnd = new Random();
            return rnd.Next(1, 999999);
        }

        public decimal GetDecimal()
        {
            Random rnd = new Random();
            return (decimal)rnd.Next(1, 999999);
        }

        public bool GetBool()
        {
            Random rnd = new Random();
            return rnd.Next(1, 999999) % 2 == 0;
        }
    }
}
