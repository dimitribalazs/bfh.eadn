using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;

using CommonContracts = BFH.EADN.Common.Types.Contracts;

namespace BFH.EADN.Persistence.EF.Repositories
{
    /// <summary>
    /// Question repository to interact with the persistence layer
    /// </summary>
    public sealed class QuestionRepository : BaseRepository<CommonContracts.Question, Guid>
    {
        ///<inheritdoc />
        public override void Create(CommonContracts.Question data)
        {
            Entities.Question newQuestion = Mapper.Map<Entities.Question>(data);
            if (data.Topics != null && data.Topics.Count > 0)
            {
                List<Guid> topicIds = data.Topics.Select(dq => dq.Id).ToList();
                newQuestion.Topics = Context.Topics.Where(q => topicIds.Contains(q.Id)).ToList();
            }
            newQuestion.LastUsed = DateTime.Now;
            Context.Questions.Add(newQuestion);
            Context.SaveChanges();
        }

        ///<inheritdoc />
        public override void Delete(Guid Id)
        {
            Entities.Question question = Context.Questions.Single(q => q.Id == Id);
            if (CanBeDeleted(question.LastUsed, Common.Constants.DeletionThreshold) == false)
            {
                throw new InvalidOperationException("Cannot delete quiz because time threshold is not reached yet");
            }
            Context.Questions.Remove(question);
            Context.SaveChanges();
        }

        ///<inheritdoc />
        public override CommonContracts.Question Get(Guid Id)
        {
            Entities.Question question = Context.Questions.Single(q => q.Id == Id);
            CommonContracts.Question contractQuestion = Mapper.Map<CommonContracts.Question>(question);
            contractQuestion.CanBeDeleted = CanBeDeleted(question.LastUsed, Common.Constants.DeletionThreshold);
            return contractQuestion;
        }

        ///<inheritdoc />
        public override List<CommonContracts.Question> GetAll()
        {
            List<Entities.Question> questions = Context.Questions.ToList();
            List<CommonContracts.Question> contractQuestions = Mapper.Map<List<Entities.Question>, List<CommonContracts.Question>>(questions);
            contractQuestions.ForEach(q => CanBeDeleted(q.LastUsed, Common.Constants.DeletionThreshold));
            return contractQuestions;
        }

        ///<inheritdoc />
        public override List<CommonContracts.Question> GetListByIds(List<Guid> ids)
        {
            if (ids == null)
            {
                return null;
            }
            List<Entities.Question> quetsions = Context.Questions.Where(q => ids.Contains(q.Id)).ToList();
            List<CommonContracts.Question> contractQuestions = Mapper.Map<List<Entities.Question>, List<CommonContracts.Question>>(quetsions);
            contractQuestions.ForEach(q => CanBeDeleted(q.LastUsed, Common.Constants.DeletionThreshold));
            return contractQuestions;
        }

        ///<inheritdoc />
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
