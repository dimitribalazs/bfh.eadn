using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace BFH.EADN.Common.Types.Contracts
{
    [DataContract(Namespace = Constants.XMLNamespace, Name = "QuestionAnswerState", IsReference = true)]
    public class QuestionAnswerState : BaseContract
    {
        [DataMember(Order = 0, Name = "QuizStateId")]
        public Guid QuestionAnswerStateId { get; set; }

        [DataMember(Order = 0, Name = "Question")]
        public Question Question { get; set; }

        [DataMember(Order = 0, Name = "Answers")]
        public List<Answer> Answers { get; set; }
    }
}
