namespace Utilities
{
    using System;
    using System.ComponentModel;
    using System.Diagnostics;
    using System.Diagnostics.Contracts;
    using System.Runtime.Serialization;

    using Extensions;

    [Serializable]
    public class Result<TFailure> : ISerializable
    {
        private readonly TFailure failureReason;

        protected Result(SerializationInfo serializationInfo, StreamingContext context)
        {
            if (serializationInfo.GetBoolean(nameof(IsSuccess)))
            {
                IsSuccess = true;
            }
            else
            {
                failureReason = (TFailure)serializationInfo.GetValue(nameof(failureReason), typeof(TFailure));
                IsSuccess = false;
            }
        }

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

        public bool IsFailure => !IsSuccess;

        public static implicit operator bool(Result<TFailure> result) => result.IsSuccess;

        public static implicit operator Result<TFailure>(TFailure failure) => new Result<TFailure>(failure);

        public static explicit operator TFailure(Result<TFailure> result)
        {
            if (result.IsFailure)
            {
                return result.failureReason;
            }

            throw Error.InvalidCast(result.ToString(), typeof(TFailure));
        }

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
        public TFailure GetFailureOrDefault() => this.Match(default, failureReason);

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
                () => $"Success<{typeof(TFailure).Name}>",
                failure => $"Failure<{typeof(TFailure).Name}>: {failure}");
        }

        public virtual void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            info.AddValue(nameof(IsSuccess), IsSuccess);

            if (IsFailure)
            {
                info.AddValue(nameof(failureReason), failureReason);
            }
        }

        [EditorBrowsable(EditorBrowsableState.Never)]
        public void Deconstruct(out bool isFailure, out TFailure failure)
            => (isFailure, failure) = (IsFailure, failureReason);
    }
}
