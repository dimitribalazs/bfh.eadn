using BFH.EADN.Common.Types;
using System;
using System.Linq;
using CommonContracts = BFH.EADN.Common.Types.Contracts;

namespace BFH.EADN.Persistence.EF.Repositories
{
    //statt object data contract verwenden
    public sealed class QuizRepository : BaseRepository<CommonContracts.Quiz, Guid>
    {
        public override void Create(CommonContracts.Quiz data)
        {
            Context.Quizzes.Add(new Entities.Quiz
            {
                Text = data.Text,
                Type = data.Type
            });
            Context.SaveChanges();
        }

        public override void Delete(Guid Id)
        {
            Context.Quizzes.Remove(Context.Quizzes.Single(q => q.Id == Id));
        }

        public override CommonContracts.Quiz Get(Guid Id)
        {
            return Context.Quizzes.Select(q => new CommonContracts.Quiz
            {
                Text = q.Text,
                Type = q.Type
            }).First();
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
