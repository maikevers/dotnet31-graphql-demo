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
    public class TestMutationEditTopicWithInvalidTopicIdReturnsError : TestFixture
    {
        [Test]
        public async Task TestThatMutationEditTopicWithInvalidTopicIdReturnsError()
        {
            string query = $@"mutation {{
                                editTopic(authorId: ""{Guid.Empty}"" topic: {{ id: ""{Guid.Empty}"" title: """" description: """"}}){{
                                    ... on Topic {{
                                        id title description
                                    }}
                                    ... on Error {{
                                        errorMessage
                                    }}
                                }}
                            }}";

            HttpResponseMessage response = await Server.PostGraphqlQuery(query);

            response.EnsureSuccessStatusCode();
            string responseString = response.Content.ReadAsStringAsync().GetAwaiter().GetResult();
            TopicsResponseOnError parsedResponse = JsonConvert.DeserializeObject<TopicsResponseOnError>(responseString);

            Assert.IsNotNull(parsedResponse?.Data?.EditTopic?.ErrorMessage, "No error message returned");
        }
    }
}