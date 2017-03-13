using Microsoft.VisualStudio.TestTools.UnitTesting;
//using Ploeh.AutoFixture;
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
        public static void TestProperties<T>()
            where T : class
        {
            T instance = (T)Activator.CreateInstance(typeof(T));
            Random random = new Random();
            foreach (PropertyInfo property in instance.GetType().GetProperties())
            {
                Type currentProperty = property.PropertyType;
                if (currentProperty == typeof(bool))
                {
                    TestProperty<T, bool>(property, instance, false);
                }
                else if (currentProperty == typeof(Guid))
                {
                    TestProperty<T, Guid>(property, instance, Guid.NewGuid());
                }
                else if (currentProperty == typeof(byte))
                {
                    byte input = 81;
                    TestProperty<T, byte>(property, instance, input);
                }
                else if (currentProperty == typeof(sbyte))
                {
                    sbyte input = 81;
                    TestProperty<T, sbyte>(property, instance, input);
                }
                else if (currentProperty == typeof(char))
                {
                    char input = (char)random.Next(0, 25);
                    TestProperty<T, char>(property, instance, input);
                }
                else if (currentProperty == typeof(decimal))
                {
                    decimal input = random.Next(int.MaxValue);
                    TestProperty<T, decimal>(property, instance, input);
                }
                else if (currentProperty == typeof(double))
                {
                    double input = random.NextDouble();
                    TestProperty<T, double>(property, instance, input);
                }
                else if (currentProperty == typeof(float))
                {
                    //see http://stackoverflow.com/questions/3365337/best-way-to-generate-a-random-float-in-c-sharp
                    double mantissa = (random.NextDouble() * 2.0) - 1.0;
                    double exponent = Math.Pow(2.0, random.Next(-126, 128));
                    float input = (float)(mantissa * exponent);
                    TestProperty<T, float>(property, instance, input);
                }
                else if (currentProperty == typeof(int))
                {
                    int input = random.Next();
                    TestProperty<T, int>(property, instance, input);
                }
                else if (currentProperty == typeof(uint))
                {
                    uint input = (uint)random.Next();
                    TestProperty<T, uint>(property, instance, input);
                }
                else if (currentProperty == typeof(long))
                {
                    long input = random.Next();
                    TestProperty<T, long>(property, instance, input);
                }
                else if (currentProperty == typeof(ulong))
                {
                    ulong input = (ulong)random.Next();
                    TestProperty<T, ulong>(property, instance, input);
                }
                else if (currentProperty == typeof(short))
                {
                    short input = (short)random.Next(short.MaxValue);
                    TestProperty<T, short>(property, instance, input);
                }
                else if (currentProperty == typeof(ushort))
                {
                    ushort input = (ushort)random.Next(ushort.MaxValue);
                    TestProperty<T, ushort>(property, instance, input);
                }
                else if (currentProperty == typeof(string))
                {
                    string input = "fooobaarfooobar";
                    TestProperty<T>(property, instance, input);
                }
                //nullable type
                else if (Nullable.GetUnderlyingType(currentProperty) != null)
                {
                    Type type = Nullable.GetUnderlyingType(currentProperty);
                    Type typeToTest;
                    if (type.IsValueType)
                    { 
                        typeToTest = typeof(Nullable<>).MakeGenericType(type);
                    }
                    else
                    { 
                        typeToTest = type;
                    }
                    //sets null
                    var newInstance = Activator.CreateInstance(typeToTest);
                    TestProperty(property, instance, newInstance);
                }
                else if (currentProperty.IsPrimitive == false)
                {
                    //if constructor need parameter it will return null
                    if (currentProperty.GetConstructor(Type.EmptyTypes) != null)
                    {
                        var newInstance = Activator.CreateInstance(currentProperty);
                        TestProperty(property, instance, newInstance);
                    }
                }
            }
        }

        public static void TestProperty<TInstance, TPropertyType>(PropertyInfo property, TInstance instance, TPropertyType input)
            where TInstance : class
            //where TPropertyType : struct
        {
            property.SetValue(instance, input);
            TPropertyType output = (TPropertyType)property.GetValue(instance);

            Assert.AreEqual(input, output);
        }

        public static void TestProperty<TInstance>(PropertyInfo property, TInstance instance, string input)
            where TInstance : class
        {
            property.SetValue(instance, input);
            string output = (string)property.GetValue(instance);
            Assert.AreEqual(input, output);
        }
    }
}
