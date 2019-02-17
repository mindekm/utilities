namespace Utilities.Extensions
{
    using System;

    public static class EnsureBoolExtensions
    {
        public static void IsTrue(this in That<bool> that) => that.IsTrue(new EnsureException());

        public static void IsTrue(this in That<bool> that, Exception exception)
        {
            Guard.NotNull(exception, nameof(exception));

            if (that.Item is false)
            {
                throw exception;
            }
        }

        public static void IsFalse(this in That<bool> that) => that.IsFalse(new EnsureException());

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
