using BFH.EADN.Common.Types;
using BFH.EADN.Persistence.EF.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BFH.EADN.Common.Types.Contracts;

namespace BFH.EADN.Persistence.EF
{
    public class FactoryEF : IFactoryPersistence
    {
       
        IRepository<Quiz, Guid> IFactoryPersistence.CreateQuizRepo()
        {
            return new QuizRepository();
        }
    }
}