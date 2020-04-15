using ApiTests.Functional.Helpers;
using Newtonsoft.Json;
using NUnit.Framework;
using System.Net.Http;
using System.Threading.Tasks;

namespace ApiTests.Functional.Topic
{
    [TestFixture]
    [Parallelizable(ParallelScope.All)]
    public class TestQueryForTopicsGetsNonEmptyListOfTopics : TestFixture
    {
        [Test]
        public async Task TestThatQueryForTopicsGetsNonEmptyListOfTopics()
        {
            string query = @"
                        {
                          topics {
                            id title description
                          }
                        }";

            HttpResponseMessage response = await Server.PostGraphqlQuery(query);

            response.EnsureSuccessStatusCode();
            string responseString = response.Content.ReadAsStringAsync().GetAwaiter().GetResult();
            TopicsResponse parsedResponse = JsonConvert.DeserializeObject<TopicsResponse>(responseString);

            Assert.IsNotNull(parsedResponse?.Data?.Topics, "Nothing returned.");
            Assert.IsNotEmpty(parsedResponse.Data.Topics, "No topics found.");
        }
    }
}