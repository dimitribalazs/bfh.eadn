using BFH.EADN.Common.Types;
using System;
using System.Collections.Generic;
using System.Linq;
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
        public static T CreateInstance<T>() where T : IFactoryPersistence
        {
            return (T)Activator.CreateInstance("", "").Unwrap();
        }
    }
}
