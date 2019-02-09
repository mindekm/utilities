namespace Utilities.Extensions
{
    using System;

    public static class EnsureBoolExtensions
    {
        public static void IsTrue(this in That<bool> that)
        {
            that.IsTrue(new EnsureException());
        }

        public static void IsTrue<TException>(this in That<bool> that, TException exception)
            where TException : Exception
        {
            Guard.NotNull(exception, nameof(exception));

            that.IsTrue(() => exception);
        }

        public static void IsTrue<TException>(this in That<bool> that, Func<TException> exceptionFactory)
            where TException : Exception
        {
            Guard.NotNull(exceptionFactory, nameof(exceptionFactory));

            that.IsTrue(_ => exceptionFactory());
        }

        public static void IsTrue<TException>(this in That<bool> that, Func<bool, TException> exceptionFactory)
            where TException : Exception
        {
            Guard.NotNull(exceptionFactory, nameof(exceptionFactory));

            that.Passes(value => value is true, exceptionFactory);
        }

        public static void IsFalse(this in That<bool> that)
        {
            that.IsFalse(new EnsureException());
        }

        public static void IsFalse<TException>(this in That<bool> that, TException exception)
            where TException : Exception
        {
            Guard.NotNull(exception, nameof(exception));

            that.IsFalse(() => exception);
        }

        public static void IsFalse<TException>(this in That<bool> that, Func<TException> exceptionFactory)
            where TException : Exception
        {
            Guard.NotNull(exceptionFactory, nameof(exceptionFactory));

            that.IsFalse(_ => exceptionFactory());
        }

        public static void IsFalse<TException>(this in That<bool> that, Func<bool, TException> exceptionFactory)
            where TException : Exception
        {
            Guard.NotNull(exceptionFactory, nameof(exceptionFactory));

            that.Passes(value => value is false, exceptionFactory);
        }
    }
}
