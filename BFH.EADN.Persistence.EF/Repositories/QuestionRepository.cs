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
            if (data.Topics != null && data.Topics.Count > 0)
            {
                List<Guid> topicIds = data.Topics.Select(dq => dq.Id).ToList();
                newQuestion.Topics = Context.Topics.Where(q => topicIds.Contains(q.Id)).ToList();
            }
            Context.Questions.Add(newQuestion);
            Context.SaveChanges();
        }

        public override void Delete(Guid Id)
        {
            Context.Questions.Remove(Context.Questions.Single(q => q.Id == Id));
            Context.SaveChanges();
        }

        public override CommonContracts.Question Get(Guid Id)
        {
            Entities.Question question = Context.Questions.Single(q => q.Id == Id);
            return Mapper.Map<CommonContracts.Question>(question);
        }

        public override List<CommonContracts.Question> GetAll()
        {
            List<Entities.Question> quetsions = Context.Questions.ToList();
            return Mapper.Map<List<Entities.Question>, List<CommonContracts.Question>>(quetsions);
        }

        public override List<CommonContracts.Question> GetListByIds(List<Guid> ids)
        {
            if (ids == null)
            {
                return null;
            }
            List<Entities.Question> quetsions = Context.Questions.Where(q => ids.Contains(q.Id)).ToList();
            return Mapper.Map<List<Entities.Question>, List<CommonContracts.Question>>(quetsions);
        }

        public override void Update(CommonContracts.Question data)
        {
            Entities.Question question = Context.Questions.Single(q => q.Id == data.Id);
            question.Hint = data.Hint;
            question.IsMultipleChoice = data.IsMultipleChoice;
            question.Text = data.Text;
            
            List<Guid> guids = data.Topics.Select(t => t.Id).ToList();
            foreach (var topic in Context.Topics.ToList())
            {
                if (guids.Contains(topic.Id) == false)
                {
                    question.Topics.Remove(topic);
                }
                else
                {
                    question.Topics.Add(topic);
                }
            }

            guids = data.Answers.Select(t => t.Id).ToList();
            foreach (var answer in Context.Answers.ToList())
            {
                if (guids.Contains(answer.Id) == false)
                {
                    question.Answers.Remove(answer);
                }
                else
                {
                    question.Answers.Add(answer);
                }
            }

            Context.SaveChanges();
        }
    }
}
