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
        /// Establishes a connection to a service
        /// </summary>
        /// <typeparam name="T">service contract type T</typeparam>
        /// <returns>a service of type T</returns>
        public static T GetProxy<T>() where T : class
        {
            string nameOfConfiguration = typeof(T).Name.Substring(1);
            WcfClient<T> service = WcfClientFactory.CreateClient<T>(nameOfConfiguration);
            return service.GetProxy();
        }
    }
}