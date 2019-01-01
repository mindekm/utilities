namespace Utilities.Extensions
{
    using System;

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
    }
}