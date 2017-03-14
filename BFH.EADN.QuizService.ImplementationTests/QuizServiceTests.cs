using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using BFH.EADN.QuizService.Contracts;
using BFH.EADN.Common.Types;
using BFH.EADN.Common;
using BFH.EADN.Common.Types.Contracts;
using System.ServiceModel;

namespace BFH.EADN.QuizService.Implementation.Tests
{
    [TestClass]
    public class QuizServiceTests
    {
        private static IFactoryPersistence _factoryPersistence = Factory.CreateInstance<IFactoryPersistence>();
         
        [TestMethod]
        public void GetQuiz()
        {
            IFactoryPersistence _persistenceFactory = Factory.CreateInstance<IFactoryPersistence>();
            IRepository<Quiz, Guid> repo = _persistenceFactory.CreateQuizRepository();

            List<Quiz> quizzes = repo.GetAll();

            IPlay service = new QuizService();

            Quiz fix = quizzes.Where(q => q.Type == Common.Types.Enums.QuizType.Fix).FirstOrDefault();
            if (fix != null)
            {
                Quiz fixQuiz1 = service.GetQuiz(fix.Id);
                Quiz fixQuiz2 = service.GetQuiz(fix.Id);
                bool listAreEqual = Enumerable.SequenceEqual(
                    fixQuiz1.Questions.Select(q => q.Id).ToList(),
                    fixQuiz2.Questions.Select(q => q.Id).ToList()
                    );
                Assert.IsTrue(listAreEqual, "List are not equal for fix quiz");
            }

            Quiz dynamic = quizzes.Where(q => q.Type == Common.Types.Enums.QuizType.Dynamic).FirstOrDefault();
            if (dynamic != null)
            {
                Quiz dynamicQuiz1 = service.GetQuiz(dynamic.Id);
                Quiz dynamicQuiz2 = service.GetQuiz(dynamic.Id);
                bool listAreEqual = Enumerable.SequenceEqual(
                    dynamicQuiz1.Questions.Select(q => q.Id).ToList(),
                    dynamicQuiz2.Questions.Select(q => q.Id).ToList()
                    );
                Assert.IsFalse(listAreEqual, "List are equal for dynamic quiz");
            }

            Quiz variable = quizzes.Where(q => q.Type == Common.Types.Enums.QuizType.Variable).FirstOrDefault();
            if (variable != null)
            {
                Quiz variableQuiz1 = service.GetQuiz(dynamic.Id);
                Quiz variableQuiz2 = service.GetQuiz(dynamic.Id);
                bool listAreEqual = Enumerable.SequenceEqual(
                    variableQuiz1.Questions.Select(q => q.Id).ToList(),
                    variableQuiz2.Questions.Select(q => q.Id).ToList()
                    );
                Assert.IsFalse(listAreEqual, "List are equal for variable quiz");
            }
        }

        [TestMethod]
        public void GetQuizzesSuccess()
        {
            IPlay service = new QuizService(_factoryPersistence);
            List<Quiz> quizzes = service.GetQuizzes();
            Assert.IsNotNull(quizzes);
        }

        [TestMethod]
        [ExpectedException(typeof(FaultException<ServiceFault>))]
        public void GetQuizzesSuccessException()
        {
            IPlay service = new QuizService(null);
            service.GetQuizzes();
        }

        /* Quiz area */
        [TestMethod]
        public void GetQuizSuccess()
        {
            IPlay service = new QuizService(_factoryPersistence);
            Quiz quiz = service.GetQuizzes().Last();
            Quiz quizById = service.GetQuiz(quiz.Id);

            Assert.IsNotNull(quizById);
            Assert.AreEqual(quiz.Id, quizById.Id);
        }

        [TestMethod]
        [ExpectedException(typeof(FaultException<ServiceFault>))]
        public void GetQuizSuccessException()
        {
            IPlay service = new QuizService(_factoryPersistence);
            Quiz quiz = service.GetQuizzes().Last();
            service = new QuizService(null);
            Quiz quizById = service.GetQuiz(quiz.Id);
        }

