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
                            Id = question.Id,
                            Text = question.Text
                        };
                        topicQuizzes[topic.Name].QuizItems.Add(item);
                    }
                }
            }

            return topicQuizzes.Values.ToList();
        }
    }
}