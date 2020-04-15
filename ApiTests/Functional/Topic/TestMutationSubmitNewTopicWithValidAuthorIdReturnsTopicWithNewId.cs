using ApiTests.Functional.Helpers;
using NUnit.Framework;
using System;
using System.Threading.Tasks;

namespace ApiTests.Functional.Topic
{
    [TestFixture]
    [Parallelizable(ParallelScope.All)]
    public class TestMutationSubmitNewTopicWithValidAuthorIdReturnsTopicWithNewId : TestFixture
    {
        [Test]
        public async Task TestThatMutationSubmitNewTopicWithValidAuthorIdReturnsTopicWithNewId()
        {
            string topicTitle = "test title";
            string topicDescription = "test description";

            Guid newAuthorId = await Server.CreateValidAuthor("test author name").ContinueWith(task => task.Result.Data.CreateAuthor.Id);
            TopicsResponse parsedResponse = await Server.CreateTopic<TopicsResponse>(newAuthorId, topicTitle, topicDescription);

            Assert.Multiple(() =>
            {
                Assert.IsNotNull(parsedResponse?.Data?.SubmitTopic, "No topic returned");
                Assert.AreNotEqual(Guid.Empty, parsedResponse.Data.SubmitTopic.Id, "No valid new topicId returned");
                Assert.AreEqual(topicTitle, parsedResponse.Data.SubmitTopic.Title, "Returned topic title does not match");
                Assert.AreEqual(topicDescription, parsedResponse.Data.SubmitTopic.Description, "Returned topic description does not match");
            });
        }
    }
}