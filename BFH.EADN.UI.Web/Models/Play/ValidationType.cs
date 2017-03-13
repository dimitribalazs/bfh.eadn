using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BFH.EADN.UI.Web.Models.Play
{
    public class ValidationType
    {
        public Guid QuizId { get; set; }
        public Guid QuestionId { get; set; }
        public bool EvaluationAtEnd { get; set; }
    }
}