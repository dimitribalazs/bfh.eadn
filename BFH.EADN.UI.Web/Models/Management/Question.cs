using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ContractTypes = BFH.EADN.Common.Types.Contracts;

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
        public Guid[] SelectedTopicIds { get; set; }
        public List<ContractTypes.Topic> Topics { get; set; }
        public List<SelectListItem> SelectTopics
            => Topics.Select(t => new SelectListItem
            {
                Value = t.Id.ToString(),
                Text = t.Name,
                Selected = SelectedTopicIds != null && SelectedTopicIds.Contains(t.Id)
            }).ToList();

        [Required]
        public Guid[] SelectedAnswerIds { get; set; }

        public List<ContractTypes.Answer> Answers { get; set; }
        public List<SelectListItem> SelectAnswers
            => Answers.Select(a => new SelectListItem
            {
                Value = a.Id.ToString(),
                Text = a.Text,
                Selected = SelectedAnswerIds != null && SelectedAnswerIds.Contains(a.Id)
            }).ToList();

        public bool TopicIsSelected(Guid id)
            => SelectedTopicIds != null && SelectedTopicIds.Contains(id);

        public bool AnswerIsSelected(Guid id)
            => SelectedAnswerIds != null && SelectedAnswerIds.Contains(id);
    }
}