﻿using BFH.EADN.Common.Types;
using System;
using System.Collections.Generic;
using System.IO;
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
        /// Creates an instance of class of type T.
        /// DLL and TypeName must be set in App.config
        /// </summary>
        /// <returns>class of type T</returns>
        public static T CreateInstance<T>() where T : class
        {
            string dllPath = Common.GetConfigValue<string>("DLL");
            string typeName = Common.GetConfigValue<string>("TypeName");
            string fullPath = Path.GetFullPath(dllPath);
            Assembly assembly = Assembly.LoadFrom(fullPath);
            return (T)assembly.CreateInstance(typeName);
        }
    }
}
