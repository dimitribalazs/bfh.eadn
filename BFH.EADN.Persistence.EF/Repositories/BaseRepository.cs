using System;
using System.Collections.Generic;
using AutoMapper;

using BFH.EADN.Common.Types;
using ContractTypes = BFH.EADN.Common.Types.Contracts;


namespace BFH.EADN.Persistence.EF.Repositories
{
    /// <summary>
    /// Base repository which all concrete repositories must inherit from.
    /// </summary>
    /// <typeparam name="T">repository type (BaseContract) </typeparam>
    /// <typeparam name="K">type of id which the the repository uses</typeparam>
    public abstract class BaseRepository<T, K> : IRepository<T, K>, IDisposable
        where T : ContractTypes.BaseContract
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
                        opt => opt.MapFrom(src => src.Answers))
                    .ForMember(
                        dest => dest.Topics,
                        opt => opt.MapFrom(src => src.Topics));
                 cfg.CreateMap<ContractTypes.Question, Entities.Question>();

                 cfg.CreateMap<Entities.Topic, ContractTypes.Topic>();
                 cfg.CreateMap<ContractTypes.Topic, Entities.Topic>();

                 cfg.CreateMap<Entities.QuestionAnswerState, ContractTypes.QuestionAnswerState>();                 
                 cfg.CreateMap<ContractTypes.QuestionAnswerState, Entities.QuestionAnswerState>();
             });
        }

        ///<inheritdoc />
        internal QuizDataContext Context { get; } = new QuizDataContext();

        ///<inheritdoc />
        public abstract void Create(T data);

        ///<inheritdoc />
        public abstract void Delete(K Id);

        ///<inheritdoc />
        public abstract T Get(K Id);

        ///<inheritdoc />
        public abstract List<T> GetAll();

        ///<inheritdoc />
        public abstract List<T> GetListByIds(List<K> ids);

        ///<inheritdoc />
        public abstract void Update(T data);

        /// <summary>
        /// Disposes the context
        /// </summary>
        public void Dispose()
        {
            Context?.Dispose();
        }

        public bool CanBeDeleted(DateTime lastUsed, int lastUsedThresholdInDay)
            => (DateTime.Now - lastUsed).Days > lastUsedThresholdInDay;
    }
}
