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

        
        private IRepository<Topic, Guid> TopicRepository => _persistenceFactory.CreateTopicRepository();
        




        static QuizManagement()
        {
            if (_persistenceFactory == null)
            {
                _persistenceFactory = Factory.CreateInstance<IFactoryPersistence>();
            }
        }

        public string Test(string foo)
        {
            return foo;
        }

        public void CreateTopic(Topic topic)
        {
            try
            { 
                TopicRepository.Create(topic);
            }
            catch (Exception ex)
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
            try
            {
                TopicRepository.Delete(id);
            }
            catch (Exception ex)
            {
                ServiceFault fault = new ServiceFault
                {
                    Message = ex.Message,
                    Reason = "Error during deletion of a topic"
                };
                throw new FaultException<ServiceFault>(fault);
            }
        }

        public void UpdateTopic(Topic topic)
        {
            try
            {
                TopicRepository.Update(topic);
            }
            catch (Exception ex)
            {
                ServiceFault fault = new ServiceFault
                {
                    Message = ex.Message,
                    Reason = "Error during creation of a new topic"
                };
                throw new FaultException<ServiceFault>(fault);
            }
        }

        public Topic GetTopic(Guid id)
        {
            try
            {
               return TopicRepository.Get(id);
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
            try
            {
                return TopicRepository.GetAll();
            }
            catch (Exception ex)
            {
                ServiceFault fault = new ServiceFault
                {
                    Message = ex.Message,
                    Reason = "Error while getting topics"
                };
                throw new FaultException<ServiceFault>(fault);
            }
        }
    }
}
