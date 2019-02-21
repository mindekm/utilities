namespace Utilities.Extensions
{
    using System;

    public static class EnsureBoolExtensions
    {
        public static void IsTrue(this in That<bool> that) => that.IsTrue(Error.EnsureFailure());

        public static void IsTrue(this in That<bool> that, Exception exception)
        {
            Guard.NotNull(exception, nameof(exception));

            if (that.Item is false)
            {
                throw exception;
            }
        }

        public static void IsFalse(this in That<bool> that) => that.IsFalse(Error.EnsureFailure());

        public static void IsFalse(this in That<bool> that, Exception exception)
        {
            Guard.NotNull(exception, nameof(exception));

            if (that.Item is true)
            {
                throw exception;
            }
        }
    }
}
