using BFH.EADN.QuizService.Contracts;
using BFH.EADN.UI.Web.Models.Play;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ContractTypes = BFH.EADN.Common.Types.Contracts;

namespace BFH.EADN.UI.Web.Services
{
    public class PlayService : BaseService
    {
        public List<Overview> GetOverview()
        {


            List<ContractTypes.Quiz> quizzes = GetQuizProxy<IPlay>().GetQuizzes();
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
                        QuizItem item = new QuizItem
                        {
                            QuizId = quiz.Id,
                            Text = quiz.Text
                        };
                        topicQuizzes[topic.Name].QuizItems.Add(item);
                    }
                }
            }

            return topicQuizzes.Values.ToList();
        }

        internal bool CheckAnswers(Guid questionId, List<Guid> answers)
        {
            return GetQuizProxy<IPlay>().CheckAnswers(questionId, answers);
        }

        public Question GetFirstQuestion(Guid quizId)
        {
            ContractTypes.PlayQuestion question = GetQuizProxy<IPlay>().GetFirstQuestion(quizId);

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
            ContractTypes.PlayQuestion question = GetQuizProxy<IPlay>().GetQuestion(quizId, questionId);

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
    }
}