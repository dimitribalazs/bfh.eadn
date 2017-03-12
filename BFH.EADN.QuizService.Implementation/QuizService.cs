using BFH.EADN.Common;
using BFH.EADN.QuizService.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;
using BFH.EADN.Common.Types.Contracts;
using BFH.EADN.Common.Types;
using BFH.EADN.Common.Types.Enums;

namespace BFH.EADN.QuizService.Implementation
{
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.PerCall, Namespace = Constants.XMLNamespace, Name = "QuizService")]
    public class QuizService : IPlay
    {
        private static IFactoryPersistence _persistenceFactory;
        private IRepository<Question, Guid> QuestionRepository => _persistenceFactory.CreateQuestionRepository();
        private IRepository<Quiz, Guid> QuizRepository => _persistenceFactory.CreateQuizRepository();

        static QuizService()
        {
            if (_persistenceFactory == null)
            {
                _persistenceFactory = Factory.CreateInstance<IFactoryPersistence>();
            }
        }

        public Quiz GetQuiz(Guid id)
        {
            using (IRepository<Quiz, Guid> repo = QuizRepository)
            {
                Quiz quiz = repo.Get(id);
                //fix always same questions and same order
                if (quiz.Type == QuizType.Fix)
                {
                    List<Question> questions = quiz.Questions.OrderBy(q => q.Id).Take(quiz.MaxQuestionCount).ToList();
                    quiz.Questions = questions;
                    return quiz;
                }
                else if (quiz.Type == QuizType.Variable)
                {
                    List<Question> questions = quiz.Questions.Take(quiz.MaxQuestionCount).ToList();
                    Random random = new Random();
                    List<Question> randomizedList = new List<Question>(quiz.MaxQuestionCount);

                    while (questions.Count > 0)
                    {
                        int next = random.Next(quiz.MaxQuestionCount);
                        Question question = questions[next];
                        randomizedList.Add(question);
                        questions.Remove(question);
                    }

                    quiz.Questions = randomizedList;
                    return quiz;
                }
                else
                {
                    List<Question> questions = quiz.Questions.ToList();
                    List<Question> randomizedList = new List<Question>(quiz.MaxQuestionCount);

                    Random random = new Random();
                    while (randomizedList.Count < quiz.MaxQuestionCount && questions.Any())
                    {
                        int next = random.Next(questions.Count);
                        Question question = questions[next];
                        randomizedList.Add(question);
                        questions.Remove(question);
                    }

                    quiz.Questions = randomizedList;
                    return quiz;
                }
            }
        }

        public List<Quiz> GetQuizzes()
        {
            using (IRepository<Quiz, Guid> repo = QuizRepository)
            {
                return repo.GetAll();
            }
        }

        
        public PlayQuestion GetFirstQuestion(Guid quizId)
        {
            using (IRepository<Quiz, Guid> repo = QuizRepository)
            {
                List<Question> questions = repo.Get(quizId).Questions.OrderBy(q => q.Id).ToList();
                return GetQuestion(quizId, questions.First().Id);
            }
        }

        public PlayQuestion GetQuestion(Guid quizId, Guid questionId)
        {
            List<Question> questions;
            using (IRepository<Quiz, Guid> repo = QuizRepository)
            {
                 questions = repo.Get(quizId).Questions.OrderBy(q => q.Id).ToList();
            }

            Question currentQuestion = null;
            Guid? previousQuestion = null;
            Guid? nextQuestion = null;
            for (int i = 0; i < questions.Count; i++)
            {
                if (questions[i].Id == questionId)
                {
                    if (i <= questions.Count - 1)
                    {
                        currentQuestion = questions[i];
                    }

                    if (i > 0)
                    {
                        previousQuestion = questions[i - 1].Id;
                    }

                    if (i + 1 <= questions.Count - 1)
                    {
                        nextQuestion = questions[i + 1].Id;
                    }
                }
            }

            PlayQuestion playQuestion = new PlayQuestion
            {
                Id = currentQuestion.Id,
                Answers = currentQuestion.Answers,
                Hint = currentQuestion.Hint,
                IsMultipleChoice = currentQuestion.IsMultipleChoice,
                Text = currentQuestion.Text,
                NextQuestion = nextQuestion,
                PreviousQuestion = previousQuestion
            };
            return playQuestion;
        }

        public bool CheckAnswers(Guid questionId, List<Guid> answers)
        {
            using (IRepository<Question, Guid> repo = QuestionRepository)
            { 
                Question question = repo.Get(questionId);
                List<Guid> solutionsAnswers = question.Answers.Where(a => a.IsSolution).Select(a => a.Id).ToList();
                return solutionsAnswers.Aggregate(true, (acc, solutionId) => acc & answers.Contains(solutionId));
            }
        }
    }
}
