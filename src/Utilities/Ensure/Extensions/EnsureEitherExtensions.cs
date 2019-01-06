namespace Utilities.Extensions
{
    using System;

    public static class EnsureEitherExtensions
    {
        public static void IsLeft<TLeft, TRight>(this in That<Either<TLeft, TRight>> that)
            => that.IsLeft(new EnsureException());

        public static void IsLeft<TLeft, TRight, TException>(
            this in That<Either<TLeft, TRight>> that,
            TException exception)
            where TException : Exception
        {
            Guard.NotNull(exception, nameof(exception));

            that.IsLeft(() => exception);
        }

        public static void IsLeft<TLeft, TRight, TException>(
            this in That<Either<TLeft, TRight>> that,
            Func<TException> exceptionFactory)
            where TException : Exception
        {
            Guard.NotNull(exceptionFactory, nameof(exceptionFactory));

            that.IsLeft(_ => exceptionFactory());
        }

        public static void IsLeft<TLeft, TRight, TException>(
            this in That<Either<TLeft, TRight>> that,
            Func<Either<TLeft, TRight>, TException> exceptionFactory)
            where TException : Exception
        {
            Guard.NotNull(exceptionFactory, nameof(exceptionFactory));

            that.Passes(either => either.IsLeft, exceptionFactory);
        }

        public static void IsRight<TLeft, TRight>(this in That<Either<TLeft, TRight>> that)
            => that.IsRight(new EnsureException());

        public static void IsRight<TLeft, TRight, TException>(
            this in That<Either<TLeft, TRight>> that,
            TException exception)
            where TException : Exception
        {
            Guard.NotNull(exception, nameof(exception));

            that.IsRight(() => exception);
        }

        public static void IsRight<TLeft, TRight, TException>(
            this in That<Either<TLeft, TRight>> that,
            Func<TException> exceptionFactory)
            where TException : Exception
        {
            Guard.NotNull(exceptionFactory, nameof(exceptionFactory));

            that.IsRight(_ => exceptionFactory());
        }

        public static void IsRight<TLeft, TRight, TException>(
            this in That<Either<TLeft, TRight>> that,
            Func<Either<TLeft, TRight>, TException> exceptionFactory)
            where TException : Exception
        {
            Guard.NotNull(exceptionFactory, nameof(exceptionFactory));

            that.Passes(either => either.IsRight, exceptionFactory);
        }

        public static void IsInitialized<TLeft, TRight>(this in That<Either<TLeft, TRight>> that)
            => that.IsInitialized(new EnsureException());

        public static void IsInitialized<TLeft, TRight, TException>(
            this in That<Either<TLeft, TRight>> that,
            TException exception)
            where TException : Exception
        {
            Guard.NotNull(exception, nameof(exception));

            that.IsInitialized(() => exception);
        }

        public static void IsInitialized<TLeft, TRight, TException>(
            this in That<Either<TLeft, TRight>> that,
            Func<TException> exceptionFactory)
            where TException : Exception
        {
            Guard.NotNull(exceptionFactory, nameof(exceptionFactory));

            that.IsInitialized(_ => exceptionFactory());
        }

        public static void IsInitialized<TLeft, TRight, TException>(
            this in That<Either<TLeft, TRight>> that,
            Func<Either<TLeft, TRight>, TException> exceptionFactory)
            where TException : Exception
        {
            Guard.NotNull(exceptionFactory, nameof(exceptionFactory));

            that.Passes(either => either.IsLeft || either.IsRight, exceptionFactory);
        }
    }
}
