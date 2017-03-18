using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BFH.EADN.UI.Web.Models.Play
{
    /// <summary>
    /// Validation type view model
    /// </summary>
    public class ValidationType
    {
        /// <summary>
        /// Quiz id for which the validation type is set
        /// </summary>
        public Guid QuizId { get; set; }

        /// <summary>
        /// Question to go to after validation is set
        /// </summary>
        public Guid QuestionId { get; set; }

        /// <summary>
        /// Used to render the different evaluations
        /// </summary>
        public bool EvaluationAtEnd { get; set; }
    }
}