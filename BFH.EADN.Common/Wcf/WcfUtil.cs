using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel.Channels;
using System.Text;
using System.Threading.Tasks;

namespace BFH.EADN.Common.Wcf
{
    /// <summary>
    /// Provides util methods for WCF
    /// </summary>
    public static class WcfUtil
    {
        /// <summary>
        /// Try get from MessageHeaders
        /// </summary>
        /// <typeparam name="T">type of header entry</typeparam>
        /// <param name="headers">MessageHeaders</param>
        /// <param name="name">name of entry</param>
        /// <param name="ns">namespace</param>
        /// <param name="result">output result</param>
        /// <returns></returns>
        public static bool TryGetHeader<T>(this MessageHeaders headers, string name, string ns, out T result)
        {
            //header found
            result = default(T);
            if (headers.FindHeader(name, ns) != -1)
            {
                result = headers.GetHeader<T>(name, ns);
                return true;
            }
            return false;
        }
    }
}
