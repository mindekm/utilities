namespace Utilities
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Diagnostics;
    using System.Diagnostics.Contracts;
    using System.Runtime.Serialization;

    using Extensions;

    [Serializable]
    public readonly struct Maybe<T> : IEquatable<Maybe<T>>, IEquatable<T>, ISerializable
    {
        private readonly T value;

        private Maybe(T value)
        {
            this.value = value;
            IsSome = true;
        }

        private Maybe(SerializationInfo serializationInfo, StreamingContext context)
        {
            if (serializationInfo.GetBoolean(nameof(IsSome)))
            {
                value = (T)serializationInfo.GetValue(nameof(value), typeof(T));
                IsSome = true;
            }
            else
            {
                value = default;
                IsSome = false;
            }
        }

        public bool IsSome { get; }

        public bool IsNone => !IsSome;

        public static implicit operator Maybe<T>(T value) => ReferenceEquals(value, null) ? default : new Maybe<T>(value);

        public static explicit operator T(in Maybe<T> maybe)
            => maybe.IsSome ? maybe.value : throw Error.InvalidCast(maybe.ToString(), typeof(T));

        public static bool operator ==(in Maybe<T> left, in Maybe<T> right) => left.Equals(right);

        public static bool operator !=(in Maybe<T> left, in Maybe<T> right) => !left.Equals(right);

        public static bool operator ==(in Maybe<T> left, T right) => left.Equals(right);

        public static bool operator !=(in Maybe<T> left, T right) => !left.Equals(right);

        public static bool operator ==(T left, in Maybe<T> right) => right.Equals(left);

        public static bool operator !=(T left, in Maybe<T> right) => !right.Equals(left);

        public static Maybe<T> Some(T value) => new Maybe<T>(value);

        public static Maybe<T> None() => default;

        [Pure]
        [DebuggerStepThrough]
        public T GetValue()
        {
            if (IsSome)
            {
                return value;
            }

            throw new InvalidOperationException($"Cannot retrieve value from {ToString()}");
        }

        [Pure]
        [DebuggerStepThrough]
        public bool TryGetValue(out T result)
        {
            if (IsSome)
            {
                result = value;
                return true;
            }

            result = default;
            return false;
        }

        [Pure]
        [DebuggerStepThrough]
        public T GetValueOrDefault() => this.Match(value, default);

        [Pure]
        [DebuggerStepThrough]
        public T GetValueOr(T alternative) => this.Match(value, alternative);

        [Pure]
        [DebuggerStepThrough]
        public T GetValueOr(Func<T> alternativeFactory)
        {
            Guard.NotNull(alternativeFactory, nameof(alternativeFactory));

            return this.Match(value, alternativeFactory());
        }

        public bool Equals(Maybe<T> other)
            => IsSome == other.IsSome && EqualityComparer<T>.Default.Equals(value, other.value);

        public bool Equals(T other) => EqualityComparer<T>.Default.Equals(value, other);

        public override bool Equals(object obj)
        {
            switch (obj)
            {
                case null:
                    return IsNone;
                case Maybe<T> other:
                    return Equals(other);
                case T other:
                    return Equals(other);
                default:
                    return false;
            }
        }

        public override int GetHashCode() => IsSome ? EqualityComparer<T>.Default.GetHashCode(value) : 0;

        public override string ToString() => IsSome ? value.ToString() : $"None<{typeof(T).Name}>";

        public void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            info.AddValue(nameof(IsSome), IsSome);
            if (IsSome)
            {
                info.AddValue(nameof(value), value);
            }
        }

        [EditorBrowsable(EditorBrowsableState.Never)]
        public void Deconstruct(out bool isSome, out T maybeValue) => (isSome, maybeValue) = (IsSome, value);
    }
}
