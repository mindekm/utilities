namespace Utilities
{
    using System;
    using System.Text.RegularExpressions;

    public static class EnsureRegexMatchExtensions
    {
        public static void IsSuccess(this That<Match> that) => that.IsSuccess(Error.EnsureFailure());

        public static void IsSuccess(this in That<Match> that, Exception exception)
        {
            Guard.NotNull(exception, nameof(exception));

            if (!that.Item.Success)
            {
                throw exception;
            }
        }

        public static void IsSuccess(this in That<Group> that) => that.IsSuccess(Error.EnsureFailure());

        public static void IsSuccess(this in That<Group> that, Exception exception)
        {
            Guard.NotNull(exception, nameof(exception));

            if (!that.Item.Success)
            {
                throw exception;
            }
        }

        public static void IsFailure(this in That<Match> that) => that.IsFailure(Error.EnsureFailure());

        public static void IsFailure(this in That<Match> that, Exception exception)
        {
            Guard.NotNull(exception, nameof(exception));

            if (that.Item.Success)
            {
                throw exception;
            }
        }

        public static void IsFailure(this in That<Group> that) => that.IsFailure(Error.EnsureFailure());

        public static void IsFailure(this in That<Group> that, Exception exception)
        {
            Guard.NotNull(exception, nameof(exception));

            if (that.Item.Success)
            {
                throw exception;
            }
        }
    }
}