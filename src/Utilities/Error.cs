namespace Utilities
{
    using System;

    internal static class Error
    {
        private const string NoParameterMessage = "Name not available";

        public static ArgumentNullException NullArgument(string parameterName = NoParameterMessage)
            => new ArgumentNullException(parameterName, $"Parameter [{parameterName}] can not be null.");

        public static ArgumentException EmptyStringArgument(string parameterName = NoParameterMessage)
            => new ArgumentException($"Parameter [{parameterName}] can not be empty or whitespace.", parameterName);

        public static InvalidOperationException TooManyElements()
            => new InvalidOperationException("Sequence contains more than one element.");

        public static InvalidCastException InvalidCast(string source, string target)
            => new InvalidCastException($"Casting {source} to {target} is not possible.");

        public static InvalidCastException InvalidCast(string source, Type target)
            => InvalidCast(source, target.Name);
    }
}