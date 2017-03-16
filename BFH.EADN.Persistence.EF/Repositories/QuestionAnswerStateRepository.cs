using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;

using CommonContracts = BFH.EADN.Common.Types.Contracts;

namespace BFH.EADN.Persistence.EF.Repositories
{
    /// <summary>
    /// QuestionAnswerState repository to interact with the persistence layer
    /// </summary>
    public sealed class QuestionAnswerStateRepository : BaseRepository<CommonContracts.QuestionAnswerState, Guid>
    {
        ///<inheritdoc />
        public override void Create(CommonContracts.QuestionAnswerState data)
        {
            if (data.Answers != null && data.Answers.Count > 0)
            {
                foreach (CommonContracts.Answer answer in data.Answers)
                {
                    Entities.QuestionAnswerState newQuestionAnswerState = Mapper.Map<Entities.QuestionAnswerState>(data);
                    //we have to do this. else EF creates new Question
                    newQuestionAnswerState.Question = Context.Questions.Single(q => q.Id == data.Question.Id);
                    newQuestionAnswerState.Answer = Context.Answers.Single(a => a.Id == answer.Id);
                    Context.QuestionAnswerStates.Add(newQuestionAnswerState);
                }
            }
            else
            {
                Entities.QuestionAnswerState newQuestionAnswerState = Mapper.Map<Entities.QuestionAnswerState>(data);
                newQuestionAnswerState.Question = Context.Questions.Single(q => q.Id == data.Question.Id);
                newQuestionAnswerState.Answer = null;
                Context.QuestionAnswerStates.Add(newQuestionAnswerState);
            }
            Context.SaveChanges();
        }

        ///<inheritdoc />
        public override void Delete(Guid quizStateId)
        {
            List<Entities.QuestionAnswerState> entities = Context.QuestionAnswerStates.Where(q => q.QuestionAnswerStateId == quizStateId).ToList();
            Context.QuestionAnswerStates.RemoveRange(entities);
            Context.SaveChanges();
        }

        ///<inheritdoc />
        public override CommonContracts.QuestionAnswerState Get(Guid id)
        {
            throw new NotImplementedException();
        }

        ///<inheritdoc />
        public override List<CommonContracts.QuestionAnswerState> GetAll()
        {
            List<Entities.QuestionAnswerState> results = Context.QuestionAnswerStates.ToList();
            List<CommonContracts.QuestionAnswerState> returnData = new List<CommonContracts.QuestionAnswerState>();

            Guid? oldQuestionId = null;
            Guid? oldQuestionAnswerStateId = null;
            CommonContracts.QuestionAnswerState qas = null;
            foreach (Entities.QuestionAnswerState result in results.OrderBy(r => r.QuestionAnswerStateId))
            {
                bool createNewState = oldQuestionId.HasValue == false
                                        || oldQuestionId.Value != result.Question.Id
                                        || oldQuestionAnswerStateId.Value != result.QuestionAnswerStateId;
                if(createNewState)
                {
                    oldQuestionId = result.Question.Id;
                    oldQuestionAnswerStateId = result.QuestionAnswerStateId;
                    Entities.Question question = result.Question;
                    qas = new CommonContracts.QuestionAnswerState
                    {
                        QuestionAnswerStateId = result.QuestionAnswerStateId,
                        Question = new CommonContracts.Question
                        {
                            Id = question.Id,
                            Hint = question.Hint,
                            Text = question.Text,
                            IsMultipleChoice = question.IsMultipleChoice
                        },
                        Answers = new List<CommonContracts.Answer>()
                    };
                    returnData.Add(qas);
                }
                //case where nothing was selected
                if (result.Answer != null)
                {
                    qas.Answers.Add(new CommonContracts.Answer
                    {
                        Id = result.Answer.Id,
                        IsSolution = result.Answer.IsSolution,
                        Text = result.Answer.Text
                    });
                }
            }
            return returnData;
        }

        ///<inheritdoc />
        public override List<CommonContracts.QuestionAnswerState> GetListByIds(List<Guid> ids)
        {
            throw new NotImplementedException();
        }

        ///<inheritdoc />
        public override void Update(CommonContracts.QuestionAnswerState data)
        {
            //delete old entries
            List<Entities.QuestionAnswerState> entities = Context.QuestionAnswerStates.Where(q => q.QuestionAnswerStateId == data.QuestionAnswerStateId && q.Question.Id == data.Question.Id).ToList();
            Context.QuestionAnswerStates.RemoveRange(entities);
            Context.SaveChanges();
            //then create new entries
            Create(data);
        }
    }
}
