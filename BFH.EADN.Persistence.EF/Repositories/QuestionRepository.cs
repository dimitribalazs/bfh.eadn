using AutoMapper;
using BFH.EADN.Common.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using CommonContracts = BFH.EADN.Common.Types.Contracts;

namespace BFH.EADN.Persistence.EF.Repositories
{
    //statt object data contract verwenden
    public sealed class QuestionRepository : BaseRepository<CommonContracts.Question, Guid>
    {
        public override void Create(CommonContracts.Question data)
        {
            Entities.Question newQuestion = Mapper.Map<Entities.Question>(data);
            Context.Questions.Add(newQuestion);
            Context.SaveChanges();
        }

        public override void Delete(Guid Id)
        {
            Context.Questions.Remove(Context.Questions.Single(q => q.Id == Id));
        }

        public override CommonContracts.Question Get(Guid Id)
        {
            Entities.Question question = Context.Questions.Find(Id);
            if (question == null)
            {
                return null;
            }

            return Mapper.Map<CommonContracts.Question>(question);
        }

        public override List<CommonContracts.Question> GetAll()
        {
            List<Entities.Question> quetsions = Context.Questions.ToList();
            return Mapper.Map<List<Entities.Question>, List<CommonContracts.Question>>(quetsions);
        }

        public override List<CommonContracts.Question> GetListByIds(List<Guid> ids)
        {
            List<Entities.Question> quetsions = Context.Questions.Where(q => ids.Contains(q.Id)).ToList();
            return Mapper.Map<List<Entities.Question>, List<CommonContracts.Question>>(quetsions);
        }

        public override void Update(CommonContracts.Question data)
        {
            Entities.Question question = Context.Questions.Single(q => q.Id == data.Id);
            question.Hint = data.Hint;
            question.IsYesOrNo = data.IsMultipleChoise;
            question.Text = data.Text;
            Context.SaveChanges();
        }
    }
}
