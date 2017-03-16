using System;
using System.Linq;
using System.Collections.Generic;
using AutoMapper;

using CommonContracts = BFH.EADN.Common.Types.Contracts

namespace BFH.EADN.Persistence.EF.Repositories
{
    /// <summary>
    /// Answer repository to interact with the persistence layer
    /// </summary>
    public sealed class AnswerRepository : BaseRepository<CommonContracts.Answer, Guid>
    {
        ///<inheritdoc />
        public override void Create(CommonContracts.Answer data)
        {
            Entities.Answer newAnswer = Mapper.Map<Entities.Answer>(data);
            newAnswer.Question = Context.Questions.Single(q => q.Id == data.QuestionId);
            Context.Answers.Add(newAnswer);
            Context.SaveChanges();
        }

        ///<inheritdoc />
        public override void Delete(Guid Id)
        {
            Context.Answers.Remove(Context.Answers.Single(a => a.Id == Id));
            Context.SaveChanges();
        }

        ///<inheritdoc />
        public override CommonContracts.Answer Get(Guid Id)
        {
            Entities.Answer answer = Context.Answers.Single(a => a.Id == Id);
            return Mapper.Map<CommonContracts.Answer>(answer);
        }

        ///<inheritdoc />
        public override List<CommonContracts.Answer> GetAll()
        {
            List<Entities.Answer> answers = Context.Answers.ToList();
            return Mapper.Map<List<Entities.Answer>, List<CommonContracts.Answer>>(answers);
        }

        ///<inheritdoc />
        public override List<CommonContracts.Answer> GetListByIds(List<Guid> ids)
        {
            if(ids == null)
            {
                return null;
            }
            List<Entities.Answer> answers = Context.Answers.Where(a => ids.Contains(a.Id)).ToList();
            return Mapper.Map<List<Entities.Answer>, List<CommonContracts.Answer>>(answers);
        }

        ///<inheritdoc />
        public override void Update(CommonContracts.Answer data)
        {
            Entities.Answer answer = Context.Answers.Single(a => a.Id == data.Id);
            answer.Text = data.Text;
            answer.IsSolution = data.IsSolution;   
            Context.SaveChanges();
        }
    }
}
