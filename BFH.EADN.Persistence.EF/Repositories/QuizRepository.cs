using AutoMapper;
using BFH.EADN.Common.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using CommonContracts = BFH.EADN.Common.Types.Contracts;

namespace BFH.EADN.Persistence.EF.Repositories
{
    //statt object data contract verwenden
    public sealed class QuizRepository : BaseRepository<CommonContracts.Quiz, Guid>
    {
        public override void Create(CommonContracts.Quiz data)
        {
            Entities.Quiz newQuiz = Mapper.Map<Entities.Quiz>(data);
            if(data.Questions != null && data.Questions.Count > 0)
            {
                List<Guid> questionIds = data.Questions.Select(dq => dq.Id).ToList();
                newQuiz.Questions = Context.Questions.Where(q => questionIds.Contains(q.Id)).ToList();
            }
            Context.Quizzes.Add(newQuiz);
            Context.SaveChanges();
        }

        public override void Delete(Guid Id)
        {
            Entities.Quiz quiz = Context.Quizzes.Single(q => q.Id == Id);
            //we wont delete the questions
            quiz.Questions = null;            
            Context.Quizzes.Remove(quiz);
            Context.SaveChanges();
        }

        public override CommonContracts.Quiz Get(Guid Id)
        {
            Entities.Quiz quiz = Context.Quizzes.Single(q => q.Id == Id);
            return Mapper.Map<CommonContracts.Quiz>(quiz);
        }

        public override List<CommonContracts.Quiz> GetAll()
        {
            List<Entities.Quiz> quizzes = Context.Quizzes.ToList();
            return Mapper.Map<List<Entities.Quiz>, List<CommonContracts.Quiz>>(quizzes);
        }

        public override List<CommonContracts.Quiz> GetListByIds(List<Guid> ids)
        {
            if (ids == null)
            {
                return null;
            }
            List<Entities.Quiz> quizzes = Context.Quizzes.Where(q => ids.Contains(q.Id)).ToList();
            return Mapper.Map<List<Entities.Quiz>, List<CommonContracts.Quiz>>(quizzes);
        }

        public override void Update(CommonContracts.Quiz data)
        {
            Entities.Quiz quiz = Context.Quizzes.Find(data.Id);
            quiz.Text = data.Text;
            quiz.Type = data.Type;
            quiz.MaxQuestionCount = data.MaxQuestionCount;
            quiz.MinQuestionCount = data.MinQuestionCount;
            
            List<Guid> guids = data.Questions.Select(q => q.Id).ToList();
            foreach(var question in Context.Questions.ToList())
            {
                if(guids.Contains(question.Id) == false)
                {
                    quiz.Questions.Remove(question);
                }
                else
                {
                    quiz.Questions.Add(question);
                }
            }

            Context.SaveChanges();
        }
    }
}
