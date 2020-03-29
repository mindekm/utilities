namespace Utilities
{
    using System;
    using System.Collections.Generic;

    public static class EnsureCollectionExtensions
    {
        public static void IsEmpty<T>(this in That<ICollection<T>> that) => that.IsEmpty(Error.EnsureFailure());

        public static void IsEmpty<T>(this in That<ICollection<T>> that, Exception exception)
        {
            Guard.NotNull(exception, nameof(exception));

            if (that.Item.Count != 0)
            {
                throw exception;
            }
        }

        public static void IsNotEmpty<T>(this in That<ICollection<T>> that) => that.IsNotEmpty(Error.EnsureFailure());

        public static void IsNotEmpty<T>(this in That<ICollection<T>> that, Exception exception)
        {
            Guard.NotNull(exception, nameof(exception));

            if (that.Item.Count == 0)
            {
                throw exception;
            }
        }
    }
}