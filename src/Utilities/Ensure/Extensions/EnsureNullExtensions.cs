namespace Utilities
{
    using System;

    public static class EnsureNullExtensions
    {
        public static void IsNull<T>(this in That<T> that)
            where T : class
        {
            that.IsNull(Error.EnsureFailure());
        }

        public static void IsNull<T>(this in That<T> that, Exception exception)
            where T : class
        {
            Guard.NotNull(exception, nameof(exception));

            if (!(that.Item is null))
            {
                throw exception;
            }
        }

        public static void IsNotNull<T>(this in That<T> that)
            where T : class
        {
            that.IsNotNull(Error.EnsureFailure());
        }

        public static void IsNotNull<T>(this in That<T> that, Exception exception)
            where T : class
        {
            Guard.NotNull(exception, nameof(exception));

            if (that.Item is null)
            {
                throw exception;
            }
        }

        public static void IsNull<T>(this in That<T?> that)
            where T : struct
        {
            that.IsNull(Error.EnsureFailure());
        }

        public static void IsNull<T>(this in That<T?> that, Exception exception)
            where T : struct
        {
            Guard.NotNull(exception, nameof(exception));

            if (that.Item.HasValue)
            {
                throw exception;
            }
        }

        public static void IsNotNull<T>(this in That<T?> that)
            where T : struct
        {
            that.IsNotNull(Error.EnsureFailure());
        }

        public static void IsNotNull<T>(this in That<T?> that, Exception exception)
            where T : struct
        {
            Guard.NotNull(exception, nameof(exception));

            if (!that.Item.HasValue)
            {
                throw exception;
            }
        }
    }
}