using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BFH.EADN.UI.Web.Models.Play
{
    /// <summary>
    /// Complete view model, used to show the end evaluation
    /// </summary>
    public class Complete
    {
        /// <summary>
        /// Question id
        /// </summary>
        public Guid QuestionId { get; set; }

        /// <summary>
        /// Question Text
        /// </summary>
        public string QuestionText { get; set; }

        /// <summary>
        /// If I has been answered correctly
        /// </summary>
        public bool AnsweredCorrectly { get; set; }
    }
}