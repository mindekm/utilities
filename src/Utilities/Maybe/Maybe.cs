namespace Utilities
{
    public static class Maybe
    {
        public static NoneOption None { get; } = default;

        public static Maybe<T> Some<T>(T value) => new Maybe<T>(value);
    }
}
