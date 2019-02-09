namespace Utilities.Extensions
{
    using System;
    using System.Collections.Generic;
    using System.Runtime.InteropServices.ComTypes;

    public static class EnsureStringExtensions
    {
        public static void IsNotNullOrEmpty(this in That<string> that)
        {
            that.IsNotNull();
        }

        public static void IsNotNullOrEmpty<TException>(this in That<string> that, TException exception)
            where TException : Exception
        {

        }

        public static void IsNotNullOrEmpty<TException>(this in That<string> that, Func<TException> exceptionFactory)
            where TException : Exception
        {

        }

        public static void IsNotNullOrEmpty<TException>(this in That<string> that, Func<string, TException> exceptionFactory)
            where TException : Exception
        {

        }

        public static void DoesNotContain<TException>(this in That<string> that, string target)
            where TException : Exception
        {
            that.DoesNotContain(target, new EnsureException());
        }

        public static void DoesNotContain<TException>(this in That<string> that, string target, TException exception)
            where TException : Exception
        {
            Guard.NotNull(exception, nameof(exception));

            that.DoesNotContain(target, () => exception);
        }

        public static void DoesNotContain<TException>(this in That<string> that,
            string target,
            Func<TException> exceptionFactory)
            where TException : Exception
        {
            Guard.NotNull(exceptionFactory, nameof(exceptionFactory));

            that.DoesNotContain(target, _ => exceptionFactory());
        }

        public static void DoesNotContain<TException>(this in That<string> that, string target, Func<string, TException> exceptionFactory)
            where TException : Exception
        {
            Guard.NotNull(exceptionFactory, nameof(exceptionFactory));

            that.Fails(value => value.Contains(target), exceptionFactory);
        }
    }
}