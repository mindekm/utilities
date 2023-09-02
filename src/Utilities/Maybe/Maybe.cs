namespace Utilities;

using System.Diagnostics.Contracts;

public static class Maybe
{
    [Pure]
    public static NoneOption None => default;

    [Pure]
    public static Maybe<T> Some<T>(T value)
    {
        Guard.NotNull(value);

        return new Maybe<T>(value);
    }

    [Pure]
    public static Maybe<T> UnsafeSome<T>(T value) => new Maybe<T>(value);
}
