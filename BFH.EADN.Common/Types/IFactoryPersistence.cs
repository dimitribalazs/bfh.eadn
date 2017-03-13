using BFH.EADN.Common.Types.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BFH.EADN.Common.Types
{
    /// <summary>
    /// Creates the specific repositories to read/write data 
    /// from/to a persistence layer
    /// </summary>
    public interface IFactoryPersistence
    {
        /// <summary>
        /// Creates a Quiz repository
        /// </summary>
        /// <returns>repository of type IRepository</returns>
        IRepository<Quiz, Guid> CreateQuizRepository();
        /// <summary>
        /// Creates a Answer repository
        /// </summary>
        /// <returns>repository of type IRepository</returns>
        IRepository<Answer, Guid> CreateAnswerRepository();
        /// <summary>
        /// Creates a Question repository
        /// </summary>
        /// <returns>repository of type IRepository</returns>
        IRepository<Question, Guid> CreateQuestionRepository();
        /// <summary>
        /// Creates a Topic repository
        /// </summary>
        /// <returns>repository of type IRepository</returns>
        IRepository<Topic, Guid> CreateTopicRepository();
        /// <summary>
        /// Creates a QuestionAnswerState repository
        /// </summary>
        /// <returns>repository of type IRepository</returns>
        IRepository<QuestionAnswerState, Guid> CreateQuestionAnswerStateRepository();
    }
}
