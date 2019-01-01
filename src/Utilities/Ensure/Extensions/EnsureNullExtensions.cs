namespace Utilities.Extensions
{
    using System;

    public static class EnsureNullExtensions
    {
        public static void IsNotNull<T>(this in That<T> that)
            where T : class
        {
            that.IsNotNull(new EnsureException());
        }

        public static void IsNotNull<T, TException>(this in That<T> that, TException exception)
            where T : class
            where TException : Exception
        {
            that.IsNotNull(() => exception);
        }

        public static void IsNotNull<T, TException>(this in That<T> that, Func<TException> exceptionFactory)
            where T : class
            where TException : Exception
        {
            that.IsNotNull(_ => exceptionFactory());
        }

        public static void IsNotNull<T, TException>(this in That<T> that, Func<T, TException> exceptionFactory)
            where T : class
            where TException : Exception
        {
            that.Fails(item => item is null, exceptionFactory);
        }

        public static void IsNotNull<T>(this in That<T?> that)
            where T : struct
        {
            that.IsNotNull(new EnsureException());
        }

        public static void IsNotNull<T, TException>(this in That<T?> that, TException exception)
            where T : struct
            where TException : Exception
        {
            that.IsNotNull(() => exception);
        }

        public static void IsNotNull<T, TException>(this in That<T?> that, Func<TException> exceptionFactory)
            where T : struct
            where TException : Exception
        {
            that.IsNotNull(_ => exceptionFactory());
        }

        public static void IsNotNull<T, TException>(this in That<T?> that, Func<T?, TException> exceptionFactory)
            where T : struct
            where TException : Exception
        {
            that.Passes(nullable => nullable.HasValue, exceptionFactory);
        }
    }
}