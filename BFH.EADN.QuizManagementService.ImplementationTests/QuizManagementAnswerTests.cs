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
    public class QuizManagementAnswerTests : TestBaseWithDb
    {
        private static IFactoryPersistence _factoryPersistence = Factory.CreateInstance<IFactoryPersistence>();

        /* Answer area */
        [TestMethod]
        public void CreateAnswerSuccess()
        {
            IAnswerManagement service = new QuizManagement(_factoryPersistence);
            IQuestionManagement qs = new QuizManagement(_factoryPersistence);
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
        public void CreateAnswerException()
        {
            IAnswerManagement service = new QuizManagement(_factoryPersistence);
            service.CreateAnswer(null);
        }

        [TestMethod]
        public void UpdateAnswerSuccess()
        {
            IAnswerManagement service = new QuizManagement(_factoryPersistence);
            Answer answer = service.GetAnswers().First();

            bool newIsSolution = !answer.IsSolution;
            string newText = "UpdateText";
            Guid id = answer.Id;
            answer.IsSolution = !answer.IsSolution;
            answer.Text = newText;
            service.UpdateAnswer(answer);

            Answer updatedAnswer = service.GetAnswer(id);

            Assert.AreEqual(newIsSolution, updatedAnswer.IsSolution);
            Assert.AreEqual(newText, updatedAnswer.Text);
        }

        [TestMethod]
        [ExpectedException(typeof(FaultException<ServiceFault>))]
        public void UpdateAnswerException()
        {
            IAnswerManagement service = new QuizManagement(_factoryPersistence);
            Answer answer = service.GetAnswers().First();
            answer.Text = "Foobar";
            service = new QuizManagement(null);
            service.UpdateAnswer(answer);
        }

        [TestMethod]
        public void GetListAndDeleteAnswerSuccess()
        {
            IAnswerManagement service = new QuizManagement(_factoryPersistence);
            List<Answer> answers = service.GetAnswers();
            Assert.IsNotNull(answers);

            int answerCount = answers.Count;

            Guid toDelete = answers.Last().Id;
            service.DeleteAnswer(toDelete);

            Assert.AreEqual(answerCount - 1, service.GetAnswers().Count, "Count of answers not the same");
        }

        [TestMethod]
        [ExpectedException(typeof(FaultException<ServiceFault>))]
        public void DeleteAnswerExcpetion()
        {
            Guid randomGuid = Guid.NewGuid();
            IAnswerManagement service = new QuizManagement(_factoryPersistence);
            service.DeleteAnswer(randomGuid);
        }

        [TestMethod]
        [ExpectedException(typeof(FaultException<ServiceFault>))]
        public void GetAnswerException()
        {
            Guid randomGuid = Guid.NewGuid();
            IAnswerManagement service = new QuizManagement(_factoryPersistence);
            service.GetAnswer(randomGuid);
        }

        [TestMethod]
        public void GetAnswerSuccess()
        {
            IAnswerManagement service = new QuizManagement(_factoryPersistence);
            Guid expectedResult = service.GetAnswers().First().Id;
            Answer answer = service.GetAnswer(expectedResult);
            Assert.AreEqual(expectedResult, answer.Id, "Ids are not the same");
        }

        [TestMethod]
        [ExpectedException(typeof(FaultException<ServiceFault>))]
        public void GetAnswersException()
        {
            //set persistenceFactory to null
            IAnswerManagement service = new QuizManagement(null);
            List<Answer> answers = service.GetAnswers();
        }
    }
}
