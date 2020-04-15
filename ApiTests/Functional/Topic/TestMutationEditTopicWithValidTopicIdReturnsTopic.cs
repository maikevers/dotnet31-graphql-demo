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
    public class TestMutationEditTopicWithValidTopicIdReturnsTopic : TestFixture
    {
        [Test]
        public async Task TestThatMutationEditTopicWithValidTopicIdReturnsTopic()
        {
            string topicTitleBeforeEdit = "title before edit";
            string topicTitleAfterEdit = "title after edit";
            string topicDescriptionBeforeEdit = "description before edit";
            string topicDescriptionAfterEdit = "description after edit";

            Guid newAuthorId = await Server.CreateValidAuthor("test author name").ContinueWith(task => task.Result.Data.CreateAuthor.Id);
            Guid newTopicId = await Server.CreateTopic<TopicsResponse>(newAuthorId, topicTitleBeforeEdit, topicDescriptionBeforeEdit).ContinueWith(task => task.Result.Data.SubmitTopic.Id);

            string query = $@"mutation {{
                                editTopic(authorId: ""{newAuthorId}"" topic: {{ id: ""{newTopicId}"" title: ""{topicTitleAfterEdit}"" description: ""{topicDescriptionAfterEdit}""}}){{
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
            TopicsResponse parsedResponse = JsonConvert.DeserializeObject<TopicsResponse>(responseString);

            Assert.Multiple(() =>
            {
                Assert.IsNotNull(parsedResponse?.Data?.EditTopic, "No topic returned");
                Assert.AreEqual(newTopicId, parsedResponse.Data.EditTopic.Id, "Returned topicId does not match");
                Assert.AreEqual(topicTitleAfterEdit, parsedResponse.Data.EditTopic.Title, "Returned topic title does not match");
                Assert.AreEqual(topicDescriptionAfterEdit, parsedResponse.Data.EditTopic.Description, "Returned topic description does not match");
            });
        }
    }
}