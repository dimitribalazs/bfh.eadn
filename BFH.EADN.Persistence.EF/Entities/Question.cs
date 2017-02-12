using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BFH.EADN.Persistence.EF.Entities
{
    public class Question
    {
        public Question()
        {
            Topics = new HashSet<Topic>();
            Anwers = new HashSet<Answer>();
        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }
        public string Text { get; set; }
        public string Hint { get; set; }

        /// <summary>
        /// If it is a yes or not question. If false, then its an multiple choice
        /// </summary>
        public bool IsYesOrNo { get; set; }


        public virtual ICollection<Topic> Topics { get; set; }
        public virtual ICollection<Answer> Anwers { get; set; }

       
    }
}
