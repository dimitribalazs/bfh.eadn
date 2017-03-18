
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.ServiceModel.Channels;
using System.Text;
using System.Threading.Tasks;

namespace BFH.EADN.Common.Wcf.Client
{
    /// <summary>
    /// Wcf client
    /// </summary>
    /// <typeparam name="TServiceContract">ServiceContract type</typeparam>
    public sealed class WcfClient<TServiceContract> : WcfBaseClient<TServiceContract> 
        where TServiceContract : class
    {
        public WcfClient(string configurationName) : base(configurationName) { }
        public WcfClient(EndpointAddress endpointAddress, Binding binding) : base(endpointAddress, binding) { }

        ///<inheritdoc />
        protected override ChannelFactory<TServiceContract> CreateChannelFactory()
        {
            ChannelFactory<TServiceContract> cf;
            //if configuration is not set Binding and EndpointAddress from property
            if(Binding != null && EndpointAddress != null)
            {
                cf = new ChannelFactory<TServiceContract>(Binding, EndpointAddress);
            }
            else
            {
                //at least configuration must be set
                cf = new ChannelFactory<TServiceContract>(ConfigurationName);    
            }

            cf.Endpoint.EndpointBehaviors.Add(new CustomBehavior());
            return cf;
            
        }
    }
}  
