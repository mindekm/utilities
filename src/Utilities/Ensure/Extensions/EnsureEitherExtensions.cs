namespace Utilities.Extensions
{
    using System;

    public static class EnsureEitherExtensions
    {
        public static void IsLeft<TLeft, TRight>(this in That<Either<TLeft, TRight>> that)
            => that.IsLeft(Error.EnsureFailure());

        public static void IsLeft<TLeft, TRight>(this in That<Either<TLeft, TRight>> that, Exception exception)
        {
            Guard.NotNull(exception, nameof(exception));

            if (!that.Item.IsLeft)
            {
                throw exception;
            }
        }

        public static void IsRight<TLeft, TRight>(this in That<Either<TLeft, TRight>> that)
            => that.IsRight(Error.EnsureFailure());

        public static void IsRight<TLeft, TRight>(this in That<Either<TLeft, TRight>> that, Exception exception)
        {
            Guard.NotNull(exception, nameof(exception));

            if (!that.Item.IsRight)
            {
                throw exception;
            }
        }

        public static void IsInitialized<TLeft, TRight>(this in That<Either<TLeft, TRight>> that)
            => that.IsInitialized(Error.EnsureFailure());

        public static void IsInitialized<TLeft, TRight>(this in That<Either<TLeft, TRight>> that, Exception exception)
        {
            Guard.NotNull(exception, nameof(exception));

            if (!that.Item.IsLeft && !that.Item.IsRight)
            {
                throw exception;
            }
        }
    }
}
