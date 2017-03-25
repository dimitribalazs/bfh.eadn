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
    /// Quiz WCF contract
    /// </summary>
    [DataContract(Namespace = Constants.XMLNamespace, Name = "Quiz", IsReference = true)]
    public sealed class Quiz : BaseContract
    {
        /// <summary>
        /// Text of the quiz
        /// </summary>
        [DataMember(Order = 0, Name = "Text")]
        public string Text { get; set; }

        /// <summary>
        /// Minimum question count of the quiz
        /// </summary>
        [DataMember(Order = 0, Name = "MinQuestionCount")]
        public int MinQuestionCount { get; set; }

        /// <summary>
        /// Maximum question count of the quiz
        /// </summary>
        [DataMember(Order = 0, Name = "MaxQuestionCount")]
        public int MaxQuestionCount { get; set; }

        /// <summary>
        /// Type of the quiz (dynamic, fix, variable)
        /// </summary>
        [DataMember(Order = 0, Name = "Type")]
        public QuizType Type { get; set; }

        /// <summary>
        /// When the quiz was last used
        /// </summary>
        [DataMember(Order = 0, Name = "LastUsed")]
        public DateTime? LastUsed { get; set; }

        /// <summary>
        /// Can be deleted
        /// </summary>
        [DataMember(Order = 0, Name = "CanBeDeleted")]
        public bool CanBeDeleted { get; set; }

        /// <summary>
        /// Relations to questions
        /// </summary>
        [DataMember(Order = 0, Name = "Questions")]
        public List<Question> Questions { get; set; }   
    }
}
