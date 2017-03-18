using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BFH.EADN.UI.Web.Models.Play
{
    /// <summary>
    /// Overview view model
    /// </summary>
    public class Overview
    {
        /// <summary>
        /// Url to continue the quiz
        /// </summary>
        public string ContinueQuizUrl { get; set; }

        /// <summary>
        /// Topic area title
        /// </summary>
        public string TopicName { get; set; }

        /// <summary>
        /// Items per topic area
        /// </summary>
        public List<QuizItem> QuizItems { get; set; } = new List<QuizItem>();
    }

    /// <summary>
    /// Quiz per topic
    /// </summary>
    public class QuizItem
    {
        /// <summary>
        /// Quiz text
        /// </summary>
        public string Text { get; set; }

        /// <summary>
        /// Quiz first question id
        /// </summary>
        public Guid QuestionId { get; set; }

        /// <summary>
        /// Quiz id
        /// </summary>
        public Guid QuizId { get; set; }
    }   
}