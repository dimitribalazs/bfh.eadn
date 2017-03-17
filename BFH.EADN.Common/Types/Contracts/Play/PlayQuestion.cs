using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace BFH.EADN.Common.Types.Contracts
{
    /// <summary>
    /// PlayQuestion WCF contract
    /// </summary>
    [DataContract(Namespace = Constants.XMLNamespace, Name = "PlayQuestion", IsReference = true)]
    public class PlayQuestion : Question
    {
        /// <summary>
        /// PreviousQuestion for navigation. If null, there is no previous item
        /// </summary>
        [DataMember(Order = 0, Name = "PreviousQuestion")]
        public Guid? PreviousQuestion { get; set; }

        /// <summary>
        /// Next question for navigation. If null, there is no next question.
        /// End of the quiz follows
        /// </summary>
        [DataMember(Order = 0, Name = "NextQuestion")]
        public Guid? NextQuestion { get; set; }
    }
}
