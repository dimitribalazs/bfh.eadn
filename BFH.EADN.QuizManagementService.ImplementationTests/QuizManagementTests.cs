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

namespace BFH.EADN.QuizManagementService.Implementation.Tests
{
    [TestClass()]
    public class QuizManagementTests
    {
        private Guid topicGuid = Guid.NewGuid();



        /// <summary>
        /// If no exception occurs assert is true
        /// </summary>
        [TestMethod]
        public void CreateTopicSuccess()
        {
            IFactoryPersistence _persistenceFactory = Factory.CreateInstance<IFactoryPersistence>();
            using (IRepository<Topic, Guid> repo = _persistenceFactory.CreateTopicRepository())
            {
                //create new topic
                Topic topic = new Topic
                {
                    Id = topicGuid,
                    Name = "Test Topic",
                    Description = "This is a test topic description"
                };

                repo.Create(topic);
            }

            Assert.IsTrue(true);
        }


        [TestMethod]
        public void GetTopicSuccess()
        {
            IFactoryPersistence _persistenceFactory = Factory.CreateInstance<IFactoryPersistence>();
            IRepository<Topic, Guid> repo = _persistenceFactory.CreateTopicRepository();
            List<Topic> topics = repo.GetAll();

            Topic firstTopic = topics.First();
            Topic topicById = repo.Get(firstTopic.Id);
            Assert.AreEqual(firstTopic.Id, topicById.Id);

        }

        [TestMethod]
        public void GetTopicFailure()
        {
            IFactoryPersistence _persistenceFactory = Factory.CreateInstance<IFactoryPersistence>();
            IRepository<Topic, Guid> repo = _persistenceFactory.CreateTopicRepository();
            List<Topic> topics = repo.GetAll();

            Topic firstTopic = topics.First();

            Topic topicById = repo.Get(Guid.NewGuid());

            Assert.IsNull(topicById);
        }

        [TestMethod]
        [ExpectedException(typeof(FaultException<ServiceFault>))]
        public void GetTopicException()
        {

        }
    }
}