        [TestMethod]
        public void GetFirstQuestionSuccess()
        {
            IPlay service = new QuizService(_factoryPersistence);
            Quiz quiz = service.GetQuizzes().Last();
            List<Question> questions = quiz.Questions.OrderBy(q => q.Id).ToList();
            Guid firstQuestionId = questions.First().Id;

            PlayQuestion pq = service.GetFirstQuestion(quiz.Id);

            Assert.IsNotNull(pq);
            Assert.AreEqual(firstQuestionId, pq.Id);
        }

        [TestMethod]
        [ExpectedException(typeof(FaultException<ServiceFault>))]
        public void GetFirstQuestionException()
        {
            IPlay service = new QuizService(_factoryPersistence);
            Quiz quiz = service.GetQuizzes().Last();
            service = new QuizService(null);
            service.GetFirstQuestion(quiz.Id);
        }

        [TestMethod]
        public void GetQuestionSuccess()
        {
            IPlay service = new QuizService(_factoryPersistence);
            Quiz quiz = service.GetQuizzes().Last();
            Question question = quiz.Questions.Last();

            PlayQuestion pq = service.GetQuestion(quiz.Id, question.Id);
            
            Assert.IsNotNull(pq);
            Assert.AreEqual(question.Id, pq.Id);
        }

        [TestMethod]
        [ExpectedException(typeof(FaultException<ServiceFault>))]
        public void GetQuestionException()
        {
            IPlay service = new QuizService(_factoryPersistence);
            Quiz quiz = service.GetQuizzes().Last();
            Question question = quiz.Questions.Last();

            service = new QuizService(null);
            service.GetQuestion(quiz.Id, question.Id);
        }

        [TestMethod]
        public void CheckAnswersSuccess()
        {
            IPlay service = new QuizService(_factoryPersistence);
            Quiz quiz = service.GetQuizzes().Where(q => q.Questions != null && q.Questions.Count > 0).First();
            Question question = quiz.Questions.First();

            List<Guid> allAnswers = question.Answers.Select(a => a.Id).ToList();
            List<Guid> correctAnswers = question.Answers.Where(a => a.IsSolution).Select(a => a.Id).ToList();

            bool notCorrect = service.CheckAnswers(question.Id, allAnswers);
            bool allCorrect = service.CheckAnswers(question.Id, correctAnswers);

            Assert.IsFalse(notCorrect);
            Assert.IsTrue(allCorrect);
        }

        [TestMethod]
        [ExpectedException(typeof(FaultException<ServiceFault>))]
        public void CheckAnswersException()
        {
            IPlay service = new QuizService(_factoryPersistence);
            Quiz quiz = service.GetQuizzes().Where(q => q.Questions != null && q.Questions.Count > 0).First();
            Question question = quiz.Questions.First();

            List<Guid> allAnswers = question.Answers.Select(a => a.Id).ToList();
            service = new QuizService(null);
            service.CheckAnswers(question.Id, allAnswers);
        }



            //[TestMethod]
            //public void CreateAnswerSuccess()
            //{
            //    IAnswerManagement service = new QuizManagement(_factoryPersistence);
            //    IQuestionManagement qs = new QuizManagement(_factoryPersistence);
            //    Guid questionId = qs.GetQuestions().First().Id;

            //    //create new answer
            //    Answer answer = new Answer
            //    {
            //        IsSolution = true,
            //        Text = "test answer",
            //        QuestionId = questionId
            //    };

            //    service.CreateAnswer(answer);
            //    Assert.IsTrue(true);
            //}

            //[TestMethod]
            //[ExpectedException(typeof(FaultException<ServiceFault>))]
            //public void CreateAnswerException()
            //{
            //    IAnswerManagement service = new QuizManagement(_factoryPersistence);
            //    service.CreateAnswer(null);
            //}
        }
}