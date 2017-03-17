using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace BFH.EADN.Common.Types.Contracts
{
    /// <summary>
    /// Service Fault which is used by the WCF
    /// </summary>
    [DataContract(Namespace = Constants.XMLNamespace, Name = "ServiceFault")]
    public class ServiceFault
    {
        /// <summary>
        /// Reason is additional information
        /// </summary>
        [DataMember(Order = 0, Name = "Reason")]
        public string Reason { get; set; }

        /// <summary>
        /// Message of the exception
        /// </summary>
        [DataMember(Order = 0, Name = "Message")]
        public string Message { get; set; }

        /// <summary>
        /// Inner exception of the causing exception
        /// </summary>
        [DataMember(Order = 0, Name = "InnerException")]
        public Exception InnerException { get; set; }
    }
}
