using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace BFH.EADN.Common.Types.Contracts
{
    [DataContract(Namespace = Constants.XMLNamespace, Name = "BaseContract", IsReference = true)]
    public abstract class BaseContract
    {
        [DataMember(Order = 0, Name = "Id")]
        public Guid Id { get; set; }
    }
}
