using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ContractTypes = BFH.EADN.Common.Types.Contracts;

namespace BFH.EADN.UI.Web.Models.Management
{
    /// <summary>
    /// Question view model
    /// </summary>
    public sealed class Question : BaseModel
    {
        /// <summary>
        /// Text of question
        /// </summary>
        [Required]
        public string Text { get; set; }

        /// <summary>
        /// Hint of question
        /// </summary>
        public string Hint { get; set; }

        /// <summary>
        /// If question is multiple choice or not
        /// </summary>
        [Required]
        public bool IsMultipleChoice { get; set; }

        /// <summary>
        /// Selected topic ids
        /// </summary>
        [Required]
        public Guid[] SelectedTopicIds { get; set; }

        /// <summary>
        /// Topics to choose from
        /// </summary>
        public List<ContractTypes.Topic> Topics { get; set; }

        /// <summary>
        /// Selected answer ids
        /// </summary>
        public Guid[] SelectedAnswerIds { get; set; }

        /// <summary>
        /// Answer to choose from
        /// </summary>
        public List<ContractTypes.Answer> Answers { get; set; }

        /// <summary>
        /// Checks if topic has already selected
        /// </summary>
        /// <param name="id">topic id</param>
        /// <returns>true if is already selected</returns>
        public bool TopicIsSelected(Guid id)
            => SelectedTopicIds != null && SelectedTopicIds.Contains(id);

        /// <summary>
        /// Checks if answer has already selected
        /// </summary>
        /// <param name="id">answer id</param>
        /// <returns>true if is already selected</returns>
        public bool AnswerIsSelected(Guid id)
            => SelectedAnswerIds != null && SelectedAnswerIds.Contains(id);
    }
}