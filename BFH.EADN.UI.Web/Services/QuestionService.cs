using System.Collections.Generic;
using System.Linq;
using System;
using BFH.EADN.UI.Web.Models.Management;
using ContractTypes = BFH.EADN.Common.Types.Contracts;
using BFH.EADN.QuizManagementService.Contracts;
using AutoMapper;
using System.Web.Mvc;

namespace BFH.EADN.UI.Web.Services
{
    public sealed class QuestionService : BaseService, IService<Question, Guid>
    {
        /// <summary>
        /// Get all questions
        /// </summary>
        /// <returns>list of questions</returns>
        public List<Question> GetList()
        {
            List<ContractTypes.Question> questions = GetProxy<IQuestionManagement>().GetQuestions();
            List<Question> mappedList = Mapper.Map<List<ContractTypes.Question>, List<Question>>(questions);
            return mappedList;
        }

        /// <summary>
        /// Get a concrete question
        /// </summary>
        /// <param name="id">a question id</param>
        /// <returns>the concrete question</returns>
        public Question Get(Guid id)
        {
            ContractTypes.Question question = GetProxy<IQuestionManagement>().GetQuestion(id);
            //todo selectlistitems
            return Mapper.Map<Question>(question);
        }

        /// <summary>
        /// Creates a new question
        /// </summary>
        /// <param name="newQuestion"></param>
        public void Create(Question newQuestion)
        {
            if (newQuestion.Id == default(Guid))
            {
                newQuestion.Id = Guid.NewGuid();
            }
            ContractTypes.Question contractQuestion = Mapper.Map<ContractTypes.Question>(newQuestion);
            GetProxy<IQuestionManagement>().CreateQuestion(contractQuestion);
        }

        /// <summary>
        /// Update an existing question with new information
        /// </summary>
        /// <param name="id">id of an existing question</param>
        /// <param name="question">question with the new values</param>
        public void Edit(Guid id, Question question)
        { 
            ContractTypes.Question contractQuestion = GetProxy<IQuestionManagement>().GetQuestion(id);
            contractQuestion = Mapper.Map<ContractTypes.Question>(question);
            GetProxy<IQuestionManagement>().UpdateQuestion(contractQuestion);
        }

        /// <summary>
        /// Delete a question
        /// </summary>
        /// <param name="id">id of the question which should be deleted</param>
        public void Delete(Guid id)
        {
            GetProxy<IQuestionManagement>().DeleteQuestion(id);
        }
    }
}