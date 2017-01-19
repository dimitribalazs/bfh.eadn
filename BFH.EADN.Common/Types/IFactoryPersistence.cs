using BFH.EADN.Common.Types.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BFH.EADN.Common.Types
{
    public interface IFactoryPersistence
    {
        //todo change string to datacontract 
        IRepository<Quiz, Guid> CreateQuizRepository();
    }
}
