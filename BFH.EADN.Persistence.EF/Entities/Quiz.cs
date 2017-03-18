using System.Collections.Generic;

using BFH.EADN.Common.Types.Enums;
using System;

namespace BFH.EADN.Persistence.EF.Entities
{
    public class Quiz : BaseEntity
    {
        /// <summary>
        /// Initializes the Hashsets (one to many, many to many)
        /// </summary>
        public Quiz()
        {
            Questions = new HashSet<Question>();
        }

        /// <summary>
        /// Text of the quiz
        /// </summary>
        public string Text { get; set; }

        /// <summary>
        /// Type of the quiz (dynamic, fix, variable)
        /// </summary>
        public QuizType Type { get; set; }

        /// <summary>
        /// The minimum question amount per quiz
        /// </summary>
        public int MinQuestionCount { get; set; }

        /// <summary>
        /// The maximu question amount per quiz
        /// </summary>
        public int MaxQuestionCount { get; set; }

        /// <summary>
        /// When the quiz was last used
        /// </summary>
        public DateTime LastUsed { get; set; } = DateTime.Now;

        /// <summary>
        /// Relations to questions
        /// </summary>
        public virtual ICollection<Question> Questions { get; set; }
    }
}
