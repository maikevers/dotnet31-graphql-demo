using Api.Database;
using Api.Database.Dtos;
using Api.GraphQL.Types;
using Api.GraphQL.Types.Author;
using Api.GraphQL.Types.Topic;
using Api.GraphQL.Types.TypeConverters;
using HotChocolate;
using LanguageExt;
using System;
using System.Threading.Tasks;

namespace Api.GraphQL
{
    public class Mutations
    {
        [GraphQLNonNullType]
        [GraphQLName("createAuthor")]
        [GraphQLType(typeof(EitherType<ErrorType, AuthorType>))]
        public async Task<object> CreateAuthor([Service] Context context, [GraphQLNonNullType] CreateAuthor author)
        {
            return await context.AddAuthorAsync(new AuthorDto { Name = author.Name }).ContinueWith(task => task.MapAsync(authorId =>
                         context.GetAuthorAsync(authorId)), TaskContinuationOptions.OnlyOnRanToCompletion)
                             .Flatten().ContinueWith(t => t.Result.Flatten().Unwrap());
        }

        [GraphQLNonNullType]
        [GraphQLName("submitTopic")]
        [GraphQLType(typeof(EitherType<ErrorType, TopicType>))]
        public async Task<object> SubmitTopic([Service] Context context, [GraphQLNonNullType] Guid authorId, [GraphQLNonNullType] SubmitTopic topic)
        {
            TopicDto topicDto = topic.ToTopicDto();
            return await context.AddTopicAsync(authorId, topicDto).ContinueWith(task => task.MapAsync(topicId =>
                         context.GetTopicAsync(authorId, topicId)), TaskContinuationOptions.OnlyOnRanToCompletion)
                             .Unwrap().ContinueWith(t => t.Result.Flatten().Unwrap());
        }

        [GraphQLNonNullType]
        [GraphQLName("editTopic")]
        [GraphQLType(typeof(EitherType<ErrorType, TopicType>))]
        public async Task<object> EditTopic([Service] Context context, [GraphQLNonNullType] Guid authorId, [GraphQLNonNullType] EditTopic topic)
        {
            return await context.UpdateTopicAsync(authorId, topic).ContinueWith(task =>
                         task.Result.Unwrap(), TaskContinuationOptions.OnlyOnRanToCompletion);
        }

        [GraphQLNonNullType]
        [GraphQLName("revokeTopic")]
        [GraphQLType(typeof(EitherType<ErrorType, UnitType>))]
        public async Task<object> RevokeTopic([Service] Context context, [GraphQLNonNullType] Guid authorId, [GraphQLNonNullType] Guid topicId)
        {
            return await context.DeleteTopicAsync(authorId, topicId).ContinueWith(task =>
                         task.Result.Unwrap(), TaskContinuationOptions.OnlyOnRanToCompletion);
        }
    }
}