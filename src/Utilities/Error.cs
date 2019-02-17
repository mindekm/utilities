namespace Utilities
{
    using System;

    internal static class Error
    {
        public static ArgumentNullException NullArgument(string parameterName)
            => new ArgumentNullException(parameterName, $"Parameter [{parameterName}] can not be null.");

        public static ArgumentException EmptyStringArgument(string parameterName)
            => new ArgumentException($"Parameter [{parameterName}] can not be empty or whitespace.", parameterName);

        public static ArgumentException EmptyCollectionArgument(string parameterName)
            => new ArgumentException($"Parameter [{parameterName}] can not be an empty collection.", parameterName);

        public static InvalidOperationException TooManyElements()
            => new InvalidOperationException("Sequence contains more than one element.");

        public static InvalidCastException InvalidCast(string source, string target)
            => new InvalidCastException($"Casting {source} to {target} is not possible.");

        public static InvalidCastException InvalidCast(string source, Type target)
            => InvalidCast(source, target.Name);

        public static EnsureException EnsureFailure()
            => new EnsureException("Ensure condition failed.");
    }
}