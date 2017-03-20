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
    /// <summary>
    /// Handles the user session
    /// </summary>
    [ServiceContract(Namespace = Constants.XMLNamespace, Name = "Session")]
    public interface ISession
    {
        /// <summary>
        /// Logs the user in as admin and creates new claim
        /// </summary>
        /// <returns>admin role claim</returns>
        [OperationContract(Name = "LogIn")]
        [FaultContract(typeof(ServiceFault))]
        Claim LogIn();

        /// <summary>
        /// Logs the user out
        /// </summary>
        [OperationContract(Name = "LogOut")]
        [FaultContract(typeof(ServiceFault))]
        void LogOut();

        /// <summary>
        /// Checks if the user exists
        /// </summary>
        /// <param name="name">user name</param>
        /// <returns>found user or null</returns>
        [OperationContract(Name = "GetUserByName")]
        [FaultContract(typeof(ServiceFault))]
        User GetUserByName(string name);
    }
}
