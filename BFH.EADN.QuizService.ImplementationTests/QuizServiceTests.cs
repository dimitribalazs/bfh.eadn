using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using BFH.EADN.QuizService.Contracts;
using BFH.EADN.Common.Types;
using BFH.EADN.Common;
using BFH.EADN.Common.Types.Contracts;
using System.ServiceModel;
using BFH.EADN.CommonTests.TestHelper;
using System.IO;

namespace BFH.EADN.QuizService.Implementation.Tests
{
    [TestClass]
    public class QuizServiceTests : TestBaseWithDb
    {
        private static IFactoryPersistence _factoryPersistence = Factory.CreateInstance<IFactoryPersistence>();


        [AssemblyInitialize]
        public static void InitLocal(TestContext context)
        {
            Init(context);
        }

        [TestMethod]
        public void GetQuiz()
        {
            IPlay service = new QuizService(_factoryPersistence);
            List<Quiz> quizzes = service.GetQuizzes();

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
                //Assert.IsFalse(listAreEqual, "List are equal for dynamic quiz");
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
                //Assert.IsFalse(listAreEqual, "List are equal for variable quiz");
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
        public void GetQuizzesException()
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
        public void GetQuizException()
        {
            IPlay service = new QuizService(_factoryPersistence);
            Quiz quiz = service.GetQuizzes().Last();
            service = new QuizService(null);
            Quiz quizById = service.GetQuiz(quiz.Id);
        }
        
        [TestMethod]
        public void GetQuestionSuccess()
        {
            IPlay service = new QuizService(_factoryPersistence);
            Quiz quiz = service.GetQuizzes().Last(q => q.Questions.Count > 0);
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
            Quiz quiz = service.GetQuizzes().Last(q => q.Questions.Count > 0);
            Question question = quiz.Questions.Last();

            service = new QuizService(null);
            service.GetQuestion(quiz.Id, question.Id);
        }

        [TestMethod]
        public void CheckAnswersSuccess()
        {
            IPlay service = new QuizService(_factoryPersistence);
            Quiz quiz = service.GetQuizzes().First(q => q.Questions.Count > 0);

            Question question = null;
            //find a question in which not all answers are right
            foreach (Question q in quiz.Questions)
            {
                if (q.Answers.Any(a => a.IsSolution == false))
                {
                    question = q;
                    break;
                }
            }

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
            Quiz quiz = service.GetQuizzes().Last(q => q.Questions.Count > 0);
            Question question = quiz.Questions.First();

            List<Guid> allAnswers = question.Answers.Select(a => a.Id).ToList();
            service = new QuizService(null);
            service.CheckAnswers(question.Id, allAnswers);
        }

        [TestMethod]
        public void UpdateAndGetAllSavedQuestionAnswerStateSuccess()
        {
            Tuple<Guid, Guid, List<Guid>> data = CreateQuestionAnswerState();
            Guid questionAnswerStateId = data.Item1;
            Guid questionId = data.Item2;
            List<Guid> firstAnswers = data.Item3;

            IPlay service = new QuizService(_factoryPersistence);

            List<QuestionAnswerState> qasList = service.GetAllSavedQuestionAnswerStates(questionAnswerStateId);

            foreach(QuestionAnswerState qas in qasList)
            {
                Assert.AreEqual(questionAnswerStateId, qas.QuestionAnswerStateId);
                Assert.AreEqual(questionId, qas.Question.Id);
                bool answersInDb = firstAnswers.Intersect(qas.Answers.Select(a => a.Id)).ToList().Count == firstAnswers.Count;
                Assert.IsTrue(answersInDb);
            }

            //check updated data
            service.UpdateQuestionAnswerState(questionAnswerStateId, questionId, null);

            qasList = service.GetAllSavedQuestionAnswerStates(questionAnswerStateId);

            foreach (QuestionAnswerState qas in qasList)
            {
                Assert.AreEqual(questionAnswerStateId, qas.QuestionAnswerStateId);
                Assert.AreEqual(questionId, qas.Question.Id);
                bool answersInDb = qas.Answers.Count == 0;
                Assert.IsTrue(answersInDb);
            }
        }

        [TestMethod]
        [ExpectedException(typeof(FaultException<ServiceFault>))]
        public void UpdateQuestionAnswerStateException()
        {
            IPlay service = new QuizService(null);
            service.GetAllSavedQuestionAnswerStates(Guid.NewGuid());
        }

        [TestMethod]
        public void DeleteQuestionAnswerStateSuccess()
        {
            Tuple<Guid, Guid, List<Guid>> data = CreateQuestionAnswerState();
            Guid questionAnswerStateId = data.Item1;
            Guid questionId = data.Item2;
            List<Guid> firstAnswers = data.Item3;

            IPlay service = new QuizService(_factoryPersistence);
            service.DeleteQuestionAnswerState(questionAnswerStateId);

            bool deletionSuccess = service.GetAllSavedQuestionAnswerStates(questionAnswerStateId).Count == 0;
            Assert.IsTrue(deletionSuccess);
        }

        [TestMethod]
        [ExpectedException(typeof(FaultException<ServiceFault>))]
        public void DeleteQuestionAnswerStateException()
        {
            IPlay service = new QuizService(null);
            service.GetAllSavedQuestionAnswerStates(Guid.NewGuid());
        }

        [TestMethod]
        public void GetAllSavedQuestionAnswerStatesSuccess()
        {
            Tuple<Guid, Guid, List<Guid>> data = CreateQuestionAnswerState();
            Guid questionAnswerStateId = data.Item1;
            Guid questionId = data.Item2;
            List<Guid> firstAnswers = data.Item3;

            IPlay service = new QuizService(_factoryPersistence);
            bool entryCountIsSame = service.GetAllSavedQuestionAnswerStates(questionAnswerStateId).First().Answers.Count == firstAnswers.Count;
            Assert.IsTrue(entryCountIsSame);
        }

        [TestMethod]
        [ExpectedException(typeof(FaultException<ServiceFault>))]
        public void GetAllSavedQuestionAnswerStatesException()
        {
            IPlay service = new QuizService(null);
            service.GetAllSavedQuestionAnswerStates(Guid.NewGuid());
        }

        /// <summary>
        /// Creates a new QuestionAnswerState entry
        /// </summary>
        /// <returns>Tuple with questionAnswerStateId, questionId, List of Answer Ids</returns>
        private Tuple<Guid, Guid, List<Guid>> CreateQuestionAnswerState()
        {
            IPlay service = new QuizService(_factoryPersistence);
            Guid questionAnswerStateId = Guid.NewGuid();

            Quiz quiz = service.GetQuizzes().First(q => q.Questions.Count > 0);

            Question question = null;
            //find a question in which not all answers are right
            foreach (Question q in quiz.Questions)
            {
                if (q.Answers.Count > 1)
                {
                    question = q;
                    break;
                }
            }

            Guid questionId = question.Id;
            List<Guid> firstAnswers = question.Answers.Select(a => a.Id).ToList();


            //creates new entry
            service.UpdateQuestionAnswerState(questionAnswerStateId, questionId, firstAnswers);

            return new Tuple<Guid, Guid, List<Guid>>(questionAnswerStateId, questionId, firstAnswers);
        }
    }
}