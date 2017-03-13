using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using BFH.EADN.QuizService.Contracts;
using BFH.EADN.Common.Types;
using BFH.EADN.Common;
using BFH.EADN.Common.Types.Contracts;

namespace BFH.EADN.QuizService.Implementation.Tests
{
    [TestClass()]
    public class QuizServiceTests
    {
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
    }
}