using ApiTests.Functional.Author.Dtos;
using Microsoft.AspNetCore.TestHost;
using Newtonsoft.Json;
using System;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace ApiTests.Functional.Helpers
{
    public static class TestServerExtensions
    {
        public static async Task<HttpResponseMessage> PostGraphqlQuery(this TestServer testServer, string queryString)
        {
            HttpClient client = testServer.CreateClient();
            var postData = new { query = queryString };
            StringContent stringContent = new StringContent(JsonConvert.SerializeObject(postData), Encoding.UTF8, "application/json");
            HttpResponseMessage response = await client.PostAsync("/graphql", stringContent);
            return response;
        }

        public static async Task<AuthorsResponse> CreateValidAuthor(this TestServer testServer, string name)
        {
            string query = $@"
                            mutation {{
                              createAuthor(author: {{name: ""{name}""}}){{
                                ... on Author {{
                                  id
                                  name
                                }}
                                ... on Error {{
                                  errorMessage
                                }}
                              }}
                            }}";

            HttpResponseMessage response = await testServer.PostGraphqlQuery(query);

            response.EnsureSuccessStatusCode();
            string responseString = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<AuthorsResponse>(responseString);
        }

        public static async Task<TOut> CreateTopic<TOut>(this TestServer testServer, Guid authorId, string topicTitle, string topicDescription)
        {
            string query = $@"mutation {{
                                submitTopic(authorId: ""{authorId}"" topic: {{ title: ""{topicTitle}"" description: ""{topicDescription}""}}){{
                                    ... on Topic {{
                                        id title description
                                    }}
                                    ... on Error {{
                                        errorMessage
                                    }}
                                }}
                            }}";

            HttpResponseMessage response = await testServer.PostGraphqlQuery(query);

            response.EnsureSuccessStatusCode();
            string responseString = response.Content.ReadAsStringAsync().GetAwaiter().GetResult();
            TOut parsedResponse = JsonConvert.DeserializeObject<TOut>(responseString);
            return parsedResponse;
        }
    }
}