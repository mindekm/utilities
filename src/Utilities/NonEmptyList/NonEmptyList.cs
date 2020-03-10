namespace Utilities
{
    using System;
    using System.Collections.Generic;

    public static class NonEmptyList
    {
        public static bool TryParse<T>(IEnumerable<T> source, out NonEmptyList<T> result)
        {
            Guard.NotNull(source, nameof(source));

            var data = new List<T>(source);
            if (data.Count == 0)
            {
                result = default;
                return false;
            }

            result = new NonEmptyList<T>(data);
            return true;
        }

        public static NonEmptyList<T> Parse<T>(IEnumerable<T> source)
        {
            if (TryParse(source, out var result))
            {
                return result;
            }

            throw new InvalidOperationException("Source enumerable must not be empty.");
        }
    }
}
