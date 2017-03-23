using BFH.EADN.Common.Types.Contracts;
using BFH.EADN.QuizService.Contracts;
using BFH.EADN.UI.Web.Models.Play;
using BFH.EADN.UI.Web.Utils;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ContractTypes = BFH.EADN.Common.Types.Contracts;

namespace BFH.EADN.UI.Web.Services
{
    /// <summary>
    /// Play service which provides UI methods to convert data from contracts to viewmodels,
    /// call service methods, validations
    /// </summary>
    public sealed class PlayService
    {
        /// <summary>
        /// Groups toppics by its topics for list view
        /// </summary>
        /// <returns>grouped by topic list of quizzes</returns>
        public List<Overview> GetOverview()
        {
            //only get quizzes which have a question and the max question count is > 0
            //because if max question is 0, nothing is going to be selected
            List<ContractTypes.Quiz> quizzes = ClientProxy.GetProxy<IPlay>()
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

        /// <summary>
        /// Validation of answers
        /// </summary>
        /// <param name="state">ModelStateDictionary to add errors</param>
        /// <param name="questionId">question id</param>
        /// <param name="answers">selected list of answer ids</param>
        public void Validation(ModelStateDictionary state, Guid questionId, List<Guid> answers)
        {
            if (CheckAnswers(questionId, answers) == false)
            {
                state.AddModelError("answers", "Wrong answers");
            }
        }


        /// <summary>
        /// Checks answers against the service
        /// </summary>
        /// <param name="questionId">question id</param>
        /// <param name="answers">selected list of answer ids</param>
        /// <returns>true if answers are correct, else false</returns>
        public bool CheckAnswers(Guid questionId, List<Guid> answers)
        {
            return ClientProxy.GetProxy<IPlay>().CheckAnswers(questionId, answers);
        }

        /// <summary>
        /// Get quiz from the service by its id
        /// </summary>
        /// <param name="quizId">quiz id</param>
        /// <returns>contract type quiz</returns>
        public ContractTypes.Quiz GetContractQuiz(Guid quizId)
        {
            return ClientProxy.GetProxy<IPlay>().GetQuiz(quizId);
        }

        /// <summary>
        /// Get the question by quiz 
        /// </summary>
        /// <param name="quiz"></param>
        /// <param name="currentQuestionId"></param>
        /// <returns></returns>
        public Models.Play.Question GetQuestion(ContractTypes.Quiz quiz, Guid currentQuestionId)
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

            Models.Play.Question retQuestion = new Models.Play.Question
            {
                QuizId = quiz.Id,
                QuestionId = question.Id,
                Hint = question.Hint,
                Text = question.Text,
                IsMultipleChoice = question.IsMultipleChoice,
                NextQuestion = nextQuestion,
                PreviousQuestion = previousQuestion
            };

            retQuestion.Answers = new List<Models.Play.Answer>(question.Answers.Count);
            foreach (ContractTypes.Answer answer in question.Answers)
            {
                Models.Play.Answer answerItem = new Models.Play.Answer
                {
                    Id = answer.Id,
                    Text = answer.Text,
                    IsSolution = answer.IsSolution
                };
                retQuestion.Answers.Add(answerItem);
            }

            return retQuestion;
        }

