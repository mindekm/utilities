namespace Utilities
{
    using System;
    using System.ComponentModel;
    using System.Runtime.Serialization;

    [Serializable]
    public class FailureDetails<T> : ISerializable
    {
        public FailureDetails(T details, FailureLevel level = FailureLevel.Error)
        {
            if (ReferenceEquals(details, null))
            {
                throw Error.NullArgument(nameof(details));
            }

            Details = details;
            Level = level;
        }

        protected FailureDetails(SerializationInfo serializationInfo, StreamingContext context)
        {
            Details = (T)serializationInfo.GetValue(nameof(Details), typeof(T));
            Level = (FailureLevel)serializationInfo.GetValue(nameof(Level), typeof(FailureLevel));
        }

        public T Details { get; }

        public FailureLevel Level { get; }

        public override string ToString()
        {
            return $"[{Level}] {Details}";
        }

        public virtual void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            info.AddValue(nameof(Details), Details);
            info.AddValue(nameof(Level), Level);
        }

        [EditorBrowsable(EditorBrowsableState.Never)]
        public void Deconstruct(out FailureLevel level, out T details) => (level, details) = (Level, Details);
    }
}
