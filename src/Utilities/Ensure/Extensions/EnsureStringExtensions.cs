namespace Utilities
{
    using System;

    public static class EnsureStringExtensions
    {
        public static void IsNotNullOrEmpty(this in That<string> that)
            => that.IsNotNullOrEmpty(Error.EnsureFailure());

        public static void IsNotNullOrEmpty(this in That<string> that, Exception exception)
        {
            Guard.NotNull(exception, nameof(exception));

            that.IsNotNull(exception);
            that.IsNotEmpty(exception);
        }

        public static void IsEmpty(this in That<string> that)
            => that.IsEmpty(Error.EnsureFailure());

        public static void IsEmpty(this in That<string> that, Exception exception)
        {
            Guard.NotNull(exception, nameof(exception));

            if (that.Item.Trim().Length != 0)
            {
                throw exception;
            }
        }

        public static void IsNotEmpty(this in That<string> that)
            => that.IsNotEmpty(Error.EnsureFailure());

        public static void IsNotEmpty(this in That<string> that, Exception exception)
        {
            Guard.NotNull(exception, nameof(exception));

            if (that.Item.Trim().Length == 0)
            {
                throw exception;
            }
        }

        public static void Contains(this in That<string> that, string value)
            => that.Contains(value, Error.EnsureFailure());

        public static void Contains(this in That<string> that, string value, Exception exception)
        {
            Guard.NotNull(exception, nameof(exception));
            Guard.NotNull(value, nameof(value));

            if (!that.Item.Contains(value))
            {
                throw exception;
            }
        }

        public static void DoesNotContain(this in That<string> that, string value)
            => that.DoesNotContain(value, Error.EnsureFailure());

        public static void DoesNotContain(this in That<string> that, string value, Exception exception)
        {
            Guard.NotNull(exception, nameof(exception));
            Guard.NotNull(value, nameof(value));

            if (that.Item.Contains(value))
            {
                throw exception;
            }
        }

        public static void DoesNotContainWhitespace(this in That<string> that)
            => that.DoesNotContainWhitespace(Error.EnsureFailure());

        public static void DoesNotContainWhitespace(this in That<string> that, Exception exception)
        {
            Guard.NotNull(exception, nameof(exception));

            foreach (var character in that.Item)
            {
                if (char.IsWhiteSpace(character))
                {
                    throw exception;
                }
            }
        }
    }
}