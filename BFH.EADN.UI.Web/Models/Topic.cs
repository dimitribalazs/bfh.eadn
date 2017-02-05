using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace BFH.EADN.UI.Web.Models
{
    public class Topic
    {
        public Guid Id { get; set; }
        
        [Required]
        public string Name { get; set; }

        [Required]
        public string Description { get; set; }
    }
}