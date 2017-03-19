using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.ServiceModel;
using System.ServiceModel.Channels;
using System.ServiceModel.Dispatcher;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace BFH.EADN.UI.Web.Utils
{
    public class ClientMessageInspector : IClientMessageInspector
    {
        public void AfterReceiveReply(ref Message reply, object correlationState)
        {
            Console.WriteLine("got message");
        }

        public object BeforeSendRequest(ref Message request, IClientChannel channel)
        {
            if (HttpContext.Current != null)
            {
                string sessionId = HttpContext.Current.Session.SessionID;
                string userName = HttpContext.Current.GetOwinContext().Authentication.User.Identity.Name;

                request.Headers.RemoveAll(sessionId, Common.Constants.XMLNamespace);
                request.Headers.RemoveAll(userName, Common.Constants.XMLNamespace);

                MessageHeader userHeader = MessageHeader.CreateHeader(Common.Constants.UserName, Common.Constants.XMLNamespace, userName);
                MessageHeader sessionHeader = MessageHeader.CreateHeader(Common.Constants.UserName, Common.Constants.XMLNamespace, sessionId);

                request.Headers.Add(userHeader);
                request.Headers.Add(sessionHeader);
            }

            return null;
        }
    }
}
