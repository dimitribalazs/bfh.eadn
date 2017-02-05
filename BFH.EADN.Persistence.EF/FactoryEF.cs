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
        public IRepository<Answer, Guid> CreateAnswerRepository() => new AnswerRepository();
        public IRepository<Question, Guid> CreateQuestionRepository() => new QuestionRepository();
        public IRepository<Topic, Guid> CreateTopicRepository() => new TopicRepository();     
        IRepository<Quiz, Guid> IFactoryPersistence.CreateQuizRepository() => new QuizRepository();
    }
}