using Microsoft.VisualStudio.TestTools.UnitTesting;
using BFH.EADN.QuizManagementService.Implementation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ServiceModel;
using BFH.EADN.Common.Types.Contracts;
using BFH.EADN.Common;
using BFH.EADN.Common.Types;
using BFH.EADN.QuizManagementService.Contracts;
using System.Configuration;
using System.IO;
using BFH.EADN.CommonTests.TestHelper;

namespace BFH.EADN.QuizManagementService.Implementation.Tests
{
    [TestClass]
    public class QuizManagementTopicTests : TestBaseWithDb
    {
        private static IFactoryPersistence _factoryPersistence = Factory.CreateInstance<IFactoryPersistence>();
        
        /* Topic area */
        [TestMethod]
        public void CreateTopicSuccess()
        {
            ITopicManagement service = new QuizManagement(_factoryPersistence);
            //create new topic
            Topic topic = new Topic
            {
                Name = "Test Topic",
                Description = "This is a test topic description"
            };

            service.CreateTopic(topic);
            Assert.IsTrue(true);
        }

        [TestMethod]
        [ExpectedException(typeof(FaultException<ServiceFault>))]
        public void CreateTopicException()
        {
            ITopicManagement service = new QuizManagement(_factoryPersistence);
            service.CreateTopic(null);
        }

        [TestMethod]
        public void GetListAndDeleteTopicSuccess()
        {
            ITopicManagement service = new QuizManagement(_factoryPersistence);
            List<Topic> results = service.GetTopics();
            int count = results.Count;

            Guid toDelete = results.Last().Id;
            service.DeleteTopic(toDelete);

            Assert.AreEqual(count - 1, service.GetTopics().Count, "Count of topic not the same");
        }

        [TestMethod]
        [ExpectedException(typeof(FaultException<ServiceFault>))]
        public void DeleteTopicException()
        {
            ITopicManagement service = new QuizManagement(_factoryPersistence);
            Guid guid = Guid.NewGuid();
            service.DeleteTopic(guid);
        }

        [TestMethod]
        [ExpectedException(typeof(FaultException<ServiceFault>))]
        public void GetTopicException()
        {
            Guid randomGuid = Guid.NewGuid();
            ITopicManagement service = new QuizManagement(_factoryPersistence);
            service.GetTopic(randomGuid);
        }

        [TestMethod]
        public void GetTopicSuccess()
        {
            ITopicManagement service = new QuizManagement(_factoryPersistence);
            Guid expectedResult = service.GetTopics().First().Id;
            Topic result = service.GetTopic(expectedResult);
            Assert.AreEqual(expectedResult, result.Id, "Ids are not the same");
        }

        [TestMethod]
        [ExpectedException(typeof(FaultException<ServiceFault>))]
        public void GetTopicsException()
        {
            //set persistenceFactory to null
            ITopicManagement service = new QuizManagement(null);
            service.GetTopics();
        }

        [TestMethod]
        public void UpdateTopicSuccess()
        {
            ITopicManagement service = new QuizManagement(_factoryPersistence);
            Topic topic = service.GetTopics().First();

            string newName = "UpdateTopicSuccess";

            Guid id = topic.Id;
            topic.Name = newName;

            service.UpdateTopic(topic);

            Topic updatedTopic = service.GetTopic(id);

            Assert.AreEqual(newName, updatedTopic.Name);
        }

        [TestMethod]
        [ExpectedException(typeof(FaultException<ServiceFault>))]
        public void UpdateTopicException()
        {
            ITopicManagement service = new QuizManagement(_factoryPersistence);
            Topic topic = service.GetTopics().First();
            topic.Name = "UpdateTopicException";
            service = new QuizManagement(null);
            service.UpdateTopic(topic);
        }

        [TestMethod]
        public void GetTopicsByIdsSuccess()
        {
            ITopicManagement service = new QuizManagement(_factoryPersistence);
            List<Guid> guids = service.GetTopics().Select(q => q.Id).ToList();
            List<Guid> guidsByIds = service.GetTopicsByIds(guids).Select(q => q.Id).ToList();
            bool listsNotSame = guids.Where(g => !guidsByIds.Any(gBIds => gBIds == g)).Any();
            Assert.IsFalse(listsNotSame);
        }

        [TestMethod]
        [ExpectedException(typeof(FaultException<ServiceFault>))]
        public void GetTopicsByIdsException()
        {
            ITopicManagement service = new QuizManagement(null);
            service.GetTopicsByIds(new List<Guid> { Guid.NewGuid() });
        }
    }
}
