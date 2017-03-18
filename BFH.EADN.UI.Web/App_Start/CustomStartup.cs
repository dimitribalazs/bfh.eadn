using AutoMapper;
using BFH.EADN.UI.Web.Models.Management;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ContractTypes = BFH.EADN.Common.Types.Contracts;

namespace BFH.EADN.UI.Web
{
    /// <summary>
    /// Custom startup class
    /// </summary>
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

                //question area
                cfg.CreateMap<Question, ContractTypes.Question>()
                    .ForMember(
                                dest => dest.Topics,
                                opt => opt.MapFrom(src => src.Topics));
                cfg.CreateMap<ContractTypes.Question, Question>()
                   .ForMember(
                            dest => dest.Topics,
                            opt => opt.MapFrom(src => src.Topics));

                //question area
                cfg.CreateMap<Quiz, ContractTypes.Quiz>()
                    .ForMember(
                            dest => dest.Questions,
                            opt => opt.MapFrom(src => src.Questions));
                cfg.CreateMap<ContractTypes.Quiz, Quiz>()
                    .ForMember(
                            dest => dest.Questions,
                            opt => opt.MapFrom(src => src.Questions));
            });
        }
	}
}