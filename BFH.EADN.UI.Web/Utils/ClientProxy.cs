using BFH.EADN.Common.Wcf.Client;
using System.ServiceModel;

namespace BFH.EADN.UI.Web.Utils
{
    /// <summary>
    /// Provides methods for client proxy creation
    /// </summary>
    public static class ClientProxy
    {
        /// <summary>
        /// Establishes a connection to the management service
        /// </summary>
        /// <typeparam name="T">service type T</typeparam>
        /// <returns>a service of type T</returns>
        public static T GetProxy<T>() where T : class
        {
            string nameOfProxy = typeof(T).Name;
            WcfClient<T> service = WcfClientFactory.CreateClient<T>(
                        new EndpointAddress("net.tcp://localhost:5002/" + nameOfProxy.Substring(1)),
                        new NetTcpBinding()
                    );
            return service.GetProxy();
        }

        /// <summary>
        /// Establishes a connection to the play service
        /// </summary>
        /// <typeparam name="T">service type T</typeparam>
        /// <returns>a service of type T</returns>
        public static T GetQuizProxy<T>() where T : class
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