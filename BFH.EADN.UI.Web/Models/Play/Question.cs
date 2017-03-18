using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BFH.EADN.UI.Web.Models.Play
{
    /// <summary>
    /// Question view model
    /// </summary>
    public class Question
    {
        /// <summary>
        /// Current quiz id
        /// </summary>
        public Guid QuizId { get; set; }

        /// <summary>
        /// Current question id
        /// </summary>
        public Guid QuestionId { get; set; }

        /// <summary>
        /// Next question id for navigation
        /// </summary>
        public Guid? NextQuestion { get; set; }

        /// <summary>
        /// Previous question id for navigation
        /// </summary>
        public Guid? PreviousQuestion { get; set; }

        /// <summary>
        /// Question is multiple choice or not
        /// </summary>
        public bool IsMultipleChoice { get; set; }

        /// <summary>
        /// Text of the question
        /// </summary>
        public string Text { get; set; }

        /// <summary>
        /// Hint for the user
        /// </summary>
        public string Hint { get; set; }

        /// <summary>
        /// Answers to this question
        /// </summary>
        public List<Answer> Answers { get; set; }
    }

    /// <summary>
    /// Answer view model
    /// </summary>
    public class Answer
    {
        /// <summary>
        /// Answer id
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// Answer text
        /// </summary>
        public string Text { get; set; }

        /// <summary>
        /// Answer is solution or not
        /// </summary>
        public bool IsSolution { get; set; }
    }
}