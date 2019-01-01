namespace Utilities.Extensions
{
    using System.Collections.Generic;

    public static class EnsureEqualityExtensions
    {
        public static void IsEqualTo<T>(this in That<T> that, T other)
        {
            that.IsEqualTo(other, EqualityComparer<T>.Default);
        }

        public static void IsEqualTo<T>(this in That<T> that, T other, IEqualityComparer<T> comparer)
        {
            Guard.NotNull(comparer, nameof(comparer));

            that.Passes(value => comparer.Equals(value, other));
        }

        public static void IsNotEqualTo<T>(this in That<T> that, T other)
        {
            that.IsNotEqualTo(other, EqualityComparer<T>.Default);
        }

        public static void IsNotEqualTo<T>(this in That<T> that, T other, IEqualityComparer<T> comparer)
        {
            Guard.NotNull(comparer, nameof(comparer));

            that.Fails(value => comparer.Equals(value, other));
        }
    }
}