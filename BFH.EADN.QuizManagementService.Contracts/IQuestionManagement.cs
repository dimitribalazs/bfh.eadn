using System;
using System.Collections.Generic;
using System.ServiceModel;

using BFH.EADN.Common;
using BFH.EADN.Common.Types.Contracts;

namespace BFH.EADN.QuizManagementService.Contracts
{
    [ServiceContract(Namespace = Constants.XMLNamespace, Name = "QuestionManagement")]
    public interface IQuestionManagement
    {
        /// <summary>
        /// Creates a new topic
        /// </summary>
        /// <param name="question">new question</param>
        [OperationContract(Name = "CreateQuestion")]
        [FaultContract(typeof(ServiceFault))]
        void CreateQuestion(Question question);

        /// <summary>
        /// Updates an existing question
        /// </summary>
        /// <param name="question">existing question with new data</param>
        [OperationContract(Name = "UpdateQuestion")]
        [FaultContract(typeof(ServiceFault))]
        void UpdateQuestion(Question question);

        /// <summary>
        /// Deletes a question
        /// </summary>
        /// <param name="id">id of a question</param>
        [OperationContract(Name = "DeleteQuestion")]
        [FaultContract(typeof(ServiceFault))]
        void DeleteQuestion(Guid id);

        /// <summary>
        /// Gets a question by its id
        /// </summary>
        /// <param name="id">id of a question</param>
        [OperationContract(Name = "GetQuestion")]
        [FaultContract(typeof(ServiceFault))]
        Question GetQuestion(Guid id);

        /// <summary>
        /// Gets all questions
        /// </summary>
        /// <returns></returns>
        [OperationContract(Name = "GetQuestions")]
        [FaultContract(typeof(ServiceFault))]
        List<Question> GetQuestions();

        /// <summary>
        /// Gets all questions which do not have a topic
        /// </summary>
        /// <returns></returns>
        [OperationContract(Name = "GetQuestionsWithoutTopic")]
        [FaultContract(typeof(ServiceFault))]
        List<Question> GetQuestionsWithoutTopic();

        /// <summary>
        /// Gets all questions by ids
        /// </summary>
        /// <returns></returns>
        [OperationContract(Name = "GetQuestionsByIds")]
        [FaultContract(typeof(ServiceFault))]
        List<Question> GetQuestionsByIds(List<Guid> ids);
    }
}
