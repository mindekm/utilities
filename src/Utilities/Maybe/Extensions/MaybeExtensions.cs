namespace Utilities.Extensions
{
    using System;
    using System.Collections.Generic;

    public static class MaybeExtensions
    {
        // Return
        public static Maybe<T> ToMaybe<T>(this T value)
            where T : class => value;

        public static Maybe<T> ToMaybe<T>(this T? value)
            where T : struct => value.HasValue ? Maybe.Some(value.Value) : Maybe.None<T>();

        public static Maybe<TValue> ToMaybe<TValue, TError>(this Result<TValue, TError> result)
        {
            return result.Match(Maybe<TValue>.Some, _ => Maybe<TValue>.None());
        }

        public static T? ToNullable<T>(this in Maybe<T> maybe)
            where T : struct
        {
            if (maybe.IsSome)
            {
                return maybe.GetValue();
            }

            return null;
        }

        // Pattern match
        public static TOut Match<TIn, TOut>(this in Maybe<TIn> maybe, TOut some, TOut none)
            => maybe.IsSome ? some : none;

        public static TOut Match<TIn, TOut>(this in Maybe<TIn> maybe, Func<TIn, TOut> some, Func<TOut> none)
        {
            Guard.NotNull(some, nameof(some));
            Guard.NotNull(none, nameof(none));

            return maybe.IsSome ? some(maybe.GetValue()) : none();
        }

        public static void Match<T>(this in Maybe<T> maybe, Action some, Action none)
        {
            Guard.NotNull(some, nameof(some));
            Guard.NotNull(none, nameof(none));

            if (maybe.IsSome)
            {
                some();
            }
            else
            {
                none();
            }
        }

        public static void Match<T>(this in Maybe<T> maybe, Action<T> some, Action none)
        {
            Guard.NotNull(some, nameof(some));
            Guard.NotNull(none, nameof(none));

            if (maybe.IsSome)
            {
                some(maybe.GetValue());
            }
            else
            {
                none();
            }
        }

        // Bind
        public static Maybe<TOut> Bind<TIn, TOut>(this in Maybe<TIn> maybe, Func<TIn, Maybe<TOut>> binder)
        {
            Guard.NotNull(binder, nameof(binder));

            return Match(maybe, binder, Maybe.None<TOut>);
        }

        // Flatten
        public static Maybe<T> Flatten<T>(this in Maybe<Maybe<T>> maybe)
            => maybe.Match(some => some, Maybe.None<T>);

        // Enumerable
        public static IEnumerable<T> AsEnumerable<T>(this Maybe<T> maybe)
        {
            if (maybe.IsSome)
            {
                yield return maybe.GetValue();
            }
        }

        // Returns values from Some cases only. None cases are ignored.
        public static IEnumerable<TSource> GetValues<TSource>(this IEnumerable<Maybe<TSource>> source)
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
