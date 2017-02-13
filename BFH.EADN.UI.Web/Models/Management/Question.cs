using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BFH.EADN.UI.Web.Models.Management
{
	public class Question : BaseModel
	{
        [Required]
        public string Text { get; set; }
        public string Hint { get; set; }
        [Required]
        public bool IsMultipleChoise { get; set; }
        [Required]
        public Guid[] AnswerIds { get; set; }
        public List<SelectListItem> Answers { get; set; }
    }
}