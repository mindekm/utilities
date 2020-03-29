namespace Utilities
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Diagnostics;
    using System.Runtime.Serialization;

    [Serializable]
    public readonly struct Either<TLeft, TRight> : IEquatable<Either<TLeft, TRight>>, ISerializable
    {
        private readonly TLeft leftValue;

        private readonly TRight rightValue;

        private readonly EitherState state;

        internal Either(bool isLeft, TLeft leftValue, TRight rightValue)
        {
            state = isLeft ? EitherState.Left : EitherState.Right;
            this.leftValue = leftValue;
            this.rightValue = rightValue;
        }

        private Either(SerializationInfo serializationInfo, StreamingContext context)
        {
            switch ((EitherState)serializationInfo.GetValue(nameof(state), typeof(EitherState)))
            {
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
                    state = EitherState.Neither;
                    leftValue = default;
                    rightValue = default;
                    break;
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

        public static implicit operator Either<TLeft, TRight>(LeftOption<TLeft> left)
            => new Either<TLeft, TRight>(true, left.Value, default);

        public static implicit operator Either<TLeft, TRight>(RightOption<TRight> right)
            => new Either<TLeft, TRight>(false, default, right.Value);

        public static bool operator ==(Either<TLeft, TRight> left, Either<TLeft, TRight> right)
            => left.Equals(right);

        public static bool operator !=(Either<TLeft, TRight> left, Either<TLeft, TRight> right)
            => !left.Equals(right);

        [DebuggerStepThrough]
        public TLeft UnwrapLeft()
        {
            if (IsLeft)
            {
                return leftValue;
            }

            throw new InvalidOperationException(
                $"Failed to unwrap Left<{typeof(TLeft).Name}> value. Either is not in Left<{typeof(TLeft).Name}> state.");
        }

        [DebuggerStepThrough]
        public bool TryUnwrapLeft(out TLeft result)
        {
            if (IsLeft)
            {
                result = leftValue;
                return true;
            }

            result = default;
            return false;
        }

        [DebuggerStepThrough]
        public TLeft UnwrapLeftOrDefault() => IsLeft ? leftValue : default;

        [DebuggerStepThrough]
        public TRight UnwrapRight()
        {
            if (IsRight)
            {
                return rightValue;
            }

            throw new InvalidOperationException(
                $"Failed to unwrap Right<{typeof(TRight).Name}> value. Either is not in Right<{typeof(TRight).Name}> state.");
        }

        [DebuggerStepThrough]
        public bool TryUnwrapRight(out TRight result)
        {
            if (IsRight)
            {
                result = rightValue;
                return true;
            }

            result = default;
            return false;
        }

        [DebuggerStepThrough]
        public TRight UnwrapRightOrDefault() => IsRight ? rightValue : default;

        public TOut Match<TOut>(Func<TLeft, TOut> onLeft, Func<TRight, TOut> onRight)
        {
            Guard.NotNull(onLeft, nameof(onLeft));
            Guard.NotNull(onRight, nameof(onRight));

            return state switch
            {
                EitherState.Left => onLeft(leftValue),
                EitherState.Right => onRight(rightValue),
                _ => throw UninitializedException(),
            };
        }

        public Either<TLeft, TRight> Do(Action onLeft, Action onRight)
        {
            Guard.NotNull(onLeft, nameof(onLeft));
            Guard.NotNull(onRight, nameof(onRight));

            if (IsLeft)
            {
                onLeft();
            }

            if (IsRight)
            {
                onRight();
            }

            return this;
        }

        public Either<TLeft, TRight> Do(Action<TLeft> onLeft, Action<TRight> onRight)
        {
            Guard.NotNull(onLeft, nameof(onLeft));
            Guard.NotNull(onRight, nameof(onRight));

            if (IsLeft)
            {
                onLeft(leftValue);
            }

            if (IsRight)
            {
                onRight(rightValue);
            }

            return this;
        }

        public Either<TLeft, TRight> DoOnLeft(Action onLeft)
        {
            Guard.NotNull(onLeft, nameof(onLeft));

            if (IsLeft)
            {
                onLeft();
            }

            return this;
        }

        public Either<TLeft, TRight> DoOnLeft(Action<TLeft> onLeft)
        {
            Guard.NotNull(onLeft, nameof(onLeft));

            if (IsLeft)
            {
                onLeft(leftValue);
            }

            return this;
        }

        public Either<TLeft, TRight> DoOnRight(Action onRight)
        {
            Guard.NotNull(onRight, nameof(onRight));

            if (IsRight)
            {
                onRight();
            }

            return this;
        }

        public Either<TLeft, TRight> DoOnRight(Action<TRight> onRight)
        {
            Guard.NotNull(onRight, nameof(onRight));

            if (IsRight)
            {
                onRight(rightValue);
            }

            return this;
        }

        public Either<TLeft, TRight> DoOnBoth(Action onBoth)
        {
            Guard.NotNull(onBoth, nameof(onBoth));

            onBoth();

            return this;
        }

        public Either<TOutLeft, TOutRight> Bind<TOutLeft, TOutRight>(
            Func<TLeft, Either<TOutLeft, TOutRight>> leftBinder,
            Func<TRight, Either<TOutLeft, TOutRight>> rightBinder)
        {
            Guard.NotNull(leftBinder, nameof(leftBinder));
            Guard.NotNull(rightBinder, nameof(rightBinder));

            return state switch
            {
                EitherState.Left => leftBinder(leftValue),
                EitherState.Right => rightBinder(rightValue),
                _ => throw UninitializedException(),
            };
        }

        public Either<TLeft, TRight> BindOnLeft(Func<TLeft, Either<TLeft, TRight>> leftBinder)
        {
            Guard.NotNull(leftBinder, nameof(leftBinder));

            return IsLeft ? leftBinder(leftValue) : this;
        }

        public Either<TLeft, TRight> BindOnRight(Func<TRight, Either<TLeft, TRight>> rightBinder)
        {
            Guard.NotNull(rightBinder, nameof(rightBinder));

            return IsRight ? rightBinder(rightValue) : this;
        }

        public bool Equals(Either<TLeft, TRight> other)
        {
            return state switch
            {
                EitherState.Left => other.IsLeft && EqualityComparer<TLeft>.Default.Equals(leftValue, other.leftValue),
                EitherState.Right => other.IsRight && EqualityComparer<TRight>.Default.Equals(rightValue, other.rightValue),
                _ => state == other.state,
            };
        }

        public override bool Equals(object obj) => obj is Either<TLeft, TRight> other && Equals(other);

        public override int GetHashCode()
        {
            unchecked
            {
                return state switch
                {
                    EitherState.Left => (EqualityComparer<TLeft>.Default.GetHashCode(leftValue) * 397) ^ (int)state,
                    EitherState.Right => (EqualityComparer<TRight>.Default.GetHashCode(rightValue) * 397) ^ (int)state,
                    _ => 0,
                };
            }
        }

        public override string ToString()
        {
            var types = $"<{typeof(TLeft).Name}, {typeof(TRight).Name}>";

            return state switch
            {
                EitherState.Left => $"Left{types}: {leftValue}",
                EitherState.Right => $"Right{types}: {rightValue}",
                _ => $"Neither{types}",
            };
        }

        public void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            info.AddValue(nameof(state), state);

            switch (state)
            {
                case EitherState.Left:
                    info.AddValue(nameof(leftValue), leftValue);
                    break;
                case EitherState.Right:
                    info.AddValue(nameof(rightValue), rightValue);
                    break;
            }
        }

        [EditorBrowsable(EditorBrowsableState.Never)]
        public void Deconstruct(out bool isLeft, out bool isRight)
            => (isLeft, isRight) = (IsLeft, IsRight);

        [EditorBrowsable(EditorBrowsableState.Never)]
        public void Deconstruct(out bool isLeft, out TLeft leftValue, out bool isRight, out TRight rightValue)
            => (isLeft, leftValue, isRight, rightValue) = (IsLeft, this.leftValue, IsRight, this.rightValue);

        private static Exception UninitializedException() =>
            new InvalidOperationException($"Either<{typeof(TLeft).Name}, {typeof(TRight).Name}> is uninitialized.");
    }
}
