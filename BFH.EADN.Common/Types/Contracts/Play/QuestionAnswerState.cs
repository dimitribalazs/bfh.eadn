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
        [DataMember(Name = "QuizStateId", Order = 0)]
        public Guid QuestionAnswerStateId { get; set; }

        [DataMember(Name = "Question", Order = 0)]
        public Question Question { get; set; }

        [DataMember(Name = "Answers", Order = 0)]
        public List<Answer> Answers { get; set; }
    }
}
