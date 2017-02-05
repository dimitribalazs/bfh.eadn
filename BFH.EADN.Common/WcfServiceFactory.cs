using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.ServiceModel.Channels;
using System.Text;
using System.Threading.Tasks;

namespace BFH.EADN.Common
{
    public sealed class WcfServiceFactory : IDisposable
    {
        private static Dictionary<string, IClientChannel> _proxies = new Dictionary<string, IClientChannel>();

        string proxyName = "";

        public void Dispose()
        {
            //todo close connection if dipose
        }

        private T GetProxy<T>(T proxy, Binding binding, string address)
        {
            IClientChannel proxyImpl = proxy as IClientChannel;

            if (proxy != null && proxyImpl.State == CommunicationState.Opened)
            {
                return (T)proxy;
            }
            else if (proxy != null && proxyImpl.State == CommunicationState.Faulted)
            {
                proxyImpl.Abort();
            }
            proxy = ChannelFactory<T>.CreateChannel(
                binding,
                new EndpointAddress(address));
            return (T)proxy;

            // TODO
            // Proxy = ChannelFactory<T>.CreateChannel("ClientDeclarative");
            // Verschieben, raus aus dem UI Code
        }

        private T GetService<T>() where T : IClientChannel
        {
            proxyName = typeof(T).Name;
            string serviceUrl = Common.GetConfigValue<string>(proxyName);

            IClientChannel proxy;
            if(_proxies.TryGetValue(proxyName, out proxy))
            {
                proxy = GetProxy(proxy, new WSHttpBinding(), serviceUrl);
                _proxies.Add(proxyName, proxy);
            }

            return (T)proxy;
        }

    }
}
