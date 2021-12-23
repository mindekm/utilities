namespace Utilities;

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Diagnostics.Contracts;

public readonly struct Either<TLeft, TRight> : IEquatable<Either<TLeft, TRight>>
{
    private readonly TLeft? leftValue;

    private readonly TRight? rightValue;

    private Either(TLeft leftValue)
    {
        Guard.NotNull(leftValue);

        this.leftValue = leftValue;
        rightValue = default;
        IsLeft = true;
        IsRight = false;
    }

    private Either(TRight rightValue)
    {
        Guard.NotNull(rightValue);

        leftValue = default;
        this.rightValue = rightValue;
        IsLeft = false;
        IsRight = true;
    }

    [MemberNotNullWhen(true, nameof(leftValue))]
    public bool IsLeft { get; }

    [MemberNotNullWhen(true, nameof(rightValue))]
    public bool IsRight { get; }

    public static implicit operator Either<TLeft, TRight>(LeftOption<TLeft> left)
        => new Either<TLeft, TRight>(left.Value);

    public static implicit operator Either<TLeft, TRight>(RightOption<TRight> right)
        => new Either<TLeft, TRight>(right.Value);

    public static bool operator ==(Either<TLeft, TRight> left, Either<TLeft, TRight> right)
        => left.Equals(right);

    public static bool operator !=(Either<TLeft, TRight> left, Either<TLeft, TRight> right)
        => !left.Equals(right);

    [Pure]
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
    public bool TryUnwrapLeft([MaybeNullWhen(false)] out TLeft result)
    {
        if (IsLeft)
        {
            result = leftValue;
            return true;
        }

        result = default;
        return false;
    }

    [Pure]
    [DebuggerStepThrough]
    public TLeft? UnwrapLeftOrDefault() => IsLeft ? leftValue : default;

    [Pure]
    [DebuggerStepThrough]
    public TLeft? UnwrapLeftOr(TLeft? alternative) => IsLeft ? leftValue : alternative;

    [Pure]
    [DebuggerStepThrough]
    public TLeft? UnwrapLeftOrCreate(Func<TLeft?> valueFactory)
    {
        Guard.NotNull(valueFactory);

        return IsLeft ? leftValue : valueFactory();
    }

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
    public bool TryUnwrapRight([MaybeNullWhen(false)] out TRight result)
    {
        if (IsRight)
        {
            result = rightValue;
            return true;
        }

        result = default;
        return false;
    }

    [Pure]
    [DebuggerStepThrough]
    public TRight? UnwrapRightOrDefault() => IsRight ? rightValue : default;

    [Pure]
    [DebuggerStepThrough]
    public TRight? UnwrapRightOr(TRight? alternative) => IsRight ? rightValue : alternative;

    [Pure]
    [DebuggerStepThrough]
    public TRight? UnwrapRightOr(Func<TRight?> valueFactory)
    {
        Guard.NotNull(valueFactory);

        return IsRight ? rightValue : valueFactory();
    }

    public TOut? Match<TOut>(Func<TLeft, TOut?> onLeft, Func<TRight, TOut?> onRight)
    {
        Guard.NotNull(onLeft);
        Guard.NotNull(onRight);

        if (IsRight)
        {
            return onRight(rightValue);
        }

        if (IsLeft)
        {
            return onLeft(leftValue);
        }

        throw UninitializedException();
    }

    public Either<TLeft, TRight> Do(Action onLeft, Action onRight)
    {
        Guard.NotNull(onLeft);
        Guard.NotNull(onRight);

        if (IsRight)
        {
            onRight();
        }

        if (IsLeft)
        {
            onLeft();
        }

        return this;
    }

    public Either<TLeft, TRight> Do(Action<TLeft> onLeft, Action<TRight> onRight)
    {
        Guard.NotNull(onLeft);
        Guard.NotNull(onRight);

        if (IsRight)
        {
            onRight(rightValue);
        }

        if (IsLeft)
        {
            onLeft(leftValue);
        }

        return this;
    }

    public Either<TLeft, TRight> DoOnLeft(Action onLeft)
    {
        Guard.NotNull(onLeft);

        if (IsLeft)
        {
            onLeft();
        }

        return this;
    }

    public Either<TLeft, TRight> DoOnLeft(Action<TLeft> onLeft)
    {
        Guard.NotNull(onLeft);

        if (IsLeft)
        {
            onLeft(leftValue);
        }

        return this;
    }

    public Either<TLeft, TRight> DoOnRight(Action onRight)
    {
        Guard.NotNull(onRight);

        if (IsRight)
        {
            onRight();
        }

        return this;
    }

    public Either<TLeft, TRight> DoOnRight(Action<TRight> onRight)
    {
        Guard.NotNull(onRight);

        if (IsRight)
        {
            onRight(rightValue);
        }

        return this;
    }

    public Either<TLeft, TRight> DoOnBoth(Action onBoth)
    {
        Guard.NotNull(onBoth);

        onBoth();

        return this;
    }

    public Either<TOutLeft, TOutRight> Bind<TOutLeft, TOutRight>(
        Func<TLeft, Either<TOutLeft, TOutRight>> leftBinder,
        Func<TRight, Either<TOutLeft, TOutRight>> rightBinder)
    {
        Guard.NotNull(leftBinder);
        Guard.NotNull(rightBinder);

        if (IsRight)
        {
            return rightBinder(rightValue);
        }

        if (IsLeft)
        {
            return leftBinder(leftValue);
        }

        throw UninitializedException();
    }

    public Either<TLeft, TRight> BindOnLeft(Func<TLeft, Either<TLeft, TRight>> leftBinder)
    {
        Guard.NotNull(leftBinder);

        return IsLeft ? leftBinder(leftValue) : this;
    }

    public Either<TLeft, TRight> BindOnRight(Func<TRight, Either<TLeft, TRight>> rightBinder)
    {
        Guard.NotNull(rightBinder);

        return IsRight ? rightBinder(rightValue) : this;
    }

    public bool Equals(Either<TLeft, TRight> other)
    {
        return IsRight == other.IsRight &&
               IsLeft == other.IsLeft &&
               EqualityComparer<TRight?>.Default.Equals(rightValue, other.rightValue) &&
               EqualityComparer<TLeft?>.Default.Equals(leftValue, other.leftValue);
    }

    public override bool Equals(object? obj) => obj is Either<TLeft, TRight> other && Equals(other);

    public override int GetHashCode() => HashCode.Combine(leftValue, rightValue, IsLeft, IsRight);

    public override string ToString()
    {
        if (IsRight)
        {
            return $"Right<{typeof(TRight).Name}>: {rightValue}";
        }

        if (IsLeft)
        {
            return $"Left<{typeof(TLeft).Name}>: {leftValue}";
        }

        return $"Neither<{typeof(TLeft).Name}, {typeof(TRight).Name}>";
    }

    [EditorBrowsable(EditorBrowsableState.Never)]
    public void Deconstruct(out bool isLeft, out bool isRight)
        => (isLeft, isRight) = (IsLeft, IsRight);

    [EditorBrowsable(EditorBrowsableState.Never)]
    public void Deconstruct(out bool isLeft, out TLeft? left, out bool isRight, out TRight? right)
        => (isLeft, left, isRight, right) = (IsLeft, leftValue, IsRight, rightValue);

    private static Exception UninitializedException() =>
        new InvalidOperationException($"Either<{typeof(TLeft).Name}, {typeof(TRight).Name}> is uninitialized.");
}