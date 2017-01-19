using BFH.EADN.Common.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace BFH.EADN.Common
{
    /// <summary>
    /// General purpose factory
    /// </summary>
    public static class Factory
    {
        /// <summary>
        /// todo generic
        /// </summary>
        /// <returns></returns>
        public static T CreateInstance<T>() where T : class
        {
            string dllPath = Common.GetConfigValue<string>("DLL");
            string typeName = Common.GetConfigValue<string>("TypeName");
            var assembly = Assembly.LoadFrom(dllPath);
            return (T)assembly.CreateInstance(typeName);
        }
    }
}
