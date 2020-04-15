using HotChocolate.Types;
using LanguageExt;

namespace Api.GraphQL.Types
{
    public class UnitType : ObjectType<Unit>
    {
        protected override void Configure(IObjectTypeDescriptor<Unit> descriptor)
        {
            descriptor.Name("Unit");
            descriptor.Field(x => x.ToString()).Type<NonNullType<StringType>>().Name("Unit").Description("Unit");
            descriptor.BindFieldsExplicitly();
        }
    }
}