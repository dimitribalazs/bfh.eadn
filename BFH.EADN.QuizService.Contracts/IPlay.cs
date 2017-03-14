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
    [ServiceContract(Namespace = Constants.XMLNamespace, Name = "IPlay")]
    public interface IPlay
    {
        [OperationContract(Name = "GetQuizzes")]
        [FaultContract(typeof(ServiceFault))]
        List<Quiz> GetQuizzes();

        [OperationContract(Name = "GetQuiz")]
        [FaultContract(typeof(ServiceFault))]
        Quiz GetQuiz(Guid id);

        [OperationContract(Name = "GetFirstQuestion")]
        [FaultContract(typeof(ServiceFault))]
        PlayQuestion GetFirstQuestion(Guid quizId);

        [OperationContract(Name = "GetQuestion")]
        [FaultContract(typeof(ServiceFault))]
        PlayQuestion GetQuestion(Guid quizId, Guid questionId);

        [OperationContract(Name = "CheckAnswers")]
        [FaultContract(typeof(ServiceFault))]
        bool CheckAnswers(Guid questionId, List<Guid> answers);

        [OperationContract(Name = "CreateQuestionAnswerState")]
        [FaultContract(typeof(ServiceFault))]
        void CreateQuestionAnswerState(Guid quizStateId, Guid questionId, List<Guid> answers);

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
