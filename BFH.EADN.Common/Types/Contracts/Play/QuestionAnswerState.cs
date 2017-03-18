using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace BFH.EADN.Common.Types.Contracts
{
    /// <summary>
    /// QuestionAnswerState WCF contract
    /// </summary>
    [DataContract(Namespace = Constants.XMLNamespace, Name = "QuestionAnswerState", IsReference = true)]
    public class QuestionAnswerState : BaseContract
    {
        /// <summary>
        /// QuestionAnswerStateId created by the client
        /// </summary>
        [DataMember(Order = 0, Name = "QuestionAnswerStateId")]
        public Guid QuestionAnswerStateId { get; set; }

        /// <summary>
        /// Relation to question
        /// </summary>
        [DataMember(Order = 0, Name = "Question")]
        public Question Question { get; set; }

        /// <summary>
        /// Relations to answers
        /// </summary>
        [DataMember(Order = 0, Name = "Answers")]
        public List<Answer> Answers { get; set; }
    }
}
