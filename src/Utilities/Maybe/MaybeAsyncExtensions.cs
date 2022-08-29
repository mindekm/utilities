namespace Utilities;

public static class MaybeAsyncExtensions
{
    public static async Task<TOut> MatchAsync<TIn, TOut>(
        this Task<Maybe<TIn>> maybe,
        Func<TIn, TOut> onSome,
        Func<TOut> onNone)
    {
        Guard.NotNull(maybe);

        return (await maybe.ConfigureAwait(false)).Match(onSome, onNone);
    }

    public static async Task<TOut> MatchAsync<TIn, TOut>(
        this Maybe<TIn> maybe,
        Func<TIn, Task<TOut>> onSome,
        Func<Task<TOut>> onNone)
    {
        return await maybe.Match(onSome, onNone).ConfigureAwait(false);
    }

    public static async Task<TOut> MatchAsync<TIn, TOut>(
        this Task<Maybe<TIn>> maybe,
        Func<TIn, Task<TOut>> onSome,
        Func<Task<TOut>> onNone)
    {
        return await (await maybe.ConfigureAwait(false)).Match(onSome, onNone).ConfigureAwait(false);
    }

    public static async Task<Maybe<T>> DoAsync<T>(this Task<Maybe<T>> maybe, Action onSome, Action onNone)
    {
        Guard.NotNull(maybe);

        return (await maybe.ConfigureAwait(false)).Do(onSome, onNone);
    }

    public static async Task<Maybe<T>> DoOnSomeAsync<T>(this Task<Maybe<T>> maybe, Action onSome)
    {
        Guard.NotNull(maybe);

        return (await maybe.ConfigureAwait(false)).DoOnSome(onSome);
    }

    public static async Task<Maybe<T>> DoOnNoneAsync<T>(this Task<Maybe<T>> maybe, Action onNone)
    {
        Guard.NotNull(maybe);

        return (await maybe.ConfigureAwait(false)).DoOnNone(onNone);
    }

    public static async Task<Maybe<T>> DoOnBothAsync<T>(this Task<Maybe<T>> maybe, Action action)
    {
        Guard.NotNull(maybe);

        return (await maybe.ConfigureAwait(false)).DoOnBoth(action);
    }

    public static async Task<Maybe<T>> DoAsync<T>(
        this Task<Maybe<T>> maybe,
        Action<T> onSome,
        Action onNone)
    {
        Guard.NotNull(maybe);

        return (await maybe.ConfigureAwait(false)).Do(onSome, onNone);
    }

    public static async Task<Maybe<T>> DoOnSomeAsync<T>(this Task<Maybe<T>> maybe, Action<T> onSome)
    {
        Guard.NotNull(maybe);

        return (await maybe.ConfigureAwait(false)).DoOnSome(onSome);
    }

    public static async Task<Maybe<TOut>> BindAsync<TIn, TOut>(
        this Task<Maybe<TIn>> maybe,
        Func<TIn, Maybe<TOut>> someBinder,
        Func<Maybe<TOut>> noneBinder)
    {
        Guard.NotNull(maybe);

        return (await maybe.ConfigureAwait(false)).Bind(someBinder, noneBinder);
    }

    public static async Task<Maybe<TOut>> BindAsync<TIn, TOut>(
        this Task<Maybe<TIn>> maybe,
        Func<TIn, Task<Maybe<TOut>>> someBinder,
        Func<Task<Maybe<TOut>>> noneBinder)
    {
        Guard.NotNull(maybe);

        return await (await maybe.ConfigureAwait(false)).Match(someBinder, noneBinder).ConfigureAwait(false);
    }

    public static async Task<Maybe<TOut>> BindAsync<TIn, TOut>(
        this Maybe<TIn> maybe,
        Func<TIn, Task<Maybe<TOut>>> someBinder,
        Func<Task<Maybe<TOut>>> noneBinder)
    {
        return await maybe.Match(someBinder, noneBinder).ConfigureAwait(false);
    }

    public static async Task<Maybe<TOut>> BindOnSomeAsync<TIn, TOut>(
        this Task<Maybe<TIn>> maybe,
        Func<TIn, Maybe<TOut>> binder)
    {
        Guard.NotNull(maybe);

        return (await maybe.ConfigureAwait(false)).BindOnSome(binder);
    }

    public static async Task<Maybe<TOut>> BindOnSomeAsync<TIn, TOut>(
        this Task<Maybe<TIn>> maybe,
        Func<TIn, Task<Maybe<TOut>>> binder)
    {
        Guard.NotNull(maybe);

        return await (await maybe.ConfigureAwait(false))
            .Match(
                binder,
                () => Task.FromResult<Maybe<TOut>>(Maybe.None))
            .ConfigureAwait(false);
    }

    public static async Task<Maybe<TOut>> BindOnSomeAsync<TIn, TOut>(
        this Maybe<TIn> maybe,
        Func<TIn, Task<Maybe<TOut>>> binder)
    {
        return await maybe
            .Match(
                binder,
                () => Task.FromResult<Maybe<TOut>>(Maybe.None))
            .ConfigureAwait(false);
    }

    public static async Task<Maybe<T>> BindOnNoneAsync<T>(this Task<Maybe<T>> maybe, Func<Maybe<T>> binder)
    {
        Guard.NotNull(maybe);

        return (await maybe.ConfigureAwait(false)).BindOnNone(binder);
    }

    public static async Task<Maybe<T>> BindOnNoneAsync<T>(this Task<Maybe<T>> maybe, Func<Task<Maybe<T>>> binder)
    {
        Guard.NotNull(maybe);
        Guard.NotNull(binder);

        var awaited = await maybe.ConfigureAwait(false);
        return awaited.IsNone ? await binder().ConfigureAwait(false) : awaited;
    }

    public static async Task<Maybe<T>> BindOnNoneAsync<T>(this Maybe<T> maybe, Func<Task<Maybe<T>>> binder)
    {
        Guard.NotNull(binder);

        return maybe.IsNone ? await binder().ConfigureAwait(false) : maybe;
    }

    public static async Task<Maybe<T>> FlattenAsync<T>(this Task<Maybe<Maybe<T>>> maybe)
    {
        Guard.NotNull(maybe);

        return (await maybe.ConfigureAwait(false)).Flatten();
    }

    public static async Task<IEnumerable<TSource>> GetValuesAsync<TSource>(
        this Task<IEnumerable<Maybe<TSource>>> source)
    {
        Guard.NotNull(source);

        return (await source.ConfigureAwait(false)).GetValues();
    }

    public static async Task<T> UnwrapAsync<T>(this Task<Maybe<T>> maybe)
    {
        Guard.NotNull(maybe);

        return (await maybe.ConfigureAwait(false)).Unwrap();
    }

    public static async Task<T?> UnwrapOrDefaultAsync<T>(this Task<Maybe<T>> maybe)
    {
        Guard.NotNull(maybe);

        return (await maybe.ConfigureAwait(false)).UnwrapOrDefault();
    }
}
