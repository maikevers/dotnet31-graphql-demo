using Api.Database.Dtos;
using Api.GraphQL.Types.Topic;
using HotChocolate.Types;

namespace Api.GraphQL.Types.Author
{
    public class AuthorType : ObjectType<AuthorDto>
    {
        protected override void Configure(IObjectTypeDescriptor<AuthorDto> descriptor)
        {
            descriptor.Name("Author");
            descriptor.Field(x => x.Id).Type<NonNullType<UuidType>>().Description("The id of the author.");
            descriptor.Field(x => x.Name).Type<NonNullType<StringType>>().Description("The name of the author.");
            descriptor.Field(x => x.Topics).Type<NonNullType<ListType<NonNullType<TopicType>>>>().Description("Topics of the author.");
            descriptor.IsOfType((context, result) => result is AuthorDto);
        }
    }
}