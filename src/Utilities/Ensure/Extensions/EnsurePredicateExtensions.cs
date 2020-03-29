namespace Utilities
{
    using System;

    public static class EnsurePredicateExtensions
    {
        public static void IsTrueFor<T>(this in That<T> that, Func<T, bool> predicate)
            => that.IsTrueFor(predicate, new EnsureException());

        public static void IsTrueFor<T>(this in That<T> that, Func<T, bool> predicate, Exception exception)
        {
            Guard.NotNull(predicate, nameof(predicate));
            Guard.NotNull(exception, nameof(exception));

            if (!predicate(that.Item))
            {
                throw exception;
            }
        }

        public static void IsFalseFor<T>(this in That<T> that, Func<T, bool> predicate)
            => that.IsFalseFor(predicate, Error.EnsureFailure());

        public static void IsFalseFor<T>(this in That<T> that, Func<T, bool> predicate, Exception exception)
        {
            Guard.NotNull(predicate, nameof(predicate));
            Guard.NotNull(exception, nameof(exception));

            if (predicate(that.Item))
            {
                throw exception;
            }
        }
    }
}