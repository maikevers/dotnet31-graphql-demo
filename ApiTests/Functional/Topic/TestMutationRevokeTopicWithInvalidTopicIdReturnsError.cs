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
    public class TestMutationRevokeTopicWithInvalidTopicIdReturnsError : TestFixture
    {
        [Test]
        public async Task TestThatMutationRevokeTopicWithInvalidTopicIdReturnsError()
        {
            string query = $@"mutation {{
                                revokeTopic(authorId: ""{Guid.Empty}"" topicId: ""{Guid.Empty}""){{
                                    ... on Error {{
                                        errorMessage
                                    }}
                                }}
                            }}";

            HttpResponseMessage response = await Server.PostGraphqlQuery(query);

            response.EnsureSuccessStatusCode();
            string responseString = response.Content.ReadAsStringAsync().GetAwaiter().GetResult();
            TopicsResponseOnError parsedResponse = JsonConvert.DeserializeObject<TopicsResponseOnError>(responseString);

            Assert.IsNotNull(parsedResponse?.Data?.RevokeTopic?.ErrorMessage, "No error message returned");
        }
    }
}