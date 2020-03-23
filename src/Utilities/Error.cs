namespace Utilities
{
    using System;

    internal static class Error
    {
        public static InvalidCastException InvalidCast(string source, string target)
            => new InvalidCastException($"Casting {source} to {target} is not possible.");

        public static InvalidCastException InvalidCast(string source, Type target)
            => InvalidCast(source, target.Name);

        public static EnsureException EnsureFailure()
            => new EnsureException("Ensure condition failed.");
    }
}
