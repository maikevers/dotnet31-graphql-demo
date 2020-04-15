using Api.Database.Dtos;
using HotChocolate.Types;

namespace Api.GraphQL.Types
{
    public class ErrorType : ObjectType<Error>
    {
        protected override void Configure(IObjectTypeDescriptor<Error> descriptor)
        {
            descriptor.Name("Error");
            descriptor.Field(x => x.ErrorMessage).Type<NonNullType<StringType>>().Description("Error message.");
            descriptor.IsOfType((context, result) => result is Error);
        }
    }
}