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
    public static Maybe<string> ToMaybe(this string value, NoneWhen noneWhen)
    {
        return noneWhen switch
        {
            NoneWhen.Null => value is null ? Maybe.None : Maybe.Some(value),
            NoneWhen.NullOrEmpty => string.IsNullOrEmpty(value) ? Maybe.None : Maybe.Some(value),
            NoneWhen.NullOrWhitespace => string.IsNullOrWhiteSpace(value) ? Maybe.None : Maybe.Some(value),
            _ => throw new ArgumentOutOfRangeException(nameof(noneWhen), noneWhen, null),
        };
    }

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

    public static TCollection Add<TCollection, T>(this TCollection collection, Maybe<T> maybe)
        where TCollection : ICollection<T>
    {
        Guard.NotNull(collection);

        if (maybe.TryUnwrap(out var result))
        {
            collection.Add(result);
        }

        return collection;
    }
}
