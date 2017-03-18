﻿using BFH.EADN.Common.Types.Enums;
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
    /// Quiz view model
    /// </summary>
    public sealed class Quiz : BaseModel
    {
        /// <summary>
        /// Text of the quiz
        /// </summary>
        [Required]
        public string Text { get; set; }

        /// <summary>
        /// Minimium question count
        /// </summary>
        [Required]
        [Range(0, int.MaxValue)]
        public int MinQuestionCount { get; set; }

        /// <summary>
        /// Maximum question count
        /// </summary>
        [Required]
        [Range(1, int.MaxValue)]
        public int MaxQuestionCount { get; set; }

        /// <summary>
        /// Quiz type (variable, fix, dynamic)
        /// </summary>
        [Required]
        public QuizType Type { get; set; }

        /// <summary>
        /// Selected question ids
        /// </summary>
        [Required]
        public Guid[] SelectedQuestionIds { get; set; }

        /// <summary>
        /// Questions to choose from
        /// </summary>
        public List<ContractTypes.Question> Questions { get; set; }

        /// <summary>
        /// Checks if question has already selected
        /// </summary>
        /// <param name="id">question id</param>
        /// <returns>true if is already selected</returns>
        public bool QuestionIsSelected(Guid id)
            => SelectedQuestionIds != null && SelectedQuestionIds.Contains(id);

    }
}