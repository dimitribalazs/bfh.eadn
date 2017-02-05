using BFH.EADN.Common.Types.Contracts;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;


namespace BFH.EADN.Persistence.EF.Repositories.Tests
{
    [TestClass()]
    public class TopicRepositoryTests
    {
        private Guid _topicGuid = Guid.NewGuid();
        private TopicRepository _repo;

        [TestInitialize]
        public void TestInit()
        {
            _repo = new TopicRepository();
        }

        [TestCleanup]
        public void TestCleanup()
        {
            _repo.Dispose();
        }

        /// <summary>
        /// If no exception occurs assert is true
        /// </summary>
        [TestMethod()]
        public void CreateTestSuccess()
        {
            //create new topic
            Topic topic = new Topic
            {
                Id = _topicGuid,
                Name = "Test Topic",
                Description = "This is a test topic description"
            };

            _repo.Create(topic);
            Assert.IsTrue(true);
        }

        [TestMethod()]
        public void CreateTestFail()
        {

        }

        [TestMethod]
        public void GetTopicSuccess()
        { 
            Topic firstTopic = GetFirstTopic();
            Topic topicById = _repo.Get(firstTopic.Id);
            Assert.AreEqual(firstTopic.Id, topicById.Id);
        }

        [TestMethod]
        public void UpdateTopicSuccess()
        {
            string newName = "New name for topic";
            string newDescription = "New description for topic";
            Topic topic = GetFirstTopic();
            topic.Name = newName;
            topic.Description = newDescription;

            _repo.Update(topic);

            topic = _repo.Get(topic.Id);
            Assert.AreEqual(newName, topic.Name);
            Assert.AreEqual(newDescription, topic.Description);
        }

        [TestMethod]
        public void DeleteTopicSuccess()
        {
            Topic topic = GetFirstTopic();
            Guid oldFirstTopicId = topic.Id;
            _repo.Delete(topic.Id);
            topic = GetFirstTopic();
            Assert.AreNotEqual(oldFirstTopicId, topic.Id);
        }





        private Topic GetFirstTopic() => _repo.GetAll().First();
        
    }
}