namespace Utilities
{
    public static class MaybeBuilder
    {
        public static Maybe<T> Some<T>(T value) => Maybe.Some(value);

        public static Maybe<T> None<T>() => default;
    }
}