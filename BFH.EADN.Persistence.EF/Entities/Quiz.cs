using BFH.EADN.Common.Types.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BFH.EADN.Persistence.EF.Entities
{
    public class Quiz : BaseEntity
    {
        public Quiz()
        {
            Questions = new HashSet<Question>();
        }

        public string Text { get; set; }
        public QuizType Type { get; set; }
        public DateTime? LastUsed { get; set; }
        public int MinQuestionCount { get; set; }
        public int MaxQuestionCount { get; set; }
        public virtual ICollection<Question> Questions { get; set; }
    }
}
