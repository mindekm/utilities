namespace Utilities
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Runtime.Serialization;
    using Extensions;

    [Serializable]
    public readonly struct OneOf<TFirst, TSecond, TThird, TFourth> : IEquatable<OneOf<TFirst, TSecond, TThird, TFourth>>
    {
        private readonly OneOfFour index;

        private readonly TFirst firstValue;

        private readonly TSecond secondValue;

        private readonly TThird thirdValue;

        private readonly TFourth fourthValue;

        private OneOf(TFirst firstValue)
            : this(OneOfFour.First, firstValue)
        {
        }

        private OneOf(TSecond secondValue)
            : this(OneOfFour.Second, secondValue: secondValue)
        {
        }

        private OneOf(TThird thirdValue)
            : this(OneOfFour.Third, thirdValue: thirdValue)
        {
        }

        public OneOf(TFourth fourthValue)
            : this(OneOfFour.Fourth, fourthValue: fourthValue)
        {
        }

        private OneOf(
            OneOfFour index,
            TFirst firstValue = default,
            TSecond secondValue = default,
            TThird thirdValue = default,
            TFourth fourthValue = default)
        {
            this.index = index;
            this.firstValue = firstValue;
            this.secondValue = secondValue;
            this.thirdValue = thirdValue;
            this.fourthValue = fourthValue;
        }

        private enum OneOfFour
        {
            First,
            Second,
            Third,
            Fourth
        }

        public static implicit operator OneOf<TFirst, TSecond, TThird, TFourth>(TFirst value)
            => new OneOf<TFirst, TSecond, TThird, TFourth>(value);

        public static implicit operator OneOf<TFirst, TSecond, TThird, TFourth>(TSecond value)
            => new OneOf<TFirst, TSecond, TThird, TFourth>(value);

        public static implicit operator OneOf<TFirst, TSecond, TThird, TFourth>(TThird value)
            => new OneOf<TFirst, TSecond, TThird, TFourth>(value);

        public static implicit operator OneOf<TFirst, TSecond, TThird, TFourth>(TFourth value)
            => new OneOf<TFirst, TSecond, TThird, TFourth>(value);

        public bool IsFirst => index == OneOfFour.First;

        public bool IsSecond => index == OneOfFour.Second;

        public bool IsThird => index == OneOfFour.Third;

        public bool IsFourth => index == OneOfFour.Fourth;

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

        public TFourth GetFourth()
        {
            if (!IsFourth)
            {
                throw new InvalidOperationException();
            }

            return fourthValue;
        }

        public bool Equals(OneOf<TFirst, TSecond, TThird, TFourth> other)
        {
            if (index != other.index)
            {
                return false;
            }

            switch (index)
            {
                case OneOfFour.First:
                    return EqualityComparer<TFirst>.Default.Equals(firstValue, other.firstValue);
                case OneOfFour.Second:
                    return EqualityComparer<TSecond>.Default.Equals(secondValue, other.secondValue);
                case OneOfFour.Third:
                    return EqualityComparer<TThird>.Default.Equals(thirdValue, other.thirdValue);
                case OneOfFour.Fourth:
                    return EqualityComparer<TFourth>.Default.Equals(fourthValue, other.fourthValue);
                default:
                    return false;
            }
        }

        public override bool Equals(object obj)
        {
            switch (obj)
            {
                case OneOf<TFirst, TSecond, TThird, TFourth> other:
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
                    case OneOfFour.First:
                        hashCode = EqualityComparer<TFirst>.Default.GetHashCode(firstValue);
                        break;
                    case OneOfFour.Second:
                        hashCode = EqualityComparer<TSecond>.Default.GetHashCode(secondValue);
                        break;
                    case OneOfFour.Third:
                        hashCode = EqualityComparer<TThird>.Default.GetHashCode(thirdValue);
                        break;
                    case OneOfFour.Fourth:
                        hashCode = EqualityComparer<TFourth>.Default.GetHashCode(fourthValue);
                        break;
                }

                return (hashCode * 397) ^ (int)index;
            }
        }

        public override string ToString() => this.Match(first => first.ToString(), second => second.ToString(),
            third => third.ToString(), fourth => fourth.ToString());

        public void GetObjectData(SerializationInfo serializationInfo, StreamingContext context)
        {
            serializationInfo.AddValue(nameof(index), index);

            switch (index)
            {
                case OneOfFour.First:
                    serializationInfo.AddValue(nameof(firstValue), firstValue);
                    break;
                case OneOfFour.Second:
                    serializationInfo.AddValue(nameof(secondValue), secondValue);
                    break;
                case OneOfFour.Third:
                    serializationInfo.AddValue(nameof(thirdValue), thirdValue);
                    break;
                case OneOfFour.Fourth:
                    serializationInfo.AddValue(nameof(fourthValue), fourthValue);
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
            out TThird third,
            out bool isFourth,
            out TFourth fourth)
            => (isFirst, first, isSecond, second, isThird, third, isFourth, fourth) =
                (IsFirst, firstValue, IsSecond, secondValue, IsThird, thirdValue, IsFourth, fourthValue);
    }
}
