using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace BFH.EADN.Common.Types.Contracts
{
    [DataContract(Namespace = Constants.XMLNamespace, Name = "PlayQuestion", IsReference = true)]
    public class PlayQuestion : Question
    {
        [DataMember(Name = "PreviousQuestion", Order = 0)]
        public Guid? PreviousQuestion { get; set; }

        [DataMember(Name = "NextQuestion", Order = 0)]
        public Guid? NextQuestion { get; set; }
    }
}
