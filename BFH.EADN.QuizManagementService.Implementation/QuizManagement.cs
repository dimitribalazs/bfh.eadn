using BFH.EADN.Common;
using BFH.EADN.Common.Types;
using BFH.EADN.Common.Types.Contracts;
using BFH.EADN.QuizManagementService.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace BFH.EADN.QuizManagementService.Implementation
{
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.PerCall, Namespace = Constants.XMLNamespace, Name = "QuizManagement")]
    public class QuizManagement : IQuizManagement
    {
        private static IFactoryPersistence _persistenceFactory;

        static QuizManagement()
        {
            if(_persistenceFactory == null)
            { 
                _persistenceFactory = Factory.CreateInstance<IFactoryPersistence>();
            }
        }
        
        public void CreateTopic(Topic topic)
        {
            try
            {
                IRepository<Topic, Guid> repository = _persistenceFactory.CreateTopicRepository();
                repository.Create(topic);
            }
            catch(Exception ex)
            {
                ServiceFault fault = new ServiceFault
                {
                    Message = ex.Message,
                    Reason = "Error during creation of a new topic"
                };
                throw new FaultException<ServiceFault>(fault);
            }
        }

        public void DeleteTopic(Guid id)
        {
            throw new NotImplementedException();
        }

        public void UpdateTopic(Topic topic)
        {
        }

        public Topic GetTopic(Guid id)
        {
            try
            {
                IRepository<Topic, Guid> repository = _persistenceFactory.CreateTopicRepository();
                return repository.Get(id);
            }
            catch (Exception ex)
            {
                ServiceFault fault = new ServiceFault
                {
                    Message = ex.Message,
                    Reason = "Error while getting topic"
                };
                throw new FaultException<ServiceFault>(fault);
            }
        }

        public List<Topic> GetTopics()
        {
            IRepository<Topic, Guid> repository = _persistenceFactory.CreateTopicRepository();
            return repository.GetAll();
        }
    }
}
