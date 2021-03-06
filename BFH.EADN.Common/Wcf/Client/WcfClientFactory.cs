﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.ServiceModel.Channels;
using System.Text;
using System.Threading.Tasks;

namespace BFH.EADN.Common.Wcf.Client
{
    /// <summary>
    /// Creates a wcf client
    /// </summary>
    public static class WcfClientFactory
    {
        public static WcfClient<TServiceContract> CreateClient<TServiceContract>(EndpointAddress endpointAddress, Binding binding) 
            where TServiceContract : class
        {
            return new WcfClient<TServiceContract>(endpointAddress, binding);
        }

        public static WcfClient<TServiceContract> CreateClient<TServiceContract>(string configurationName)
            where TServiceContract : class
        {
            return new WcfClient<TServiceContract>(configurationName);
        }
    }
}
