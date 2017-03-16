using System;

using BFH.EADN.Common.Types.Contracts;
using BFH.EADN.Common.Types;
using BFH.EADN.Persistence.EF.Repositories;

namespace BFH.EADN.Persistence.EF
{
    /// <summary>
    /// Creates and repository which uses the entity framework to save the data
    /// </summary>
    public class FactoryEF : IFactoryPersistence
    {
        /// <summary>
        /// Create an Answer repository
        /// </summary>
        /// <returns>an IRepository of type Answer with key type Guid</returns>
        public IRepository<Answer, Guid> CreateAnswerRepository() => new AnswerRepository();

        /// <summary>
        /// Create an Question repository
        /// </summary>
        /// <returns>an IRepository of type Question with key type Guid</returns>
        public IRepository<Question, Guid> CreateQuestionRepository() => new QuestionRepository();

        /// <summary>
        /// Create an Topic repository
        /// </summary>
        /// <returns>an IRepository of type Topic with key type Guid</returns>
        public IRepository<Topic, Guid> CreateTopicRepository() => new TopicRepository();

        /// <summary>
        /// Create an Quiz repository
        /// </summary>
        /// <returns>an IRepository of type Quiz with key type Guid</returns>
        public IRepository<Quiz, Guid> CreateQuizRepository() => new QuizRepository();

        /// <summary>
        /// Create an QuestionAnswerState repository
        /// </summary>
        /// <returns>an IRepository of type QuestionAnswerState with key type Guid</returns>
        public IRepository<QuestionAnswerState, Guid> CreateQuestionAnswerStateRepository() => new QuestionAnswerStateRepository();
    }
}