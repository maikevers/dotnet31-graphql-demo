using ApiTests.Functional.Helpers;
using NUnit.Framework;
using System;
using System.Threading.Tasks;

namespace ApiTests.Functional.Topic
{
    [TestFixture]
    [Parallelizable(ParallelScope.All)]
    public class TestMutationSubmitNewTopicWithInValidAuthorIdReturnsError : TestFixture
    {
        [Test]
        public async Task TestThatMutationSubmitNewTopicWithInValidAuthorIdReturnsError()
        {
            string topicTitle = "test title";
            string topicDescription = "test description";

            TopicsResponseOnError parsedResponse = await Server.CreateTopic<TopicsResponseOnError>(Guid.Empty, topicTitle, topicDescription);

            Assert.IsNotNull(parsedResponse?.Data?.SubmitTopic?.ErrorMessage, "No error message returned");
        }
    }
}