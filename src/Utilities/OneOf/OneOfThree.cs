namespace Utilities
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Runtime.Serialization;
    using Extensions;


    [Serializable]
    public readonly struct OneOf<TFirst, TSecond, TThird> : IEquatable<OneOf<TFirst, TSecond, TThird>>
    {
        private readonly OneOfThree index;

        private readonly TFirst firstValue;

        private readonly TSecond secondValue;

        private readonly TThird thirdValue;

        private OneOf(TFirst firstValue)
            : this(OneOfThree.First, firstValue)
        {
        }

        private OneOf(TSecond secondValue)
            : this(OneOfThree.Second, secondValue: secondValue)
        {
        }

        private OneOf(TThird thirdValue)
            : this(OneOfThree.Third, thirdValue: thirdValue)
        {
        }

        private OneOf(
            OneOfThree index,
            TFirst firstValue = default,
            TSecond secondValue = default,
            TThird thirdValue = default)
        {
            this.index = index;
            this.firstValue = firstValue;
            this.secondValue = secondValue;
            this.thirdValue = thirdValue;
        }

        private enum OneOfThree
        {
            First,
            Second,
            Third,
        }

        private OneOf(SerializationInfo serializationInfo, StreamingContext streamingContext)
        {
            switch ((OneOfThree)serializationInfo.GetValue(nameof(index), typeof(OneOfThree)))
            {
                case OneOfThree.First:
                    index = OneOfThree.First;
                    firstValue = (TFirst)serializationInfo.GetValue(nameof(firstValue), typeof(TFirst));
                    secondValue = default;
                    thirdValue = default;
                    break;
                case OneOfThree.Second:
                    index = OneOfThree.Second;
                    firstValue = default;
                    secondValue = (TSecond)serializationInfo.GetValue(nameof(secondValue), typeof(TSecond));
                    thirdValue = default;
                    break;
                case OneOfThree.Third:
                    index = OneOfThree.Third;
                    firstValue = default;
                    secondValue = default;
                    thirdValue = (TThird)serializationInfo.GetValue(nameof(thirdValue), typeof(TThird));
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        public static implicit operator OneOf<TFirst, TSecond, TThird>(TFirst value)
            => new OneOf<TFirst, TSecond, TThird>(value);

        public static implicit operator OneOf<TFirst, TSecond, TThird>(TSecond value)
            => new OneOf<TFirst, TSecond, TThird>(value);

        public static implicit operator OneOf<TFirst, TSecond, TThird>(TThird value)
            => new OneOf<TFirst, TSecond, TThird>(value);

        public bool IsFirst => index == OneOfThree.First;

        public bool IsSecond => index == OneOfThree.Second;

        public bool IsThird => index == OneOfThree.Third;

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

        public TThird GetThird()
        {
            if (!IsThird)
            {
                throw new InvalidOperationException();
            }

            return thirdValue;
        }

        public bool Equals(OneOf<TFirst, TSecond, TThird> other)
        {
            if (index != other.index)
            {
                return false;
            }

            switch (index)
            {
                case OneOfThree.First:
                    return EqualityComparer<TFirst>.Default.Equals(firstValue, other.firstValue);
                case OneOfThree.Second:
                    return EqualityComparer<TSecond>.Default.Equals(secondValue, other.secondValue);
                case OneOfThree.Third:
                    return EqualityComparer<TThird>.Default.Equals(thirdValue, other.thirdValue);
                default:
                    return false;
            }
        }

        public override bool Equals(object obj)
        {
            switch (obj)
            {
                case OneOf<TFirst, TSecond, TThird> other:
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
                    case OneOfThree.First:
                        hashCode = EqualityComparer<TFirst>.Default.GetHashCode(firstValue);
                        break;
                    case OneOfThree.Second:
                        hashCode = EqualityComparer<TSecond>.Default.GetHashCode(secondValue);
                        break;
                    case OneOfThree.Third:
                        hashCode = EqualityComparer<TThird>.Default.GetHashCode(thirdValue);
                        break;
                }

                return (hashCode * 397) ^ (int)index;
            }
        }

        public override string ToString() => this.Match(first => first.ToString(), second => second.ToString(),
            third => third.ToString());

        public void GetObjectData(SerializationInfo serializationInfo, StreamingContext context)
        {
            serializationInfo.AddValue(nameof(index), index);

            switch (index)
            {
                case OneOfThree.First:
                    serializationInfo.AddValue(nameof(firstValue), firstValue);
                    break;
                case OneOfThree.Second:
                    serializationInfo.AddValue(nameof(secondValue), secondValue);
                    break;
                case OneOfThree.Third:
                    serializationInfo.AddValue(nameof(thirdValue), thirdValue);
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        [EditorBrowsable(EditorBrowsableState.Never)]
        public void Deconstruct(
            out bool isFirst,
            out TFirst first,
            out bool isSecond,
            out TSecond second,
            out bool isThird,
            out TThird third)
            => (isFirst, first, isSecond, second, isThird, third) =
                (IsFirst, firstValue, IsSecond, secondValue, IsThird, thirdValue);
    }
}
