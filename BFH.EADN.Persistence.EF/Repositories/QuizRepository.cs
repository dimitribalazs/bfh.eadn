using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;

using CommonContracts = BFH.EADN.Common.Types.Contracts;

namespace BFH.EADN.Persistence.EF.Repositories
{
    /// <summary>
    /// Quiz repository to interact with the persistence layer
    /// </summary>
    public sealed class QuizRepository : BaseRepository<CommonContracts.Quiz, Guid>
    {
        ///<inheritdoc />
        public override void Create(CommonContracts.Quiz data)
        {
            Entities.Quiz newQuiz = Mapper.Map<Entities.Quiz>(data);
            if(data.Questions != null && data.Questions.Count > 0)
            {
                //Get all questions and set LastUsed state
                List<Guid> questionIds = data.Questions.Select(dq => dq.Id).ToList();
                List<Entities.Question> questions = Context.Questions.Where(q => questionIds.Contains(q.Id)).ToList();
                questions.ForEach(q => q.LastUsed = DateTime.Now);

                newQuiz.Questions = questions;
            }
            newQuiz.LastUsed = DateTime.Now;

            Context.Quizzes.Add(newQuiz);
            Context.SaveChanges();
        }

        ///<inheritdoc />
        public override void Delete(Guid Id)
        {
            Entities.Quiz quiz = Context.Quizzes.Single(q => q.Id == Id);
            if (CanBeDeleted(quiz.LastUsed, Common.Constants.DeletionThreshold) == false)
            {
                throw new InvalidOperationException("Cannot delete quiz because time threshold is not reached yet");
            }
            //we wont delete the questions
            quiz.Questions = null;            
            Context.Quizzes.Remove(quiz);
            Context.SaveChanges();
        }

        ///<inheritdoc />
        public override CommonContracts.Quiz Get(Guid Id)
        {
            Entities.Quiz quiz = Context.Quizzes.Single(q => q.Id == Id);
            CommonContracts.Quiz contractQuiz = Mapper.Map<CommonContracts.Quiz>(quiz);
            contractQuiz.CanBeDeleted = CanBeDeleted(quiz.LastUsed, Common.Constants.DeletionThreshold);
            return contractQuiz;
        }

        ///<inheritdoc />
        public override List<CommonContracts.Quiz> GetAll()
        {
            List<Entities.Quiz> quizzes = Context.Quizzes.ToList();
            List<CommonContracts.Quiz> contractQuizzes = Mapper.Map<List<Entities.Quiz>, List<CommonContracts.Quiz>>(quizzes);
            contractQuizzes.ForEach(q => q.CanBeDeleted = CanBeDeleted(q.LastUsed, Common.Constants.DeletionThreshold));
            return contractQuizzes;
        }

        ///<inheritdoc />
        public override List<CommonContracts.Quiz> GetListByIds(List<Guid> ids)
        {
            if (ids == null)
            {
                return null;
            }
            List<Entities.Quiz> quizzes = Context.Quizzes.Where(q => ids.Contains(q.Id)).ToList();
            List<CommonContracts.Quiz> contractQuizzes = Mapper.Map<List<Entities.Quiz>, List<CommonContracts.Quiz>>(quizzes);
            contractQuizzes.ForEach(q => CanBeDeleted(q.LastUsed, Common.Constants.DeletionThreshold));
            return contractQuizzes;
        }

        ///<inheritdoc />
        public override void Update(CommonContracts.Quiz data)
        {
            Entities.Quiz quiz = Context.Quizzes.Find(data.Id);
            quiz.Text = data.Text;
            quiz.Type = data.Type;
            quiz.MaxQuestionCount = data.MaxQuestionCount;
            quiz.MinQuestionCount = data.MinQuestionCount;
            quiz.LastUsed = DateTime.Now;
            
            List<Guid> guids = data.Questions.Select(q => q.Id).ToList();
            foreach(var question in Context.Questions.ToList())
            {
                if(guids.Contains(question.Id) == false)
                {
                    quiz.Questions.Remove(question);
                }
                else
                {
                    question.LastUsed = DateTime.Now;
                    quiz.Questions.Add(question);
                }
            }

            Context.SaveChanges();
        }
    }
}
