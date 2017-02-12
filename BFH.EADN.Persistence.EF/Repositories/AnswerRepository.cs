using BFH.EADN.Common.Types;
using System;
using System.Linq;
using CommonContracts = BFH.EADN.Common.Types.Contracts;
using System.Linq.Expressions;
using System.Collections.Generic;

namespace BFH.EADN.Persistence.EF.Repositories
{
    //statt object data contract verwenden
    public sealed class AnswerRepository : BaseRepository<CommonContracts.Answer, Guid>
    {
        public override void Create(CommonContracts.Answer data)
        {
            Context.Answers.Add(new Entities.Answer
            {
                Text = data.Text,
                IsSolution = data.IsSolution

            });
            Context.SaveChanges();
        }

        public override void Delete(Guid Id)
        {
            Context.Answers.Remove(Context.Answers.Single(a => a.Id == Id));
        }

        public override CommonContracts.Answer Get(Guid Id)
        {
            Entities.Answer answer = Context.Answers.Find(Id);
            if (answer == null)
            {
                return null;
            }

            return new CommonContracts.Answer
            {
                Id = answer.Id,
                IsSolution = answer.IsSolution,
                Text = answer.Text,
                //Type = answer.
            };
        }

        public override List<CommonContracts.Answer> GetAll()
        {
            IQueryable<CommonContracts.Answer> query = Context.Answers.Select(a => new CommonContracts.Answer
            {
                Id = a.Id,
                IsSolution = a.IsSolution,
                Text = a.Text,
                //todo type
                //Type = a.            
            });
            return query.ToList();
        }

        public override List<CommonContracts.Answer> GetListByIds(List<Guid> ids)
        {
            IQueryable<CommonContracts.Answer> query = Context.Answers
                .Where(a => ids.Contains(a.Id))
                .Select(a => new CommonContracts.Answer
                {
                    Id = a.Id,
                    IsSolution = a.IsSolution,
                    Text = a.Text,
                    //todo type           
                });
            return query.ToList();
        }

        public override void Update(CommonContracts.Answer data)
        {
            Entities.Answer answer = Context.Answers.Single(a => a.Id == data.Id);
            //todo question references
            //quiz.Questions = data.
            answer.Text = data.Text;
            answer.IsSolution = data.IsSolution;
            answer.Topics = new HashSet<Entities.Topic>();
    
            List<Guid> topicIds = data.Topics.Select(t => t.Id).ToList();
            List<Entities.Topic> topics = Context.Topics.Where(t => topicIds.Contains(t.Id)).ToList();
            answer.Topics = topics;
            
            Context.SaveChanges();
        }
    }
}
