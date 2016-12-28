using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace BFH.EADN.QuizService.Contracts
{
    [ServiceContract]
    public interface IQuizServiceContract
    {
        [OperationContract]
        string TestToUpper(string text);
    }
}
