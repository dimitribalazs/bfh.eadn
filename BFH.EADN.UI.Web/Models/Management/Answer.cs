using BFH.EADN.Common.Types.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BFH.EADN.UI.Web.Models.Management
{
    public class Answer : BaseModel
    {
        [Required]
        public string Text { get; set; }
        [Required]
        public bool IsSolution { get; set; }
        
        public Guid[] SelectedTopicIds { get; set; }
        public List<SelectListItem> Topics { get; set; }
    }
}