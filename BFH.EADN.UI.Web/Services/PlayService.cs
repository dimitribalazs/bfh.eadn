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
                            QuestionId = question.Id,
                            Text = question.Text
                        };
                        topicQuizzes[topic.Name].QuizItems.Add(item);
                    }
                }
            }

            return topicQuizzes.Values.ToList();
        }

        public Question GetQuestion(Guid quizId, Guid questionId)
        {
            ContractTypes.Quiz quiz = GetQuizProxy<IPlay>().GetQuiz(quizId);
            List<ContractTypes.Question> questions = quiz.Questions.OrderBy(q => q.Id).ToList();

            ContractTypes.Question currentQuestion = null;
            Guid? previousQuestion = null;
            Guid? nextQuestion = null;
            for(int i = 0; i < questions.Count; i++)
            {
                if(questions[i].Id == questionId)
                {
                    currentQuestion = questions[i];
                    if(i > 0)
                    {
                        previousQuestion = questions[i - 1].Id;
                    }

                    if(i + 1 <= questions.Count - 1)
                    {
                        nextQuestion = questions[i + 1].Id;
                    }
                }
            }
            
            Question retQuestion = new Question
            {
                Hint = currentQuestion.Hint,
                Text = currentQuestion.Text,
                IsMultipleChoice = currentQuestion.IsMultipleChoice,
            };

            retQuestion.Answers = new List<Answer>(currentQuestion.Answers.Count);
            foreach(ContractTypes.Answer answer in currentQuestion.Answers)
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