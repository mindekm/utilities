namespace Utilities
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Runtime.Serialization;
    using Extensions;

    [Serializable]
    public readonly struct OneOf<TFirst, TSecond> : IEquatable<OneOf<TFirst, TSecond>>
    {
        private readonly OneOfTwo index;

        private readonly TFirst firstValue;

        private readonly TSecond secondValue;

        private OneOf(TFirst firstValue)
            : this(OneOfTwo.First, firstValue)
        {
        }

        private OneOf(TSecond secondValue)
            : this(OneOfTwo.Second, secondValue: secondValue)
        {
        }

        private OneOf(OneOfTwo index, TFirst firstValue = default, TSecond secondValue = default)
        {
            this.index = index;
            this.firstValue = firstValue;
            this.secondValue = secondValue;
        }

        private enum OneOfTwo
        {
            First,
            Second,
        }

        private OneOf(SerializationInfo serializationInfo, StreamingContext context)
        {
            switch ((OneOfTwo)serializationInfo.GetValue(nameof(index), typeof(OneOfTwo)))
            {
                case OneOfTwo.First:
                    index = OneOfTwo.First;
                    firstValue = (TFirst)serializationInfo.GetValue(nameof(firstValue), typeof(TFirst));
                    secondValue = default;
                    break;
                case OneOfTwo.Second:
                    index = OneOfTwo.Second;
                    firstValue = default;
                    secondValue = (TSecond)serializationInfo.GetValue(nameof(secondValue), typeof(TSecond));
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        public static implicit operator OneOf<TFirst, TSecond>(TFirst value) => new OneOf<TFirst, TSecond>(value);

        public static implicit operator OneOf<TFirst, TSecond>(TSecond value) => new OneOf<TFirst, TSecond>(value);

        public static bool operator ==(in OneOf<TFirst, TSecond> left, OneOf<TFirst, TSecond> right) => left.Equals(right);

        public static bool operator !=(OneOf<TFirst, TSecond> left, OneOf<TFirst, TSecond> right) => !left.Equals(right);

        public bool IsFirst => index == OneOfTwo.First;

        public bool IsSecond => index == OneOfTwo.Second;

        public TFirst GetFirst()
        {
            if (!IsFirst)
            {
                throw new InvalidOperationException();
            }

            return firstValue;
        }

        public TSecond GetSecond()
        {
            if (!IsSecond)
            {
                throw new InvalidOperationException();
            }

            return secondValue;
        }

        public bool Equals(OneOf<TFirst, TSecond> other)
        {
            if (index != other.index)
            {
                return false;
            }

            switch (index)
            {
                case OneOfTwo.First:
                    return EqualityComparer<TFirst>.Default.Equals(firstValue, other.firstValue);
                case OneOfTwo.Second:
                    return EqualityComparer<TSecond>.Default.Equals(secondValue, other.secondValue);
                default:
                    return false;
            }
        }

        public override bool Equals(object obj)
        {
            switch (obj)
            {
                case OneOf<TFirst, TSecond> other:
                    return Equals(other);
                default:
                    return false;
            }
        }

        public override int GetHashCode()
        {
            unchecked
            {
                var hashCode = 0;
                switch (index)
                {
                    case OneOfTwo.First:
                        hashCode = EqualityComparer<TFirst>.Default.GetHashCode(firstValue);
                        break;
                    case OneOfTwo.Second:
                        hashCode = EqualityComparer<TSecond>.Default.GetHashCode(secondValue);
                        break;
                }

                return (hashCode * 397) ^ (int)index;
            }
        }

        public override string ToString() => this.Match(first => first.ToString(), second => second.ToString());

        public void GetObjectData(SerializationInfo serializationInfo, StreamingContext context)
        {
            serializationInfo.AddValue(nameof(index), index);

            switch (index)
            {
                case OneOfTwo.First:
                    serializationInfo.AddValue(nameof(firstValue), firstValue);
                    break;
                case OneOfTwo.Second:
                    serializationInfo.AddValue(nameof(secondValue), secondValue);
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        [EditorBrowsable(EditorBrowsableState.Never)]
        public void Deconstruct(out bool isFirst, out TFirst first, out bool isSecond, out TSecond second)
            => (isFirst, first, isSecond, second) = (IsFirst, firstValue, IsSecond, secondValue);
    }
}
