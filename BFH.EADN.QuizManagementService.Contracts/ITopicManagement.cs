using System;
using System.Collections.Generic;
using System.ServiceModel;

using BFH.EADN.Common;
using BFH.EADN.Common.Types.Contracts;


namespace BFH.EADN.QuizManagementService.Contracts
{
    [ServiceContract(Namespace = Constants.XMLNamespace, Name = "ITopicManagement")]
    public interface ITopicManagement
    {
        /// <summary>
        /// Creates a new topic
        /// </summary>
        /// <param name="topic">new topic</param>
        [OperationContract(Name = "CreateTopic")]
        [FaultContract(typeof(ServiceFault))]
        void CreateTopic(Topic topic);

        /// <summary>
        /// Updates an existing topic
        /// </summary>
        /// <param name="topic">existing topic with new data</param>
        [OperationContract(Name = "UpdateTopic")]
        [FaultContract(typeof(ServiceFault))]
        void UpdateTopic(Topic topic);

        /// <summary>
        /// Deletes a topic
        /// </summary>
        /// <param name="id">id of a topic</param>
        [OperationContract(Name = "DeleteTopic")]
        [FaultContract(typeof(ServiceFault))]
        void DeleteTopic(Guid id);

        /// <summary>
        /// Gets a topic by its id
        /// </summary>
        /// <param name="id">id of a topic</param>
        [OperationContract(Name = "GetTopic")]
        [FaultContract(typeof(ServiceFault))]
        Topic GetTopic(Guid id);

        /// <summary>
        /// Gets all topics
        /// </summary>
        /// <returns></returns>
        [OperationContract(Name = "GetTopics")]
        [FaultContract(typeof(ServiceFault))]
        List<Topic> GetTopics();

        /// <summary>
        /// Gets all topics by ids
        /// </summary>
        /// <returns></returns>
        [OperationContract(Name = "GetTopicsByIds")]
        [FaultContract(typeof(ServiceFault))]
        List<Topic> GetTopicsByIds(List<Guid> ids);
    }
}
