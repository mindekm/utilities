namespace Utilities
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Diagnostics;
    using System.Runtime.Serialization;

    [Serializable]
    public readonly struct Maybe<T> : IEquatable<Maybe<T>>, ISerializable
    {
        private readonly T value;

        internal Maybe(T value)
        {
            this.value = value;
            IsSome = true;
        }

        private Maybe(SerializationInfo serializationInfo, StreamingContext context)
        {
            if (serializationInfo.GetBoolean(nameof(IsSome)))
            {
                value = (T)serializationInfo.GetValue(nameof(IsSome), typeof(T));
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

        public static implicit operator Maybe<T>(NoneOption none) => default;

        public static bool operator ==(Maybe<T> left, Maybe<T> right) => left.Equals(right);

        public static bool operator !=(Maybe<T> left, Maybe<T> right) => !left.Equals(right);

        [DebuggerStepThrough]
        public T Unwrap()
        {
            if (IsSome)
            {
                return value;
            }

            throw new InvalidOperationException($"Cannot unwrap None<{typeof(T).Name}>.");
        }

        [DebuggerStepThrough]
        public bool TryUnwrap(out T result)
        {
            if (IsSome)
            {
                result = value;
                return true;
            }

            result = default;
            return false;
        }

        [DebuggerStepThrough]
        public T UnwrapOrDefault() => IsSome ? value : default;

        public TOut Match<TOut>(Func<T, TOut> onSome, Func<TOut> onNone)
        {
            Guard.NotNull(onSome, nameof(onSome));
            Guard.NotNull(onNone, nameof(onNone));

            return IsSome ? onSome(value) : onNone();
        }

        public Maybe<T> Do(Action onSome, Action onNone)
        {
            Guard.NotNull(onSome, nameof(onSome));
            Guard.NotNull(onNone, nameof(onNone));

            if (IsSome)
            {
                onSome();
            }
            else
            {
                onNone();
            }

            return this;
        }

        public Maybe<T> Do(Action<T> onSome, Action onNone)
        {
            Guard.NotNull(onSome, nameof(onSome));
            Guard.NotNull(onNone, nameof(onNone));

            if (IsSome)
            {
                onSome(value);
            }
            else
            {
                onNone();
            }

            return this;
        }

        public Maybe<T> DoOnSome(Action onSome)
        {
            Guard.NotNull(onSome, nameof(onSome));

            if (IsSome)
            {
                onSome();
            }

            return this;
        }

        public Maybe<T> DoOnSome(Action<T> onSome)
        {
            Guard.NotNull(onSome, nameof(onSome));

            if (IsSome)
            {
                onSome(value);
            }

            return this;
        }

        public Maybe<T> DoOnNone(Action onNone)
        {
            Guard.NotNull(onNone, nameof(onNone));

            if (IsNone)
            {
                onNone();
            }

            return this;
        }

        public Maybe<T> DoOnBoth(Action onBoth)
        {
            Guard.NotNull(onBoth, nameof(onBoth));

            onBoth();

            return this;
        }

        public Maybe<TOut> Bind<TOut>(Func<T, Maybe<TOut>> someBinder, Func<Maybe<TOut>> noneBinder)
        {
            Guard.NotNull(someBinder, nameof(someBinder));
            Guard.NotNull(noneBinder, nameof(noneBinder));

            return IsSome ? someBinder(value) : noneBinder();
        }

        public Maybe<TOut> BindOnSome<TOut>(Func<T, Maybe<TOut>> binder)
        {
            Guard.NotNull(binder, nameof(binder));

            return IsSome ? binder(value) : Maybe.None;
        }

        public Maybe<T> BindOnNone(Func<Maybe<T>> binder)
        {
            Guard.NotNull(binder, nameof(binder));

            return IsNone ? binder() : this;
        }

        public IEnumerable<T> AsEnumerable()
        {
            if (IsSome)
            {
                yield return value;
            }
        }

        public bool Equals(Maybe<T> other)
        {
            if (IsNone && other.IsNone)
            {
                return true;
            }

            return IsSome && other.IsSome && EqualityComparer<T>.Default.Equals(value, other.value);
        }

        public override bool Equals(object obj) => obj is Maybe<T> other && Equals(other);

        public override int GetHashCode() => IsSome ? EqualityComparer<T>.Default.GetHashCode(value) : 0;

        public override string ToString() => IsSome ? $"Some<{typeof(T).Name}>: {value}" : $"None<{typeof(T).Name}>";

        public void GetObjectData(SerializationInfo serializationInfo, StreamingContext context)
        {
            serializationInfo.AddValue(nameof(IsSome), IsSome);
            if (IsSome)
            {
                serializationInfo.AddValue(nameof(value), value);
            }
        }

        [EditorBrowsable(EditorBrowsableState.Never)]
        public void Deconstruct(out bool isSome, out T value) => (isSome, value) = (IsSome, this.value);
    }
}
