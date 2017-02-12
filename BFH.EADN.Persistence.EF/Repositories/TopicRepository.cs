using BFH.EADN.Common.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using CommonContracts = BFH.EADN.Common.Types.Contracts;

namespace BFH.EADN.Persistence.EF.Repositories
{
    //statt object data contract verwenden
    public sealed class TopicRepository : BaseRepository<CommonContracts.Topic, Guid>
    {
        public override void Create(CommonContracts.Topic data)
        {
            Context.Topics.Add(new Entities.Topic
            {
                Name = data.Name,
                Description = data.Description
            });
            Context.SaveChanges();
        }

        public override void Delete(Guid Id)
        {
            Context.Topics.Remove(Context.Topics.Single(q => q.Id == Id));
            Context.SaveChanges();
        }

        public override CommonContracts.Topic Get(Guid Id)
        {
            Entities.Topic topic = Context.Topics.Find(Id);
            if (topic == null)
            {
                return null;
            }

            return new CommonContracts.Topic
            {
                Id = topic.Id,
                Name = topic.Name,
                Description = topic.Description
            };
        }

        public override List<CommonContracts.Topic> GetAll()
        {            
            IQueryable<CommonContracts.Topic> query = Context.Topics.Select(t => new CommonContracts.Topic
            {
                Id = t.Id,
                Description = t.Description,
                Name = t.Name
            });
            return query.ToList();
        }

        public override void Update(CommonContracts.Topic data)
        {
            Entities.Topic topic = Context.Topics.Single(q => q.Id == data.Id);
            topic.Name = data.Name;
            topic.Description = data.Description;
            Context.SaveChanges();
        }
    }
}
