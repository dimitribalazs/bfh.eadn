using BFH.EADN.Common.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BFH.EADN.Persistence.EF.Repositories
{
    //statt object data contract verwenden
    public class QuizRepository : IRepository<Common.Types.Contracts.Quiz, Guid>
    {
        public void Create(Common.Types.Contracts.Quiz data)
        {
            throw new NotImplementedException();
        }

        public void Delete(Guid Id)
        {
            throw new NotImplementedException();
        }

        public Common.Types.Contracts.Quiz Get(Guid Id)
        {
            throw new NotImplementedException();
        }

        public void Update(Common.Types.Contracts.Quiz data)
        {
            throw new NotImplementedException();
        }
    }
}
