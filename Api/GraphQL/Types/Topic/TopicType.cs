using Api.Database.Dtos;
using HotChocolate.Types;

namespace Api.GraphQL.Types.Topic
{
    public class TopicType : ObjectType<TopicDto>
    {
        protected override void Configure(IObjectTypeDescriptor<TopicDto> descriptor)
        {
            descriptor.Name("Topic");
            descriptor.Field(x => x.Id).Type<NonNullType<UuidType>>().Description("The id of the topic.");
            descriptor.Field(x => x.Title).Type<NonNullType<StringType>>().Description("The title of the topic.");
            descriptor.Field(x => x.Description).Type<NonNullType<StringType>>().Description("The description of the topic");
            descriptor.IsOfType((context, result) => result is TopicDto);
        }
    }
}