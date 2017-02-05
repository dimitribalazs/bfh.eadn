using BFH.EADN.Common;
using BFH.EADN.QuizManagementService.Contracts;
using BFH.EADN.UI.Web.Models;
using ContractTypes = BFH.EADN.Common.Types.Contracts;
using System.Collections.Generic;
using System.Linq;

namespace BFH.EADN.UI.Web.Services
{
    public class TopicService
    {

        private List<Topic> _topics;
        public List<Topic> Topics {
            get
            {
                return null;
            }
            private set
            {
                _topics = value;
            }
        }

        public List<Topic> GetTopics()
        {
            WcfServiceFactory factory = new WcfServiceFactory();
            IQuizManagement service = factory.GetService<IQuizManagement>();

            List<ContractTypes.Topic> topics = service.GetTopics();

            return topics.Select(t => new Topic
            {
                Id = t.Id,
                Description = t.Description,
                Name = t.Name
            }).ToList();
        }
    }
}