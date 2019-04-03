namespace Utilities
{
    using System;
    using System.ComponentModel;
    using System.Diagnostics;
    using System.Diagnostics.Contracts;
    using System.Runtime.Serialization;

    using Extensions;

    [Serializable]
    public class Result<TPayload, TFailure> : ISerializable
    {
        private readonly TPayload payloadValue;

        private readonly TFailure failureReason;

        protected Result(SerializationInfo serializationInfo, StreamingContext context)
        {
            if (serializationInfo.GetBoolean(nameof(IsSuccess)))
            {
                payloadValue = (TPayload)serializationInfo.GetValue(nameof(payloadValue), typeof(TPayload));
                IsSuccess = true;
            }
            else
            {
                failureReason = (TFailure)serializationInfo.GetValue(nameof(failureReason), typeof(TFailure));
                IsSuccess = false;
            }
        }

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

        public bool IsSuccess { get; }

        public bool IsFailure => !IsSuccess;

        public static implicit operator bool(Result<TPayload, TFailure> result) => result.IsSuccess;

        public static implicit operator Result<TPayload, TFailure>(TPayload payloadValue)
            => new Result<TPayload, TFailure>(payloadValue);

        public static implicit operator Result<TPayload, TFailure>(TFailure failureReason)
            => new Result<TPayload, TFailure>(failureReason);

        public static explicit operator TPayload(Result<TPayload, TFailure> result)
        {
            if (result.IsSuccess)
            {
                return result.payloadValue;
            }

            throw Error.InvalidCast(result.ToString(), typeof(TPayload));
        }

        public static explicit operator TFailure(Result<TPayload, TFailure> result)
        {
            if (result.IsFailure)
            {
                return result.failureReason;
            }

            throw Error.InvalidCast(result.ToString(), typeof(TFailure));
        }

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

        [Pure]
        [DebuggerStepThrough]
        public bool TryGetValue(out TPayload value)
        {
            if (IsSuccess)
            {
                value = payloadValue;
                return true;
            }

            value = default;
            return false;
        }

        [Pure]
        [DebuggerStepThrough]
        public bool TryGetFailure(out TFailure failure)
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
        public TPayload GetValueOrDefault() => this.Match(payloadValue, default);

        [Pure]
        [DebuggerStepThrough]
        public TFailure GetFailureOrDefault() => this.Match(default, failureReason);

        [Pure]
        [DebuggerStepThrough]
        public TPayload GetValueOr(TPayload alternative) => this.Match(payloadValue, alternative);

        [Pure]
        [DebuggerStepThrough]
        public TPayload GetValueOr(Func<TPayload> alternativeFactory)
        {
            Guard.NotNull(alternativeFactory, nameof(alternativeFactory));

            return this.Match(alternativeFactory(), payloadValue);
        }

        [Pure]
        [DebuggerStepThrough]
        public TFailure GetFailureOr(TFailure alternative) => this.Match(alternative, failureReason);

        [Pure]
        [DebuggerStepThrough]
        public TFailure GetFailureOr(Func<TFailure> alternativeFactory)
        {
            Guard.NotNull(alternativeFactory, nameof(alternativeFactory));

            return this.Match(alternativeFactory(), failureReason);
        }

        public override string ToString()
        {
            return this.Match(
                payload => $"Success<{typeof(TPayload).Name}, {typeof(TFailure).Name}>: {payload}",
                failure => $"Failure<{typeof(TPayload).Name}, {typeof(TFailure).Name}>: {failure}");
        }

        public virtual void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            info.AddValue(nameof(IsSuccess), IsSuccess);

            if (IsSuccess)
            {
                info.AddValue(nameof(payloadValue), payloadValue);
            }
            else
            {
                info.AddValue(nameof(failureReason), failureReason);
            }
        }

        [EditorBrowsable(EditorBrowsableState.Never)]
        public void Deconstruct(out bool isSuccess, out TPayload payload, out bool isFailure, out TFailure failure)
            => (isSuccess, payload, isFailure, failure) = (IsSuccess, payloadValue, IsFailure, failureReason);
    }
}
