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
    }
            
}
