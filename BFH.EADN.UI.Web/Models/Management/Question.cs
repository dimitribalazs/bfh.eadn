using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace BFH.EADN.UI.Web.Models.Management
{
    public class Question : BaseModel
    {   
        [Required]
        public string Text { get; set; }
        public string Hint { get; set; }
        [Required]
        public bool IsYesOrNo { get; set; }
        public List<Answer> Answers { get; set; }
    }
}