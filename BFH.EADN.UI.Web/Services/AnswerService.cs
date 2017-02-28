using System.Collections.Generic;
using System.Linq;
using System;

using BFH.EADN.UI.Web.Models.Management;
using ContractTypes = BFH.EADN.Common.Types.Contracts;
using BFH.EADN.QuizManagementService.Contracts;
using System.Web.Mvc;
using AutoMapper;
using BFH.EADN.UI.Web.Utils;

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
            List<ContractTypes.Answer> answers = ClientProxy.GetProxy<IAnswerManagement>().GetAnswers();
            List<Answer> mappedList = Mapper.Map<List<ContractTypes.Answer>, List<Answer>>(answers);
            return mappedList;
        }

        /// <summary>
        /// Get a concrete topic
        /// </summary>
        /// <param name="id">a topic id</param>
        /// <returns>the concrete topic</returns>
        public Answer Get(Guid id)
        {
            ContractTypes.Answer answer = ClientProxy.GetProxy<IAnswerManagement>().GetAnswer(id);
           
            //get topics for selection
            List<ContractTypes.Topic> topics = ClientProxy.GetProxy<ITopicManagement>().GetTopics();

            //used for select
            //List<SelectListItem> modelTopics = topics.Select(t => new SelectListItem
            //{
            //    Text = t.Name,
            //    Value = t.Id.ToString()
            //}).ToList();

            Answer returnAnswer = Mapper.Map<Answer>(answer);
            //returnAnswer.Topics = modelTopics;

            return returnAnswer; 
        }

        /// <summary>
        /// Creates a new topic
        /// </summary>
        /// <param name="newTopic"></param>
        public void Create(Answer newAnswer)
        {
            if (newAnswer.Id == default(Guid))
            {
                newAnswer.Id = Guid.NewGuid();
            }
            ContractTypes.Answer contractAnswer = Mapper.Map<ContractTypes.Answer>(newAnswer);
            ClientProxy.GetProxy<IAnswerManagement>().CreateAnswer(contractAnswer);
        }

        /// <summary>
        /// Update an existing topic with new information
        /// </summary>
        /// <param name="id">id of an existing topic</param>
        /// <param name="topic">topic with the new values</param>
        public void Edit(Guid id, Answer answer)
        {
            ContractTypes.Answer contractAnswer = ClientProxy.GetProxy<IAnswerManagement>().GetAnswer(id);
            contractAnswer = Mapper.Map<ContractTypes.Answer>(contractAnswer);

            ClientProxy.GetProxy<IAnswerManagement>().UpdateAnswer(contractAnswer);
        }

        /// <summary>
        /// Delete a topic
        /// </summary>
        /// <param name="id">id of the topic which should be deleted</param>
        public void Delete(Guid id)
        {
            ClientProxy.GetProxy<IAnswerManagement>().DeleteAnswer(id);
        }
    }
}