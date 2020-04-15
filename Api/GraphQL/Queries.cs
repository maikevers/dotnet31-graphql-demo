using Api.Database;
using Api.Database.Dtos;
using Api.GraphQL.Types;
using Api.GraphQL.Types.Author;
using HotChocolate;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Api.GraphQL
{
    public class Queries
    {
        [GraphQLNonNullType]
        public async Task<IReadOnlyList<AuthorDto>> GetAuthors([Service] Context context) => await context.GetAllAuthorsAsync();

        [GraphQLNonNullType]
        [GraphQLType(typeof(EitherType<ErrorType, AuthorType>))]
        public async Task<object> GetAuthor([Service] Context context, [GraphQLNonNullType] Guid id) => await context.GetAuthorAsync(id).ContinueWith(t => t.Result.Unwrap(), TaskContinuationOptions.OnlyOnRanToCompletion);

        [GraphQLNonNullType]
        public async Task<IReadOnlyList<TopicDto>> GetTopics([Service] Context repo) => await repo.GetAllTopicsAsync();
    }
}