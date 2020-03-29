namespace Utilities
{
    using System;
    using System.Collections.Generic;

    [Obsolete]
    public static class StringExtensions
    {
        private static readonly HashSet<string> TrueValues = new HashSet<string> { "TRUE", "T", "YES", "1" };

        private static readonly HashSet<string> FalseValues = new HashSet<string> { "FALSE", "F", "NO", "0" };

        public static bool IsNullOrEmpty(this string item) => string.IsNullOrEmpty(item);

        public static bool IsNullOrWhiteSpace(this string item) => string.IsNullOrWhiteSpace(item);

        public static bool TryConvertToBool(this string item, out bool result)
        {
            Guard.NotNull(item, nameof(item));

            if (TrueValues.Contains(item.ToUpperInvariant()))
            {
                result = true;
                return true;
            }

            if (FalseValues.Contains(item.ToUpperInvariant()))
            {
                result = false;
                return true;
            }

            result = default;
            return false;
        }
    }
}
