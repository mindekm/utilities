namespace Utilities.Extensions
{
    using System;

    public static class EnsurePredicateExtensions
    {
        public static void Passes<T>(this in That<T> that, Func<T, bool> predicate)
        {
            that.Passes(predicate, new EnsureException());
        }

        public static void Passes<T, TException>(this in That<T> that, Func<T, bool> predicate, TException exception)
            where TException : Exception
        {
            that.Passes(predicate, () => exception);
        }

        public static void Passes<T, TException>(
            this in That<T> that,
            Func<T, bool> predicate,
            Func<TException> exceptionFactory)
            where TException : Exception
        {
            that.Passes(predicate, _ => exceptionFactory());
        }

        public static void Passes<T, TException>(
            this in That<T> that,
            Func<T, bool> predicate,
            Func<T, TException> exceptionFactory)
            where TException : Exception
        {
            if (!predicate(that.Item))
            {
                throw exceptionFactory(that.Item);
            }
        }

        public static void Fails<T>(
            this in That<T> that,
            Func<T, bool> predicate)
        {
            that.Fails(predicate, new EnsureException());
        }

        public static void Fails<T, TException>(
            this in That<T> that,
            Func<T, bool> predicate,
            TException exception)
            where TException : Exception
        {
            that.Fails(predicate, () => exception);
        }

        public static void Fails<T, TException>(
            this in That<T> that,
            Func<T, bool> predicate,
            Func<TException> exceptionFactory)
            where TException : Exception
        {
            that.Fails(predicate, _ => exceptionFactory());
        }

        public static void Fails<T, TException>(
            this in That<T> that,
            Func<T, bool> predicate,
            Func<T, TException> exceptionFactory)
            where TException : Exception
        {
            if (predicate(that.Item))
            {
                throw exceptionFactory(that.Item);
            }
        }
    }
}