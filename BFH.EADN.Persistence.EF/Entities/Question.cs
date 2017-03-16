using System.Collections.Generic;

namespace BFH.EADN.Persistence.EF.Entities
{
    /// <summary>
    /// Question entity
    /// </summary>
    public class Question : BaseEntity
    {
        /// <summary>
        /// Initializes the Hashsets (one to many, many to many)
        /// </summary>
        public Question()
        {
            Topics = new HashSet<Topic>();
            Quizzes = new HashSet<Quiz>();
            Answers = new HashSet<Answer>();
        }

        /// <summary>
        /// Text of the question
        /// </summary>
        public string Text { get; set; }

        /// <summary>
        /// Hint for the question
        /// </summary>
        public string Hint { get; set; }
        
        /// <summary>
        /// If it is a yes or not question. If false, then its an multiple choice
        /// </summary>
        public bool IsMultipleChoice { get; set; }

        /// <summary>
        /// Relations to topic
        /// </summary>
        public virtual ICollection<Topic> Topics { get; set; }

        /// <summary>
        /// Relations to quizzes
        /// </summary>
        public virtual ICollection<Quiz> Quizzes { get; set; }

        /// <summary>
        /// Relations to answers
        /// </summary>
        public virtual ICollection<Answer> Answers { get; set; }  
    }
}
