using BFH.EADN.Common.Wcf.Client;
using System.ServiceModel;
using ContractTypes = BFH.EADN.Common.Types.Contracts;

namespace BFH.EADN.UI.Web.Services
{
    public abstract class BaseService
    {
        /// <summary>
        /// Establishes a connection to the server
        /// </summary>
        /// <typeparam name="T">service type T</typeparam>
        /// <returns>a service of type T</returns>
        protected T GetProxy<T>() where T : class
        {
            string nameOfProxy = typeof(T).Name;
            WcfClient<T> service = WcfClientFactory.CreateClient<T>(
                        new EndpointAddress("net.tcp://localhost:5002/" + nameOfProxy.Substring(1)),
                        new NetTcpBinding()
                    );
            return service.GetProxy();
        }

        protected T GetQuizProxy<T>() where T : class
        {
            string nameOfProxy = typeof(T).Name;
            WcfClient<T> service = WcfClientFactory.CreateClient<T>(
                        new EndpointAddress("net.tcp://localhost:5011/" + nameOfProxy.Substring(1)),
                        new NetTcpBinding()
                    );
            return service.GetProxy();
        }
    }
}