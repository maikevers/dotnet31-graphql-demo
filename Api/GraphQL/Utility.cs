using LanguageExt;

namespace Api.GraphQL
{
    public static class Utility
    {
        public static object Unwrap<TLeft, TRight>(this Either<TLeft, TRight> either)
        {
            return either.Match(right => right as object, left => left as object);
        }

        public static Either<TLeft, Unit> ToUnit<TLeft, TRight>(this Either<TLeft, TRight> either)
        {
            return either.Map(right => new Unit());
        }
    }
}