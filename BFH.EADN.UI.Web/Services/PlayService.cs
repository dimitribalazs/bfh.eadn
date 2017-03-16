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
        /// <summary>
        /// Groups toppics by its topics for list view
        /// </summary>
        /// <returns>grouped by topic list of quizzes</returns>
        public List<Overview> GetOverview()
        {
            //only get quizzes which have a question and the max question count is > 0
            //because if max question is 0, nothing is going to be selected
            List<ContractTypes.Quiz> quizzes = ClientProxy.GetQuizProxy<IPlay>()
                                                            .GetQuizzes()
                                                                .Where(q => 
                                                                    q.Questions.Count > 0
                                                                    && q.MaxQuestionCount > 0
                                                                ).ToList();

            Dictionary<string, Overview> topicQuizzes = new Dictionary<string, Overview>();

            //go over all quizzes
            foreach (ContractTypes.Quiz quiz in quizzes)
            {
                //go over all  questions
                foreach (ContractTypes.Question question in quiz.Questions)
                {
                    //get all topics of the question
                    foreach (ContractTypes.Topic topic in question.Topics)
                    {
                        //create new overview group (topic name)
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
            if (question == null)
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

        /// <summary>
        /// Get current quiz from session. If its not set or if its a different quiz 
        /// call service
        /// </summary>
        /// <param name="context">current HttpContextBase</param>
        /// <param name="quizId">current quiz id</param>
        /// <returns>current quiz</returns>
        public ContractTypes.Quiz GetQuiz(HttpContextBase context, Guid quizId)
        {
            ContractTypes.Quiz quiz = context.Session.GetSessionContext().CurrentQuiz;
            //check if there is already a quiz in the session, or if the quiz changes
            if (quiz == null || quiz.Id != quizId)
            {
                quiz = GetContractQuiz(quizId);
                context.Session.GetSessionContext().CurrentQuiz = quiz;
            }
            return quiz;
        }

        public void SaveQuestionAnswerState(Guid quizStateId, Guid questionId, List<Guid> answers)
        {
            ClientProxy.GetQuizProxy<IPlay>().UpdateQuestionAnswerState(quizStateId, questionId, answers);
        }

        public void DeleteQuestionAnswerState(Guid quizStateId)
        {
            ClientProxy.GetQuizProxy<IPlay>().DeleteQuestionAnswerState(quizStateId);
        }

        public List<Complete> EvaluateAnswers(Guid questionAnswerStateId)
        {
            List<Complete> complete = new List<Complete>();
            IPlay service = ClientProxy.GetQuizProxy<IPlay>();
            List<ContractTypes.QuestionAnswerState> allQAS = service.GetAllSavedQuestionAnswerStates(questionAnswerStateId);
            foreach(ContractTypes.QuestionAnswerState qas in allQAS)
            {
                bool isCorrect = service.CheckAnswers(qas.Question.Id, qas.Answers.Select(a => a.Id).ToList());
                complete.Add(new Complete
                {
                    QuestionId = qas.Question.Id,
                    QuestionText = qas.Question.Text,
                    AnsweredCorrectly = isCorrect
                });
            }

            return complete;
        }
    }
}