using System.Collections.Generic;
using System.Linq;
using System;
using BFH.EADN.UI.Web.Models.Management;
using ContractTypes = BFH.EADN.Common.Types.Contracts;
using BFH.EADN.QuizManagementService.Contracts;
using AutoMapper;
using System.Web.Mvc;
using BFH.EADN.UI.Web.Utils;

namespace BFH.EADN.UI.Web.Services
{
    /// <summary>
    /// Question service which provides UI methods to convert data from contracts to viewmodels,
    /// call service methods, validations
    /// </summary>
    public sealed class QuestionService : IService<Question, Guid>
    {
        /// <summary>
        /// Get all questions
        /// </summary>
        /// <returns>list of questions</returns>
        public List<Question> GetList()
        {
            List<ContractTypes.Question> questions = ClientProxy.GetProxy<IQuestionManagement>().GetQuestions();
            List<Question> mappedList = Mapper.Map<List<ContractTypes.Question>, List<Question>>(questions);
            return mappedList;
        }

        public List<Question> GetQuestionsWithoutTopics()
        {
            List<ContractTypes.Question> questions = ClientProxy.GetProxy<IQuestionManagement>().GetQuestionsWithoutTopic();
            List<Question> mappedList = Mapper.Map<List<ContractTypes.Question>, List<Question>>(questions);
            return mappedList;
        }

        /// <summary>
        /// create a new question model
        /// </summary>
        /// <returns>new initialized question model</returns>
        public Question Get()
        {
            Question questionModel = new Question();
            questionModel.Topics = ClientProxy.GetProxy<ITopicManagement>().GetTopics();
            return questionModel;
        }

        /// <summary>
        /// Get a concrete question
        /// </summary>
        /// <param name="id">a question id</param>
        /// <returns>the concrete question</returns>
        public Question Get(Guid id)
        {
            ContractTypes.Question question = ClientProxy.GetProxy<IQuestionManagement>().GetQuestion(id);
            Question questionModel = Mapper.Map<Question>(question);
            questionModel.SelectedTopicIds = questionModel.Topics.Select(q => q.Id).ToArray();
            //get whole list for selection
            questionModel.Topics = ClientProxy.GetProxy<ITopicManagement>().GetTopics();
                        
            questionModel.SelectedAnswerIds = questionModel.Answers.Select(a => a.Id).ToArray();
            questionModel.Answers = questionModel.Answers;
            return questionModel;
        }

        /// <summary>
        /// Creates a new question
        /// </summary>
        /// <param name="newQuestion"></param>
        public void Create(Question newQuestion)
        {
            ContractTypes.Question contractQuestion = Mapper.Map<ContractTypes.Question>(newQuestion);
            List<ContractTypes.Topic> topics  = ClientProxy.GetProxy<ITopicManagement>().GetTopicsByIds(newQuestion.SelectedTopicIds.ToList());
            contractQuestion.Topics = topics;
            ClientProxy.GetProxy<IQuestionManagement>().CreateQuestion(contractQuestion);

        }

        /// <summary>
        /// Update an existing question with new information
        /// </summary>
        /// <param name="id">id of an existing question</param>
        /// <param name="question">question with the new values</param>
        public void Edit(Guid id, Question question)
        { 
            ContractTypes.Question contractQuestion = ClientProxy.GetProxy<IQuestionManagement>().GetQuestion(id);
            contractQuestion = Mapper.Map(question, contractQuestion);

            //set topics
            List<ContractTypes.Topic> topics = ClientProxy.GetProxy<ITopicManagement>().GetTopicsByIds(question.SelectedTopicIds.ToList());
            contractQuestion.Topics = topics;

            //set answers
            List<Guid> selectedAnswerIds = question.SelectedAnswerIds?.ToList();
            List<ContractTypes.Answer> answers = ClientProxy.GetProxy<IAnswerManagement>().GetAnswersByIds(selectedAnswerIds);
            contractQuestion.Answers = answers;


            ClientProxy.GetProxy<IQuestionManagement>().UpdateQuestion(contractQuestion);
        }

        /// <summary>
        /// Delete a question
        /// </summary>
        /// <param name="id">id of the question which should be deleted</param>
        public void Delete(Guid id)
        {
            ClientProxy.GetProxy<IQuestionManagement>().DeleteQuestion(id);
        }

        /// <summary>
        /// Validates edit and creat calls
        /// </summary>
        /// <param name="state">ModelStateDictionary</param>
        /// <param name="question">current Question</param>
        public void Validation(ModelStateDictionary state, Question question, bool isEdit)
        {
            
            //checks if there is a single answer the solution
            if(question.IsMultipleChoice == false)
            {
                List<Guid> selectedAnswerIds = question.SelectedAnswerIds != null ? question.SelectedAnswerIds.ToList() : null;
                List<ContractTypes.Answer> answers = ClientProxy.GetProxy<IAnswerManagement>().GetAnswersByIds(selectedAnswerIds);
                int answerIsSolutionCount = answers != null ? answers.Where(a => a.IsSolution).ToList().Count : 0;
                if (answerIsSolutionCount > 1)
                {
                    state.AddModelError("SelectedAnswerIds", "You cannot set more than one answer as solution");
                }
                else if(answerIsSolutionCount == 0 )
                {
                    state.AddModelError("SelectedAnswerIds", "You must set at least one answer as solution");
                }
            }

            if(isEdit)
            {
                if(question.SelectedAnswerIds == null || question.SelectedAnswerIds.Length == 0)
                {
                    state.AddModelError("SelectedAnswerIds", "Add at least one answer");
                }
            }
        }
    }
}