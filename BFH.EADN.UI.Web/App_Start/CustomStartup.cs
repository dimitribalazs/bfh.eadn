using AutoMapper;
using BFH.EADN.UI.Web.Models.Management;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ContractTypes = BFH.EADN.Common.Types.Contracts;

namespace BFH.EADN.UI.Web
{
	public class CustomStartup
	{
		public static void Init()
        {
            //Automapper configuration
            Mapper.Initialize(cfg =>
            {
				//topic area
                cfg.CreateMap<Topic, ContractTypes.Topic>();
                cfg.CreateMap<ContractTypes.Topic, Topic>();

                //answer area
                cfg.CreateMap<Answer, ContractTypes.Answer>();
                cfg.CreateMap<ContractTypes.Answer, Answer>();
            });
        }
	}
}