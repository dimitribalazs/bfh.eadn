using Microsoft.VisualStudio.TestTools.UnitTesting;
using Ploeh.AutoFixture;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace BFH.EADN.CommonTests.TestHelper
{
    public class TestHelper
    {

        /// <summary>
        /// Checks all primitive types and some other types like Guid, string. 
        /// If it's not a primitive type result is true
        /// </summary>
        /// <typeparam name="T">class to test</typeparam>
        /// <returns></returns>
        public static bool TestProperties<T>()
            where T : class
        {
            Fixture fixture = new Fixture();
            T instance = fixture.Build<T>().Create();
            foreach (PropertyInfo property in instance.GetType().GetProperties())
            {
                bool testResult = false;

                Type currentProperty = property.PropertyType;
                if (currentProperty == typeof(bool)) 
                {
                    testResult = TestProperty<bool, T>(property, instance);
                }
                else if (currentProperty == typeof(Guid))
                {
                    testResult = TestProperty<Guid, T>(property, instance);
                }
                else if (currentProperty == typeof(byte))
                {
                    testResult = TestProperty<byte, T>(property, instance);
                }
                else if(currentProperty == typeof(sbyte))
                {
                    testResult = TestProperty<sbyte, T>(property, instance);
                }
                else if (currentProperty == typeof(char))
                {
                    testResult = TestProperty<char, T>(property, instance);
                }
                else if (currentProperty == typeof(decimal))
                {
                    testResult = TestProperty<decimal, T>(property, instance);
                }
                else if (currentProperty == typeof(double))
                {
                    testResult = TestProperty<double, T>(property, instance);
                }
                else if (currentProperty == typeof(float))
                {
                    testResult = TestProperty<float, T>(property, instance);
                }
                else if (currentProperty == typeof(int))
                {
                    testResult = TestProperty<int, T>(property, instance);
                }
                else if (currentProperty == typeof(uint))
                {
                    testResult = TestProperty<uint, T>(property, instance);
                }
                else if (currentProperty == typeof(long))
                {
                    testResult = TestProperty<long, T>(property, instance);
                }
                else if (currentProperty == typeof(ulong))
                {
                    testResult = TestProperty<ulong, T>(property, instance);
                }
                else if (currentProperty == typeof(short))
                {
                    testResult = TestProperty<short, T>(property, instance);
                }
                else if (currentProperty == typeof(ushort))
                {
                    testResult = TestProperty<ushort, T>(property, instance);
                }
                else if (currentProperty == typeof(string))
                {
                    testResult = TestProperty<string, T>(property, instance);
                }
                else if(currentProperty.IsPrimitive == false)
                {
                    testResult = true;
                }

                if(testResult == false)
                {
                    return false;
                }
            }

            return true;
        }

        public static bool TestProperty<TPropertyType, TInstance>(PropertyInfo property, TInstance instance)
            where TInstance : class
        {
            Fixture fixture = new Fixture();
            TPropertyType input = fixture.Create<TPropertyType>();
            property.SetValue(instance, input);
            TPropertyType output = (TPropertyType)property.GetValue(instance);

            return input.Equals(output);
        }
    }
}
