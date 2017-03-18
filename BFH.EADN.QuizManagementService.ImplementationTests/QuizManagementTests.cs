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
    public class QuizManagementTests : TestBaseWithDb
    {
        private static IFactoryPersistence _factoryPersistence = Factory.CreateInstance<IFactoryPersistence>();

        [ClassInitialize]
        public static void InitLocal(TestContext context)
        {
            Init(context);
        }
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

        /* Topic area */
        [TestMethod]
        public void CreateTopicSuccess()
        {
            ITopicManagement service = new QuizManagement(_factoryPersistence);
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
        public void CreateTopicException()
        {
            ITopicManagement service = new QuizManagement(_factoryPersistence);
            service.CreateTopic(null);
        }

        [TestMethod]
        public void GetListAndDeleteTopicSuccess()
        {
            ITopicManagement service = new QuizManagement(_factoryPersistence);
            List<Topic> results = service.GetTopics();
            int count = results.Count;

            Guid toDelete = results.Last().Id;
            service.DeleteTopic(toDelete);

            Assert.AreEqual(count - 1, service.GetTopics().Count, "Count of topic not the same");
        }

        [TestMethod]
        [ExpectedException(typeof(FaultException<ServiceFault>))]
        public void DeleteTopicException()
        {
            ITopicManagement service = new QuizManagement(_factoryPersistence);
            Guid guid = Guid.NewGuid();
            service.DeleteTopic(guid);
        }

        [TestMethod]
        [ExpectedException(typeof(FaultException<ServiceFault>))]
        public void GetTopicException()
        {
            Guid randomGuid = Guid.NewGuid();
            ITopicManagement service = new QuizManagement(_factoryPersistence);
            service.GetTopic(randomGuid);
        }

        [TestMethod]
        public void GetTopicSuccess()
        {
            ITopicManagement service = new QuizManagement(_factoryPersistence);
            Guid expectedResult = service.GetTopics().First().Id;
            Topic result = service.GetTopic(expectedResult);
            Assert.AreEqual(expectedResult, result.Id, "Ids are not the same");
        }

        [TestMethod]
        [ExpectedException(typeof(FaultException<ServiceFault>))]
        public void GetTopicsException()
        {
            //set persistenceFactory to null
            ITopicManagement service = new QuizManagement(null);
            service.GetTopics();
        }

        [TestMethod]
        public void UpdateTopicSuccess()
        {
            ITopicManagement service = new QuizManagement(_factoryPersistence);
            Topic topic = service.GetTopics().First();

            string newName = "UpdateTopicSuccess";

            Guid id = topic.Id;
            topic.Name = newName;

            service.UpdateTopic(topic);

            Topic updatedTopic = service.GetTopic(id);

            Assert.AreEqual(newName, updatedTopic.Name);
        }

        [TestMethod]
        [ExpectedException(typeof(FaultException<ServiceFault>))]
        public void UpdateTopicException()
        {
            ITopicManagement service = new QuizManagement(_factoryPersistence);
            Topic topic = service.GetTopics().First();
            topic.Name = "UpdateTopicException";
            service = new QuizManagement(null);
            service.UpdateTopic(topic);
        }

        [TestMethod]
        public void GetTopicsByIdsSuccess()
        {
            ITopicManagement service = new QuizManagement(_factoryPersistence);
            List<Guid> guids = service.GetTopics().Select(q => q.Id).ToList();
            List<Guid> guidsByIds = service.GetTopicsByIds(guids).Select(q => q.Id).ToList();
            bool listsNotSame = guids.Where(g => !guidsByIds.Any(gBIds => gBIds == g)).Any();
            Assert.IsFalse(listsNotSame);
        }

        [TestMethod]
        [ExpectedException(typeof(FaultException<ServiceFault>))]
        public void GetTopicsByIdsException()
        {
            ITopicManagement service = new QuizManagement(null);
            service.GetTopicsByIds(new List<Guid> { Guid.NewGuid() });
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
            Guid toDelete = results.First(q => q.CanBeDeleted).Id;
            service.DeleteQuestion(toDelete);

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
