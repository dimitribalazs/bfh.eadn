using Microsoft.VisualStudio.TestTools.UnitTesting;
using BFH.EADN.QuizManagementService.Implementation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ServiceModel;
using BFH.EADN.Common.Types.Contracts;
using BFH.EADN.Common;
using BFH.EADN.Common.Types;
using BFH.EADN.QuizManagementService.Contracts;

namespace BFH.EADN.QuizManagementService.Implementation.Tests
{
    [TestClass()]
    public class QuizManagementTests
    {
        /* Answer area */
        [TestMethod]
        public void CreateAnswerSuccess()
        {
            IAnswerManagement service = new QuizManagement();
            IQuestionManagement qs = new QuizManagement();
            Guid questionId = qs.GetQuestions().First().Id;

            //create new answer
            Answer answer = new Answer
            {
               IsSolution = true,
               Text = "test answer",
               QuestionId = questionId
            };

            service.CreateAnswer(answer);
            Assert.IsTrue(true);
        }

        [TestMethod]
        [ExpectedException(typeof(FaultException<ServiceFault>))]
        public void CreateAnswerFailure()
        {
            IAnswerManagement service = new QuizManagement();
            service.CreateAnswer(null);
        }

        [TestMethod]
        public void GetListAndDeleteAnswerSuccess()
        {
            IAnswerManagement service = new QuizManagement();
            List<Answer> answers = service.GetAnswers();
            int answerCount = answers.Count;

            service.DeleteAnswer(answers.First().Id);

            Assert.AreEqual(answerCount - 1, service.GetAnswers().Count, "Count of answers not the same");
        }

        [TestMethod]
        [ExpectedException(typeof(FaultException<ServiceFault>))]
        public void DeleteAnswerExcpetion()
        {
            Guid randomGuid = Guid.NewGuid();
            IAnswerManagement service = new QuizManagement();
            service.DeleteAnswer(randomGuid);
        }

        [TestMethod]
        [ExpectedException(typeof(FaultException<ServiceFault>))]
        public void GetAnswerException()
        {
            Guid randomGuid = Guid.NewGuid();
            IAnswerManagement service = new QuizManagement();
            service.GetAnswer(randomGuid);
        }
        
        [TestMethod]
        public void GetAnswerSuccess()
        {
            IAnswerManagement service = new QuizManagement();
            Guid expectedResult = service.GetAnswers().First().Id;
            Answer answer = service.GetAnswer(expectedResult);
            Assert.AreEqual(expectedResult, answer.Id, "Ids are not the same");
       }

        /* Topic area */
        [TestMethod]
        public void CreateTopicSuccess()
        {
            ITopicManagement service = new QuizManagement();
            //create new topic
            Topic topic = new Topic
            {
                Name = "Test Topic",
                Description = "This is a test topic description"
            };

            service.CreateTopic(topic);
            Assert.IsTrue(true);
        }

        [TestMethod]
        [ExpectedException(typeof(FaultException<ServiceFault>))]
        public void CreateTopicFailure()
        {
            ITopicManagement service = new QuizManagement();
            service.CreateTopic(null);
        }

        [TestMethod]
        public void GetListAndDeleteTopicSuccess()
        {
            ITopicManagement service = new QuizManagement();
            List<Topic> results = service.GetTopics();
            int count = results.Count;

            service.DeleteTopic(results.First().Id);

            Assert.AreEqual(count - 1, service.GetTopics().Count, "Count of topic not the same");
        }

        [TestMethod]
        [ExpectedException(typeof(FaultException<ServiceFault>))]
        public void DeleteTopicFail()
        {
            ITopicManagement service = new QuizManagement();
            Guid guid = Guid.NewGuid();
            service.DeleteTopic(guid);
        }

        [TestMethod]
        [ExpectedException(typeof(FaultException<ServiceFault>))]
        public void GetTopicException()
        {
            Guid randomGuid = Guid.NewGuid();
            ITopicManagement service = new QuizManagement();
            service.GetTopic(randomGuid);
        }

        [TestMethod]
        public void GetTopicSuccess()
        {
            ITopicManagement service = new QuizManagement();
            Guid expectedResult = service.GetTopics().First().Id;
            Topic result = service.GetTopic(expectedResult);
            Assert.AreEqual(expectedResult, result.Id, "Ids are not the same");
        }

        /* Question area */
        [TestMethod]
        public void GetListAndDeleteQuestionSuccess()
        {
            IQuestionManagement service = new QuizManagement();
            List<Question> results = service.GetQuestions();
            int count = results.Count;

            service.DeleteQuestion(results.First().Id);

            Assert.AreEqual(count - 1, service.GetQuestions().Count, "Count of questions not the same");
        }

        [TestMethod]
        [ExpectedException(typeof(FaultException<ServiceFault>))]
        public void DeleteQuestionFail()
        {
            IQuestionManagement service = new QuizManagement();
            Guid guid = Guid.NewGuid();
            service.DeleteQuestion(guid);
        }

        [TestMethod]
        [ExpectedException(typeof(FaultException<ServiceFault>))]
        public void GetQuestionException()
        {
            Guid randomGuid = Guid.NewGuid();
            IQuestionManagement service = new QuizManagement();
            service.GetQuestion(randomGuid);
        }

        [TestMethod]
        public void GetQuestionSuccess()
        {
            IQuestionManagement service = new QuizManagement();
            Guid expectedResult = service.GetQuestions().First().Id;
            Question result = service.GetQuestion(expectedResult);
            Assert.AreEqual(expectedResult, result.Id, "Ids are not the same");
        }

        /* Quiz area */
        [TestMethod]
        public void GetListAndDeleteQuizSuccess()
        {
            IQuizManagement service = new QuizManagement();
            List<Quiz> results = service.GetQuizzes();
            int count = results.Count;

            service.DeleteQuiz(results.First().Id);

            Assert.AreEqual(count - 1, service.GetQuizzes().Count, "Count of quizzes not the same");
        }

        [TestMethod]
        [ExpectedException(typeof(FaultException<ServiceFault>))]
        public void DeleteQuizFail()
        {
            IQuizManagement service = new QuizManagement();
            Guid guid = Guid.NewGuid();
            service.DeleteQuiz(guid);
        }

        [TestMethod]
        [ExpectedException(typeof(FaultException<ServiceFault>))]
        public void GetQuizException()
        {
            Guid randomGuid = Guid.NewGuid();
            IQuizManagement service = new QuizManagement();
            service.GetQuiz(randomGuid);
        }

        [TestMethod]
        public void GetQuizSuccess()
        {
            IQuizManagement service = new QuizManagement();
            Guid expectedResult = service.GetQuizzes().First().Id;
            Quiz result = service.GetQuiz(expectedResult);
            Assert.AreEqual(expectedResult, result.Id, "Ids are not the same");
        }



    }
}
