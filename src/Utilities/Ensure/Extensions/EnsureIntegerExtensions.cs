namespace Utilities.Extensions
{
    using System;

    public static class EnsureIntegerExtensions
    {
        public static void IsPositive(this in That<int> that) => that.IsPositive(Error.EnsureFailure());

        public static void IsPositive(this in That<int> that, Exception exception)
        {
            Guard.NotNull(exception, nameof(exception));

            if (that.Item <= 0)
            {
                throw exception;
            }
        }

        public static void IsNegative(this in That<int> that) => that.IsNegative(Error.EnsureFailure());

        public static void IsNegative(this in That<int> that, Exception exception)
        {
            Guard.NotNull(exception, nameof(exception));

            if (that.Item >= 0)
            {
                throw exception;
            }
        }

        public static void IsZero(this in That<int> that) => that.IsZero(Error.EnsureFailure());

        public static void IsZero(this in That<int> that, Exception exception)
        {
            Guard.NotNull(exception, nameof(exception));

            if (that.Item != 0)
            {
                throw exception;
            }
        }
    }
}