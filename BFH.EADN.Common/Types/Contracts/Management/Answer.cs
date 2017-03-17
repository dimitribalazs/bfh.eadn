using BFH.EADN.Common.Types.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace BFH.EADN.Common.Types.Contracts
{
    /// <summary>
    /// Answer WCF contract
    /// </summary>
    [DataContract(Namespace = Constants.XMLNamespace, Name = "Answer", IsReference = true)]
    public sealed class Answer : BaseContract
    {
        /// <summary>
        /// Question id to which the answer belongs
        /// </summary>
        [DataMember(Order = 0, Name = "QuestionId")]
        public Guid QuestionId { get; set; }

        /// <summary>
        /// Text of the answer
        /// </summary>
        [DataMember(Order = 0, Name = "Text")]
        public string Text { get; set; }

        /// <summary>
        /// Marks whetere the anwser is solution or not
        /// </summary>
        [DataMember(Order = 0, Name = "IsSolution")]
        public bool IsSolution { get; set; }
    }
}
