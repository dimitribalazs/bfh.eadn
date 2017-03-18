using BFH.EADN.Common.Types.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BFH.EADN.UI.Web.Models.Management
{
    /// <summary>
    /// Answer view model
    /// </summary>
    public sealed class Answer : BaseModel
    {
        /// <summary>
        /// Id of the question to which the answer belongs
        /// </summary>
        [Required]
        public Guid QuestionId { get; set; }

        /// <summary>
        /// Text of the answer
        /// </summary>
        [Required]
        public string Text { get; set; }

        /// <summary>
        /// Answer is solution
        /// </summary>
        [Required]
        public bool IsSolution { get; set; }
    }
}