        public Models.Play.Question GetFirstQuestion(ContractTypes.Quiz quiz)
        {
            ContractTypes.Question question = quiz.Questions.FirstOrDefault();
            if (question == null)
            {
                return null;
            }
            ContractTypes.Question nextQuestion = quiz.Questions.Skip(1).FirstOrDefault();

            Models.Play.Question retQuestion = new Models.Play.Question
            {
                QuizId = quiz.Id,
                QuestionId = question.Id,
                Hint = question.Hint,
                Text = question.Text,
                IsMultipleChoice = question.IsMultipleChoice,
                NextQuestion = nextQuestion?.Id
            };

            retQuestion.Answers = new List<Models.Play.Answer>(question.Answers.Count);
            foreach (ContractTypes.Answer answer in question.Answers)
            {
                Models.Play.Answer answerItem = new Models.Play.Answer
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
        public ContractTypes.Quiz GetQuiz(HttpContextBase context, Guid quizId, Guid? questionAnswerStateId = null, string savedQuestionIdsString = null)
        {
            ContractTypes.Quiz quiz = context.Session.GetSessionContext().CurrentQuiz;
            //check if there is already a quiz in the session, or if the quiz changes
            if (quiz == null || quiz.Id != quizId)
            {
                quiz = GetContractQuiz(quizId);

                List<QuestionAnswerState> qas = new List<QuestionAnswerState>();
                IPlay service = ClientProxy.GetProxy<IPlay>();
                //check if there is a questionanswerstate
                if (questionAnswerStateId.HasValue)
                {
                    qas = service.GetAllSavedQuestionAnswerStates(questionAnswerStateId.Value);
                }

                if (string.IsNullOrEmpty(savedQuestionIdsString) == false)
                {
                    //delete all questions, because we add those from cookie
                    quiz.Questions = new List<ContractTypes.Question>();

                    string[] questionIds = savedQuestionIdsString.Split(',');
                    for (int i = 0; i < questionIds.Length; i++)
                    {
                        string questionId = questionIds[i];
                        Guid? previousQuestion = null;
                        Guid? nextQuestion = null;

                        //has previous question
                        if (i > 0)
                        {
                            Guid localPreviousQuestion;
                            if (Guid.TryParse(questionIds[i - 1], out localPreviousQuestion))
                            {
                                previousQuestion = localPreviousQuestion;
                            }
                        }

                        //has next question
                        if (i + 1 <= questionIds.Length - 1)
                        {
                            Guid localNextQuestion;
                            if (Guid.TryParse(questionIds[i + 1], out localNextQuestion))
                            {
                                previousQuestion = localNextQuestion;
                            }
                        }


                        Guid questionGuid;
                        if (Guid.TryParse(questionId, out questionGuid))
                        {
                            //get question and reset
                            PlayQuestion question = service.GetQuestion(quizId, questionGuid);
                            question.PreviousQuestion = previousQuestion;
                            question.NextQuestion = nextQuestion;
                            quiz.Questions.Add(question);
                        }
                    }
                }

                context.Session.GetSessionContext().CurrentQuiz = quiz;
            }
            return quiz;
        }

        /// <summary>
        /// Saved the current state 
        /// </summary>
        /// <param name="questionAnswerStateId">question answer state id</param>
        /// <param name="questionId">question id</param>
        /// <param name="answers">answer id</param>
        public void SaveQuestionAnswerState(Guid questionAnswerStateId, Guid questionId, List<Guid> answers)
        {
            ClientProxy.GetProxy<IPlay>().UpdateQuestionAnswerState(questionAnswerStateId, questionId, answers);
        }

        /// <summary>
        /// Delete the state
        /// </summary>
        /// <param name="questionAnswerStateId">question answer state id</param>
        public void DeleteQuestionAnswerState(Guid questionAnswerStateId)
        {
            ClientProxy.GetProxy<IPlay>().DeleteQuestionAnswerState(questionAnswerStateId);
        }

        /// <summary>
        /// Get all saved questions answer states entries by id
        /// </summary>
        /// <param name="questionAnswerStateId">question answer state id</param>
        /// <returns>list of QuestionAnswerStates</returns>
        public List<ContractTypes.QuestionAnswerState> GetAllSavedQuestionAnswerStates(Guid questionAnswerStateId)
        {
            return ClientProxy.GetProxy<IPlay>().GetAllSavedQuestionAnswerStates(questionAnswerStateId);
        }

        /// <summary>
        /// Evaluate answers
        /// </summary>
        /// <param name="questionAnswerStateId">question answer state id</param>
        /// <returns>List of complete items with question text and if was answered correctly</returns>
        public List<Complete> EvaluateAnswers(Guid questionAnswerStateId)
        {
            List<Complete> complete = new List<Complete>();
            List<ContractTypes.QuestionAnswerState> allQAS = GetAllSavedQuestionAnswerStates(questionAnswerStateId);
            foreach (ContractTypes.QuestionAnswerState qas in allQAS)
            {
                bool isCorrect = CheckAnswers(qas.Question.Id, qas.Answers.Select(a => a.Id).ToList());
                complete.Add(new Complete
                {
                    QuestionId = qas.Question.Id,
                    QuestionText = qas.Question.Text,
                    AnsweredCorrectly = isCorrect
                });
            }

            return complete;
        }

        /// <summary>
        /// Checks if the url is still valid
        /// </summary>
        /// <param name="url">saved url</param>
        /// <returns>true if url is still valid</returns>
        public bool UrlIsStillValid(string url)
        {
            Uri uri = new Uri(url);
            NameValueCollection queryString = HttpUtility.ParseQueryString(uri.Query);
            Guid questionId = Guid.Parse(queryString["questionId"]);
            Guid quizId = Guid.Parse(queryString["quizId"]);

            ContractTypes.Quiz quiz = ClientProxy.GetProxy<IPlay>().GetQuizzes().SingleOrDefault(q => q.Id == quizId);
            if (quiz != null)
            {
                return quiz.Questions.Any(q => q.Id == questionId);
            }

            return false;
        }
    }
}