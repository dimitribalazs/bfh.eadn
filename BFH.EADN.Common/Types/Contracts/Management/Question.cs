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
    /// Question WCF contract
    /// </summary>
    [DataContract(Namespace = Constants.XMLNamespace, Name = "Question", IsReference = true)]
    public class Question : BaseContract
    {
        /// <summary>
        /// Text of the question
        /// </summary>
        [DataMember(Order = 0, Name = "Text")]
        public string Text { get; set; }

        /// <summary>
        /// Hint for the question
        /// </summary>
        [DataMember(Order = 0, Name = "Hint")]
        public string Hint { get; set; }

        /// <summary>
        /// Defines whether the quiz is multiple choice or not
        /// </summary>
        [DataMember(Order = 0, Name = "IsMultipleChoice")]
        public bool IsMultipleChoice { get; set; }

        /// <summary>
        /// Relations to topics
        /// </summary>
        [DataMember(Order = 0, Name = "Topics")]
        public List<Topic> Topics { get; set; }

        /// <summary>
        /// Relations to answers
        /// </summary>
        [DataMember(Order = 0, Name = "Answers")]
        public List<Answer> Answers { get; set; }
    }
}
