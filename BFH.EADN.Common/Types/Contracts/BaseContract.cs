using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace BFH.EADN.Common.Types.Contracts
{
    [DataContract]
    public abstract class BaseContract
    {
        //todo not sure if id necessary, how to solve this without id
        [DataMember(Order = 0)]
        public Guid Id { get; set; }
    }
}
