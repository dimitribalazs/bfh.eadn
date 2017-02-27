using BFH.EADN.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.ServiceModel;
using System.ServiceModel.Channels;
using System.ServiceModel.Dispatcher;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace BFH.EADN.QuizManagementService.Implementation
{
    public class ServiceMessageInspector : IDispatchMessageInspector
    {
        private string _sessionId;
        public object AfterReceiveRequest(ref Message request, IClientChannel channel, InstanceContext instanceContext)
        {
            //check if already signed in
            //request.
            //OperationContext.Current.SessionId


            //bool isLoggedIn = QuizManagement.IsLoggedIn("");
            //if(isLoggedIn == false)
            //{
            // request.Close();
            //}

            //Console.WriteLine("Hello Request");
            //return null;

            GenericIdentity identity = new GenericIdentity("foo", "bar");
            GenericPrincipal gprincipal = new GenericPrincipal(identity, new[] { "QuizAdmin" });
            Thread.CurrentPrincipal = gprincipal;

            return null;

        }

        public void BeforeSendReply(ref Message reply, object correlationState)
        {
            MessageHeader header = MessageHeader.CreateHeader("WcfSessionId", Constants.XMLNamespace, OperationContext.Current.SessionId);
            reply.Headers.Add(header);
            Console.WriteLine("Bye reply");
        }
    }
}
