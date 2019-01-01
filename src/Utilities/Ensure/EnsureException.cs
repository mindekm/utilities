namespace Utilities
{
    using System;
    using System.Runtime.Serialization;

    [Serializable]
    public sealed class EnsureException : Exception
    {
        public EnsureException()
        {
        }

        public EnsureException(string message)
            : base(message)
        {
        }

        public EnsureException(string message, Exception innerException)
            : base(message, innerException)
        {
        }

        private EnsureException(SerializationInfo serializationInfo, StreamingContext context)
            : base(serializationInfo, context)
        {
        }
    }
}