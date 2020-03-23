namespace Utilities
{
    using System.Collections.Generic;

    public static class MaybeExtensions
    {
        public static Maybe<T> ToMaybe<T>(this T value)
            where T : class => value is null ? Maybe.None : Maybe.Some(value);

        public static Maybe<T> ToMaybe<T>(this T? value)
            where T : struct => value.HasValue ? Maybe.Some(value.Value) : Maybe.None;

        public static T? ToNullable<T>(this in Maybe<T> maybe)
            where T : struct => maybe.Match(value => new T?(value), () => default);

        public static Maybe<T> Flatten<T>(this in Maybe<Maybe<T>> maybe)
            => maybe.Match(some => some, () => Maybe.None);

        public static IEnumerable<T> GetValues<T>(this IEnumerable<Maybe<T>> source)
        {
            Guard.NotNull(source, nameof(source));

            foreach (var (isSome, value) in source)
            {
                if (isSome)
                {
                    yield return value;
                }
            }
        }
    }
}