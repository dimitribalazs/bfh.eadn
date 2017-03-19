using BFH.EADN.Common;
using BFH.EADN.Common.Types.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace BFH.EADN.QuizManagementService.Contracts
{
    [ServiceContract(Namespace = Constants.XMLNamespace, Name = "Session")]
    public interface ISession
    {
        [OperationContract(Name = "LogIn")]
        [FaultContract(typeof(ServiceFault))]
        Claim LogIn();

        [OperationContract(Name = "LogOut")]
        [FaultContract(typeof(ServiceFault))]
        void LogOut();

        [OperationContract(Name = "GetUserByName")]
        [FaultContract(typeof(ServiceFault))]
        User GetUserByName(string name);
    }
}
