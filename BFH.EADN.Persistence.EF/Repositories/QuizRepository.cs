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
            Context.Quizzes.Add(newQuiz);
            Context.SaveChanges();
        }

        public override void Delete(Guid Id)
        {
            Context.Quizzes.Remove(Context.Quizzes.Single(q => q.Id == Id));
        }

        public override CommonContracts.Quiz Get(Guid Id)
        {
            Entities.Quiz quiz = Context.Quizzes.Find(Id);
            if (quiz == null)
            {
                return null;
            }

            return Mapper.Map<CommonContracts.Quiz>(quiz);
        }

        public override List<CommonContracts.Quiz> GetAll()
        {
            List<Entities.Quiz> quizzes = Context.Quizzes.ToList();
            return Mapper.Map<List<Entities.Quiz>, List<CommonContracts.Quiz>>(quizzes);
        }

        public override List<CommonContracts.Quiz> GetListByIds(List<Guid> ids)
        {
            List<Entities.Quiz> quizzes = Context.Quizzes.Where(q => ids.Contains(q.Id)).ToList();
            return Mapper.Map<List<Entities.Quiz>, List<CommonContracts.Quiz>>(quizzes);
        }

        public override void Update(CommonContracts.Quiz data)
        {
            Entities.Quiz quiz = Context.Quizzes.Single(q => q.Id == data.Id);
            //todo question references
            //quiz.Questions = data.
            quiz.Text = data.Text;
            quiz.Type = data.Type;
            Context.SaveChanges();
        }
    }
}
