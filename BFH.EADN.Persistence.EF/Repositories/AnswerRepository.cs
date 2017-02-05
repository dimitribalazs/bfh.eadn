using BFH.EADN.Common.Types;
using System;
using System.Linq;
using CommonContracts = BFH.EADN.Common.Types.Contracts;

namespace BFH.EADN.Persistence.EF.Repositories
{
    //statt object data contract verwenden
    public class AnswerRepository : BaseRepository<CommonContracts.Answer, Guid>
    {
        public override void Create(CommonContracts.Answer data)
        {
            Context.Answers.Add(new Entities.Answer
            {
                Text = data.Text,
                IsSolution = data.IsSolution
                
            });
            Context.SaveChanges();
        }

        public override void Delete(Guid Id)
        {
            Context.Answers.Remove(Context.Answers.Single(a => a.Id == Id));
        }

        public override CommonContracts.Answer Get(Guid Id)
        {
            Entities.Answer answer = Context.Answers.Find(Id);
            if(answer == null)
            {
                //throw new excepiton 
            }

            return new CommonContracts.Answer
            {
                Id = answer.Id,
                IsSolution = answer.IsSolution,
                Text = answer.Text,
                //Type = answer.
            };
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
