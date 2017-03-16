using System;

namespace BFH.EADN.Persistence.EF.Entities
{
    /// <summary>
    /// QuestionAnswerState entity
    /// </summary>
    public class QuestionAnswerState : BaseEntity
    {
        /// <summary>
        /// Client generated QuestionAnswerStateId
        /// </summary>
        public Guid QuestionAnswerStateId { get; set; }

        /// <summary>
        /// Relation to question
        /// </summary>
        public virtual Question Question { get; set; }

        /// <summary>
        /// Relation to answer
        /// </summary>
        public virtual Answer Answer { get; set; }
    }
}
