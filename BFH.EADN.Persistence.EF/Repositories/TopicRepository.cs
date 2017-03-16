using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;

using CommonContracts = BFH.EADN.Common.Types.Contracts;

namespace BFH.EADN.Persistence.EF.Repositories
{
    /// <summary>
    /// Topic repository to interact with the persistence layer
    /// </summary>
    public sealed class TopicRepository : BaseRepository<CommonContracts.Topic, Guid>
    {
        ///<inheritdoc />
        public override void Create(CommonContracts.Topic data)
        {
            Entities.Topic newTopic = Mapper.Map<Entities.Topic>(data);
            Context.Topics.Add(newTopic);
            Context.SaveChanges();
        }

        ///<inheritdoc />
        public override void Delete(Guid Id)
        {
            Context.Topics.Remove(Context.Topics.Single(q => q.Id == Id));
            Context.SaveChanges();
        }

        ///<inheritdoc />
        public override CommonContracts.Topic Get(Guid Id)
        {
            Entities.Topic topic = Context.Topics.Single(t => t.Id == Id);
            return Mapper.Map<CommonContracts.Topic>(topic);
        }
        
        ///<inheritdoc />

        public override List<CommonContracts.Topic> GetAll()
        {
            List<Entities.Topic> topics = Context.Topics.ToList();
            return Mapper.Map<List<Entities.Topic>, List<CommonContracts.Topic>>(topics);
        }

        ///<inheritdoc />
        public override List<CommonContracts.Topic> GetListByIds(List<Guid> ids)
        {
            if (ids == null)
            {
                return null;
            }
            List<Entities.Topic> topic = Context.Topics.Where(t => ids.Contains(t.Id)).ToList();
            return Mapper.Map<List<Entities.Topic>, List<CommonContracts.Topic>>(topic);
        }

        ///<inheritdoc />
        public override void Update(CommonContracts.Topic data)
        {
            Entities.Topic topic = Context.Topics.Single(q => q.Id == data.Id);
            topic.Name = data.Name;
            topic.Description = data.Description;
            Context.SaveChanges();
        }
    }
}
