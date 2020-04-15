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
    public class TestQueryForNewlyCreatedAuthorReturnsThatAuthor : TestFixture
    {
        [Test]
        public async Task TestThatQueryForNewlyCreatedAuthorReturnsThatAuthor()
        {
            string testName = "Mr Bond";
            AuthorsResponse createdParsed = await Server.CreateValidAuthor(testName);

            string queryForNewAuthor = $@"{{author(id: ""{createdParsed.Data.CreateAuthor.Id}"") {{ 	... on Author {{
            id name
			}} }}}}";

            HttpResponseMessage response = await Server.PostGraphqlQuery(queryForNewAuthor);
            response.EnsureSuccessStatusCode();
            string responseString = await response.Content.ReadAsStringAsync();
            AuthorsResponse parsedResponse = JsonConvert.DeserializeObject<AuthorsResponse>(responseString);

            Assert.AreEqual(createdParsed.Data.CreateAuthor.Name, parsedResponse.Data.Author.Name, "Returned author name does not match.");
            Assert.AreEqual(createdParsed.Data.CreateAuthor.Id, parsedResponse.Data.Author.Id, "No new author id returned.");
        }
    }
}