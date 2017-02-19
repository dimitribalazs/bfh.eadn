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
        public Guid QuestionId { get; set; }
        [Required]
        public string Text { get; set; }
        [Required]
        public bool IsSolution { get; set; }
    }
}