namespace Utilities
{
    using System;

    public static class Maybe
    {
        public static Maybe<T> Some<T>(T value)
        {
            if (ReferenceEquals(value, null))
            {
                throw new ArgumentNullException(nameof(value));
            }

            return Maybe<T>.Some(value);
        }

        public static Maybe<T> None<T>() => Maybe<T>.None();
    }
}
