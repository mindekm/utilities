namespace Utilities;

using System.ComponentModel;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Diagnostics.Contracts;

[DebuggerDisplay("{DebuggerDisplay,nq}")]
[DebuggerTypeProxy(typeof(Maybe<>.DebuggerProxy))]
public readonly struct Maybe<T> : IEquatable<Maybe<T>>, IComparable<Maybe<T>>, IComparable
{
    private readonly T? value;

    internal Maybe(T value)
    {
        this.value = value;
        IsSome = true;
    }

    [MemberNotNullWhen(true, nameof(value))]
    public bool IsSome { get; }

    [MemberNotNullWhen(false, nameof(value))]
    public bool IsNone => !IsSome;

    [DebuggerBrowsable(DebuggerBrowsableState.Never)]
    [EditorBrowsable(EditorBrowsableState.Never)]
    private string DebuggerDisplay => ToString();

    public static implicit operator Maybe<T>(NoneOption none) => default;

    public static bool operator ==(Maybe<T> left, Maybe<T> right) => left.Equals(right);

    public static bool operator !=(Maybe<T> left, Maybe<T> right) => !left.Equals(right);

    public static bool operator <(Maybe<T> left, Maybe<T> right) => left.CompareTo(right) < 0;

    public static bool operator >(Maybe<T> left, Maybe<T> right) => left.CompareTo(right) > 0;

    public static bool operator <=(Maybe<T> left, Maybe<T> right) => left.CompareTo(right) <= 0;

    public static bool operator >=(Maybe<T> left, Maybe<T> right) => left.CompareTo(right) >= 0;

    [Pure]
    public T Expect(string message)
    {
        if (IsSome)
        {
            return value;
        }

        ThrowInvalidUnwrapException(message);
        return default; // Unreachable
    }

    [Pure]
    public bool IsSomeAnd(Func<T, bool> predicate)
    {
        Guard.NotNull(predicate);

        return IsSome && predicate(value);
    }

    [Pure]
    public bool IsSomeAnd<TState>(TState state, Func<T, TState, bool> predicate)
    {
        Guard.NotNull(predicate);

        return IsSome && predicate(value, state);
    }

    [Pure]
    public T Unwrap()
    {
        if (IsSome)
        {
            return value;
        }

        ThrowInvalidUnwrapException($"Cannot unwrap None<{typeof(T).Name}>.");
        return default; // Unreachable
    }

    public bool TryUnwrap([MaybeNullWhen(false)] out T result)
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
    public T? UnwrapOrDefault() => IsSome ? value : default;

    [Pure]
    public T? UnwrapOr(T? alternative) => IsSome ? value : alternative;

    [Pure]
    public T? UnwrapOrElse(Func<T?> valueFactory)
    {
        Guard.NotNull(valueFactory);

        return IsSome ? value : valueFactory();
    }

    [Pure]
    public T? UnwrapOrElse<TState>(TState state, Func<TState, T?> valueFactory)
    {
        Guard.NotNull(valueFactory);

        return IsSome ? value : valueFactory(state);
    }

    [Pure]
    public TOut? Match<TOut>(Func<T, TOut?> onSome, Func<TOut?> onNone)
    {
        Guard.NotNull(onSome);
        Guard.NotNull(onNone);

        return IsSome ? onSome(value) : onNone();
    }

    [Pure]
    public TOut? Match<TOut, TState>(TState state, Func<T, TState, TOut?> onSome, Func<TOut?> onNone)
    {
        Guard.NotNull(onSome);
        Guard.NotNull(onNone);

        return IsSome ? onSome(value, state) : onNone();
    }

    [Pure]
    public TOut? Match<TOut, TState>(TState state, Func<T, TState, TOut?> onSome, Func<TState, TOut?> onNone)
    {
        Guard.NotNull(onSome);
        Guard.NotNull(onNone);

        return IsSome ? onSome(value, state) : onNone(state);
    }

    public Maybe<T> Do(Action onSome, Action onNone)
    {
        Guard.NotNull(onSome);
        Guard.NotNull(onNone);

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
        Guard.NotNull(onSome);
        Guard.NotNull(onNone);

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
        Guard.NotNull(onSome);

        if (IsSome)
        {
            onSome();
        }

        return this;
    }

    public Maybe<T> DoOnSome(Action<T> onSome)
    {
        Guard.NotNull(onSome);

        if (IsSome)
        {
            onSome(value);
        }

        return this;
    }

    public Maybe<T> DoOnNone(Action onNone)
    {
        Guard.NotNull(onNone);

        if (IsNone)
        {
            onNone();
        }

        return this;
    }

    public Maybe<T> DoOnBoth(Action onBoth)
    {
        Guard.NotNull(onBoth);

        onBoth();

        return this;
    }

    [Pure]
    public Maybe<TOut> Bind<TOut>(Func<T, Maybe<TOut>> binder)
    {
        Guard.NotNull(binder);

        return IsSome ? binder(value) : Maybe.None;
    }

    [Pure]
    public Maybe<TOut> Bind<TOut, TState>(TState state, Func<T, TState, Maybe<TOut>> binder)
    {
        Guard.NotNull(binder);

        return IsSome ? binder(value, state) : Maybe.None;
    }

    [Pure]
    public Maybe<TOut> BindOr<TOut>(Func<T, Maybe<TOut>> binder, Maybe<TOut> alternative)
    {
        Guard.NotNull(binder);

        return IsSome ? binder(value) : alternative;
    }

    [Pure]
    public Maybe<TOut> BindOr<TOut, TState>(TState state, Func<T, TState, Maybe<TOut>> binder, Maybe<TOut> alternative)
    {
        Guard.NotNull(binder);

        return IsSome ? binder(value, state) : alternative;
    }

    [Pure]
    public Maybe<TOut> BindOrElse<TOut>(Func<T, Maybe<TOut>> binder, Func<Maybe<TOut>> valueFactory)
    {
        Guard.NotNull(binder);
        Guard.NotNull(valueFactory);

        return IsSome ? binder(value) : valueFactory();
    }

    [Pure]
    public Maybe<TOut> BindOrElse<TOut, TState>(TState state, Func<T, TState, Maybe<TOut>> binder, Func<Maybe<TOut>> valueFactory)
    {
        Guard.NotNull(binder);
        Guard.NotNull(valueFactory);

        return IsSome ? binder(value, state) : valueFactory();
    }

    [Pure]
    public Maybe<TOut> BindOrElse<TOut, TState>(TState state, Func<T, TState, Maybe<TOut>> binder, Func<TState, Maybe<TOut>> valueFactory)
    {
        Guard.NotNull(binder);
        Guard.NotNull(valueFactory);

        return IsSome ? binder(value, state) : valueFactory(state);
    }

    [Pure]
    public Maybe<T> Filter(Func<T, bool> predicate)
    {
        Guard.NotNull(predicate);

        if (IsNone)
        {
            return Maybe.None;
        }

        return predicate(value) ? this : Maybe.None;
    }

    [Pure]
    public Maybe<T> Filter<TState>(TState state, Func<T, TState, bool> predicate)
    {
        Guard.NotNull(predicate);

        if (IsNone)
        {
            return Maybe.None;
        }

        return predicate(value, state) ? this : Maybe.None;
    }

    [Pure]
    public Maybe<TOut> Map<TOut>(Func<T, TOut> mapper)
    {
        Guard.NotNull(mapper);

        return IsSome ? Maybe.Some(mapper(value)) : Maybe.None;
    }

    [Pure]
    public Maybe<TOut> Map<TOut, TState>(TState state, Func<T, TState, TOut> mapper)
    {
        Guard.NotNull(mapper);

        return IsSome ? Maybe.Some(mapper(value, state)) : Maybe.None;
    }

    [Pure]
    public Maybe<TOther> And<TOther>(Maybe<TOther> other)
    {
        return IsNone ? Maybe.None : other;
    }

    [Pure]
    public Maybe<T> Or(Maybe<T> other)
    {
        return IsSome ? this : other;
    }

    [Pure]
    public Maybe<T> OrElse(Func<Maybe<T>> valueFactory)
    {
        Guard.NotNull(valueFactory);

        return IsSome ? this : valueFactory();
    }

    [Pure]
    public Maybe<T> OrElse<TState>(TState state, Func<TState, Maybe<T>> valueFactory)
    {
        Guard.NotNull(valueFactory);

        return IsSome ? this : valueFactory(state);
    }

    [Pure]
    public Maybe<T> Xor(Maybe<T> other)
    {
        if (IsSome && other.IsNone)
        {
            return this;
        }

        if (IsNone && other.IsSome)
        {
            return other;
        }

        return Maybe.None;
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

    public override bool Equals(object? obj) => obj is Maybe<T> other && Equals(other);

    public override int GetHashCode() => IsSome ? EqualityComparer<T>.Default.GetHashCode(value) : 0;

    public int CompareTo(Maybe<T> other)
    {
        if (IsSome && other.IsNone)
        {
            return 1;
        }

        if (IsNone && other.IsSome)
        {
            return -1;
        }

        return Comparer<T>.Default.Compare(value, other.value);
    }

    public int CompareTo(object? obj)
    {
        if (obj is Maybe<T> other)
        {
            return CompareTo(other);
        }

        ThrowInvalidComparisonException();
        return 0; // Unreachable
    }

    public override string ToString() => IsSome ? $"Some<{typeof(T).Name}>: {value}" : $"None<{typeof(T).Name}>";

    [EditorBrowsable(EditorBrowsableState.Never)]
    public void Deconstruct(out bool isSome, out T? wrappedValue) => (isSome, wrappedValue) = (IsSome, value);

    [DoesNotReturn]
    private static void ThrowInvalidUnwrapException(string message)
        => throw new InvalidOperationException(message);

    [DoesNotReturn]
    private static void ThrowInvalidComparisonException()
        => throw new ArgumentException("Object must be of type Maybe<T>");

    private sealed class DebuggerProxy
    {
        private readonly Maybe<T> maybe;

        public DebuggerProxy(Maybe<T> maybe)
        {
            this.maybe = maybe;
        }

        public bool IsSome => maybe.IsSome;

        public T? Value => maybe.value;
    }
}
