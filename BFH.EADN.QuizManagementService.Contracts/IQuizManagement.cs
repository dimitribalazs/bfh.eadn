﻿using BFH.EADN.Common;
using BFH.EADN.Common.Types.Contracts;
using System;
using System.Collections.Generic;
using System.ServiceModel;


namespace BFH.EADN.QuizManagementService.Contracts
{
    [ServiceContract(Namespace = Constants.XMLNamespace, Name = "IQuizManagement")]
    public interface IQuizManagement
    {
        /// <summary>
        /// Creates a new quiz
        /// </summary>
        /// <param name="quiz">new quiz</param>
        [OperationContract]
        [FaultContract(typeof(ServiceFault))]
        void CreateQuiz(Quiz quiz);

        /// <summary>
        /// Updates an existing quiz
        /// </summary>
        /// <param name="quiz">existing quiz with new data</param>
        [OperationContract]
        [FaultContract(typeof(ServiceFault))]
        void UpdateQuiz(Quiz quiz);

        /// <summary>
        /// Deletes a quiz
        /// </summary>
        /// <param name="id">id of a quiz</param>
        [OperationContract]
        [FaultContract(typeof(ServiceFault))]
        void DeleteQuiz(Guid id);

        /// <summary>
        /// Gets a quiz by its id
        /// </summary>
        /// <param name="id">id of a quiz</param>
        [OperationContract]
        [FaultContract(typeof(ServiceFault))]
        Quiz GetQuiz(Guid id);

        /// <summary>
        /// Gets all quizzes
        /// </summary>
        /// <returns></returns>
        [OperationContract]
        [FaultContract(typeof(ServiceFault))]
        List<Quiz> GetQuizzes();

        /// <summary>
        /// Gets all quizs by ids
        /// </summary>
        /// <returns></returns>
        [OperationContract]
        [FaultContract(typeof(ServiceFault))]
        List<Quiz> GetQuizzesByIds(List<Guid> ids);
    }
}
