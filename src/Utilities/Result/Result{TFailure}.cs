namespace Utilities;

using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Diagnostics.Contracts;

public class Result<TFailure>
{
    private readonly TFailure? failureReason;

    private Result()
    {
        IsSuccess = true;
    }

    private Result(TFailure failureReason)
    {
        this.failureReason = failureReason;
        IsSuccess = false;
    }

    public bool IsSuccess { get; }

    [MemberNotNullWhen(true, nameof(failureReason))]
    public bool IsFailure => !IsSuccess;

    public static Result<TFailure> Success() => new Result<TFailure>();

    public static Result<TFailure> Failure(TFailure failureReason) => new Result<TFailure>(failureReason);

    [Pure]
    [DebuggerStepThrough]
    public TFailure GetFailure()
    {
        if (IsFailure)
        {
            return failureReason;
        }

        throw new InvalidOperationException($"Cannot retrieve failure from {ToString()}");
    }

    [DebuggerStepThrough]
    public bool TryGetFailure([MaybeNullWhen(false)] out TFailure failure)
    {
        if (IsFailure)
        {
            failure = failureReason;
            return true;
        }

        failure = default;
        return false;
    }

    [Pure]
    [DebuggerStepThrough]
    public TFailure? GetFailureOrDefault() => IsFailure ? failureReason : default;

    [Pure]
    [DebuggerStepThrough]
    public TFailure? GetFailureOr(TFailure? alternative) => IsFailure ? failureReason : alternative;

    [Pure]
    [DebuggerStepThrough]
    public TFailure? GetFailureOr(Func<TFailure?> valueFactory)
    {
        Guard.NotNull(valueFactory);

        return IsFailure ? failureReason : valueFactory();
    }

    public override string ToString()
    {
        return IsFailure
            ? $"Failure<{typeof(TFailure).Name}>: {failureReason}"
            : $"Success<{typeof(TFailure).Name}>";
    }

    [EditorBrowsable(EditorBrowsableState.Never)]
    public void Deconstruct(out bool isFailure, out TFailure? failure)
        => (isFailure, failure) = (IsFailure, failureReason);

    [EditorBrowsable(EditorBrowsableState.Never)]
    public void Deconstruct(out bool isSuccess, out bool isFailure, out TFailure? failure)
        => (isSuccess, isFailure, failure) = (IsSuccess, IsFailure, failureReason);
}