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
        [DataMember(Order = 0, Name = "PreviousQuestion")]
        public Guid? PreviousQuestion { get; set; }

        [DataMember(Order = 0, Name = "NextQuestion")]
        public Guid? NextQuestion { get; set; }
    }
}
