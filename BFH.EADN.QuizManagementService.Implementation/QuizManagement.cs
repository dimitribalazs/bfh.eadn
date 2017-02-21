﻿using BFH.EADN.Common;
using BFH.EADN.Common.Types;
using BFH.EADN.Common.Types.Contracts;
using BFH.EADN.QuizManagementService.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace BFH.EADN.QuizManagementService.Implementation
{
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.PerCall, Namespace = Constants.XMLNamespace, Name = "QuizManagement")]
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

        public string Test(string foo)
        {
            return foo;
        }

        public void CreateTopic(Topic topic)
        {
            try
            {
                TopicRepository.Create(topic);
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

        public void DeleteTopic(Guid id)
        {
            try
            {
                TopicRepository.Delete(id);
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

        public void UpdateTopic(Topic topic)
        {
            try
            {
                TopicRepository.Update(topic);
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

        public Topic GetTopic(Guid id)
        {
            try
            {
                return TopicRepository.Get(id);
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

        public List<Topic> GetTopics()
        {
            try
            {
                return TopicRepository.GetAll();
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

        public void CreateQuestion(Question question)
        {
            QuestionRepository.Create(question);
        }

        public void UpdateQuestion(Question question)
        {
            QuestionRepository.Update(question);
        }

        public void DeleteQuestion(Guid id)
        {
            QuestionRepository.Delete(id);
        }

        public Question GetQuestion(Guid id)
        {
            return QuestionRepository.Get(id);
        }

        public List<Question> GetQuestions()
        {
            return QuestionRepository.GetAll();
        }

        public List<Question> GetQuestionsByIds(List<Guid> ids)
        {
            return QuestionRepository.GetListByIds(ids);
        }

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

        public void UpdateQuiz(Quiz quiz)
        {
            QuizRepository.Update(quiz);
        }

        public void DeleteQuiz(Guid id)
        {
            QuizRepository.Delete(id);
        }

        public Quiz GetQuiz(Guid id)
        {
            return QuizRepository.Get(id);
        }

        public List<Quiz> GetQuizzes()
        {
            return QuizRepository.GetAll();
        }

        public List<Quiz> GetQuizzesByIds(List<Guid> ids)
        {
            return QuizRepository.GetListByIds(ids);
        }

        public bool Test(bool test)
        {
            return test;
        }

        private static Dictionary<string, string> loginSessions = new Dictionary<string, string>();
        public void LogIn(string name, string password)
        {
            if(true)
            {
                string sessionId = OperationContext.Current.SessionId;
                loginSessions.Add(sessionId, name);
            }
        }

        public void LogOut()
        {
            string sessionId = OperationContext.Current.SessionId;
            if (IsLoggedIn(sessionId))
            {
                loginSessions.Remove(sessionId);
            }
        }

        public static bool IsLoggedIn(string key)
        {
            return loginSessions.ContainsKey(key);
        }
    }
}
