using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BFH.EADN.Persistence.EF.Entities
{
    public class Topic : BaseEntity
    {
        public Topic()
        {
            Questions = new HashSet<Question>();
        }

        public string Name { get; set; }

        public string Description { get; set; }

        public ICollection<Question> Questions { get; set; }
    }
}
