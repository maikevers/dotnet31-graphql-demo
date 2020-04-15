using Api.Database.Dtos;
using Api.GraphQL.Types.Topic;

namespace Api.GraphQL.Types.TypeConverters
{
    public static class SubmitTopicToTopicConverter
    {
        public static TopicDto ToTopicDto(this SubmitTopic submitTopic)
        {
            return new TopicDto { Title = submitTopic.Title, Description = submitTopic.Description };
        }
    }
}