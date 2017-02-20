using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace BFH.EADN.Common.Types.Contracts
{
    [DataContract]
    public class ServiceFault
    {
        [DataMember]
        public string Reason { get; set; }

        [DataMember]
        public string Message { get; set; }

        [DataMember]
        public Exception InnerException { get; set; }

    }
}
