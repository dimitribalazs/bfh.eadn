using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BFH.EADN.Persistence.EF.Entities
{
    internal class Question : BaseEntity
    {
        public Question()
        {
            Topics = new HashSet<Topic>();
            Quizzes = new HashSet<Quiz>();
            Answers = new HashSet<Answer>();
        }

        public string Text { get; set; }
        public string Hint { get; set; }
        //public DateTime? LastUsed { get; set; }
        /// <summary>
        /// If it is a yes or not question. If false, then its an multiple choice
        /// </summary>
        public bool IsMultipleChoice { get; set; }


        public virtual ICollection<Topic> Topics { get; set; }
        public virtual ICollection<Quiz> Quizzes { get; set; }
        public virtual ICollection<Answer> Answers { get; set; }  
    }
}
