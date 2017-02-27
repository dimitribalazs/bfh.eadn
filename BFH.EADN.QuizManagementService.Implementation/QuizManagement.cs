using BFH.EADN.Common;
using BFH.EADN.Common.Types;
using BFH.EADN.Common.Types.Contracts;
using BFH.EADN.QuizManagementService.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Permissions;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace BFH.EADN.QuizManagementService.Implementation
{
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.PerCall, Namespace = Constants.XMLNamespace, Name = "QuizManagement")]
    //[PrincipalPermission(SecurityAction.Demand, Role = "QuizAdmin")]
    public class QuizManagement : IAnswerManagement, ITopicManagement, IQuestionManagement, IQuizManagement//, ISession
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

        [PrincipalPermission(SecurityAction.Demand, Role = "QuizAdmin")]
        public string Test(string foo)
        {
            return foo;
        }

        [PrincipalPermission(SecurityAction.Demand, Role = "QuizAdmin")]
        public void CreateTopic(Topic topic)
        {
            try
            {
                TopicRepository.Create(topic);
                TopicRepository.Dispose();
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

        [PrincipalPermission(SecurityAction.Demand, Role = "QuizAdmin")]
        public void DeleteTopic(Guid id)
        {
            try
            {
                TopicRepository.Delete(id);
                TopicRepository.Dispose();
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

        [PrincipalPermission(SecurityAction.Demand, Role = "QuizAdminTest")]
        public void UpdateTopic(Topic topic)
        {
            try
            {
                TopicRepository.Update(topic);
                TopicRepository.Dispose();
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

        [PrincipalPermission(SecurityAction.Demand, Role = "QuizAdminTest")]
        public Topic GetTopic(Guid id)
        {
            try
            {
                Topic topic = TopicRepository.Get(id);
                TopicRepository.Dispose();
                return topic;
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

        [PrincipalPermission(SecurityAction.Demand, Role = "QuizAdmin")]
        public List<Topic> GetTopics()
        {
            try
            {
                List<Topic> topics = TopicRepository.GetAll();
                TopicRepository.Dispose();
                return topics;
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

        [PrincipalPermission(SecurityAction.Demand, Role = "QuizAdmin")]
        public List<Topic> GetTopicsByIds(List<Guid> ids)
        {
            try
            {
                if (ids == null) { throw new ArgumentNullException(nameof(ids) + "cannot be null"); }
                return TopicRepository.GetListByIds(ids);
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

        [PrincipalPermission(SecurityAction.Demand, Role = "QuizAdmin")]
        public void CreateAnswer(Answer answer)
        {
            try
            {
                AnswerRepository.Create(answer);
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

        [PrincipalPermission(SecurityAction.Demand, Role = "QuizAdmin")]
        public void UpdateAnswer(Answer answer)
        {
            try
            {
                AnswerRepository.Update(answer);
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

        [PrincipalPermission(SecurityAction.Demand, Role = "QuizAdmin")]
        public void DeleteAnswer(Guid id)
        {
            try
            {
                AnswerRepository.Delete(id);
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

        [PrincipalPermission(SecurityAction.Demand, Role = "QuizAdmin")]
        public Answer GetAnswer(Guid id)
        {
            try
            {
                return AnswerRepository.Get(id);
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

        [PrincipalPermission(SecurityAction.Demand, Role = "QuizAdmin")]
        public List<Answer> GetAnswers()
        {
            try
            {
                return AnswerRepository.GetAll();
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

        [PrincipalPermission(SecurityAction.Demand, Role = "QuizAdmin")]
        public void CreateQuestion(Question question)
        {
            QuestionRepository.Create(question);
        }

        [PrincipalPermission(SecurityAction.Demand, Role = "QuizAdmin")]
        public void UpdateQuestion(Question question)
        {
            QuestionRepository.Update(question);
        }

        [PrincipalPermission(SecurityAction.Demand, Role = "QuizAdmin")]
        public void DeleteQuestion(Guid id)
        {
            QuestionRepository.Delete(id);
        }

        [PrincipalPermission(SecurityAction.Demand, Role = "QuizAdmin")]
        public Question GetQuestion(Guid id)
        {
            return QuestionRepository.Get(id);
        }

        [PrincipalPermission(SecurityAction.Demand, Role = "QuizAdmin")]
        public List<Question> GetQuestions()
        {
            return QuestionRepository.GetAll();
        }

        [PrincipalPermission(SecurityAction.Demand, Role = "QuizAdmin")]
        public List<Question> GetQuestionsByIds(List<Guid> ids)
        {
            return QuestionRepository.GetListByIds(ids);
        }

        [PrincipalPermission(SecurityAction.Demand, Role = "QuizAdmin")]
        public void CreateQuiz(Quiz quiz)
        {
            try
            {
                QuizRepository.Create(quiz);
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

        [PrincipalPermission(SecurityAction.Demand, Role = "QuizAdmin")]
        public void UpdateQuiz(Quiz quiz)
        {
            QuizRepository.Update(quiz);
        }

        [PrincipalPermission(SecurityAction.Demand, Role = "QuizAdmin")]
        public void DeleteQuiz(Guid id)
        {
            QuizRepository.Delete(id);
        }

        [PrincipalPermission(SecurityAction.Demand, Role = "QuizAdmin")]
        public Quiz GetQuiz(Guid id)
        {
            return QuizRepository.Get(id);
        }

        [PrincipalPermission(SecurityAction.Demand, Role = "QuizAdmin")]
        public List<Quiz> GetQuizzes()
        {
            return QuizRepository.GetAll();
        }

        [PrincipalPermission(SecurityAction.Demand, Role = "QuizAdmin")]
        public List<Quiz> GetQuizzesByIds(List<Guid> ids)
        {
            return QuizRepository.GetListByIds(ids);
        }

        [PrincipalPermission(SecurityAction.Demand, Role = "QuizAdmin")]
        public bool Test(bool test)
        {
            return test;
        }

        private static Dictionary<string, string> loginSessions = new Dictionary<string, string>();
        private static Dictionary<string, string> adminLogins = new Dictionary<string, string>()
        {
            { "admin", "admin" }
        };

        public static void LogIn(string name, string password)
        {
            if(adminLogins.ContainsKey(name) && adminLogins[name].Equals(password))
            { 
                string sessionId = OperationContext.Current.SessionId;
                loginSessions.Add(sessionId, name);
            }
        }

        [PrincipalPermission(SecurityAction.Demand, Role = "QuizAdmin")]
        public static void LogOut()
        {
            string sessionId = OperationContext.Current.SessionId;
            if (IsLoggedIn(sessionId))
            {
                loginSessions.Remove(sessionId);
            }
        }

        //
        [PrincipalPermission(SecurityAction.Demand, Role = "QuizAdmin")]
        public static bool IsLoggedIn(string key)
        {
            return loginSessions.ContainsKey(key);
        }
    }
}
