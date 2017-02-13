using BFH.EADN.Common;
using BFH.EADN.Common.Types.Contracts;
using System;
using System.Collections.Generic;
using System.ServiceModel;


namespace BFH.EADN.QuizManagementService.Contracts
{
    [ServiceContract(Namespace = Constants.XMLNamespace, Name = "IQuestionManagement")]
    public interface IQuestionManagement
    {
        /// <summary>
        /// Creates a new topic
        /// </summary>
        /// <param name="question">new question</param>
        [OperationContract]
        [FaultContract(typeof(ServiceFault))]
        void CreateQuestion(Question question);

        /// <summary>
        /// Updates an existing question
        /// </summary>
        /// <param name="question">existing question with new data</param>
        [OperationContract]
        [FaultContract(typeof(ServiceFault))]
        void UpdateQuestion(Question question);

        /// <summary>
        /// Deletes a question
        /// </summary>
        /// <param name="id">id of a question</param>
        [OperationContract]
        [FaultContract(typeof(ServiceFault))]
        void DeleteQuestion(Guid id);

        /// <summary>
        /// Gets a question by its id
        /// </summary>
        /// <param name="id">id of a question</param>
        [OperationContract]
        [FaultContract(typeof(ServiceFault))]
        Question GetQuestion(Guid id);

        /// <summary>
        /// Gets all questions
        /// </summary>
        /// <returns></returns>
        [OperationContract]
        [FaultContract(typeof(ServiceFault))]
        List<Question> GetQuestions();

        /// <summary>
        /// Gets all questions by ids
        /// </summary>
        /// <returns></returns>
        [OperationContract]
        [FaultContract(typeof(ServiceFault))]
        List<Question> GetQuestionsByIds(List<Guid> ids);
    }
}
