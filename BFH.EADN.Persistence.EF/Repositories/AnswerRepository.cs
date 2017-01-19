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
            throw new NotImplementedException();
        }

        public override void Delete(Guid Id)
        {
            throw new NotImplementedException();
        }

        public override CommonContracts.Answer Get(Guid Id)
        {
            throw new NotImplementedException();
        }

        public override void Update(CommonContracts.Answer data)
        {
            throw new NotImplementedException();
        }
    }
}
