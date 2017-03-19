using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.ServiceModel.Channels;
using System.ServiceModel.Description;
using System.Text;
using System.Threading.Tasks;

namespace BFH.EADN.Common.Wcf.Client
{
    /// <summary>
    /// Client
    /// </summary>
    /// <typeparam name="TServiceContract"></typeparam>
    public abstract class WcfBaseClient<TServiceContract>
        where TServiceContract : class
    {
        /// <summary>
        /// Add own custom behavior
        /// </summary>
        public IEndpointBehavior CustomBehavior { get; set; } 

        /// <summary>
        /// Key to read configuration from config
        /// </summary>
        protected string ConfigurationName { get; set; }

        /// <summary>
        ///The endpoint address of the server
        /// </summary>
        protected EndpointAddress EndpointAddress { get; set; }

        /// <summary>
        /// The used binding
        /// </summary>
        protected Binding Binding { get; set; }

        /// <summary>
        /// Service proxy
        /// </summary>
        protected TServiceContract Proxy { get; set; }

        /// <summary>
        /// Read WCF client configuration from config
        /// </summary>
        /// <param name="configurationName">configuration name</param>
        public WcfBaseClient(string configurationName)
        {
            if(configurationName == null) { throw new ArgumentNullException(nameof(configurationName) + " cannot be null"); };
            if (string.IsNullOrEmpty(configurationName)) { throw new ArgumentException(nameof(configurationName) + " cannot be empty"); }
            ConfigurationName = configurationName;
        }

        /// <summary>
        /// Concrete params
        /// </summary>
        /// <param name="endpointAddress">EndpointAddress</param>
        /// <param name="binding">Binding</param>
        public WcfBaseClient(EndpointAddress endpointAddress, Binding binding)
        {
            EndpointAddress = endpointAddress ?? throw new ArgumentNullException(nameof(endpointAddress) + "cannot be null");
            Binding = binding ?? throw new ArgumentNullException(nameof(binding) + "cannot be null");
        }

        /// <summary>
        /// Creates a channel factory of type TServiceContract
        /// </summary>
        /// <returns>ChannelFactory of tpye TServiceContract</returns>
        protected abstract ChannelFactory<TServiceContract> CreateChannelFactory();

        /// <summary>
        /// Get the proxy of type TServiceContract
        /// </summary>
        /// <returns>a proxy of type TServiceContract</returns>
        public TServiceContract GetProxy()
        {
            IChannel channel = Proxy as IClientChannel;
            if (Proxy != null && channel.State == CommunicationState.Opened)
            {
                return Proxy;
            }
            if (Proxy != null && channel.State == CommunicationState.Faulted)
            {
                channel.Abort();
            }
            ChannelFactory<TServiceContract> cf = CreateChannelFactory();

            if(CustomBehavior != null)
            {
                cf.Endpoint.EndpointBehaviors.Add(CustomBehavior);
            }
            Proxy = cf.CreateChannel();
            return Proxy;
        }
    }
}
