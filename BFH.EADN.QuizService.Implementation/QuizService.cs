using BFH.EADN.Common;
using BFH.EADN.QuizService.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;
using BFH.EADN.Common.Types.Contracts;
using BFH.EADN.Common.Types;

namespace BFH.EADN.QuizService.Implementation
{
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.PerCall, Namespace = Constants.XMLNamespace, Name = "QuizService")]
    public class QuizService : IPlay
    {
        private static IFactoryPersistence _persistenceFactory;
        private IRepository<Question, Guid> QuestionRepository => _persistenceFactory.CreateQuestionRepository();
        private IRepository<Quiz, Guid> QuizRepository => _persistenceFactory.CreateQuizRepository();

        static QuizService()
        {
            if (_persistenceFactory == null)
            {
                _persistenceFactory = Factory.CreateInstance<IFactoryPersistence>();
            }
        }

        public Quiz GetQuiz(Guid id)
        {
            return QuizRepository.Get(id);
        }

        public List<Quiz> GetQuizzes()
        {
            return QuizRepository.GetAll();
        }
    }
}
