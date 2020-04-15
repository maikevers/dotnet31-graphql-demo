using HotChocolate.Types;

namespace Api.GraphQL.Types.Author
{
    public class CreateAuthor
    {
        public string Name { get; set; }
    }

    public class CreateAuthorInputType : InputObjectType<CreateAuthor>
    {
        protected override void Configure(IInputObjectTypeDescriptor<CreateAuthor> descriptor)
        {
            descriptor.Name("CreateAuthorInput");
            descriptor.Field(t => t.Name).Type<NonNullType<StringType>>();
        }
    }
}