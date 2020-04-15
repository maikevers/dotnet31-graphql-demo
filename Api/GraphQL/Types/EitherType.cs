using HotChocolate.Types;

namespace Api.GraphQL.Types
{
    public class EitherType<TLeft, TRight> : UnionType
        where TLeft : ObjectType
        where TRight : ObjectType
    {
        protected override void Configure(IUnionTypeDescriptor descriptor)
        {
            descriptor.Type<TLeft>();
            descriptor.Type<TRight>();
        }
    }
}