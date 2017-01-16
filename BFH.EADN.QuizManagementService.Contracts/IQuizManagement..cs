using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace BFH.EADN.QuizManagementService.Contracts
{
    [ServiceContract]
    public interface IQuizManagement
    {
        [OperationContract]
        string TestToUpper(string text);
    }
}
