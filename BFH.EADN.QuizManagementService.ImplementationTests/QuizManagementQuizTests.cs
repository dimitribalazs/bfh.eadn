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
    public class QuizManagementQuizTests : TestBaseWithDb
    {
        private static IFactoryPersistence _factoryPersistence = Factory.CreateInstance<IFactoryPersistence>();
        
        /* Quiz area */
        [TestMethod]
        public void CreateQuizSuccess()
        {
            IQuizManagement service = new QuizManagement(_factoryPersistence);

            //create new topic
            Quiz quiz = new Quiz
            {
                MaxQuestionCount = 22,
                MinQuestionCount = 2,
                Text = "Text",
                Type = Common.Types.Enums.QuizType.Dynamic
            };

            service.CreateQuiz(quiz);
            Assert.IsTrue(true);
        }

        [TestMethod]
        [ExpectedException(typeof(FaultException<ServiceFault>))]
        public void CreateQuizException()
        {
            IQuizManagement service = new QuizManagement(_factoryPersistence);
            service.CreateQuiz(null);
        }
        [TestMethod]
        public void GetListAndDeleteQuizSuccess()
        {
            IQuizManagement service = new QuizManagement(_factoryPersistence);
            List<Quiz> results = service.GetQuizzes();
            int count = results.Count;

            Guid toDelete = results.Last(q => q.CanBeDeleted).Id;
            service.DeleteQuiz(toDelete);

            Assert.AreEqual(count - 1, service.GetQuizzes().Count, "Count of quizzes not the same");
        }

        [TestMethod]
        [ExpectedException(typeof(FaultException<ServiceFault>))]
        public void DeleteQuizException()
        {
            IQuizManagement service = new QuizManagement(_factoryPersistence);
            Guid guid = Guid.NewGuid();
            service.DeleteQuiz(guid);
        }

        [TestMethod]
        [ExpectedException(typeof(FaultException<ServiceFault>))]
        public void GetQuizException()
        {
            Guid randomGuid = Guid.NewGuid();
            IQuizManagement service = new QuizManagement(_factoryPersistence);
            service.GetQuiz(randomGuid);
        }

        [TestMethod]
        public void GetQuizSuccess()
        {
            IQuizManagement service = new QuizManagement(_factoryPersistence);
            Guid expectedResult = service.GetQuizzes().First().Id;
            Quiz result = service.GetQuiz(expectedResult);
            Assert.AreEqual(expectedResult, result.Id, "Ids are not the same");
        }

        [TestMethod]
        [ExpectedException(typeof(FaultException<ServiceFault>))]
        public void GetQuizzesException()
        {
            //set persistenceFactory to null
            IQuizManagement service = new QuizManagement(null);
            service.GetQuizzes();
        }

        [TestMethod]
        public void UpdateQuizSuccess()
        {
            IQuizManagement service = new QuizManagement(_factoryPersistence);
            Quiz quiz = service.GetQuizzes().First();

            int newMaxQuestionCount = 22;
            string newText = "UpdatedQuizSuccess";

            Guid id = quiz.Id;
            quiz.MaxQuestionCount = newMaxQuestionCount;
            quiz.Text = newText;

            service.UpdateQuiz(quiz);

            Quiz updatedQuiz = service.GetQuiz(id);

            Assert.AreEqual(newMaxQuestionCount, updatedQuiz.MaxQuestionCount);
            Assert.AreEqual(newText, updatedQuiz.Text);
        }

        [TestMethod]
        [ExpectedException(typeof(FaultException<ServiceFault>))]
        public void UpdateQuizException()
        {
            IQuizManagement service = new QuizManagement(_factoryPersistence);
            Quiz quiz = service.GetQuizzes().First();
            quiz.Text = "UpdatedQuizException";
            service = new QuizManagement(null);
            service.UpdateQuiz(quiz);
        }

        [TestMethod]
        public void GetQuizzesByIdsSuccess()
        {
            IQuizManagement service = new QuizManagement(_factoryPersistence);
            List<Guid> guids = service.GetQuizzes().Select(q => q.Id).ToList();
            List<Guid> guidsByIds = service.GetQuizzesByIds(guids).Select(q => q.Id).ToList();
            bool listsNotSame = guids.Where(g => !guidsByIds.Any(gBIds => gBIds == g)).Any();
            Assert.IsFalse(listsNotSame);
        }

        [TestMethod]
        [ExpectedException(typeof(FaultException<ServiceFault>))]
        public void GetQuizzesByIdsException()
        {
            IQuizManagement service = new QuizManagement(null);
            service.GetQuizzesByIds(new List<Guid> { Guid.NewGuid() });
        }
    }
}
