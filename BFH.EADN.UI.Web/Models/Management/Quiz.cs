using BFH.EADN.Common.Types.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ContractTypes = BFH.EADN.Common.Types.Contracts;

namespace BFH.EADN.UI.Web.Models.Management
{
    public class Quiz : BaseModel
    {
        [Required]
        public string Text { get; set; }
        [Required]
        [Range(0, int.MaxValue)]
        public int MinQuestionCount { get; set; }
        [Required]
        [Range(1, int.MaxValue)]
        public int MaxQuestionCount { get; set; }
        [Required]
        public QuizType Type { get; set; }
        [Required]
        public Guid[] SelectedQuestionIds { get; set; }
        public List<ContractTypes.Question> Questions { get; set; }
        public List<SelectListItem> SelectQuestions
            => Questions.Select(q => new SelectListItem
            {
                Value = q.Id.ToString(),
                Text = q.Text,
                Selected = SelectedQuestionIds != null && SelectedQuestionIds.Contains(q.Id)
            }).ToList();

        public bool QuestionIsSelected(Guid id)
            => SelectedQuestionIds != null && SelectedQuestionIds.Contains(id);

    }
}