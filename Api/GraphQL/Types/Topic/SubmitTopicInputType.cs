using HotChocolate.Types;

namespace Api.GraphQL.Types.Topic
{
    public class SubmitTopic
    {
        public string Title { get; set; }
        public string Description { get; set; }
    }

    public class SubmitTopicInputType : InputObjectType<SubmitTopic>
    {
        protected override void Configure(IInputObjectTypeDescriptor<SubmitTopic> descriptor)
        {
            descriptor.Name("SubmitTopicInput");
            descriptor.Field(t => t.Title).Type<NonNullType<StringType>>();
            descriptor.Field(t => t.Description).Type<NonNullType<StringType>>();
        }
    }
}