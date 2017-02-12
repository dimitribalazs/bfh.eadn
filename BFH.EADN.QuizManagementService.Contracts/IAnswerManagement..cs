using BFH.EADN.Common;
using BFH.EADN.Common.Types.Contracts;
using System;
using System.Collections.Generic;
using System.ServiceModel;


namespace BFH.EADN.QuizManagementService.Contracts
{
    [ServiceContract(Namespace = Constants.XMLNamespace, Name = "IQuizManagement")]
    public interface IAnswerManagement
    {
        /// <summary>
        /// Creates a new answer
        /// </summary>
        /// <param name="topic">new answer</param>
        [OperationContract]
        [FaultContract(typeof(ServiceFault))]
        void CreateAnswer(Answer answer);

        /// <summary>
        /// Updates an existing answer
        /// </summary>
        /// <param name="topic">existing answer with new data</param>
        [OperationContract]
        [FaultContract(typeof(ServiceFault))]
        void UpdateAnswer(Answer answer);

        /// <summary>
        /// Deletes a answer
        /// </summary>
        /// <param name="id">id of a topic</param>
        [OperationContract]
        [FaultContract(typeof(ServiceFault))]
        void DeleteAnswer(Guid id);

        /// <summary>
        /// Gets a answer by its id
        /// </summary>
        /// <param name="id">id of a answer</param>
        [OperationContract]
        [FaultContract(typeof(ServiceFault))]
        Answer GetAnswer(Guid id);

        /// <summary>
        /// Gets all topics
        /// </summary>
        /// <returns></returns>
        [OperationContract]
        [FaultContract(typeof(ServiceFault))]
        List<Answer> GetAnswers();
    }
}
