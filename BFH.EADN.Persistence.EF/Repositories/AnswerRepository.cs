using BFH.EADN.Common.Types;
using System;
using System.Linq;
using CommonContracts = BFH.EADN.Common.Types.Contracts;
using System.Linq.Expressions;
using System.Collections.Generic;
using AutoMapper;

namespace BFH.EADN.Persistence.EF.Repositories
{
    //statt object data contract verwenden
    public sealed class AnswerRepository : BaseRepository<CommonContracts.Answer, Guid>
    {
        public override void Create(CommonContracts.Answer data)
        {
            Entities.Answer newAnswer = Mapper.Map<Entities.Answer>(data);
            Context.Answers.Add(newAnswer);
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

            return Mapper.Map<CommonContracts.Answer>(answer);
        }

        public override List<CommonContracts.Answer> GetAll()
        {
            List<Entities.Answer> answers = Context.Answers.ToList();
            return Mapper.Map<List<Entities.Answer>, List<CommonContracts.Answer>>(answers);
        }

        public override List<CommonContracts.Answer> GetListByIds(List<Guid> ids)
        {
            List<Entities.Answer> answers = Context.Answers.Where(a => ids.Contains(a.Id)).ToList();
            return Mapper.Map<List<Entities.Answer>, List<CommonContracts.Answer>>(answers);
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
