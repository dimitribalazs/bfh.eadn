using BFH.EADN.Common;
using BFH.EADN.Common.Types;
using BFH.EADN.Common.Types.Contracts;
using BFH.EADN.QuizManagementService.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Permissions;
using System.Security.Principal;
using System.ServiceModel;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Security.Claims;

namespace BFH.EADN.QuizManagementService.Implementation
{
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.PerCall, Namespace = Constants.XMLNamespace, Name = "QuizManagement")]
    public class QuizManagement : IAnswerManagement, ITopicManagement, IQuestionManagement, IQuizManagement, ISession
    {
        /// <summary>
        /// Holds the persistence factory during the call
        /// </summary>
        private IFactoryPersistence _persistenceFactory;

        /// <summary>
        /// Get Topic repository
        /// </summary>
        private IRepository<Topic, Guid> TopicRepository => _persistenceFactory.CreateTopicRepository();

        /// <summary>
        /// Get Answer repository
        /// </summary>
        private IRepository<Answer, Guid> AnswerRepository => _persistenceFactory.CreateAnswerRepository();

        /// <summary>
        /// Get Question repository
        /// </summary>
        private IRepository<Question, Guid> QuestionRepository => _persistenceFactory.CreateQuestionRepository();

        /// <summary>
        /// Get Quiz repository
        /// </summary>
        private IRepository<Quiz, Guid> QuizRepository => _persistenceFactory.CreateQuizRepository();

        /// <summary>
        /// Default constructor which creates a new persistence factory
        /// </summary>
        public QuizManagement()
        {
            _persistenceFactory = Factory.CreateInstance<IFactoryPersistence>();
        }

        /// <summary>
        /// Constructor to pass in persistence factory
        /// </summary>
        /// <param name="persistenceFactory">a persistence factory</param>
        public QuizManagement(IFactoryPersistence persistenceFactory)
        {
            _persistenceFactory = persistenceFactory;
        }

        ///<inheritdoc />
        public void CreateTopic(Topic topic)
        {
            try
            {
                using (IRepository<Topic, Guid> repo = TopicRepository)
                {
                    repo.Create(topic);
                }
            }
            catch (Exception ex)
            {
                ServiceFault fault = Common.Common.CreateServiceFault(ex, "Error during creation of a new topic");
                throw new FaultException<ServiceFault>(fault);
            }
        }

        ///<inheritdoc />
        public void DeleteTopic(Guid id)
        {
            try
            {
                using (IRepository<Topic, Guid> repo = TopicRepository)
                {
                    repo.Delete(id);
                }
            }
            catch (Exception ex)
            {
                ServiceFault fault = Common.Common.CreateServiceFault(ex, "Error during deletion of a topic");
                throw new FaultException<ServiceFault>(fault);
            }
        }

        ///<inheritdoc />
        public void UpdateTopic(Topic topic)
        {
            try
            {
                using (IRepository<Topic, Guid> repo = TopicRepository)
                {
                    repo.Update(topic);
                }

            }
            catch (Exception ex)
            {
                ServiceFault fault = Common.Common.CreateServiceFault(ex, "Error while updating a topic");
                throw new FaultException<ServiceFault>(fault);
            }
        }

        ///<inheritdoc />
        public Topic GetTopic(Guid id)
        {
            try
            {
                using (IRepository<Topic, Guid> repo = TopicRepository)
                {
                    return repo.Get(id);
                }
            }
            catch (Exception ex)
            {
                ServiceFault fault = Common.Common.CreateServiceFault(ex, "Error while getting topic");
                throw new FaultException<ServiceFault>(fault);
            }
        }

        ///<inheritdoc />
        public List<Topic> GetTopics()
        {
            try
            {
                using (IRepository<Topic, Guid> repo = TopicRepository)
                {
                    List<Topic> topics = repo.GetAll();
                    return topics;
                }
            }
            catch (Exception ex)
            {
                ServiceFault fault = Common.Common.CreateServiceFault(ex, "Error while getting topics");
                throw new FaultException<ServiceFault>(fault);
            }
        }

        ///<inheritdoc />
        public List<Topic> GetTopicsByIds(List<Guid> ids)
        {
            try
            {
                using (IRepository<Topic, Guid> repo = TopicRepository)
                {
                    return repo.GetListByIds(ids);
                }
            }
            catch (Exception ex)
            {
                ServiceFault fault = Common.Common.CreateServiceFault(ex, "Error while getting topics by ids");
                throw new FaultException<ServiceFault>(fault);
            }
        }

        ///<inheritdoc />
        public void CreateAnswer(Answer answer)
        {
            try
            {
                using (IRepository<Answer, Guid> repo = AnswerRepository)
                {
                    repo.Create(answer);
                }
            }
            catch (Exception ex)
            {
                ServiceFault fault = Common.Common.CreateServiceFault(ex, "Error while creating answer");
                throw new FaultException<ServiceFault>(fault);
            }
        }

        ///<inheritdoc />
        public void UpdateAnswer(Answer answer)
        {
            try
            {
                using (IRepository<Answer, Guid> repo = AnswerRepository)
                {
                    repo.Update(answer);
                }
            }
            catch (Exception ex)
            {
                ServiceFault fault = Common.Common.CreateServiceFault(ex, "Error while updating answer");
                throw new FaultException<ServiceFault>(fault);
            }
        }

        ///<inheritdoc />
        public void DeleteAnswer(Guid id)
        {
            try
            {
                using (IRepository<Answer, Guid> repo = AnswerRepository)
                {
                    repo.Delete(id);
                }
            }
            catch (Exception ex)
            {
                ServiceFault fault = Common.Common.CreateServiceFault(ex, "Error while deleting answer");
                throw new FaultException<ServiceFault>(fault);
            }
        }

        ///<inheritdoc />
        public Answer GetAnswer(Guid id)
        {
            try
            {
                using (IRepository<Answer, Guid> repo = AnswerRepository)
                {
                    return repo.Get(id);
                }
            }
            catch (Exception ex)
            {
                ServiceFault fault = Common.Common.CreateServiceFault(ex, "Error while getting answer");
                throw new FaultException<ServiceFault>(fault);
            }
        }

        ///<inheritdoc />
        public List<Answer> GetAnswers()
        {
            try
            {
                using (IRepository<Answer, Guid> repo = AnswerRepository)
                {
                    return repo.GetAll();
                }
            }
            catch (Exception ex)
            {
                ServiceFault fault = Common.Common.CreateServiceFault(ex, "Error while getting answers");
                throw new FaultException<ServiceFault>(fault);
            }
        }

        ///<inheritdoc />
        public List<Answer> GetAnswersByIds(List<Guid> ids)
        {
            try
            {
                using (IRepository<Answer, Guid> repo = AnswerRepository)
                {
                    return repo.GetListByIds(ids);
                }
            }
            catch (Exception ex)
            {
                ServiceFault fault = Common.Common.CreateServiceFault(ex, "Error while getting answers by ids");
                throw new FaultException<ServiceFault>(fault);
            }
        }

        ///<inheritdoc />
        public void CreateQuestion(Question question)
        {
            try
            {
                using (IRepository<Question, Guid> repo = QuestionRepository)
                {
                    repo.Create(question);
                }
            }
            catch (Exception ex)
            {
                ServiceFault fault = Common.Common.CreateServiceFault(ex, "Error while creating question");
                throw new FaultException<ServiceFault>(fault);
            }
        }

        ///<inheritdoc />
        public void UpdateQuestion(Question question)
        {
            try
            {
                using (IRepository<Question, Guid> repo = QuestionRepository)
                {
                    repo.Update(question);
                }
            }
            catch (Exception ex)
            {
                ServiceFault fault = Common.Common.CreateServiceFault(ex, "Error while updating question");
                throw new FaultException<ServiceFault>(fault);
            }
        }

        ///<inheritdoc />
        public void DeleteQuestion(Guid id)
        {
            try
            {
                using (IRepository<Question, Guid> repo = QuestionRepository)
                {
                    repo.Delete(id);
                }
            }
            catch (Exception ex)
            {
                ServiceFault fault = Common.Common.CreateServiceFault(ex, "Error while deleting question");
                throw new FaultException<ServiceFault>(fault);
            }
        }

        ///<inheritdoc />
        public Question GetQuestion(Guid id)
        {
            try
            {
                using (IRepository<Question, Guid> repo = QuestionRepository)
                {
                    return repo.Get(id);
                }
            }
            catch (Exception ex)
            {
                ServiceFault fault = Common.Common.CreateServiceFault(ex, "Error while getting question");
                throw new FaultException<ServiceFault>(fault);
            }
        }

        ///<inheritdoc />
        public List<Question> GetQuestions()
        {
            try
            {
                using (IRepository<Question, Guid> repo = QuestionRepository)
                {
                    return repo.GetAll();
                }
            }
            catch (Exception ex)
            {
                ServiceFault fault = Common.Common.CreateServiceFault(ex, "Error while getting questions");
                throw new FaultException<ServiceFault>(fault);
            }
        }

        ///<inheritdoc />
        public List<Question> GetQuestionsWithoutTopic()
        {
            try
            {
                using (IRepository<Question, Guid> repo = QuestionRepository)
                {
                    return repo.GetAll().Where(q => q.Topics == null || q.Topics.Count <= 0).ToList();
                }
            }
            catch (Exception ex)
            {
                ServiceFault fault = Common.Common.CreateServiceFault(ex, "Error while getting questions which do not have a topic");
                throw new FaultException<ServiceFault>(fault);
            }
        }

        ///<inheritdoc />
        public List<Question> GetQuestionsByIds(List<Guid> ids)
        {
            try
            {
                using (IRepository<Question, Guid> repo = QuestionRepository)
                {
                    return repo.GetListByIds(ids);
                }
            }
            catch (Exception ex)
            {
                ServiceFault fault = Common.Common.CreateServiceFault(ex, "Error while getting questions by ids");
                throw new FaultException<ServiceFault>(fault);
            }
        }

        ///<inheritdoc />
        public void CreateQuiz(Quiz quiz)
        {
            try
            {
                using (IRepository<Quiz, Guid> repo = QuizRepository)
                {
                    repo.Create(quiz);
                }
            }
            catch (Exception ex)
            {
                ServiceFault fault = Common.Common.CreateServiceFault(ex, "Error while creating quiz");
                throw new FaultException<ServiceFault>(fault);
            }
        }

        ///<inheritdoc />
        public void UpdateQuiz(Quiz quiz)
        {
            try
            {
                using (IRepository<Quiz, Guid> repo = QuizRepository)
                {
                    repo.Update(quiz);
                }
            }
            catch (Exception ex)
            {
                ServiceFault fault = Common.Common.CreateServiceFault(ex, "Error while updating quiz");
                throw new FaultException<ServiceFault>(fault);
            }
        }

        ///<inheritdoc />
        public void DeleteQuiz(Guid id)
        {
            try
            {
                using (IRepository<Quiz, Guid> repo = QuizRepository)
                {
                    repo.Delete(id);
                }
            }
            catch (Exception ex)
            {
                ServiceFault fault = Common.Common.CreateServiceFault(ex, "Error while deleting quiz");
                throw new FaultException<ServiceFault>(fault);
            }
        }

        ///<inheritdoc />
        public Quiz GetQuiz(Guid id)
        {
            try
            {
                using (IRepository<Quiz, Guid> repo = QuizRepository)
                {
                    return repo.Get(id);
                }
            }
            catch (Exception ex)
            {
                ServiceFault fault = Common.Common.CreateServiceFault(ex, "Error while getting quiz");
                throw new FaultException<ServiceFault>(fault);
            }
        }

        ///<inheritdoc />
        public List<Quiz> GetQuizzes()
        {
            try
            {
                using (IRepository<Quiz, Guid> repo = QuizRepository)
                {
                    return repo.GetAll();
                }
            }
            catch (Exception ex)
            {
                ServiceFault fault = Common.Common.CreateServiceFault(ex, "Error while getting quizzes");
                throw new FaultException<ServiceFault>(fault);
            }
        }

        ///<inheritdoc />
        public List<Quiz> GetQuizzesByIds(List<Guid> ids)
        {
            try
            {
                using (IRepository<Quiz, Guid> repo = QuizRepository)
                {
                    return repo.GetListByIds(ids);
                }
            }
            catch (Exception ex)
            {
                ServiceFault fault = Common.Common.CreateServiceFault(ex, "Error while getting quizzes by ids");
                throw new FaultException<ServiceFault>(fault);
            }
        }


        public User GetUserByName(string name)
        {
            Admin admin = admins.FirstOrDefault(a => a.Name.Equals(name));
            if (admin != null)
            {
                return new User
                {
                    Id = admin.Id,
                    Name = admin.Name,
                    Password = admin.Password
                };
            }
            return null;
        }

        public Claim LogIn()
             => new Claim(ClaimTypes.Role, Constants.AdminRoleName);


        public void LogOut()
        {
            throw new NotImplementedException();
        }

               
        private static List<Admin> admins = new List<Admin>
        {
            new Admin()
        };

        private class Admin
        {
            public Guid Id => Guid.Parse("61408abc-df3e-4f97-b501-4e9720530ff7");
            public string Name => "Admin";
            public string Password => "AAorcLme9Z/b9oJF5rbRcchQyM+j+SkjkOldeEIVXTx/eT4b6eDQmbyyhifxsqIYBw==";
        }
    }
}
