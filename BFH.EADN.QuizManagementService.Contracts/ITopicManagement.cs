using BFH.EADN.Common;
using BFH.EADN.Common.Types.Contracts;
using System;
using System.Collections.Generic;
using System.ServiceModel;


namespace BFH.EADN.QuizManagementService.Contracts
{
    [ServiceContract(Namespace = Constants.XMLNamespace, Name = "ITopicManagement")]
    public interface ITopicManagement
    {

        [OperationContract]
        [FaultContract(typeof(ServiceFault))]
        string Test(string foo);

        /// <summary>
        /// Creates a new topic
        /// </summary>
        /// <param name="topic">new topic</param>
        [OperationContract]
        [FaultContract(typeof(ServiceFault))]
        void CreateTopic(Topic topic);

        /// <summary>
        /// Updates an existing topic
        /// </summary>
        /// <param name="topic">existing topic with new data</param>
        [OperationContract]
        [FaultContract(typeof(ServiceFault))]
        void UpdateTopic(Topic topic);

        /// <summary>
        /// Deletes a topic
        /// </summary>
        /// <param name="id">id of a topic</param>
        [OperationContract]
        [FaultContract(typeof(ServiceFault))]
        void DeleteTopic(Guid id);

        /// <summary>
        /// Gets a topic by its id
        /// </summary>
        /// <param name="id">id of a topic</param>
        [OperationContract]
        [FaultContract(typeof(ServiceFault))]
        Topic GetTopic(Guid id);

        /// <summary>
        /// Gets all topics
        /// </summary>
        /// <returns></returns>
        [OperationContract]
        [FaultContract(typeof(ServiceFault))]
        List<Topic> GetTopics();

        /// <summary>
        /// Gets all topics by ids
        /// </summary>
        /// <returns></returns>
        [OperationContract]
        [FaultContract(typeof(ServiceFault))]
        List<Topic> GetTopicsByIds(List<Guid> ids);
    }
}
