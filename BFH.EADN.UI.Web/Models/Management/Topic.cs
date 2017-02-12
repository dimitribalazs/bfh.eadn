using System;
using System.ComponentModel.DataAnnotations;

namespace BFH.EADN.UI.Web.Models.Management
{
    public class Topic : BaseModel
    {
        [Required]
        public string Name { get; set; }

        [Required]
        public string Description { get; set; }
    }
}