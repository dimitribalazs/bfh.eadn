using System;
using System.ComponentModel.DataAnnotations;

namespace BFH.EADN.UI.Web.Models.Management
{
    /// <summary>
    /// Topic view model
    /// </summary>
    public sealed class Topic : BaseModel
    {
        /// <summary>
        /// Text of the topic
        /// </summary>
        [Required]
        public string Name { get; set; }

        /// <summary>
        /// Description of the topic
        /// </summary>
        [Required]
        public string Description { get; set; }
    }
}