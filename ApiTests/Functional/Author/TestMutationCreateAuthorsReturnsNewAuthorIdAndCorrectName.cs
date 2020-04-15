using ApiTests.Functional.Author.Dtos;
using ApiTests.Functional.Helpers;
using NUnit.Framework;
using System;
using System.Threading.Tasks;

namespace ApiTests.Functional.Author
{
    [TestFixture]
    [Parallelizable(ParallelScope.All)]
    public class TestMutationCreateAuthorsReturnsNewAuthorIdAndCorrectName : TestFixture
    {
        [Test]
        public async Task TestThatMutationCreateAuthorsReturnsNewAuthorIdAndCorrectName()
        {
            string testName = "Mr Bond";

            AuthorsResponse parsedResponse = await Server.CreateValidAuthor(testName);

            Assert.AreEqual(testName, parsedResponse.Data.CreateAuthor.Name, "Returned author name does not match.");
            Assert.AreNotEqual(Guid.Empty, parsedResponse.Data.CreateAuthor.Id, "No new author id returned.");
        }
    }
}