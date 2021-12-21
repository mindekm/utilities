namespace Utilities;

using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.ComponentModel;
using System.Diagnostics.Contracts;

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

    public bool IsNone => !IsSome;

    public static implicit operator Maybe<T>(NoneOption none) => default;

    public static bool operator ==(Maybe<T> left, Maybe<T> right) => left.Equals(right);

    public static bool operator !=(Maybe<T> left, Maybe<T> right) => !left.Equals(right);

    [Pure]
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
    [DebuggerStepThrough]
    public T? UnwrapOrDefault() => IsSome ? value : default;

    [Pure]
    [DebuggerStepThrough]
    public T? UnwrapOr(T? alternative) => IsSome ? value : alternative;

    [Pure]
    [DebuggerStepThrough]
    public T? UnwrapOr(Func<T?> valueFactory)
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

    public TOut Match<TOut>(Func<T, TOut> onSome, Func<TOut> onNone)
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

    public Maybe<TOut> Bind<TOut>(Func<T, Maybe<TOut>> someBinder, Func<Maybe<TOut>> noneBinder)
    {
        Guard.NotNull(someBinder);
        Guard.NotNull(noneBinder);

        return IsSome ? someBinder(value) : noneBinder();
    }

    public Maybe<TOut> BindOnSome<TOut>(Func<T, Maybe<TOut>> binder)
    {
        Guard.NotNull(binder);

        return IsSome ? binder(value) : Maybe.None;
    }

    public Maybe<T> BindOnNone(Func<Maybe<T>> binder)
    {
        Guard.NotNull(binder);

        return IsNone ? binder() : this;
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
}