using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BFH.EADN.UI.Web.Models.Play
{
    public class Overview
    {
        public string TopicName { get; set; }
        public List<QuizItem> QuizItems { get; set; } = new List<QuizItem>();
    }

    public class QuizItem
    {
        public string Text { get; set; }
        public Guid Id { get; set; }
    }   
}