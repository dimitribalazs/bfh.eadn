using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BFH.EADN.Persistence.EF.Entities
{
    public class Answer
    {
        public Answer()
        {
            Topics = new HashSet<Topic>();
        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }

        public string Text { get; set; }

        public bool IsSolution { get; set; }

        public ICollection<Topic> Topics { get; set; }

        public int Quark { get; set; }
    }


}
