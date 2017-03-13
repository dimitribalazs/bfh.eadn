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
        List<Quiz> GetQuizzes();
        [OperationContract(Name = "GetQuiz")]
        Quiz GetQuiz(Guid id);
        [OperationContract(Name = "GetFirstQuestion")]
        PlayQuestion GetFirstQuestion(Guid quizId);
        [OperationContract(Name = "GetQuestion")]
        PlayQuestion GetQuestion(Guid quizId, Guid questionId);
        [OperationContract(Name = "CheckAnswers")]
        bool CheckAnswers(Guid questionId, List<Guid> answers);
        [OperationContract(Name = "SaveAnswerState")]
        void SaveAnswerState(Guid quizStateId, Guid questionId, List<Guid> answers);
        void DeleteAnswersState(Guid quizStateId, Guid questionId);
        void GetAllSavedAnswerState();
    }
            
}
