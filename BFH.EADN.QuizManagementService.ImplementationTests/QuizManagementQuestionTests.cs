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
using System.Configuration;
using System.IO;
using BFH.EADN.CommonTests.TestHelper;

namespace BFH.EADN.QuizManagementService.Implementation.Tests
{
    [TestClass]
    public class QuizManagementQuestionTests : TestBaseWithDb
    {
        private static IFactoryPersistence _factoryPersistence = Factory.CreateInstance<IFactoryPersistence>();

        [AssemblyInitialize]
        public static void InitLocal(TestContext context)
        {
            Init(context);
        }

        /* Question area */
        [TestMethod]
        public void CreateQuestionSuccess()
        {
            IQuestionManagement service = new QuizManagement(_factoryPersistence);
            ITopicManagement ts = new QuizManagement(_factoryPersistence);
            //create new topic
            Question question = new Question
            {
                Hint = "Test",
                IsMultipleChoice = true,
                Text = "Test",
                Topics = ts.GetTopics().Take(2).ToList()
            };

            service.CreateQuestion(question);
            Assert.IsTrue(true);
        }

        [TestMethod]
        [ExpectedException(typeof(FaultException<ServiceFault>))]
        public void CreateQuestionException()
        {
            IQuestionManagement service = new QuizManagement(_factoryPersistence);
            service.CreateQuestion(null);
        }

        [TestMethod]
        public void GetListAndDeleteQuestionSuccess()
        {
            IQuestionManagement service = new QuizManagement(_factoryPersistence);
            List<Question> results = service.GetQuestions();
            int count = results.Count;
            //if null, set datetime before threshold
            Question deletableQuestion = results.FirstOrDefault(q => q.CanBeDeleted);
            if (deletableQuestion == null)
            {
                //just get first
                deletableQuestion = results.First();
                deletableQuestion.LastUsed = DateTime.Now.AddDays(-2 * Common.Constants.DeletionThreshold);
                _factoryPersistence.CreateQuestionRepository().Update(deletableQuestion);
            }
            service.DeleteQuestion(deletableQuestion.Id);

            Assert.AreEqual(count - 1, service.GetQuestions().Count, "Count of questions not the same");
        }

        [TestMethod]
        [ExpectedException(typeof(FaultException<ServiceFault>))]
        public void DeleteQuestionException()
        {
            IQuestionManagement service = new QuizManagement(_factoryPersistence);
            Guid guid = Guid.NewGuid();
            service.DeleteQuestion(guid);
        }

        [TestMethod]
        [ExpectedException(typeof(FaultException<ServiceFault>))]
        public void GetQuestionException()
        {
            Guid randomGuid = Guid.NewGuid();
            IQuestionManagement service = new QuizManagement(_factoryPersistence);
            service.GetQuestion(randomGuid);
        }

        [TestMethod]
        public void GetQuestionSuccess()
        {
            IQuestionManagement service = new QuizManagement(_factoryPersistence);
            Guid expectedResult = service.GetQuestions().First().Id;
            Question result = service.GetQuestion(expectedResult);
            Assert.AreEqual(expectedResult, result.Id, "Ids are not the same");
        }

        [TestMethod]
        [ExpectedException(typeof(FaultException<ServiceFault>))]
        public void GetQuestionsException()
        {
            //set persistenceFactory to null
            IQuestionManagement service = new QuizManagement(null);
            service.GetQuestions();
        }

        [TestMethod]
        public void GetQuestionsWithoutTopicSuccess()
        {
            IQuestionManagement service = new QuizManagement(_factoryPersistence);
            List<Question> questions = service.GetQuestionsWithoutTopic();

            bool hasTopics = questions.Any(q => q.Topics != null && q.Topics.Count > 0);
            Assert.IsFalse(hasTopics);
        }

        [TestMethod]
        [ExpectedException(typeof(FaultException<ServiceFault>))]
        public void GetQuestionsWithoutTopicException()
        {
            IQuestionManagement service = new QuizManagement(null);
            List<Question> questions = service.GetQuestionsWithoutTopic();
        }


        [TestMethod]
        public void GetQuestionsByIdsSuccess()
        {
            IQuestionManagement service = new QuizManagement(_factoryPersistence);
            List<Guid> guids = service.GetQuestions().Select(q => q.Id).ToList();
            List<Guid> guidsByIds = service.GetQuestionsByIds(guids).Select(q => q.Id).ToList();
            bool listsNotSame = guids.Where(g => !guidsByIds.Any(gBIds => gBIds == g)).Any();
            Assert.IsFalse(listsNotSame);
        }

        [TestMethod]
        [ExpectedException(typeof(FaultException<ServiceFault>))]
        public void GetQuestionsByIdsException()
        {
            IQuestionManagement service = new QuizManagement(null);
            service.GetQuestionsByIds(new List<Guid> { Guid.NewGuid() });
        }

        [TestMethod]
        public void UpdateQuestionSuccess()
        {
            IQuestionManagement service = new QuizManagement(_factoryPersistence);
            Question question = service.GetQuestions().First();

            string newHint = "newHint";
            string newText = "UpdateQuestionSuccess";

            Guid id = question.Id;
            question.Hint = newHint;
            question.Text = newText;

            service.UpdateQuestion(question);

            Question updatedQuuestion = service.GetQuestion(id);

            Assert.AreEqual(newHint, updatedQuuestion.Hint);
            Assert.AreEqual(newText, updatedQuuestion.Text);
        }

        [TestMethod]
        [ExpectedException(typeof(FaultException<ServiceFault>))]
        public void UpdateQuestionException()
        {
            IQuestionManagement service = new QuizManagement(_factoryPersistence);
            Question question = service.GetQuestions().First();
            question.Text = "UpdateQuestionException";
            service = new QuizManagement(null);
            service.UpdateQuestion(question);
        }
    }
}
