using System.Collections.Generic;
using System.Linq;
using System;

using BFH.EADN.UI.Web.Models.Management;
using ContractTypes = BFH.EADN.Common.Types.Contracts;
using BFH.EADN.QuizManagementService.Contracts;

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
            List<ContractTypes.Topic> topics = GetProxy<ITopicManagement>().GetTopics();

            return topics.Select(t => new Topic
            {
                Id = t.Id,
                Description = t.Description,
                Name = t.Name
            }).ToList();
        }

        /// <summary>
        /// Get a concrete topic
        /// </summary>
        /// <param name="id">a topic id</param>
        /// <returns>the concrete topic</returns>
        public Topic Get(Guid id)
        {
            ContractTypes.Topic topic = GetProxy<ITopicManagement>().GetTopic(id);
            return new Topic
            {
                Id = topic.Id,
                Description = topic.Description,
                Name = topic.Name
            };
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
            ContractTypes.Topic contractTopic = new ContractTypes.Topic
            {
                Id = newTopic.Id,
                Description = newTopic.Description,
                Name = newTopic.Name
            };
            GetProxy<ITopicManagement>().CreateTopic(contractTopic);
        }

        /// <summary>
        /// Update an existing topic with new information
        /// </summary>
        /// <param name="id">id of an existing topic</param>
        /// <param name="topic">topic with the new values</param>
        public void Edit(Guid id, Topic topic)
        {
            ContractTypes.Topic contractTopic = GetProxy<ITopicManagement>().GetTopic(id);
            contractTopic.Id = topic.Id;
            contractTopic.Name = topic.Name;
            contractTopic.Description = topic.Description;

            GetProxy<ITopicManagement>().UpdateTopic(contractTopic);
        }

        /// <summary>
        /// Delete a topic
        /// </summary>
        /// <param name="id">id of the topic which should be deleted</param>
        public void Delete(Guid id)
        {
            GetProxy<ITopicManagement>().DeleteTopic(id);
        }
    }
}