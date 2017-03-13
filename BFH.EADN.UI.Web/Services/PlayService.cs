using BFH.EADN.QuizService.Contracts;
using BFH.EADN.UI.Web.Models.Play;
using BFH.EADN.UI.Web.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ContractTypes = BFH.EADN.Common.Types.Contracts;

namespace BFH.EADN.UI.Web.Services
{
    public class PlayService : BaseService
    {
        public List<Overview> GetOverview()
        {
            List<ContractTypes.Quiz> quizzes = ClientProxy.GetQuizProxy<IPlay>().GetQuizzes();
            Dictionary<string, Overview> topicQuizzes = new Dictionary<string, Overview>();
            foreach (ContractTypes.Quiz quiz in quizzes)
            {
                foreach (ContractTypes.Question question in quiz.Questions)
                {
                    foreach (ContractTypes.Topic topic in question.Topics)
                    {
                        if (topicQuizzes.ContainsKey(topic.Name) == false)
                        {
                            topicQuizzes.Add(topic.Name, new Overview());
                        }

                        topicQuizzes[topic.Name].TopicName = topic.Name;
                        if (topicQuizzes[topic.Name].QuizItems.Any(qi => qi.QuizId == quiz.Id) == false)
                        {
                            QuizItem item = new QuizItem
                            {
                                QuizId = quiz.Id,
                                Text = quiz.Text
                            };
                            topicQuizzes[topic.Name].QuizItems.Add(item);
                        }
                    }
                }
            }

            return topicQuizzes.Values.ToList();
        }

        public void Validation(ModelStateDictionary state, Guid questionId, List<Guid> answers)
        {
            if (answers == null || answers.Count == 0)
            {
                state.AddModelError("answers", "To progress you must solve this question");
            }
            else if (CheckAnswers(questionId, answers) == false)
            {
                state.AddModelError("answers", "Wrong answers");
            }
        }

        internal bool CheckAnswers(Guid questionId, List<Guid> answers)
        {
            return ClientProxy.GetQuizProxy<IPlay>().CheckAnswers(questionId, answers);
        }

        public ContractTypes.Quiz GetContractQuiz(Guid quizId)
        {
            return ClientProxy.GetQuizProxy<IPlay>().GetQuiz(quizId);
        }

        //public Question GetFirstQuestion(ContractTypes.Quiz quiz)
        //{
        //    ContractTypes.Question question = quiz.Questions.First();
        //    Question retQuestion = new Question
        //    {
        //        QuizId = quiz.Id,
        //        QuestionId = question.Id,
        //        Hint = question.Hint,
        //        Text = question.Text,
        //        IsMultipleChoice = question.IsMultipleChoice,
        //        NextQuestion = question.NextQuestion
        //    };

        //}

        public Question GetQuestion(ContractTypes.Quiz quiz, Guid currentQuestionId)
        {
            //get next element
            ContractTypes.Question question = quiz.Questions.FirstOrDefault(q => q.Id == currentQuestionId);
            Guid? nextQuestion = quiz.Questions.SkipWhile(q => q.Id != currentQuestionId).Skip(1).Select(q => q.Id).FirstOrDefault();

            //work around
            if (nextQuestion.HasValue && nextQuestion.Value == default(Guid))
            {
                nextQuestion = null;
            }
            //make a copy because, other without it will not work (reference)
            List<ContractTypes.Question> reversedList = quiz.Questions.ToList();
            reversedList.Reverse();
            Guid? previousQuestion = reversedList.SkipWhile(q => q.Id != currentQuestionId).Skip(1).Select(q => q.Id).FirstOrDefault();

            //work around
            if (previousQuestion.HasValue && previousQuestion.Value == default(Guid))
            {
                previousQuestion = null;
            }

            Question retQuestion = new Question
            {
                QuizId = quiz.Id,
                QuestionId = question.Id,
                Hint = question.Hint,
                Text = question.Text,
                IsMultipleChoice = question.IsMultipleChoice,
                NextQuestion = nextQuestion,
                PreviousQuestion = previousQuestion
            };

            retQuestion.Answers = new List<Answer>(question.Answers.Count);
            foreach (ContractTypes.Answer answer in question.Answers)
            {
                Answer answerItem = new Answer
                {
                    Id = answer.Id,
                    Text = answer.Text,
                    IsSolution = answer.IsSolution
                };
                retQuestion.Answers.Add(answerItem);
            }

            return retQuestion;
        }

