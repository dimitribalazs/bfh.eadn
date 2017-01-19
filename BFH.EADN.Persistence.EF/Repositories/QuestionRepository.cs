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
            throw new NotImplementedException();
        }

        public override void Delete(Guid Id)
        {
            throw new NotImplementedException();
        }

        public override CommonContracts.Question Get(Guid Id)
        {
            throw new NotImplementedException();
        }

        public override void Update(CommonContracts.Question data)
        {
            throw new NotImplementedException();
        }
    }
}
