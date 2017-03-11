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

namespace BFH.EADN.QuizManagementService.Implementation
{
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.PerCall, Namespace = Constants.XMLNamespace, Name = "QuizManagement")]
    //[PrincipalPermission(SecurityAction.Demand, Role = "QuizAdmin")]
    public class QuizManagement : IAnswerManagement, ITopicManagement, IQuestionManagement, IQuizManagement, ISession
    {
        private static IFactoryPersistence _persistenceFactory;
        private IRepository<Topic, Guid> TopicRepository => _persistenceFactory.CreateTopicRepository();
        private IRepository<Answer, Guid> AnswerRepository => _persistenceFactory.CreateAnswerRepository();
        private IRepository<Question, Guid> QuestionRepository => _persistenceFactory.CreateQuestionRepository();
        private IRepository<Quiz, Guid> QuizRepository => _persistenceFactory.CreateQuizRepository();


        static QuizManagement()
        {
            if (_persistenceFactory == null)
            {
                _persistenceFactory = Factory.CreateInstance<IFactoryPersistence>();
            }
        }

        //[PrincipalPermission(SecurityAction.Demand, Role = "QuizAdmin")]
        public string Test(string foo)
        {
            return foo;
        }

        // [PrincipalPermission(SecurityAction.Demand, Role = "QuizAdmin")]
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
                ServiceFault fault = new ServiceFault
                {
                    Message = ex.Message,
                    Reason = "Error during creation of a new topic"
                };
                throw new FaultException<ServiceFault>(fault);
            }
        }

        // [PrincipalPermission(SecurityAction.Demand, Role = "QuizAdmin")]
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
                ServiceFault fault = new ServiceFault
                {
                    Message = ex.Message,
                    Reason = "Error during deletion of a topic"
                };
                throw new FaultException<ServiceFault>(fault);
            }
        }

        // [PrincipalPermission(SecurityAction.Demand, Role = "QuizAdminTest")]
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
                ServiceFault fault = new ServiceFault
                {
                    Message = ex.Message,
                    Reason = "Error while updating a topic"
                };
                throw new FaultException<ServiceFault>(fault);
            }
        }

        //[PrincipalPermission(SecurityAction.Demand, Role = "QuizAdminTest")]
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
                ServiceFault fault = new ServiceFault
                {
                    Message = ex.Message,
                    Reason = "Error while getting topic"
                };
                throw new FaultException<ServiceFault>(fault);
            }
        }

        //[PrincipalPermission(SecurityAction.Demand, Role = "QuizAdmin")]
        public List<Topic> GetTopics()
        {
            try
            {
                using (IRepository<Topic,Guid> repo = TopicRepository)
                {
                    List<Topic> topics = repo.GetAll();
                    return topics;
                }
            }
            catch (Exception ex)
            {
                ServiceFault fault = new ServiceFault
                {
                    Message = ex.Message,
                    Reason = "Error while getting topics"
                };
                throw new FaultException<ServiceFault>(fault);
            }
        }

        //[PrincipalPermission(SecurityAction.Demand, Role = "QuizAdmin")]
        public List<Topic> GetTopicsByIds(List<Guid> ids)
        {
            try
            {
                if (ids == null) { throw new ArgumentNullException(nameof(ids) + "cannot be null"); }

                using (IRepository<Topic, Guid> repo = TopicRepository)
                {
                    return repo.GetListByIds(ids);
                }
            }
            catch (Exception ex)
            {
                ServiceFault fault = new ServiceFault
                {
                    Message = ex.Message,
                    Reason = "Error while getting topics"
                };
                throw new FaultException<ServiceFault>(fault);
            }
        }

        //[PrincipalPermission(SecurityAction.Demand, Role = "QuizAdmin")]
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
                ServiceFault fault = new ServiceFault
                {
                    Message = ex.Message,
                    Reason = "Error while creating answer"
                };
                throw new FaultException<ServiceFault>(fault);
            }
        }

        // [PrincipalPermission(SecurityAction.Demand, Role = "QuizAdmin")]
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
                ServiceFault fault = new ServiceFault
                {
                    Message = ex.Message,
                    Reason = "Error while updating answer"
                };
                throw new FaultException<ServiceFault>(fault);
            }
        }

        // [PrincipalPermission(SecurityAction.Demand, Role = "QuizAdmin")]
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
                ServiceFault fault = new ServiceFault
                {
                    Message = ex.Message,
                    Reason = "Error while updating answer"
                };
                throw new FaultException<ServiceFault>(fault);
            }
        }

        // [PrincipalPermission(SecurityAction.Demand, Role = "QuizAdmin")]
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
                ServiceFault fault = new ServiceFault
                {
                    Message = ex.Message,
                    Reason = "Error while getting an answer"
                };
                throw new FaultException<ServiceFault>(fault);
            }
        }

        // [PrincipalPermission(SecurityAction.Demand, Role = "QuizAdmin")]
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
                ServiceFault fault = new ServiceFault
                {
                    Message = ex.Message,
                    Reason = "Error while getting answers"
                };
                throw new FaultException<ServiceFault>(fault);
            }
        }

        // [PrincipalPermission(SecurityAction.Demand, Role = "QuizAdmin")]
        public void CreateQuestion(Question question)
        {
            using (IRepository<Question, Guid> repo = QuestionRepository)
            {
                repo.Create(question);
            }
        }

        // [PrincipalPermission(SecurityAction.Demand, Role = "QuizAdmin")]
        public void UpdateQuestion(Question question)
        {
            using (IRepository<Question, Guid> repo = QuestionRepository)
            {
                repo.Update(question);
            }
        }

        // [PrincipalPermission(SecurityAction.Demand, Role = "QuizAdmin")]
        public void DeleteQuestion(Guid id)
        {
            using (IRepository<Question, Guid> repo = QuestionRepository)
            {
                repo.Delete(id);
            }
        }

        //[PrincipalPermission(SecurityAction.Demand, Role = "QuizAdmin")]
        public Question GetQuestion(Guid id)
        {
            using (IRepository<Question, Guid> repo = QuestionRepository)
            {
                return repo.Get(id);
            }
        }

        // [PrincipalPermission(SecurityAction.Demand, Role = "QuizAdmin")]
        public List<Question> GetQuestions()
        {
            using (IRepository<Question, Guid> repo = QuestionRepository)
            {
                return repo.GetAll();
            }
        }

        //[PrincipalPermission(SecurityAction.Demand, Role = "QuizAdmin")]
        public List<Question> GetQuestionsByIds(List<Guid> ids)
        {
            using (IRepository<Question, Guid> repo = QuestionRepository)
            {
                return repo.GetListByIds(ids);
            }
        }

        //[PrincipalPermission(SecurityAction.Demand, Role = "QuizAdmin")]
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
                ServiceFault fault = new ServiceFault
                {
                    Message = ex.Message,
                    Reason = "Error during deletion of a topic"
                };
                throw new FaultException<ServiceFault>(fault);
            }
        }

        //[PrincipalPermission(SecurityAction.Demand, Role = "QuizAdmin")]
        public void UpdateQuiz(Quiz quiz)
        {
            using (IRepository<Quiz, Guid> repo = QuizRepository)
            {
                repo.Update(quiz);
            }
        }

        // [PrincipalPermission(SecurityAction.Demand, Role = "QuizAdmin")]
        public void DeleteQuiz(Guid id)
        {
            using (IRepository<Quiz, Guid> repo = QuizRepository)
            {
                repo.Delete(id);
            }
        }

        //[PrincipalPermission(SecurityAction.Demand, Role = "QuizAdmin")]
        public Quiz GetQuiz(Guid id)
        {
            using (IRepository<Quiz, Guid> repo = QuizRepository)
            {
                return repo.Get(id);
            }
        }

        // [PrincipalPermission(SecurityAction.Demand, Role = "QuizAdmin")]
        public List<Quiz> GetQuizzes()
        {
            using (IRepository<Quiz, Guid> repo = QuizRepository)
            {
                return repo.GetAll();
            }
        }

        //[PrincipalPermission(SecurityAction.Demand, Role = "QuizAdmin")]
        public List<Quiz> GetQuizzesByIds(List<Guid> ids)
        {
            using (IRepository<Quiz, Guid> repo = QuizRepository)
            {
                return repo.GetListByIds(ids);
            }
        }

        //[PrincipalPermission(SecurityAction.Demand, Role = "QuizAdmin")]
        public bool Test(bool test)
        {
            return test;
        }

        private static Dictionary<string, string> loginSessions = new Dictionary<string, string>();
        private static List<Admin> admins = new List<Admin>
        {
            new Admin()
        };

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

        public User GetUserById(Guid id)
        {
            Admin admin = admins.FirstOrDefault(a => a.Id == id);
            User user = null;
            if (admin != null)
            {
                user = new User
                {
                    Id = admin.Id,
                    Name = admin.Name,
                    Password = admin.Password
                };
            }
            return user;
        }

        public static void LogIn(string name, string password)
        {
            //    if(adminLogins.ContainsKey(name) && adminLogins[name].Equals(password))
            //    { 
            //        string sessionId = OperationContext.Current.SessionId;
            //        loginSessions.Add(sessionId, name);
            //}
        }

        //[PrincipalPermission(SecurityAction.Demand, Role = "QuizAdmin")]
        //public static void LogOut()
        //{
        //    string sessionId = OperationContext.Current.SessionId;
        //    if (IsLoggedIn(sessionId))
        //    {
        //        loginSessions.Remove(sessionId);
        //    }
        //}

        //
        //[PrincipalPermission(SecurityAction.Demand, Role = "QuizAdmin")]
        //public static bool IsLoggedIn(string key)
        //{
        //    return loginSessions.ContainsKey(key);
        //}
    }

    internal class Admin
    {
        public Guid Id => Guid.Parse("61408abc-df3e-4f97-b501-4e9720530ff7");
        public string Name => "Admin";
        public string Password => "AAorcLme9Z/b9oJF5rbRcchQyM+j+SkjkOldeEIVXTx/eT4b6eDQmbyyhifxsqIYBw==";

    }

}
