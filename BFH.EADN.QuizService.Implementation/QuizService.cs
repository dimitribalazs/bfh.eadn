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
        /// <summary>
        /// Holds the persistence factory during the call
        /// </summary>
        private IFactoryPersistence _persistenceFactory;

        /// <summary>
        /// Get Question repository
        /// </summary>
        private IRepository<Question, Guid> QuestionRepository => _persistenceFactory.CreateQuestionRepository();

        /// <summary>
        /// Get Quiz repository
        /// </summary>
        private IRepository<Quiz, Guid> QuizRepository => _persistenceFactory.CreateQuizRepository();

        /// <summary>
        /// Get QuestionAnswerState repository
        /// </summary>
        private IRepository<QuestionAnswerState, Guid> QuestionAnswerStateRepository => _persistenceFactory.CreateQuestionAnswerStateRepository();

        /// <summary>
        /// Get Answer repository
        /// </summary>
        private IRepository<Answer, Guid> AnswerRepository => _persistenceFactory.CreateAnswerRepository();

        /// <summary>
        /// Default constructor which creates a new persistence factory
        /// </summary>
        public QuizService()
        {
            _persistenceFactory = Factory.CreateInstance<IFactoryPersistence>();
        }

        /// <summary>
        /// Constructor to pass in persistence factory
        /// </summary>
        /// <param name="persistenceFactory">a persistence factory</param>
        public QuizService(IFactoryPersistence persistenceFactory)
        {
            _persistenceFactory = persistenceFactory;
        }

        ///<inheritdoc />
        public Quiz GetQuiz(Guid id)
        {
            try
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
            catch (Exception ex)
            {
                ServiceFault fault = Common.Common.CreateServiceFault(ex, "Error while getting quiz");
                throw new FaultException<ServiceFault>(fault);
            }
        }

        ///<inheritdoc />
        public List<Quiz> GetQuizzes()
        {
            try
            {
                using (IRepository<Quiz, Guid> repo = QuizRepository)
                {
                    List<Quiz> quizzes = repo.GetAll();
                    return quizzes;
                }
            }
            catch (Exception ex)
            {
                ServiceFault fault = Common.Common.CreateServiceFault(ex, "Error while getting quizzes");
                throw new FaultException<ServiceFault>(fault);
            }
        }

        ///<inheritdoc />
        public PlayQuestion GetFirstQuestion(Guid quizId)
        {
            try
            {
                using (IRepository<Quiz, Guid> repo = QuizRepository)
                {
                    List<Question> questions = repo.Get(quizId).Questions.OrderBy(q => q.Id).ToList();
                    return GetQuestion(quizId, questions.First().Id);
                }
            }
            catch (Exception ex)
            {
                ServiceFault fault = Common.Common.CreateServiceFault(ex, "Error while getting first question");
                throw new FaultException<ServiceFault>(fault);
            }
        }

        ///<inheritdoc />
        public PlayQuestion GetQuestion(Guid quizId, Guid questionId)
        {
            try
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
            catch (Exception ex)
            {
                ServiceFault fault = Common.Common.CreateServiceFault(ex, "Error while getting question");
                throw new FaultException<ServiceFault>(fault);
            }
        }

        ///<inheritdoc />
        public bool CheckAnswers(Guid questionId, List<Guid> answers)
        {
            try
            {
                using (IRepository<Question, Guid> repo = QuestionRepository)
                {
                    Question question = repo.Get(questionId);
                    List<Guid> solutionsAnswers = question.Answers.Where(a => a.IsSolution).Select(a => a.Id).ToList();
                    return answers.Aggregate(true, (acc, answerId) => acc & solutionsAnswers.Contains(answerId));
                    //solutionsAnswers.Aggregate(true, (acc, solutionId) => acc & answers.Contains(solutionId));
                }
            }
            catch (Exception ex)
            {
                ServiceFault fault = Common.Common.CreateServiceFault(ex, "Error while checking answers");
                throw new FaultException<ServiceFault>(fault);
            }
        }

        ///<inheritdoc />
        public void UpdateQuestionAnswerState(Guid questionAnswerStateId, Guid questionId, List<Guid> answers)
        {
            try
            {
                //Guid questionAnswerStateId, Guid questionId, List<Guid> answers
                using (IRepository<QuestionAnswerState, Guid> repo = QuestionAnswerStateRepository)
                using (IRepository<Answer, Guid> answerRepo = AnswerRepository)
                using (IRepository<Question, Guid> questionRepo = QuestionRepository)
                {
                    QuestionAnswerState qas = new QuestionAnswerState();
                    qas.QuestionAnswerStateId = questionAnswerStateId;
                    qas.Question = questionRepo.Get(questionId);
                    qas.Answers = answerRepo.GetListByIds(answers);
                    repo.Update(qas);
                }
            }
            catch (Exception ex)
            {
                ServiceFault fault = Common.Common.CreateServiceFault(ex, "Error while updating QuestionAnswerState");
                throw new FaultException<ServiceFault>(fault);
            }
        }

        ///<inheritdoc />
        public void DeleteQuestionAnswerState(Guid questionAnswerStateId)
        {
            try
            {
                using (IRepository<QuestionAnswerState, Guid> repo = QuestionAnswerStateRepository)
                {
                    repo.Delete(questionAnswerStateId);
                }
            }
            catch (Exception ex)
            {
                ServiceFault fault = Common.Common.CreateServiceFault(ex, "Error while deleting QuestionAnswerState");
                throw new FaultException<ServiceFault>(fault);
            }
        }

        ///<inheritdoc />
        public List<QuestionAnswerState> GetAllSavedQuestionAnswerStates(Guid questionAnswerStateId)
        {
            try
            {

                using (IRepository<QuestionAnswerState, Guid> repo = QuestionAnswerStateRepository)
                {
                    return repo.GetAll().Where(q => q.QuestionAnswerStateId == questionAnswerStateId).ToList();
                }
            }
            catch (Exception ex)
            {
                ServiceFault fault = Common.Common.CreateServiceFault(ex, "Error while getting all saved QuestionAnswerState");
                throw new FaultException<ServiceFault>(fault);
            }
        }
    }
}
