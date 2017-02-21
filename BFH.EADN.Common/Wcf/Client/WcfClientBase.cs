using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.ServiceModel.Channels;
using System.Text;
using System.Threading.Tasks;

namespace BFH.EADN.Common.Wcf.Client
{
    public abstract class WcfClientBase<TServiceContract> where TServiceContract : class
    {
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


        public WcfClientBase(EndpointAddress endpointAddress, Binding binding)
        {
            if (endpointAddress == null) { throw new ArgumentNullException(nameof(endpointAddress) + "cannot be null"); }
            if (binding == null) { throw new ArgumentNullException(nameof(binding) + "cannot be null"); }
            EndpointAddress = endpointAddress;
            Binding = binding;
        }

        protected abstract ChannelFactory<TServiceContract> CreateChannelFactory();

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
            Proxy = CreateChannelFactory().CreateChannel();
            return Proxy;
        }
    }
}
