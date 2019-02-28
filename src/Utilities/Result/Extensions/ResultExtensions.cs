namespace Utilities.Extensions
{
    using System;
    using System.Collections.Generic;

    public static class ResultExtensions
    {
        public static TOut Match<TIn, TOut>(this Result<TIn> result, TOut success, TOut failure)
        {
            Guard.NotNull(result, nameof(result));

            return result.IsSuccess ? success : failure;
        }

        public static TOut Match<TValue, TFailure, TOut>(
            this Result<TValue, TFailure> result,
            TOut success,
            TOut failure)
        {
            Guard.NotNull(result, nameof(result));

            return result.IsSuccess ? success : failure;
        }

        public static TOut Match<TIn, TOut>(this Result<TIn> result, Func<TOut> success, Func<TIn, TOut> failure)
        {
            Guard.NotNull(result, nameof(result));
            Guard.NotNull(success, nameof(success));
            Guard.NotNull(failure, nameof(failure));

            return result.IsFailure ? failure(result.GetFailure()) : success();
        }

        public static TOut Match<TValue, TFailure, TOut>(
            this Result<TValue, TFailure> result,
            Func<TValue, TOut> success,
            Func<TFailure, TOut> failure)
        {
            Guard.NotNull(result, nameof(result));
            Guard.NotNull(success, nameof(success));
            Guard.NotNull(failure, nameof(failure));

            return result.IsSuccess ? success(result.GetValue()) : failure(result.GetFailure());
        }

        public static void Match<TIn>(this Result<TIn> result, Action success, Action failure)
        {
            Guard.NotNull(result, nameof(result));
            Guard.NotNull(success, nameof(success));
            Guard.NotNull(failure, nameof(failure));

            if (result.IsSuccess)
            {
                success();
            }
            else
            {
                failure();
            }
        }

        public static void Match<TValue, TFailure>(this Result<TValue, TFailure> result, Action success, Action failure)
        {
            Guard.NotNull(result, nameof(result));
            Guard.NotNull(success, nameof(success));
            Guard.NotNull(failure, nameof(failure));

            if (result.IsSuccess)
            {
                success();
            }
            else
            {
                failure();
            }
        }

        public static void Match<TFailure>(this Result<TFailure> result, Action success, Action<TFailure> failure)
        {
            Guard.NotNull(result, nameof(result));
            Guard.NotNull(success, nameof(success));
            Guard.NotNull(failure, nameof(failure));

            if (result.IsSuccess)
            {
                success();
            }
            else
            {
                failure(result.GetFailure());
            }
        }

        public static void Match<TValue, TFailure>(
            this Result<TValue, TFailure> result,
            Action<TValue> success,
            Action<TFailure> failure)
        {
            Guard.NotNull(result, nameof(result));
            Guard.NotNull(success, nameof(success));
            Guard.NotNull(failure, nameof(failure));

            if (result.IsSuccess)
            {
                success(result.GetValue());
            }
            else
            {
                failure(result.GetFailure());
            }
        }

        public static Result<TOut> Bind<TIn, TOut>(this Result<TIn> result, Func<TIn, Result<TOut>> binder)
        {
            Guard.NotNull(result, nameof(result));
            Guard.NotNull(binder, nameof(binder));

            return Match(result, Result<TOut>.Success, binder);
        }

        public static Result<TOutValue, TOutFailure> Bind<TInValue, TInFailure, TOutValue, TOutFailure>(
            this Result<TInValue, TInFailure> result,
            Func<TInValue, Result<TOutValue, TOutFailure>> successBinder,
            Func<TInFailure, Result<TOutValue, TOutFailure>> failureBinder)
        {
            Guard.NotNull(result, nameof(result));
            Guard.NotNull(successBinder, nameof(successBinder));
            Guard.NotNull(failureBinder, nameof(failureBinder));

            return Match(result, successBinder, failureBinder);
        }

        public static Result<TOutValue, TFailure> BindOnSuccess<TInValue, TOutValue, TFailure>(
            this Result<TInValue, TFailure> result,
            Func<TInValue, Result<TOutValue, TFailure>> successBinder)
        {
            return Bind(result, successBinder, Result<TOutValue, TFailure>.Failure);
        }

        public static Result<TFailure> Flatten<TFailure>(this Result<Result<TFailure>> result)
        {
            Guard.NotNull(result, nameof(result));

            return Match(result, Result<TFailure>.Success, failure => failure);
        }

        public static Result<TValue, TFailure> Flatten<TValue, TFailure>(
            this Result<Result<TValue, TFailure>, TFailure> result)
        {
            Guard.NotNull(result, nameof(result));

            return Match(result, success => success, Result<TValue, TFailure>.Failure);
        }

        public static IEnumerable<TFailure> AsEnumerable<TFailure>(this Result<TFailure> result)
        {
            Guard.NotNull(result, nameof(result));

            if (result.IsFailure)
            {
                yield return result.GetFailure();
            }
        }

        public static IEnumerable<TValue> AsValueEnumerable<TValue, TFailure>(this Result<TValue, TFailure> result)
        {
            Guard.NotNull(result, nameof(result));

            if (result.IsSuccess)
            {
                yield return result.GetValue();
            }
        }

        public static IEnumerable<TFailure> AsFailureEnumerable<TValue, TFailure>(this Result<TValue, TFailure> result)
        {
            Guard.NotNull(result, nameof(result));

            if (result.IsFailure)
            {
                yield return result.GetFailure();
            }
        }

        public static IEnumerable<TSource> GetFailures<TSource>(this IEnumerable<Result<TSource>> source)
        {
            Guard.NotNull(source, nameof(source));

            foreach (var (isFailure, failure) in source)
            {
                if (isFailure)
                {
                    yield return failure;
                }
            }
        }

        public static IEnumerable<TFailure> GetFailures<TValue, TFailure>(
            this IEnumerable<Result<TValue, TFailure>> source)
        {
            Guard.NotNull(source, nameof(source));

            foreach (var (_, _, isFailure, failure) in source)
            {
                if (isFailure)
                {
                    yield return failure;
                }
            }
        }

        public static IEnumerable<TValue> GetValues<TValue, TFailure>(this IEnumerable<Result<TValue, TFailure>> source)
        {
            Guard.NotNull(source, nameof(source));

            foreach (var (isSuccess, value, _, _) in source)
            {
                if (isSuccess)
                {
                    yield return value;
                }
            }
        }
    }
}
