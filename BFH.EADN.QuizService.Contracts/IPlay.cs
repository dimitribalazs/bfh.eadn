using BFH.EADN.Common;
using BFH.EADN.Common.Types.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace BFH.EADN.QuizService.Contracts
{
    /// <summary>
    /// Provides an interface for methods which are used for playing the game
    /// </summary>
    [ServiceContract(Namespace = Constants.XMLNamespace, Name = "IPlay")]
    public interface IPlay
    {

        /// <summary>
        /// Get a list of quizzes 
        /// </summary>
        /// <returns>list of all quizzes</returns>
        [OperationContract(Name = "GetQuizzes")]
        [FaultContract(typeof(ServiceFault))]
        List<Quiz> GetQuizzes();


        /// <summary>
        /// Get a quiz by its id. The list of questions is build based on the quiz type (Fix, Dynamic, Variable) 
        /// </summary>
        /// <param name="id">id of </param>
        /// <returns>the quiz by its id</returns>
        [OperationContract(Name = "GetQuiz")]
        [FaultContract(typeof(ServiceFault))]
        Quiz GetQuiz(Guid id);

        /// <summary>
        /// Get the first question of the quiz
        /// </summary>
        /// <param name="quizId"></param>
        /// <returns></returns>
        [OperationContract(Name = "GetFirstQuestion")]
        [FaultContract(typeof(ServiceFault))]
        PlayQuestion GetFirstQuestion(Guid quizId);

        [OperationContract(Name = "GetQuestion")]
        [FaultContract(typeof(ServiceFault))]
        PlayQuestion GetQuestion(Guid quizId, Guid questionId);

        [OperationContract(Name = "CheckAnswers")]
        [FaultContract(typeof(ServiceFault))]
        bool CheckAnswers(Guid questionId, List<Guid> answers);
        
        [OperationContract(Name = "UpdateQuestionAnswerState")]
        [FaultContract(typeof(ServiceFault))]
        void UpdateQuestionAnswerState(Guid quizStateId, Guid questionId, List<Guid> answers);

        [OperationContract(Name = "DeleteQuestionAnswerState")]
        [FaultContract(typeof(ServiceFault))]
        void DeleteQuestionAnswerState(Guid quizStateId);

        [OperationContract(Name = "GetAllSavedQuestionAnswerStates")]
        [FaultContract(typeof(ServiceFault))]
        List<QuestionAnswerState> GetAllSavedQuestionAnswerStates(Guid quizStateId);
    }       
}
