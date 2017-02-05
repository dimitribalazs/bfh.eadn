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

namespace BFH.EADN.QuizManagementService.Implementation.Tests
{
    [TestClass()]
    public class QuizManagementTests
    {
        [TestMethod()]
        public void GetTopicSuccess()
        {
            IFactoryPersistence _persistenceFactory = Factory.CreateInstance<IFactoryPersistence>();
            IRepository<Topic, Guid> repo = _persistenceFactory.CreateTopicRepository();
            List<Topic> topics = repo.GetAll();

            Topic firstTopic = topics.First();

            Topic topicById = repo.Get(firstTopic.Id);

            Assert.AreEqual(firstTopic.Id, topicById.Id);
        }

        [TestMethod()]
        public void GetTopicFailure()
        {
            IFactoryPersistence _persistenceFactory = Factory.CreateInstance<IFactoryPersistence>();
            IRepository<Topic, Guid> repo = _persistenceFactory.CreateTopicRepository();
            List<Topic> topics = repo.GetAll();

            Topic firstTopic = topics.First();

            Topic topicById = repo.Get(Guid.NewGuid());

            Assert.IsNull(topicById);
        }

        [TestMethod()]
        [ExpectedException(typeof(FaultException<ServiceFault>))]
        public void GetTopicException()
        {

        }
    }
}