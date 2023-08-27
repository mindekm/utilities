namespace Utilities;

using System.ComponentModel;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Diagnostics.Contracts;

[DebuggerDisplay("{DebuggerDisplay,nq}")]
[DebuggerTypeProxy(typeof(Maybe<>.DebuggerProxy))]
public readonly struct Maybe<T> : IEquatable<Maybe<T>>
{
    private readonly T? value;

    internal Maybe(T value)
    {
        Guard.NotNull(value);

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

    [Pure]
    public T Expect(string message) => IsSome ? value : throw new InvalidOperationException(message);

    [Pure]
    public bool IsSomeAnd(Func<T, bool> predicate)
    {
        Guard.NotNull(predicate);

        return IsSome && predicate(value);
    }

    [Pure]
    public T Unwrap()
    {
        if (IsSome)
        {
            return value;
        }

        throw new InvalidOperationException($"Cannot unwrap None<{typeof(T).Name}>.");
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

    [Obsolete("Causes overload collisions. To be removed. Use UnwrapOrElse.")]
    [Pure]
    public T? UnwrapOr(Func<T?> valueFactory)
    {
        Guard.NotNull(valueFactory);

        return IsSome ? value : valueFactory();
    }

    [Pure]
    public T? UnwrapOrElse(Func<T?> valueFactory)
    {
        Guard.NotNull(valueFactory);

        return IsSome ? value : valueFactory();
    }

    public bool Equals(Maybe<T> other)
    {
        if (IsNone && other.IsNone)
        {
            return true;
        }

        return IsSome && other.IsSome && EqualityComparer<T>.Default.Equals(value, other.value);
    }

    [Pure]
    public TOut? Match<TOut>(Func<T, TOut?> onSome, Func<TOut?> onNone)
    {
        Guard.NotNull(onSome);
        Guard.NotNull(onNone);

        return IsSome ? onSome(value) : onNone();
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
    public Maybe<TOut> BindOr<TOut>(Func<T, Maybe<TOut>> binder, Maybe<TOut> alternative)
    {
        Guard.NotNull(binder);

        return IsSome ? binder(value) : alternative;
    }

    [Pure]
    public Maybe<TOut> BindOrElse<TOut>(Func<T, Maybe<TOut>> binder, Func<Maybe<TOut>> valueFactory)
    {
        Guard.NotNull(binder);
        Guard.NotNull(valueFactory);

        return IsSome ? binder(value) : valueFactory();
    }

    [Obsolete("To be removed. Use BindOrElse().")]
    public Maybe<TOut> BindOr<TOut>(Func<T, Maybe<TOut>> binder, Func<Maybe<TOut>> valueFactory)
    {
        Guard.NotNull(binder);
        Guard.NotNull(valueFactory);

        return IsSome ? binder(value) : valueFactory();
    }

    [Obsolete("To be removed. Use BindOr().")]
    public Maybe<TOut> Bind<TOut>(Func<T, Maybe<TOut>> someBinder, Func<Maybe<TOut>> noneBinder)
    {
        Guard.NotNull(someBinder);
        Guard.NotNull(noneBinder);

        return IsSome ? someBinder(value) : noneBinder();
    }

    [Obsolete("To be removed. Use Bind().")]
    public Maybe<TOut> BindOnSome<TOut>(Func<T, Maybe<TOut>> binder)
    {
        Guard.NotNull(binder);

        return IsSome ? binder(value) : Maybe.None;
    }

    [Obsolete("To be removed.")]
    public Maybe<T> BindOnNone(Func<Maybe<T>> binder)
    {
        Guard.NotNull(binder);

        return IsNone ? binder() : this;
    }

    [Pure]
    public Maybe<T> Filter(Func<T, bool> predicate)
    {
        Guard.NotNull(predicate);

        if (IsNone)
        {
            return Maybe.None;
        }

        return predicate(value) ? Maybe.Some(value) : Maybe.None;
    }

    [Pure]
    public Maybe<TOut> Map<TOut>(Func<T, TOut> mapper)
    {
        Guard.NotNull(mapper);

        return IsSome ? Maybe.Some(mapper(value)) : Maybe.None;
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

    public override bool Equals(object? obj) => obj is Maybe<T> other && Equals(other);

    public override int GetHashCode() => IsSome ? EqualityComparer<T>.Default.GetHashCode(value) : 0;

    public override string ToString() => IsSome ? $"Some<{typeof(T).Name}>: {value}" : $"None<{typeof(T).Name}>";

    [EditorBrowsable(EditorBrowsableState.Never)]
    public void Deconstruct(out bool isSome, out T? wrappedValue) => (isSome, wrappedValue) = (IsSome, value);

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
