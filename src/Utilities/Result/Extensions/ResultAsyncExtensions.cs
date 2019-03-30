namespace Utilities.Extensions
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public static class ResultAsyncExtensions
    {
        public static async Task<TOut> MatchAsync<TIn, TOut>(this Task<Result<TIn>> result, TOut success, TOut failure)
        {
            Guard.NotNull(result, nameof(result));

            return (await result.ConfigureAwait(false)).IsSuccess ? success : failure;
        }

        public static async Task<TOut> MatchAsync<TValue, TFailure, TOut>(
            this Task<Result<TValue, TFailure>> result,
            TOut success,
            TOut failure)
        {
            Guard.NotNull(result, nameof(result));

            return (await result.ConfigureAwait(false)).IsSuccess ? success : failure;
        }

        public static async Task<TOut> MatchAsync<TIn, TOut>(
            this Task<Result<TIn>> result,
            Task<TOut> success,
            Task<TOut> failure)
        {
            Guard.NotNull(result, nameof(result));
            Guard.NotNull(success, nameof(success));
            Guard.NotNull(failure, nameof(failure));

            return (await result.ConfigureAwait(false)).IsSuccess ? await success : await failure;
        }

        public static async Task<TOut> MatchAsync<TValue, TFailure, TOut>(
            this Task<Result<TValue, TFailure>> result,
            Task<TOut> success,
            Task<TOut> failure)
        {
            Guard.NotNull(result, nameof(result));
            Guard.NotNull(success, nameof(success));
            Guard.NotNull(failure, nameof(failure));

            return (await result.ConfigureAwait(false)).IsSuccess ? await success : await failure;
        }

        public static async Task<TOut> MatchAsync<TIn, TOut>(
            this Task<Result<TIn>> result,
            Func<TOut> success,
            Func<TIn, TOut> failure)
        {
            Guard.NotNull(result, nameof(result));
            Guard.NotNull(success, nameof(success));
            Guard.NotNull(failure, nameof(failure));

            return (await result.ConfigureAwait(false)).Match(success, failure);
        }

        public static async Task<TOut> MatchAsync<TValue, TFailure, TOut>(
            this Task<Result<TValue, TFailure>> result,
            Func<TValue, TOut> success,
            Func<TFailure, TOut> failure)
        {
            Guard.NotNull(result, nameof(result));
            Guard.NotNull(success, nameof(success));
            Guard.NotNull(failure, nameof(failure));

            return (await result.ConfigureAwait(false)).Match(success, failure);
        }

        public static async Task<TOut> MatchAsync<TIn, TOut>(
            this Result<TIn> result,
            Func<Task<TOut>> success,
            Func<TIn, Task<TOut>> failure)
        {
            Guard.NotNull(result, nameof(result));
            Guard.NotNull(success, nameof(success));
            Guard.NotNull(failure, nameof(failure));

            return result.IsSuccess
                ? await success().ConfigureAwait(false)
                : await failure(result.GetFailure()).ConfigureAwait(false);
        }

        public static async Task<TOut> MatchAsync<TValue, TFailure, TOut>(
            this Result<TValue, TFailure> result,
            Func<TValue, Task<TOut>> success,
            Func<TFailure, Task<TOut>> failure)
        {
            Guard.NotNull(result, nameof(result));
            Guard.NotNull(success, nameof(success));
            Guard.NotNull(failure, nameof(failure));

            return result.IsSuccess
                ? await success(result.GetValue()).ConfigureAwait(false)
                : await failure(result.GetFailure()).ConfigureAwait(false);
        }

        public static async Task<TOut> MatchAsync<TIn, TOut>(
            this Task<Result<TIn>> result,
            Func<Task<TOut>> success,
            Func<TIn, Task<TOut>> failure)
        {
            Guard.NotNull(result, nameof(result));
            Guard.NotNull(success, nameof(success));
            Guard.NotNull(failure, nameof(failure));

            return await (await result.ConfigureAwait(false)).MatchAsync(success, failure).ConfigureAwait(false);
        }

        public static async Task<TOut> MatchAsync<TValue, TFailure, TOut>(
            this Task<Result<TValue, TFailure>> result,
            Func<TValue, Task<TOut>> success,
            Func<TFailure, Task<TOut>> failure)
        {
            Guard.NotNull(result, nameof(result));
            Guard.NotNull(success, nameof(success));
            Guard.NotNull(failure, nameof(failure));

            return await (await result.ConfigureAwait(false)).MatchAsync(success, failure).ConfigureAwait(false);
        }

        public static async Task<Result<TOut>> BindAsync<TIn, TOut>(
            this Result<TIn> result,
            Func<Task<Result<TOut>>> successBinder,
            Func<TIn, Task<Result<TOut>>> failureBinder)
        {
            Guard.NotNull(result, nameof(result));
            Guard.NotNull(successBinder, nameof(successBinder));
            Guard.NotNull(failureBinder, nameof(failureBinder));

            return result.IsSuccess
                ? await successBinder().ConfigureAwait(false)
                : await failureBinder(result.GetFailure()).ConfigureAwait(false);
        }

        public static async Task<Result<T>> BindOnSuccessAsync<T>(this Result<T> result, Func<Task<Result<T>>> binder)
        {
            Guard.NotNull(result, nameof(result));
            Guard.NotNull(binder, nameof(binder));

            return result.IsSuccess ? await binder().ConfigureAwait(false) : result;
        }

        public static async Task<Result<T>> BindOnFailureAsync<T>(
            this Result<T> result,
            Func<T, Task<Result<T>>> binder)
        {
            Guard.NotNull(result, nameof(result));
            Guard.NotNull(binder, nameof(binder));

            return result.IsFailure ? await binder(result.GetFailure()).ConfigureAwait(false) : result;
        }

        public static async Task<Result<TOut>> BindAsync<TIn, TOut>(
            this Task<Result<TIn>> result,
            Func<Result<TOut>> successBinder,
            Func<TIn, Result<TOut>> failureBinder)
        {
            Guard.NotNull(result, nameof(result));
            Guard.NotNull(successBinder, nameof(successBinder));
            Guard.NotNull(failureBinder, nameof(failureBinder));

            return (await result.ConfigureAwait(false)).Bind(successBinder, failureBinder);
        }

        public static async Task<Result<T>> BindOnSuccessAsync<T>(this Task<Result<T>> result, Func<Result<T>> binder)
        {
            Guard.NotNull(result, nameof(result));
            Guard.NotNull(binder, nameof(binder));

            return (await result.ConfigureAwait(false)).BindOnSuccess(binder);
        }

        public static async Task<Result<T>> BindOnFailureAsync<T>(
            this Task<Result<T>> result,
            Func<T, Result<T>> binder)
        {
            Guard.NotNull(result, nameof(result));
            Guard.NotNull(binder, nameof(binder));

            return (await result.ConfigureAwait(false)).BindOnFailure(binder);
        }

        public static async Task<Result<TOut>> BindAsync<TIn, TOut>(
            this Task<Result<TIn>> result,
            Func<Task<Result<TOut>>> successBinder,
            Func<TIn, Task<Result<TOut>>> failureBinder)
        {
            Guard.NotNull(result, nameof(result));
            Guard.NotNull(successBinder, nameof(successBinder));
            Guard.NotNull(failureBinder, nameof(failureBinder));

            return await (await result.ConfigureAwait(false)).BindAsync(successBinder, failureBinder)
                .ConfigureAwait(false);
        }

        public static async Task<Result<T>> BindOnSuccessAsync<T>(
            this Task<Result<T>> result,
            Func<Task<Result<T>>> binder)
        {
            Guard.NotNull(result, nameof(result));
            Guard.NotNull(binder, nameof(binder));

            return await (await result.ConfigureAwait(false)).BindOnSuccessAsync(binder).ConfigureAwait(false);
        }

        public static async Task<Result<T>> BindOnFailureAsync<T>(
            this Task<Result<T>> result,
            Func<T, Task<Result<T>>> binder)
        {
            Guard.NotNull(result, nameof(result));
            Guard.NotNull(binder, nameof(binder));

            return await (await result.ConfigureAwait(false)).BindOnFailureAsync(binder).ConfigureAwait(false);
        }

        public static async Task<Result<TOutValue, TOutFailure>> BindAsync<TInValue, TInFailure, TOutValue, TOutFailure>(
            this Result<TInValue, TInFailure> result,
            Func<TInValue, Task<Result<TOutValue, TOutFailure>>> successBinder,
            Func<TInFailure, Task<Result<TOutValue, TOutFailure>>> failureBinder)
        {
            Guard.NotNull(result, nameof(result));
            Guard.NotNull(successBinder, nameof(successBinder));
            Guard.NotNull(failureBinder, nameof(failureBinder));

            return result.IsSuccess
                ? await successBinder(result.GetValue()).ConfigureAwait(false)
                : await failureBinder(result.GetFailure()).ConfigureAwait(false);
        }

        public static async Task<Result<TOutValue, TFailure>> BindOnSuccessAsync<TInValue, TOutValue, TFailure>(
            this Result<TInValue, TFailure> result,
            Func<TInValue, Task<Result<TOutValue, TFailure>>> binder)
        {
            Guard.NotNull(result, nameof(result));
            Guard.NotNull(binder, nameof(binder));

            return result.IsSuccess
                ? await binder(result.GetValue()).ConfigureAwait(false)
                : Result<TOutValue, TFailure>.Failure(result.GetFailure());
        }

        public static async Task<Result<TValue, TOutFailure>> BindOnFailureAsync<TValue, TInFailure, TOutFailure>(
            this Result<TValue, TInFailure> result,
            Func<TInFailure, Task<Result<TValue, TOutFailure>>> binder)
        {
            Guard.NotNull(result, nameof(result));
            Guard.NotNull(binder, nameof(binder));

            return result.IsFailure
                ? await binder(result.GetFailure()).ConfigureAwait(false)
                : Result<TValue, TOutFailure>.Success(result.GetValue());
        }

        public static async Task<Result<TOutValue, TOutFailure>> BindAsync<TInValue, TInFailure, TOutValue, TOutFailure>(
            this Task<Result<TInValue, TInFailure>> result,
            Func<TInValue, Result<TOutValue, TOutFailure>> successBinder,
            Func<TInFailure, Result<TOutValue, TOutFailure>> failureBinder)
        {
            Guard.NotNull(result, nameof(result));
            Guard.NotNull(successBinder, nameof(successBinder));
            Guard.NotNull(failureBinder, nameof(failureBinder));

            return (await result.ConfigureAwait(false)).Bind(successBinder, failureBinder);
        }

        public static async Task<Result<TOutValue, TFailure>> BindOnSuccessAsync<TInValue, TOutValue, TFailure>(
            this Task<Result<TInValue, TFailure>> result,
            Func<TInValue, Result<TOutValue, TFailure>> binder)
        {
            Guard.NotNull(result, nameof(result));
            Guard.NotNull(binder, nameof(binder));

            return (await result.ConfigureAwait(false)).BindOnSuccess(binder);
        }

        public static async Task<Result<TValue, TOutFailure>> BindOnFailureAsync<TValue, TInFailure, TOutFailure>(
            this Task<Result<TValue, TInFailure>> result,
            Func<TInFailure, Result<TValue, TOutFailure>> binder)
        {
            Guard.NotNull(result, nameof(result));
            Guard.NotNull(binder, nameof(binder));

            return (await result.ConfigureAwait(false)).BindOnFailure(binder);
        }

        public static async Task<Result<TOutValue, TOutFailure>> BindAsync<TInValue, TInFailure, TOutValue, TOutFailure>(
            this Task<Result<TInValue, TInFailure>> result,
            Func<TInValue, Task<Result<TOutValue, TOutFailure>>> successBinder,
            Func<TInFailure, Task<Result<TOutValue, TOutFailure>>> failureBinder)
        {
            Guard.NotNull(result, nameof(result));
            Guard.NotNull(successBinder, nameof(successBinder));
            Guard.NotNull(failureBinder, nameof(failureBinder));

            return await (await result.ConfigureAwait(false)).BindAsync(successBinder, failureBinder)
                .ConfigureAwait(false);
        }

        public static async Task<Result<TOutValue, TFailure>> BindOnSuccessAsync<TInValue, TOutValue, TFailure>(
            this Task<Result<TInValue, TFailure>> result,
            Func<TInValue, Task<Result<TOutValue, TFailure>>> binder)
        {
            Guard.NotNull(result, nameof(result));
            Guard.NotNull(binder, nameof(binder));

            return await (await result.ConfigureAwait(false)).BindOnSuccessAsync(binder).ConfigureAwait(false);
        }

        public static async Task<Result<TValue, TOutFailure>> BindOnFailureAsync<TValue, TInFailure, TOutFailure>(
            this Task<Result<TValue, TInFailure>> result,
            Func<TInFailure, Task<Result<TValue, TOutFailure>>> binder)
        {
            Guard.NotNull(result, nameof(result));
            Guard.NotNull(binder, nameof(binder));

            return await (await result.ConfigureAwait(false)).BindOnFailureAsync(binder).ConfigureAwait(false);
        }

        public static async Task<Result<T>> DoOnSuccessAsync<T>(this Task<Result<T>> result, Action action)
        {
            Guard.NotNull(result, nameof(result));
            Guard.NotNull(action, nameof(action));

            return (await result.ConfigureAwait(false)).DoOnSuccess(action);
        }

        public static async Task<Result<T>> DoOnFailureAsync<T>(this Task<Result<T>> result, Action action)
        {
            Guard.NotNull(result, nameof(result));
            Guard.NotNull(action, nameof(action));

            return (await result.ConfigureAwait(false)).DoOnFailure(action);
        }

        public static async Task<Result<T>> DoOnFailureAsync<T>(this Task<Result<T>> result, Action<T> action)
        {
            Guard.NotNull(result, nameof(result));
            Guard.NotNull(action, nameof(action));

            return (await result.ConfigureAwait(false)).DoOnFailure(action);
        }

        public static async Task<Result<T>> DoOnBothAsync<T>(this Task<Result<T>> result, Action action)
        {
            Guard.NotNull(result, nameof(result));
            Guard.NotNull(action, nameof(action));

            return (await result.ConfigureAwait(false)).DoOnBoth(action);
        }

        public static async Task<Result<T>> DoOnBothAsync<T>(this Task<Result<T>> result, Action<Result<T>> action)
        {
            Guard.NotNull(result, nameof(result));
            Guard.NotNull(action, nameof(action));

            return (await result.ConfigureAwait(false)).DoOnBoth(action);
        }

        public static async Task<Result<TValue, TFailure>> DoOnSuccessAsync<TValue, TFailure>(
            this Task<Result<TValue, TFailure>> result,
            Action action)
        {
            Guard.NotNull(result, nameof(result));
            Guard.NotNull(action, nameof(action));

            return (await result.ConfigureAwait(false)).DoOnSuccess(action);
        }

        public static async Task<Result<TValue, TFailure>> DoOnSuccessAsync<TValue, TFailure>(
            this Task<Result<TValue, TFailure>> result,
            Action<TValue> action)
        {
            Guard.NotNull(result, nameof(result));
            Guard.NotNull(action, nameof(action));

            return (await result.ConfigureAwait(false)).DoOnSuccess(action);
        }

        public static async Task<Result<TValue, TFailure>> DoOnFailureAsync<TValue, TFailure>(
            this Task<Result<TValue, TFailure>> result,
            Action action)
        {
            Guard.NotNull(result, nameof(result));
            Guard.NotNull(action, nameof(action));

            return (await result.ConfigureAwait(false)).DoOnFailure(action);
        }

        public static async Task<Result<TValue, TFailure>> DoOnFailureAsync<TValue, TFailure>(
            this Task<Result<TValue, TFailure>> result,
            Action<TFailure> action)
        {
            Guard.NotNull(result, nameof(result));
            Guard.NotNull(action, nameof(action));

            return (await result.ConfigureAwait(false)).DoOnFailure(action);
        }

        public static async Task<Result<TValue, TFailure>> DoOnBothAsync<TValue, TFailure>(
            this Task<Result<TValue, TFailure>> result,
            Action action)
        {
            Guard.NotNull(result, nameof(result));
            Guard.NotNull(action, nameof(action));

            return (await result.ConfigureAwait(false)).DoOnBoth(action);
        }

        public static async Task<Result<TValue, TFailure>> DoOnBothAsync<TValue, TFailure>(
            this Task<Result<TValue, TFailure>> result,
            Action<Result<TValue, TFailure>> action)
        {
            Guard.NotNull(result, nameof(result));
            Guard.NotNull(action, nameof(action));

            return (await result.ConfigureAwait(false)).DoOnBoth(action);
        }

        public static Task<Result<TFailure>> FlattenAsync<TFailure>(this Result<Result<TFailure>> result)
        {
            Guard.NotNull(result, nameof(result));

            return Task.FromResult(result.Flatten());
        }

        public static Task<Result<TValue, TFailure>> FlattenAsync<TValue, TFailure>(
            this Result<Result<TValue, TFailure>, TFailure> result)
        {
            Guard.NotNull(result, nameof(result));

            return Task.FromResult(result.Flatten());
        }

        public static async Task<Result<TFailure>> FlattenAsync<TFailure>(this Task<Result<Result<TFailure>>> result)
        {
            Guard.NotNull(result, nameof(result));

            return await (await result.ConfigureAwait(false)).FlattenAsync().ConfigureAwait(false);
        }

        public static async Task<Result<TValue, TFailure>> FlattenAsync<TValue, TFailure>(
            this Task<Result<Result<TValue, TFailure>, TFailure>> result)
        {
            Guard.NotNull(result, nameof(result));

            return await (await result.ConfigureAwait(false)).FlattenAsync().ConfigureAwait(false);
        }

        public static Task<IEnumerable<TSource>> GetFailuresAsync<TSource>(
            this IEnumerable<Result<TSource>> source)
        {
            Guard.NotNull(source, nameof(source));

            return Task.FromResult(source.GetFailures());
        }

        public static Task<IEnumerable<TFailure>> GetFailuresAsync<TValue, TFailure>(
            this IEnumerable<Result<TValue, TFailure>> source)
        {
            Guard.NotNull(source, nameof(source));

            return Task.FromResult(source.GetFailures());
        }

        public static async Task<IEnumerable<TSource>> GetFailuresAsync<TSource>(
            this Task<IEnumerable<Result<TSource>>> source)
        {
            Guard.NotNull(source, nameof(source));

            return await (await source.ConfigureAwait(false)).GetFailuresAsync().ConfigureAwait(false);
        }

        public static async Task<IEnumerable<TFailure>> GetFailuresAsync<TValue, TFailure>(
            this Task<IEnumerable<Result<TValue, TFailure>>> source)
        {
            Guard.NotNull(source, nameof(source));

            return await (await source.ConfigureAwait(false)).GetFailuresAsync().ConfigureAwait(false);
        }

        public static Task<IEnumerable<TValue>> GetValuesAsync<TValue, TFailure>(
            this IEnumerable<Result<TValue, TFailure>> source)
        {
            Guard.NotNull(source, nameof(source));

            return Task.FromResult(source.GetValues());
        }

        public static async Task<IEnumerable<TValue>> GetValuesAsync<TValue, TFailure>(
            this Task<IEnumerable<Result<TValue, TFailure>>> source)
        {
            Guard.NotNull(source, nameof(source));

            return await (await source.ConfigureAwait(false)).GetValuesAsync().ConfigureAwait(false);
        }
    }
}