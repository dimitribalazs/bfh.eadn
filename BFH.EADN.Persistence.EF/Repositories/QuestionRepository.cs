using BFH.EADN.Common.Types;
using System;
using System.Linq;
using CommonContracts = BFH.EADN.Common.Types.Contracts;

namespace BFH.EADN.Persistence.EF.Repositories
{
    //statt object data contract verwenden
    public class QuestionRepository : BaseRepository<CommonContracts.Question, Guid>
    {
        public override void Create(CommonContracts.Question data)
        {
            Context.Questions.Add(new Entities.Question
            {
                Hint = data.Hint,
                Text = data.Text,
                IsYesOrNo = data.IsYesOrNo,
                //todo add others
            });
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
                //throw new excepiton 
            }

            return new CommonContracts.Question
            {
                Id = question.Id,
                Hint = question.Hint,
                Text = question.Text,
                IsYesOrNo = question.IsYesOrNo                
            };
        }

        public override void Update(CommonContracts.Question data)
        {
            Entities.Question question = Context.Questions.Single(q => q.Id == data.Id);
            question.Hint = data.Hint;
            question.IsYesOrNo = data.IsYesOrNo;
            question.Text = data.Text;
            Context.SaveChanges();
        }
    }
}
