using BFH.EADN.Common;
using BFH.EADN.Common.Wcf;
using System.Security.Claims;
using System.Security.Principal;
using System.ServiceModel;
using System.ServiceModel.Channels;
using System.ServiceModel.Dispatcher;
using System.Threading;

namespace BFH.EADN.QuizManagementService.Implementation
{
    public class ServiceMessageInspector : IDispatchMessageInspector
    {
        public object AfterReceiveRequest(ref Message request, IClientChannel channel, InstanceContext instanceContext)
        {
            //todo not working yet
            return null;
            
            string sessionId;
            string userName; 
            request.Headers.TryGetHeader(Constants.SessionId, Constants.XMLNamespace, out sessionId);

            if (request.Headers.TryGetHeader(Constants.UserName, Constants.XMLNamespace, out userName))
            {
                Claim claim = new Claim(ClaimTypes.Role, Constants.AdminRoleName);
                GenericIdentity genericIdentity = new GenericIdentity("genericQuizIdentity", Constants.AdminRoleName);

                //Generischer Prinzipal herstellen
                string[] roles = { Constants.AdminRoleName };
                GenericPrincipal objGenericPrincipal = new GenericPrincipal(genericIdentity, roles);

                //Generischer Prinzipal mit dem Thread verbinden
                Thread.CurrentPrincipal = objGenericPrincipal;
            }
            return null;

        }

        public void BeforeSendReply(ref Message reply, object correlationState)
        {
            //    MessageHeader header = MessageHeader.CreateHeader("token", Constants.XMLNamespace, OperationContext.Current.SessionId);
            //    reply.Headers.Add(header);
            //    Console.WriteLine("Bye reply");
        }
    }


}

