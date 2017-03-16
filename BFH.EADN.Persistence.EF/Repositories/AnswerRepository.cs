using BFH.EADN.Common.Types;
using System;
using System.Linq;
using CommonContracts = BFH.EADN.Common.Types.Contracts;
using System.Linq.Expressions;
using System.Collections.Generic;
using AutoMapper;

namespace BFH.EADN.Persistence.EF.Repositories
{
    public sealed class AnswerRepository : BaseRepository<CommonContracts.Answer, Guid>
    {
        public override void Create(CommonContracts.Answer data)
        {
            Entities.Answer newAnswer = Mapper.Map<Entities.Answer>(data);
            newAnswer.Question = Context.Questions.Single(q => q.Id == data.QuestionId);
            Context.Answers.Add(newAnswer);
            Context.SaveChanges();
        }

        public override void Delete(Guid Id)
        {
            Context.Answers.Remove(Context.Answers.Single(a => a.Id == Id));
            Context.SaveChanges();
        }

        public override CommonContracts.Answer Get(Guid Id)
        {
            Entities.Answer answer = Context.Answers.Single(a => a.Id == Id);
            return Mapper.Map<CommonContracts.Answer>(answer);
        }

        public override List<CommonContracts.Answer> GetAll()
        {
            List<Entities.Answer> answers = Context.Answers.ToList();
            return Mapper.Map<List<Entities.Answer>, List<CommonContracts.Answer>>(answers);
        }

        public override List<CommonContracts.Answer> GetListByIds(List<Guid> ids)
        {
            if(ids == null)
            {
                return null;
            }
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
            Context.SaveChanges();
        }
    }
}
