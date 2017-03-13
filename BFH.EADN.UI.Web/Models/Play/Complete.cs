using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BFH.EADN.UI.Web.Models.Play
{
    public class Complete
    {
        public Guid QuestionId { get; set; }
        public string QuestionText { get; set; }
        public bool AnsweredCorrectly { get; set; }
    }
}