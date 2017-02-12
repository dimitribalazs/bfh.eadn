using BFH.EADN.Common.Client;
using BFH.EADN.QuizManagementService.Contracts;
using System.ServiceModel;

namespace BFH.EADN.UI.Web.Services
{
    public abstract class BaseService
    {
        /// <summary>
        /// Holds the proxy
        /// </summary>
        private static IQuizManagement _proxy;
        protected IQuizManagement Proxy
        {
            get
            {
                if (_proxy == null)
                {
                    WcfClient<IQuizManagement> service = WcfClientFactory.CreateClient<IQuizManagement>(
                            new EndpointAddress("net.tcp://localhost:5002/QuizManagement"),
                            new NetTcpBinding()
                        );
                    _proxy = service.GetProxy();
                }
                return _proxy;
            }
        }
    }
}