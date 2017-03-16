using System.Collections.Generic;

namespace BFH.EADN.Persistence.EF.Entities
{
    public class Topic : BaseEntity
    {
        /// <summary>
        /// Initializes the Hashsets (one to many, many to many)
        /// </summary>
        public Topic()
        {
            Questions = new HashSet<Question>();
        }
        
        /// <summary>
        /// Name of the topic
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Description of the topic
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// Relations to questions
        /// </summary>
        public virtual ICollection<Question> Questions { get; set; }
    }
}
