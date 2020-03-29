namespace Utilities
{
    public static class EitherBuilder
    {
        public static Either<TLeft, TRight> Left<TLeft, TRight>(TLeft value)
            => Either.Left(value);

        public static Either<TLeft, TRight> Right<TLeft, TRight>(TRight value)
            => Either.Right(value);
    }
}