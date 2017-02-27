using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BFH.EADN.UI.Web.Models.Play
{
    public class Question
    {
        public Guid QuizId;
        public Guid QuestionId;
        public Guid? NextQuestion { get; set; }
        public Guid? PreviousQuestion { get; set; } 
        public bool IsMultipleChoice { get; set; }

        public string Text { get; set; }
        public string Hint { get; set; }

        public List<Answer> Answers { get; set; }
    }
    public class Answer
    {
        public Guid Id { get; set; }
        public string Text { get; set; }
        public bool IsSolution { get; set; }
    }  
}