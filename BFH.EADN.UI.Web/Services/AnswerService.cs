using System.Collections.Generic;
using System.Linq;
using System;

using BFH.EADN.UI.Web.Models.Management;
using ContractTypes = BFH.EADN.Common.Types.Contracts;
using BFH.EADN.QuizManagementService.Contracts;
using System.Web.Mvc;

namespace BFH.EADN.UI.Web.Services
{
    public sealed class AnswerService : BaseService, IService<Answer, Guid>
    {
        /// <summary>
        /// Get all topics
        /// </summary>
        /// <returns>list of topics</returns>
        public List<Answer> GetList()
        {
            List<ContractTypes.Answer> answers = GetProxy<IAnswerManagement>().GetAnswers();
            return answers.Select(a => new Answer{
                Id = a.Id,
                IsSolution = a.IsSolution,
                Text = a.Text
            }).ToList();
        }

        /// <summary>
        /// Get a concrete topic
        /// </summary>
        /// <param name="id">a topic id</param>
        /// <returns>the concrete topic</returns>
        public Answer Get(Guid id)
        {
            ContractTypes.Answer answer = GetProxy<IAnswerManagement>().GetAnswer(id);
            //get topics for selection
            List<ContractTypes.Topic> topics = GetProxy<ITopicManagement>().GetTopics();

            List<SelectListItem> modelTopics = topics.Select(t => new SelectListItem
            {
                Text = t.Name,
                Value = t.Id.ToString()
            }).ToList();

            return new Answer
            {
                Id = answer.Id,
                IsSolution = answer.IsSolution,
                Text = answer.Text,
                Topics = modelTopics
            };
        }

        /// <summary>
        /// Creates a new topic
        /// </summary>
        /// <param name="newTopic"></param>
        public void Create(Answer newTopic)
        {
            if (newTopic.Id == default(Guid))
            {
                newTopic.Id = Guid.NewGuid();
            }
            ContractTypes.Answer contracItem = new ContractTypes.Answer();
            //Proxy.Create(contracItem);
        }

        /// <summary>
        /// Update an existing topic with new information
        /// </summary>
        /// <param name="id">id of an existing topic</param>
        /// <param name="topic">topic with the new values</param>
        public void Edit(Guid id, Answer answer)
        {
            ContractTypes.Answer contractAnswer = GetProxy<IAnswerManagement>().GetAnswer(id);
            contractAnswer.Id = answer.Id;
            contractAnswer.Topics = GetProxy<ITopicManagement>().GetTopicsByIds(answer.SelectedTopicIds.ToList());
            contractAnswer.IsSolution = answer.IsSolution;
            contractAnswer.Text = answer.Text;
            
            GetProxy<IAnswerManagement>().UpdateAnswer(contractAnswer);
        }

        /// <summary>
        /// Delete a topic
        /// </summary>
        /// <param name="id">id of the topic which should be deleted</param>
        public void Delete(Guid id)
        {
            GetProxy<IAnswerManagement>().DeleteAnswer(id);
        }
    }
}