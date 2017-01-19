using BFH.EADN.Common.Types;
using System;
using System.Linq;
using CommonContracts = BFH.EADN.Common.Types.Contracts;

namespace BFH.EADN.Persistence.EF.Repositories
{
    //statt object data contract verwenden
    public class UserRepository : BaseRepository<CommonContracts.User, Guid>
    {
        public override void Create(CommonContracts.User data)
        {
            throw new NotImplementedException();
        }

        public override void Delete(Guid Id)
        {
            throw new NotImplementedException();
        }

        public override CommonContracts.User Get(Guid Id)
        {
            throw new NotImplementedException();
        }

        public override void Update(CommonContracts.User data)
        {
            throw new NotImplementedException();
        }
    }
}
