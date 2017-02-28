using BFH.EADN.Common;
using BFH.EADN.Common.Types.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace BFH.EADN.QuizManagementService.Contracts
{
    [ServiceContract(Namespace = Constants.XMLNamespace, Name = "ISession")]
    public interface ISession
    {
        [OperationContract]
        bool Test(bool test);

        //[OperationContract]
        //void LogIn(string name, string password);

        //[OperationContract]
        //void LogOut();

        [OperationContract]
        User GetUserByName(string name);

        [OperationContract]
        User GetUserById(Guid id);
    }
}
