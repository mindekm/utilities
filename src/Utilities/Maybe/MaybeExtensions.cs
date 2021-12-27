namespace Utilities;

using System.Diagnostics.Contracts;

public static class MaybeExtensions
{
    [Pure]
    public static Maybe<T> ToMaybe<T>(this T? value)
        where T : class => value is null ? Maybe.None : Maybe.Some(value);

    [Pure]
    public static Maybe<T> ToMaybe<T>(this T? value)
        where T : struct => value.HasValue ? Maybe.Some(value.Value) : Maybe.None;

    [Pure]
    public static T? ToNullable<T>(this Maybe<T> maybe)
        where T : struct => maybe.Match(value => new T?(value), () => default);

    [Pure]
    public static Maybe<T> Flatten<T>(this Maybe<Maybe<T>> maybe)
        => maybe.Match(some => some, () => Maybe.None);

    [Pure]
    public static IEnumerable<T> GetValues<T>(this IEnumerable<Maybe<T>> source)
    {
        Guard.NotNull(source);

        foreach (var maybe in source)
        {
            if (maybe.TryUnwrap(out var value))
            {
                yield return value;
            }
        }
    }
}