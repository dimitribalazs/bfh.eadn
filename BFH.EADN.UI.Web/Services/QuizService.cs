﻿using System.Collections.Generic;
using System.Linq;
using System;
using BFH.EADN.UI.Web.Models.Management;
using ContractTypes = BFH.EADN.Common.Types.Contracts;
using BFH.EADN.QuizManagementService.Contracts;
using System.Web.Mvc;
using AutoMapper;
using BFH.EADN.UI.Web.Utils;

namespace BFH.EADN.UI.Web.Services
{
    /// <summary>
    /// Quiz service which provides UI methods to convert data from contracts to viewmodels,
    /// call service methods, validations
    /// </summary>
    public sealed class QuizService : IService<Quiz, Guid>
    {
        /// <summary>
        /// Get all topics
        /// </summary>
        /// <returns>list of topics</returns>
        public List<Quiz> GetList()
        {
            List<ContractTypes.Quiz> quizzes = ClientProxy.GetProxy<IQuizManagement>().GetQuizzes();
            List<Quiz> mappedList = Mapper.Map<List<ContractTypes.Quiz>, List<Quiz>>(quizzes);
            return mappedList;
        }

        /// <summary>
        /// create a new quiz model
        /// </summary>
        /// <returns>new initialized quiz model</returns>
        public Quiz Get()
        {
            Quiz quizModel = new Quiz();
            quizModel.Questions = ClientProxy.GetProxy<IQuestionManagement>().GetQuestions();
            return quizModel;
        }

        /// <summary>
        /// Get a concrete topic
        /// </summary>
        /// <param name="id">a topic id</param>
        /// <returns>the concrete topic</returns>
        public Quiz Get(Guid id)
        {
            ContractTypes.Quiz quiz = ClientProxy.GetProxy<IQuizManagement>().GetQuiz(id);
            Quiz returnQuiz = Mapper.Map<Quiz>(quiz);
            AddQuestionsToQuiz(returnQuiz);
            return returnQuiz;
        }

        public void AddQuestionsToQuiz(Quiz quiz)
        {
            if(quiz.Questions != null)
            { 
                quiz.SelectedQuestionIds = quiz.Questions.Select(q => q.Id).ToArray();
            }
            //get whole list for selection
            quiz.Questions = ClientProxy.GetProxy<IQuestionManagement>().GetQuestions();
        }

        /// <summary>
        /// Creates a new topic
        /// </summary>
        /// <param name="newTopic"></param>
        public void Create(Quiz newQuiz)
        {
            if (newQuiz.Id == default(Guid))
            {
                newQuiz.Id = Guid.NewGuid();
            }
            ContractTypes.Quiz contractQuiz = Mapper.Map<ContractTypes.Quiz>(newQuiz);
            List<ContractTypes.Question> questions = ClientProxy.GetProxy<IQuestionManagement>().GetQuestionsByIds(newQuiz.SelectedQuestionIds.ToList());
            contractQuiz.Questions = questions;
            ClientProxy.GetProxy<IQuizManagement>().CreateQuiz(contractQuiz);
        }


        /// <summary>
        /// Update an existing topic with new information
        /// </summary>
        /// <param name="id">id of an existing topic</param>
        /// <param name="topic">topic with the new values</param>
        public void Edit(Guid id, Quiz quiz)
        {
            ContractTypes.Quiz contractQuiz = ClientProxy.GetProxy<IQuizManagement>().GetQuiz(id);
            contractQuiz = Mapper.Map(quiz, contractQuiz);

            //update the question of this quiz
            List<ContractTypes.Question> questions = ClientProxy.GetProxy<IQuestionManagement>().GetQuestionsByIds(quiz.SelectedQuestionIds.ToList());
            contractQuiz.Questions = questions;

            ClientProxy.GetProxy<IQuizManagement>().UpdateQuiz(contractQuiz);
        }

        /// <summary>
        /// Delete a topic
        /// </summary>
        /// <param name="id">id of the topic which should be deleted</param>
        public void Delete(Guid id)
        {
            ClientProxy.GetProxy<IQuizManagement>().DeleteQuiz(id);
        }

        /// <summary>
        /// Validates edit and creat calls
        /// </summary>
        /// <param name="state">ModelStateDictionary</param>
        /// <param name="quiz">current quiz</param>
        public void Validation(ModelStateDictionary state, Quiz quiz)
        {
            if (quiz.MinQuestionCount > quiz.MaxQuestionCount)
            {
                state.AddModelError("MinQuestionCount", "Cannot be bigger than MaxQuestionCount");
            }

            if (quiz.MinQuestionCount > 0)
            {
                if (quiz.SelectedQuestionIds == null || quiz.MinQuestionCount > quiz.SelectedQuestionIds.Length)
                {
                    state.AddModelError("SelectedQuestionIds", "Too few questions selected");
                }
            }
        }
    }
}