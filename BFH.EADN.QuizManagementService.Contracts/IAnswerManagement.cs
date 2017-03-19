using System;
using System.Collections.Generic;
using System.ServiceModel;

using BFH.EADN.Common;
using BFH.EADN.Common.Types.Contracts;

namespace BFH.EADN.QuizManagementService.Contracts
{
    [ServiceContract(Namespace = Constants.XMLNamespace, Name = "AnswerManagement")]
    public interface IAnswerManagement
    {
        /// <summary>
        /// Creates a new answer
        /// </summary>
        /// <param name="topic">new answer</param>
        [OperationContract(Name = "CreateAnswer")]
        [FaultContract(typeof(ServiceFault))]
        void CreateAnswer(Answer answer);

        /// <summary>
        /// Updates an existing answer
        /// </summary>
        /// <param name="topic">existing answer with new data</param>
        [OperationContract(Name = "UpdateAnswer")]
        [FaultContract(typeof(ServiceFault))]
        void UpdateAnswer(Answer answer);

        /// <summary>
        /// Deletes a answer
        /// </summary>
        /// <param name="id">id of a topic</param>
        [OperationContract(Name = "DeleteAnswer")]
        [FaultContract(typeof(ServiceFault))]
        void DeleteAnswer(Guid id);

        /// <summary>
        /// Gets a answer by its id
        /// </summary>
        /// <param name="id">id of a answer</param>
        [OperationContract(Name = "GetAnswer")]
        [FaultContract(typeof(ServiceFault))]
        Answer GetAnswer(Guid id);

        /// <summary>
        /// Gets all answers
        /// </summary>
        /// <returns></returns>
        [OperationContract(Name = "GetAnswers")]
        [FaultContract(typeof(ServiceFault))]
        List<Answer> GetAnswers();

        /// <summary>
        /// Gets all answers by its ids
        /// </summary>
        /// <returns></returns>
        [OperationContract(Name = "GetAnswersByIds")]
        [FaultContract(typeof(ServiceFault))]
        List<Answer> GetAnswersByIds(List<Guid> ids);
    }
}