        public Question GetFirstQuestion(ContractTypes.Quiz quiz)
        {
            ContractTypes.Question question = quiz.Questions.FirstOrDefault();
            if(question == null)
            {
                return null;
            }
            ContractTypes.Question nextQuestion = quiz.Questions.Skip(1).FirstOrDefault();

            Question retQuestion = new Question
            {
                QuizId = quiz.Id,
                QuestionId = question.Id,
                Hint = question.Hint,
                Text = question.Text,
                IsMultipleChoice = question.IsMultipleChoice,
                NextQuestion = nextQuestion?.Id
            };

            retQuestion.Answers = new List<Answer>(question.Answers.Count);
            foreach (ContractTypes.Answer answer in question.Answers)
            {
                Answer answerItem = new Answer
                {
                    Id = answer.Id,
                    Text = answer.Text,
                    IsSolution = answer.IsSolution
                };
                retQuestion.Answers.Add(answerItem);
            }

            return retQuestion;
        }

        public Question GetFirstQuestion(Guid quizId)
        {
            ContractTypes.PlayQuestion question = ClientProxy.GetQuizProxy<IPlay>().GetFirstQuestion(quizId);

            Question retQuestion = new Question
            {
                QuizId = quizId,
                QuestionId = question.Id,
                Hint = question.Hint,
                Text = question.Text,
                IsMultipleChoice = question.IsMultipleChoice,
                NextQuestion = question.NextQuestion
            };

            retQuestion.Answers = new List<Answer>(question.Answers.Count);
            foreach (ContractTypes.Answer answer in question.Answers)
            {
                Answer answerItem = new Answer
                {
                    Id = answer.Id,
                    Text = answer.Text,
                    IsSolution = answer.IsSolution
                };
                retQuestion.Answers.Add(answerItem);
            }

            return retQuestion;
        }

        public Question GetQuestion(Guid quizId, Guid questionId)
        {
            ContractTypes.PlayQuestion question = ClientProxy.GetQuizProxy<IPlay>().GetQuestion(quizId, questionId);

            Question retQuestion = new Question
            {
                QuizId = quizId,
                QuestionId = question.Id,
                Hint = question.Hint,
                Text = question.Text,
                IsMultipleChoice = question.IsMultipleChoice,
                NextQuestion = question.NextQuestion,
                PreviousQuestion = question.PreviousQuestion
            };

            retQuestion.Answers = new List<Answer>(question.Answers.Count);
            foreach (ContractTypes.Answer answer in question.Answers)
            {
                Answer answerItem = new Answer
                {
                    Id = answer.Id,
                    Text = answer.Text,
                    IsSolution = answer.IsSolution
                };
                retQuestion.Answers.Add(answerItem);
            }

            return retQuestion;
        }

        public void UpdateCookie(HttpCookie cookie, string key)
        {
            if(string.IsNullOrEmpty(key))
            {
                throw new ArgumentException(nameof(key) + " cannot be null or empty");
            }
            if(cookie == null)
            {
                cookie = new HttpCookie(key);
            }
        }

        public ContractTypes.Quiz GetQuiz(HttpContextBase context, Guid quizId)
        {
            //check if there is already a quiz in the session
            if (context.Session.GetSessionContext().CurrentQuiz == null)
            {
                context.Session.GetSessionContext().CurrentQuiz = GetContractQuiz(quizId);
            }
            return context.Session.GetSessionContext().CurrentQuiz;
        }

        public void SaveAnswerState(Guid quizStateId, Guid questionId, List<Guid> answers)
        {

        }
    }
}