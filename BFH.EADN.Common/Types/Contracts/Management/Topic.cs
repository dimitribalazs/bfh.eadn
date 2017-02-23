using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace BFH.EADN.Common.Types.Contracts
{
    [DataContract(Namespace = Constants.XMLNamespace, Name = "Topic", IsReference = true)]
    public sealed class Topic : BaseContract
    {
        [DataMember(Order = 0)]
        public string Name { get; set; }

        [DataMember(Order = 0)]
        public string Description { get; set; }
    }
}
