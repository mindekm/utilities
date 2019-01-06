namespace Utilities
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Diagnostics;
    using System.Diagnostics.Contracts;
    using System.Runtime.Serialization;

    [Serializable]
    public readonly struct Either<TLeft, TRight> : IEquatable<Either<TLeft, TRight>>, ISerializable
    {
        private readonly TLeft leftValue;

        private readonly TRight rightValue;

        private readonly EitherState state;

        private Either(TLeft leftValue)
            : this(EitherState.Left, leftValue)
        {
        }

        private Either(TRight rightValue)
            : this(EitherState.Right, rightValue: rightValue)
        {
        }

        private Either(EitherState state, TLeft leftValue = default, TRight rightValue = default)
        {
            this.state = state;
            this.leftValue = leftValue;
            this.rightValue = rightValue;
        }

        private Either(SerializationInfo serializationInfo, StreamingContext context)
        {
            switch ((EitherState)serializationInfo.GetValue(nameof(state), typeof(EitherState)))
            {
                case EitherState.Neither:
                    state = EitherState.Neither;
                    leftValue = default;
                    rightValue = default;
                    break;
                case EitherState.Left:
                    state = EitherState.Left;
                    leftValue = (TLeft)serializationInfo.GetValue(nameof(leftValue), typeof(TLeft));
                    rightValue = default;
                    break;
                case EitherState.Right:
                    state = EitherState.Right;
                    leftValue = default;
                    rightValue = (TRight)serializationInfo.GetValue(nameof(rightValue), typeof(TRight));
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        private enum EitherState
        {
            Neither,
            Left,
            Right,
        }

        public bool IsLeft => state == EitherState.Left;

        public bool IsRight => state == EitherState.Right;

        public static implicit operator Either<TLeft, TRight>(TLeft leftValue)
            => new Either<TLeft, TRight>(leftValue);

        public static implicit operator Either<TLeft, TRight>(TRight rightValue)
            => new Either<TLeft, TRight>(rightValue);

        public static explicit operator TLeft(in Either<TLeft, TRight> either)
        {
            if (either.IsLeft)
            {
                return either.leftValue;
            }

            throw Error.InvalidCast(nameof(either.ToString), typeof(TLeft));
        }

        public static explicit operator TRight(in Either<TLeft, TRight> either)
        {
            if (either.IsRight)
            {
                return either.rightValue;
            }

            throw Error.InvalidCast(nameof(either.ToString), typeof(TRight));
        }

        public static bool operator ==(in Either<TLeft, TRight> left, in Either<TLeft, TRight> right) => left.Equals(right);

        public static bool operator !=(in Either<TLeft, TRight> left, in Either<TLeft, TRight> right) => !left.Equals(right);

        public static Either<TLeft, TRight> Left(TLeft leftValue) => leftValue;

        public static Either<TLeft, TRight> Right(TRight rightValue) => rightValue;

        [Pure]
        [DebuggerStepThrough]
        public TLeft GetLeft()
        {
            if (IsLeft)
            {
                return leftValue;
            }

            throw Error.EitherRetrievalFailure("left", ToString());
        }

        [Pure]
        [DebuggerStepThrough]
        public TRight GetRight()
        {
            if (IsRight)
            {
                return rightValue;
            }

            throw Error.EitherRetrievalFailure("right", ToString());
        }

        [Pure]
        [DebuggerStepThrough]
        public bool TryGetLeft(out TLeft left)
        {
            if (IsLeft)
            {
                left = leftValue;
                return true;
            }

            left = default;
            return false;
        }

        [Pure]
        [DebuggerStepThrough]
        public bool TryGetRight(out TRight right)
        {
            if (IsRight)
            {
                right = rightValue;
                return true;
            }

            right = default;
            return false;
        }

        [Pure]
        [DebuggerStepThrough]
        public TLeft GetLeftOrDefault() => GetLeftOr(default(TLeft));

        [Pure]
        [DebuggerStepThrough]
        public TRight GetRightOrDefault() => GetRightOr(default(TRight));

        [Pure]
        [DebuggerStepThrough]
        public TLeft GetLeftOr(TLeft alternative) => GetLeftOr(() => alternative);

        [Pure]
        [DebuggerStepThrough]
        public TLeft GetLeftOr(Func<TLeft> alternativeFactory)
        {
            Guard.NotNull(alternativeFactory, nameof(alternativeFactory));

            return IsLeft ? leftValue : alternativeFactory();
        }

        [Pure]
        [DebuggerStepThrough]
        public TRight GetRightOr(TRight alternative) => GetRightOr(() => alternative);

        [Pure]
        [DebuggerStepThrough]
        public TRight GetRightOr(Func<TRight> alternativeFactory)
        {
            Guard.NotNull(alternativeFactory, nameof(alternativeFactory));

            return IsRight ? rightValue : alternativeFactory();
        }

        public bool Equals(Either<TLeft, TRight> other)
        {
            switch (state)
            {
                case EitherState.Neither:
                    return state == other.state
                           && EqualityComparer<TLeft>.Default.Equals(leftValue, other.leftValue)
                           && EqualityComparer<TRight>.Default.Equals(rightValue, other.rightValue);
                case EitherState.Left:
                    return state == other.state
                           && EqualityComparer<TLeft>.Default.Equals(leftValue, other.leftValue);
                case EitherState.Right:
                    return state == other.state
                           && EqualityComparer<TRight>.Default.Equals(rightValue, other.rightValue);
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        public override bool Equals(object obj)
        {
            switch (obj)
            {
                case Either<TLeft, TRight> other:
                    return Equals(other);
                default:
                    return false;
            }
        }

        public override int GetHashCode()
        {
            unchecked
            {
                switch (state)
                {
                    case EitherState.Neither:
                        return 0;
                    case EitherState.Left:
                        return (EqualityComparer<TLeft>.Default.GetHashCode(leftValue) * 397) ^ (int)state;
                    case EitherState.Right:
                        return (EqualityComparer<TRight>.Default.GetHashCode(rightValue) * 397) ^ (int)state;
                    default:
                        throw new ArgumentOutOfRangeException();
                }
            }
        }

        public override string ToString()
        {
            var types = $"<{typeof(TLeft)}, {typeof(TRight)}>";

            switch (state)
            {
                case EitherState.Neither:
                    return $"Neither{types}";
                case EitherState.Left:
                    return $"Left{types}: {leftValue}";
                case EitherState.Right:
                    return $"Right{types}: {rightValue}";
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        public void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            info.AddValue(nameof(state), state);

            switch (state)
            {
                case EitherState.Neither:
                    break;
                case EitherState.Left:
                    info.AddValue(nameof(leftValue), leftValue);
                    break;
                case EitherState.Right:
                    info.AddValue(nameof(rightValue), rightValue);
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        [EditorBrowsable(EditorBrowsableState.Never)]
        public void Deconstruct(out bool isLeft, out TLeft left, out bool isRight, out TRight right)
            => (isLeft, left, isRight, right) = (IsLeft, leftValue, IsRight, rightValue);
    }
}
