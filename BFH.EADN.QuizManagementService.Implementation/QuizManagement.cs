using BFH.EADN.Common;
using BFH.EADN.Common.Types;
using BFH.EADN.Common.Types.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BFH.EADN.QuizManagementService.Implementation
{
    public class QuizManagement
    {
        public void PersistenceTest(Quiz quiz)
        {

            IFactoryPersistence factory = Factory.CreateInstance<IFactoryPersistence>();
            IRepository<Quiz, Guid> repo =  factory.CreateQuizRepository();

            //IRepository<QuizData, Guid> repo =  Persistence.PersistenceFactory<QuizData>.Create();
            repo.Create(quiz);
        }
        
    }
}
