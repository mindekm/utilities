namespace Utilities
{
    using System;
    using System.Collections.Generic;

    public static class EnsureEqualityExtensions
    {
        public static void IsEqualTo<T>(this in That<T> that, T other)
            => that.IsEqualTo(other, EqualityComparer<T>.Default);

        public static void IsEqualTo<T>(this in That<T> that, T other, IEqualityComparer<T> comparer)
            => that.IsEqualTo(other, Error.EnsureFailure(), comparer);

        public static void IsEqualTo<T>(this in That<T> that, T other, Exception exception)
            => that.IsEqualTo(other, exception, EqualityComparer<T>.Default);

        public static void IsEqualTo<T>(this in That<T> that, T other, Exception exception, IEqualityComparer<T> comparer)
        {
            Guard.NotNull(exception, nameof(exception));
            Guard.NotNull(comparer, nameof(comparer));

            if (!comparer.Equals(that.Item, other))
            {
                throw exception;
            }
        }

        public static void IsNotEqualTo<T>(this in That<T> that, T other)
            => that.IsNotEqualTo(other, EqualityComparer<T>.Default);

        public static void IsNotEqualTo<T>(this in That<T> that, T other, IEqualityComparer<T> comparer)
            => that.IsNotEqualTo(other, Error.EnsureFailure(), comparer);

        public static void IsNotEqualTo<T>(this in That<T> that, T other, Exception exception)
            => that.IsNotEqualTo(other, exception, EqualityComparer<T>.Default);

        public static void IsNotEqualTo<T>(this in That<T> that, T other, Exception exception, IEqualityComparer<T> comparer)
        {
            Guard.NotNull(exception, nameof(exception));
            Guard.NotNull(comparer, nameof(comparer));

            if (comparer.Equals(that.Item, other))
            {
                throw exception;
            }
        }
    }
}