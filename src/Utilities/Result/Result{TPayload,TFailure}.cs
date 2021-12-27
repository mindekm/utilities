namespace Utilities;

using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Diagnostics.Contracts;

public class Result<TPayload, TFailure>
{
    private readonly TPayload? payloadValue;

    private readonly TFailure? failureReason;

    private Result(TPayload payloadValue)
    {
        IsSuccess = true;
        this.payloadValue = payloadValue;
    }

    private Result(TFailure failureReason)
    {
        IsSuccess = false;
        this.failureReason = failureReason;
    }

    [MemberNotNullWhen(true, nameof(payloadValue))]
    public bool IsSuccess { get; }

    [MemberNotNullWhen(true, nameof(failureReason))]
    public bool IsFailure => !IsSuccess;

    public static Result<TPayload, TFailure> Success(TPayload payloadValue)
        => new Result<TPayload, TFailure>(payloadValue);

    public static Result<TPayload, TFailure> Failure(TFailure failureReason)
        => new Result<TPayload, TFailure>(failureReason);

    [Pure]
    [DebuggerStepThrough]
    public TPayload GetValue()
    {
        if (IsSuccess)
        {
            return payloadValue;
        }

        throw new InvalidOperationException($"Cannot retrieve value from {ToString()}");
    }

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
    public bool TryGetValue([MaybeNullWhen(false)] out TPayload value)
    {
        if (IsSuccess)
        {
            value = payloadValue;
            return true;
        }

        value = default;
        return false;
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
    public TPayload? GetValueOrDefault() => IsSuccess ? payloadValue : default;

    [Pure]
    [DebuggerStepThrough]
    public TFailure? GetFailureOrDefault() => IsFailure ? failureReason : default;

    [Pure]
    [DebuggerStepThrough]
    public TPayload? GetValueOr(TPayload? alternative) => IsSuccess ? payloadValue : alternative;

    [Pure]
    [DebuggerStepThrough]
    public TPayload? GetValueOr(Func<TPayload?> valueFactory)
    {
        Guard.NotNull(valueFactory);

        return IsSuccess ? payloadValue : valueFactory();
    }

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
        return IsSuccess
            ? $"Success<{typeof(TPayload).Name},{typeof(TFailure).Name}>: {payloadValue}"
            : $"Failure<{typeof(TPayload).Name},{typeof(TFailure).Name}>: {failureReason}";
    }

    [EditorBrowsable(EditorBrowsableState.Never)]
    public void Deconstruct(out bool isSuccess, out TPayload? payload, out bool isFailure, out TFailure? failure)
        => (isSuccess, payload, isFailure, failure) = (IsSuccess, payloadValue, IsFailure, failureReason);
}