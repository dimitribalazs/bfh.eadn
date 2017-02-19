using System;
using BFH.EADN.Common.Types;
using System.Linq.Expressions;
using System.Collections.Generic;
using AutoMapper;
using ContractTypes = BFH.EADN.Common.Types.Contracts;

namespace BFH.EADN.Persistence.EF.Repositories
{
    public abstract class BaseRepository<T, K> : IRepository<T, K>
    {
        /// <summary>
        /// Initiliaze mappings from contract data to entity data (ef)
        /// </summary>
        static BaseRepository()
        {
            Mapper.Initialize(cfg =>
             {
                 cfg.CreateMap<Entities.Answer, ContractTypes.Answer>();
                 cfg.CreateMap<ContractTypes.Answer, Entities.Answer>();

                 //Quiz
                 cfg.CreateMap<Entities.Quiz, ContractTypes.Quiz>()
                    .ForMember(
                        dest => dest.Questions,
                        opt => opt.MapFrom(src => src.Questions));
                 cfg.CreateMap<ContractTypes.Quiz, Entities.Quiz>();

                 //Questions
                 cfg.CreateMap<Entities.Question, ContractTypes.Question>()
                    .ForMember(
                        dest => dest.Answers,
                        opt => opt.MapFrom(src => src.Anwers))
                    .ForMember(
                        dest => dest.Topics,
                        opt => opt.MapFrom(src => src.Topics));
                 cfg.CreateMap<ContractTypes.Question, Entities.Question>();


                 cfg.CreateMap<Entities.Topic, ContractTypes.Topic>();
                 cfg.CreateMap<ContractTypes.Topic, Entities.Topic>();
             });
        }
        protected QuizDataContext Context { get; } = new QuizDataContext();
        public abstract void Create(T data);
        public abstract void Delete(K Id);
        public abstract T Get(K Id);
        //public abstract T Get(Func<T, bool> expr);
        public abstract List<T> GetAll();
        public abstract List<T> GetListByIds(List<K> ids);
        public abstract void Update(T data);
        public void Dispose()
        {
            Context?.Dispose();
        }
    }
}
