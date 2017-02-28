using System.Collections.Generic;
using System.Linq;
using System;
using BFH.EADN.UI.Web.Models.Management;
using ContractTypes = BFH.EADN.Common.Types.Contracts;
using BFH.EADN.QuizManagementService.Contracts;
using AutoMapper;
using System.Security.Principal;
using System.Threading;
using BFH.EADN.UI.Web.Utils;

namespace BFH.EADN.UI.Web.Services
{
    public sealed class TopicService : BaseService, IService<Topic, Guid>
    {
        /// <summary>
        /// Get all topics
        /// </summary>
        /// <returns>list of topics</returns>
        public List<Topic> GetList()
        {
            //var test = GetProxy<ISession>().Test(true);
            List<ContractTypes.Topic> topics = ClientProxy.GetProxy<ITopicManagement>().GetTopics();
            List<Topic> mappedList = Mapper.Map<List<ContractTypes.Topic>, List<Topic>>(topics);
            return mappedList;
        }

        /// <summary>
        /// Get a concrete topic
        /// </summary>
        /// <param name="id">a topic id</param>
        /// <returns>the concrete topic</returns>
        public Topic Get(Guid id)
        {
            ContractTypes.Topic topic = ClientProxy.GetProxy<ITopicManagement>().GetTopic(id);
            return Mapper.Map<Topic>(topic);
        }

        /// <summary>
        /// Creates a new topic
        /// </summary>
        /// <param name="newTopic"></param>
        public void Create(Topic newTopic)
        {
            if (newTopic.Id == default(Guid))
            {
                newTopic.Id = Guid.NewGuid();
            }
            ContractTypes.Topic contractTopic = Mapper.Map<ContractTypes.Topic>(newTopic);
            ClientProxy.GetProxy<ITopicManagement>().CreateTopic(contractTopic);
        }

        /// <summary>
        /// Update an existing topic with new information
        /// </summary>
        /// <param name="id">id of an existing topic</param>
        /// <param name="topic">topic with the new values</param>
        public void Edit(Guid id, Topic topic)
        { 
            ContractTypes.Topic contractTopic = ClientProxy.GetProxy<ITopicManagement>().GetTopic(id);
            contractTopic = Mapper.Map<ContractTypes.Topic>(topic);
            ClientProxy.GetProxy<ITopicManagement>().UpdateTopic(contractTopic);
        }

        /// <summary>
        /// Delete a topic
        /// </summary>
        /// <param name="id">id of the topic which should be deleted</param>
        public void Delete(Guid id)
        {
            ClientProxy.GetProxy<ITopicManagement>().DeleteTopic(id);
        }
    }
}