
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.ServiceModel.Channels;
using System.Text;
using System.Threading.Tasks;

namespace BFH.EADN.Common.Wcf.Client
{
    public class WcfClient<TServiceContract> : WcfClientBase<TServiceContract> where TServiceContract : class
    {

        public WcfClient(EndpointAddress endpointAddress, Binding binding) : base(endpointAddress, binding) { }

        protected override ChannelFactory<TServiceContract> CreateChannelFactory()
        {
            ChannelFactory<TServiceContract> cf = new ChannelFactory<TServiceContract>(Binding, EndpointAddress);
            cf.Endpoint.EndpointBehaviors.Add(new CustomBehavior());
            return cf;
        }
    }
}  
