namespace Utilities.Extensions
{
    using System;
    using System.Linq;

    public static class StringExtensions
    {
        private static readonly string[] TrueValues = { "true", "yes", "1" };

        private static readonly string[] FalseValues = { "false", "no", "0" };

        public static bool IsNullOrEmpty(this string item) => string.IsNullOrEmpty(item);

        public static bool IsNullOrWhiteSpace(this string item) => string.IsNullOrWhiteSpace(item);

        public static bool TryConvertToBool(this string item, out bool result)
        {
            Guard.NotNull(item, nameof(item));

            if (TrueValues.Contains(item, StringComparer.OrdinalIgnoreCase))
            {
                result = true;
                return true;
            }

            if (FalseValues.Contains(item, StringComparer.OrdinalIgnoreCase))
            {
                result = false;
                return true;
            }

            result = default;
            return false;
        }
    }
}
