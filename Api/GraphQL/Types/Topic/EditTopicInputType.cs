using Api.Database.Dtos;
using HotChocolate.Types;

namespace Api.GraphQL.Types.Topic
{
    public class EditTopic : TopicDto { }

    public class EditTopicInputType : InputObjectType<EditTopic>
    {
        protected override void Configure(IInputObjectTypeDescriptor<EditTopic> descriptor)
        {
            descriptor.Name("EditTopicInput");
            descriptor.Field(x => x.Id).Type<NonNullType<UuidType>>().Description("The id of the topic.");
            descriptor.Field(x => x.Title).Type<NonNullType<StringType>>().Description("The title of the topic.");
            descriptor.Field(x => x.Description).Type<NonNullType<StringType>>().Description("The description of the topic");
        }
    }
}