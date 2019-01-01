namespace Utilities.Extensions.Async
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public static class MaybeAsyncExtensions
    {
        public static async Task<TOut> MatchAsync<TIn, TOut>(this Task<Maybe<TIn>> maybe, TOut some, TOut none)
        {
            Guard.NotNull(maybe, nameof(maybe));

            return (await maybe).IsSome ? some : none;
        }

        public static async Task<TOut> MatchAsync<TIn, TOut>(this Task<Maybe<TIn>> maybe, Task<TOut> some, Task<TOut> none)
        {
            Guard.NotNull(maybe, nameof(maybe));
            Guard.NotNull(some, nameof(some));
            Guard.NotNull(none, nameof(none));

            return (await maybe).IsSome ? await some : await none;
        }

        public static async Task<TOut> MatchAsync<TIn, TOut>(
            this Task<Maybe<TIn>> maybe,
            Func<TIn, TOut> some,
            Func<TOut> none)
        {
            Guard.NotNull(maybe, nameof(maybe));
            Guard.NotNull(some, nameof(some));
            Guard.NotNull(none, nameof(none));

            return (await maybe).Match(some, none);
        }

        public static async Task<TOut> MatchAsync<TIn, TOut>(
            this Maybe<TIn> maybe,
            Func<TIn, Task<TOut>> some,
            Func<Task<TOut>> none)
        {
            Guard.NotNull(some, nameof(some));
            Guard.NotNull(none, nameof(none));

            return maybe.IsSome
                ? await some(maybe.GetValue()).ConfigureAwait(false)
                : await none().ConfigureAwait(false);
        }

        public static async Task<TOut> MatchAsync<TIn, TOut>(
            this Task<Maybe<TIn>> maybe,
            Func<TIn, Task<TOut>> some,
            Func<Task<TOut>> none)
        {
            Guard.NotNull(some, nameof(some));
            Guard.NotNull(none, nameof(none));

            return await (await maybe).MatchAsync(some, none).ConfigureAwait(false);
        }

        public static async Task<Maybe<TOut>> BindAsync<TIn, TOut>(
            this Maybe<TIn> maybe,
            Func<TIn, Task<Maybe<TOut>>> binder)
        {
            Guard.NotNull(binder, nameof(binder));

            return maybe.IsSome ? await binder(maybe.GetValue()).ConfigureAwait(false) : Maybe.None<TOut>();
        }

        public static async Task<Maybe<TOut>> BindAsync<TIn, TOut>(
            this Task<Maybe<TIn>> maybe,
            Func<TIn, Maybe<TOut>> binder)
        {
            Guard.NotNull(maybe, nameof(maybe));
            Guard.NotNull(binder, nameof(binder));

            return (await maybe).Bind(binder);
        }

        public static async Task<Maybe<TOut>> BindAsync<TIn, TOut>(
            this Task<Maybe<TIn>> maybe,
            Func<TIn, Task<Maybe<TOut>>> binder)
        {
            Guard.NotNull(maybe, nameof(maybe));
            Guard.NotNull(binder, nameof(binder));

            return await (await maybe).BindAsync(binder).ConfigureAwait(false);
        }

        public static Task<Maybe<T>> FlattenAsync<T>(this in Maybe<Maybe<T>> maybe)
        {
            return Task.FromResult(maybe.Flatten());
        }

        public static async Task<Maybe<T>> FlattenAsync<T>(this Task<Maybe<Maybe<T>>> maybe)
        {
            Guard.NotNull(maybe, nameof(maybe));

            return await (await maybe).FlattenAsync().ConfigureAwait(false);
        }

        public static Task<IEnumerable<TSource>> GetValuesAsync<TSource>(this IEnumerable<Maybe<TSource>> source)
        {
            Guard.NotNull(source, nameof(source));

            return Task.FromResult(source.GetValues());
        }

        public static async Task<IEnumerable<TSource>> GetValuesAsync<TSource>(this Task<IEnumerable<Maybe<TSource>>> source)
        {
            Guard.NotNull(source, nameof(source));

            return await (await source).GetValuesAsync().ConfigureAwait(false);
        }
    }
}
