using ApiTests.Functional.Helpers;
using Newtonsoft.Json;
using NUnit.Framework;
using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace ApiTests.Functional.Topic
{
    [TestFixture]
    [Parallelizable(ParallelScope.All)]
    public class TestMutationRevokeTopicWithValidTopicIdRevokesTopic : TestFixture
    {
        [Test]
        public async Task TestThatMutationRevokeTopicWithValidTopicIdRevokesTopic()
        {
            string topicTitle = "test title";
            string topicDescription = "test description";

            Guid newAuthorId = await Server.CreateValidAuthor("test author name").ContinueWith(task => task.Result.Data.CreateAuthor.Id);
            Guid newTopicId = await Server.CreateTopic<TopicsResponse>(newAuthorId, topicTitle, topicDescription).ContinueWith(task => task.Result.Data.SubmitTopic.Id);

            string query = $@"mutation {{
                                revokeTopic(authorId: ""{newAuthorId}"" topicId: ""{newTopicId}""){{
                                    ... on Error {{
                                        errorMessage
                                    }}
                                }}
                            }}";

            HttpResponseMessage response = await Server.PostGraphqlQuery(query);

            response.EnsureSuccessStatusCode();
            string responseString = response.Content.ReadAsStringAsync().GetAwaiter().GetResult();
            TopicsResponse parsedResponse = JsonConvert.DeserializeObject<TopicsResponse>(responseString);

            Assert.IsNotNull(parsedResponse?.Data?.RevokeTopic, "Topic not revoked");
        }
    }
}