using System;
using System.Collections.Generic;
using System.ServiceModel;

using BFH.EADN.Common;
using BFH.EADN.Common.Types.Contracts;

namespace BFH.EADN.QuizManagementService.Contracts
{
    [ServiceContract(Namespace = Constants.XMLNamespace, Name = "QuizManagement")]
    public interface IQuizManagement
    {
        /// <summary>
        /// Creates a new quiz
        /// </summary>
        /// <param name="quiz">new quiz</param>
        [OperationContract(Name = "CreateQuiz")]
        [FaultContract(typeof(ServiceFault))]
        void CreateQuiz(Quiz quiz);

        /// <summary>
        /// Updates an existing quiz
        /// </summary>
        /// <param name="quiz">existing quiz with new data</param>
        [OperationContract(Name = "UpdateQuiz")]
        [FaultContract(typeof(ServiceFault))]
        void UpdateQuiz(Quiz quiz);

        /// <summary>
        /// Deletes a quiz
        /// </summary>
        /// <param name="id">id of a quiz</param>
        [OperationContract(Name = "DeleteQuiz")]
        [FaultContract(typeof(ServiceFault))]
        void DeleteQuiz(Guid id);

        /// <summary>
        /// Gets a quiz by its id
        /// </summary>
        /// <param name="id">id of a quiz</param>
        [OperationContract(Name = "GetQuiz")]
        [FaultContract(typeof(ServiceFault))]
        Quiz GetQuiz(Guid id);

        /// <summary>
        /// Gets all quizzes
        /// </summary>
        /// <returns></returns>
        [OperationContract(Name = "GetQuizzes")]
        [FaultContract(typeof(ServiceFault))]
        List<Quiz> GetQuizzes();

        /// <summary>
        /// Gets all quizs by ids
        /// </summary>
        /// <returns></returns>
        [OperationContract(Name = "GetQuizzesByIds")]
        [FaultContract(typeof(ServiceFault))]
        List<Quiz> GetQuizzesByIds(List<Guid> ids);
    }
}
