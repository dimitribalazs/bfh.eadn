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
        /// Get a list of quizze
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
        /// Get a playquestion by a quiz id and a question id. 
        /// It has navigations properties to navigate to the next and previous question
        /// </summary>
        /// <param name="quizId">current quiz id</param>
        /// <param name="questionId">current question id</param>
        /// <returns>a playquestion</returns>
        [OperationContract(Name = "GetQuestion")]
        [FaultContract(typeof(ServiceFault))]
        PlayQuestion GetQuestion(Guid quizId, Guid questionId);

        /// <summary>
        /// Checks the answers
        /// </summary>
        /// <param name="questionId">current question id</param>
        /// <param name="answers">list of selected answers</param>
        /// <returns>true if everything is correct, false if not</returns>
        [OperationContract(Name = "CheckAnswers")]
        [FaultContract(typeof(ServiceFault))]
        bool CheckAnswers(Guid questionId, List<Guid> answers);
        
        /// <summary>
        /// Updates/Creates a QuestiosAnswersState. Which is used for saving the play state
        /// </summary>
        /// <param name="quizStateId">quiz state id. Created within the client</param>
        /// <param name="questionId">current question id</param>
        /// <param name="answers">list of selected answers</param>
        [OperationContract(Name = "UpdateQuestionAnswerState")]
        [FaultContract(typeof(ServiceFault))]
        void UpdateQuestionAnswerState(Guid quizStateId, Guid questionId, List<Guid> answers);

        /// <summary>
        /// Deletes all quiz state entries by its ids
        /// </summary>
        /// <param name="questionStateId">question state id</param>
        [OperationContract(Name = "DeleteQuestionAnswerState")]
        [FaultContract(typeof(ServiceFault))]
        void DeleteQuestionAnswerState(Guid questionStateId);

        /// <summary>
        /// Get all question answer state entries
        /// </summary>
        /// <param name="questionStateId">question state id</param>
        /// <returns>all entries of this question state id</returns>
        [OperationContract(Name = "GetAllSavedQuestionAnswerStates")]
        [FaultContract(typeof(ServiceFault))]
        List<QuestionAnswerState> GetAllSavedQuestionAnswerStates(Guid questionStateId);
    }       
}
