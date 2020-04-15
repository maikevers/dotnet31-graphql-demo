using ApiTests.Functional.Author.Dtos;
using ApiTests.Functional.Helpers;
using Newtonsoft.Json;
using NUnit.Framework;
using System.Net.Http;
using System.Threading.Tasks;

namespace ApiTests.Functional.Author
{
    [TestFixture]
    [Parallelizable(ParallelScope.All)]
    public class TestQueryForAuthorsGetsListOfAuthors : TestFixture
    {
        [Test]
        public async Task TestThatQueryForAuthorsGetsListOfAuthors()
        {
            string query = @"
                        {
                          authors {
                            name
                          }
                        }";

            HttpResponseMessage response = await Server.PostGraphqlQuery(query);

            response.EnsureSuccessStatusCode();
            string responseString = await response.Content.ReadAsStringAsync();
            AuthorsResponse parsedResponse = JsonConvert.DeserializeObject<AuthorsResponse>(responseString);
            Assert.IsNotNull(parsedResponse.Data.Authors, "No list of authors returned.");
        }
    }
}