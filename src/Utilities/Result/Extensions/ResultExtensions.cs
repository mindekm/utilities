namespace Utilities;

using System;
using System.Collections.Generic;

public static class ResultExtensions
{
    public static TOut Match<TIn, TOut>(this Result<TIn> result, TOut success, TOut failure)
    {
        Guard.NotNull(result);

        return result.IsSuccess ? success : failure;
    }

    public static TOut Match<TValue, TFailure, TOut>(
        this Result<TValue, TFailure> result,
        TOut success,
        TOut failure)
    {
        Guard.NotNull(result);

        return result.IsSuccess ? success : failure;
    }

    public static TOut Match<TIn, TOut>(this Result<TIn> result, Func<TOut> success, Func<TIn, TOut> failure)
    {
        Guard.NotNull(result);
        Guard.NotNull(success);
        Guard.NotNull(failure);

        return result.IsFailure ? failure(result.GetFailure()) : success();
    }

    public static TOut Match<TValue, TFailure, TOut>(
        this Result<TValue, TFailure> result,
        Func<TValue, TOut> success,
        Func<TFailure, TOut> failure)
    {
        Guard.NotNull(result);
        Guard.NotNull(success);
        Guard.NotNull(failure);

        return result.IsSuccess ? success(result.GetValue()) : failure(result.GetFailure());
    }

    public static void Match<TIn>(this Result<TIn> result, Action success, Action failure)
    {
        Guard.NotNull(result);
        Guard.NotNull(success);
        Guard.NotNull(failure);

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
        Guard.NotNull(result);
        Guard.NotNull(success);
        Guard.NotNull(failure);

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
        Guard.NotNull(result);
        Guard.NotNull(success);
        Guard.NotNull(failure);

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
        Guard.NotNull(result);
        Guard.NotNull(success);
        Guard.NotNull(failure);

        if (result.IsSuccess)
        {
            success(result.GetValue());
        }
        else
        {
            failure(result.GetFailure());
        }
    }

    public static Result<TOut> Bind<TIn, TOut>(
        this Result<TIn> result,
        Func<Result<TOut>> successBinder,
        Func<TIn, Result<TOut>> failureBinder)
    {
        Guard.NotNull(result);
        Guard.NotNull(successBinder);
        Guard.NotNull(failureBinder);

        return result.IsSuccess ? successBinder() : failureBinder(result.GetFailure());
    }

    public static Result<T> BindOnSuccess<T>(this Result<T> result, Func<Result<T>> successBinder)
    {
        Guard.NotNull(result);
        Guard.NotNull(successBinder);

        return result.IsSuccess ? successBinder() : result;
    }

    public static Result<T> BindOnFailure<T>(this Result<T> result, Func<T, Result<T>> failureBinder)
    {
        Guard.NotNull(result);
        Guard.NotNull(failureBinder);

        return result.IsFailure ? failureBinder(result.GetFailure()) : result;
    }

    public static Result<TOutValue, TOutFailure> Bind<TInValue, TInFailure, TOutValue, TOutFailure>(
        this Result<TInValue, TInFailure> result,
        Func<TInValue, Result<TOutValue, TOutFailure>> successBinder,
        Func<TInFailure, Result<TOutValue, TOutFailure>> failureBinder)
    {
        Guard.NotNull(result);
        Guard.NotNull(successBinder);
        Guard.NotNull(failureBinder);

        return result.IsSuccess ? successBinder(result.GetValue()) : failureBinder(result.GetFailure());
    }

    public static Result<TOutValue, TFailure> BindOnSuccess<TInValue, TOutValue, TFailure>(
        this Result<TInValue, TFailure> result,
        Func<TInValue, Result<TOutValue, TFailure>> successBinder)
    {
        Guard.NotNull(result);
        Guard.NotNull(successBinder);

        return result.IsSuccess ? successBinder(result.GetValue()) : Result<TOutValue, TFailure>.Failure(result.GetFailure());
    }

    public static Result<TValue, TOutFailure> BindOnFailure<TValue, TInFailure, TOutFailure>(
        this Result<TValue, TInFailure> result,
        Func<TInFailure, Result<TValue, TOutFailure>> failureBinder)
    {
        Guard.NotNull(result);
        Guard.NotNull(failureBinder);

        return result.IsFailure ? failureBinder(result.GetFailure()) : Result<TValue, TOutFailure>.Success(result.GetValue());
    }

    public static Result<TOutFailure> Bind<TValue, TInFailure, TOutFailure>(
        this Result<TValue, TInFailure> result,
        Func<TValue, Result<TOutFailure>> successBinder,
        Func<TInFailure, Result<TOutFailure>> failureBinder)
    {
        Guard.NotNull(result);
        Guard.NotNull(successBinder);
        Guard.NotNull(failureBinder);

        return result.IsSuccess ? successBinder(result.GetValue()) : failureBinder(result.GetFailure());
    }

    public static Result<TFailure> BindOnSuccess<TValue, TFailure>(
        this Result<TValue, TFailure> result,
        Func<TValue, Result<TFailure>> successBinder)
    {
        Guard.NotNull(result);
        Guard.NotNull(successBinder);

        return result.IsSuccess ? successBinder(result.GetValue()) : Result<TFailure>.Failure(result.GetFailure());
    }

    public static Result<TOutFailure> BindOnFailure<TValue, TInFailure, TOutFailure>(
        this Result<TValue, TInFailure> result,
        Func<TInFailure, Result<TOutFailure>> failureBinder)
    {
        Guard.NotNull(result);
        Guard.NotNull(failureBinder);

        return result.IsFailure ? failureBinder(result.GetFailure()) : Result<TOutFailure>.Success();
    }

    public static Result<T> DoOnSuccess<T>(this Result<T> result, Action action)
    {
        Guard.NotNull(result);
        Guard.NotNull(action);

        if (result.IsSuccess)
        {
            action();
        }

        return result;
    }

    public static Result<T> DoOnFailure<T>(this Result<T> result, Action action)
    {
        Guard.NotNull(result);
        Guard.NotNull(action);

        if (result.IsFailure)
        {
            action();
        }

        return result;
    }

    public static Result<T> DoOnFailure<T>(this Result<T> result, Action<T> action)
    {
        Guard.NotNull(result);
        Guard.NotNull(action);

        if (result.IsFailure)
        {
            action(result.GetFailure());
        }

        return result;
    }

    public static Result<T> DoOnBoth<T>(this Result<T> result, Action action)
    {
        Guard.NotNull(result);
        Guard.NotNull(action);

        action();
        return result;
    }

    public static Result<T> DoOnBoth<T>(this Result<T> result, Action<Result<T>> action)
    {
        Guard.NotNull(result);
        Guard.NotNull(action);

        action(result);
        return result;
    }

    public static Result<TValue, TFailure> DoOnSuccess<TValue, TFailure>(
        this Result<TValue, TFailure> result,
        Action action)
    {
        Guard.NotNull(result);
        Guard.NotNull(action);

        if (result.IsSuccess)
        {
            action();
        }

        return result;
    }

    public static Result<TValue, TFailure> DoOnSuccess<TValue, TFailure>(
        this Result<TValue, TFailure> result,
        Action<TValue> action)
    {
        Guard.NotNull(result);
        Guard.NotNull(action);

        if (result.IsSuccess)
        {
            action(result.GetValue());
        }

        return result;
    }

    public static Result<TValue, TFailure> DoOnFailure<TValue, TFailure>(
        this Result<TValue, TFailure> result,
        Action action)
    {
        Guard.NotNull(result);
        Guard.NotNull(action);

        if (result.IsFailure)
        {
            action();
        }

        return result;
    }

    public static Result<TValue, TFailure> DoOnFailure<TValue, TFailure>(
        this Result<TValue, TFailure> result,
        Action<TFailure> action)
    {
        Guard.NotNull(result);
        Guard.NotNull(action);

        if (result.IsFailure)
        {
            action(result.GetFailure());
        }

        return result;
    }

    public static Result<TValue, TFailure> DoOnBoth<TValue, TFailure>(
        this Result<TValue, TFailure> result,
        Action action)
    {
        Guard.NotNull(result);
        Guard.NotNull(action);

        action();
        return result;
    }

    public static Result<TValue, TFailure> DoOnBoth<TValue, TFailure>(
        this Result<TValue, TFailure> result,
        Action<Result<TValue, TFailure>> action)
    {
        Guard.NotNull(result);
        Guard.NotNull(action);

        action(result);
        return result;
    }

    public static Result<TFailure> Flatten<TFailure>(this Result<Result<TFailure>> result)
    {
        Guard.NotNull(result);

        return Match(result, Result<TFailure>.Success, failure => failure);
    }

    public static Result<TValue, TFailure> Flatten<TValue, TFailure>(
        this Result<Result<TValue, TFailure>, TFailure> result)
    {
        Guard.NotNull(result);

        return Match(result, success => success, Result<TValue, TFailure>.Failure);
    }

    public static IEnumerable<TFailure> AsEnumerable<TFailure>(this Result<TFailure> result)
    {
        Guard.NotNull(result);

        if (result.IsFailure)
        {
            yield return result.GetFailure();
        }
    }

    public static IEnumerable<TValue> AsValueEnumerable<TValue, TFailure>(this Result<TValue, TFailure> result)
    {
        Guard.NotNull(result);

        if (result.IsSuccess)
        {
            yield return result.GetValue();
        }
    }

    public static IEnumerable<TFailure> AsFailureEnumerable<TValue, TFailure>(this Result<TValue, TFailure> result)
    {
        Guard.NotNull(result);

        if (result.IsFailure)
        {
            yield return result.GetFailure();
        }
    }

    public static IEnumerable<TSource> GetFailures<TSource>(this IEnumerable<Result<TSource>> source)
    {
        Guard.NotNull(source);

        foreach (var result in source)
        {
            if (result.TryGetFailure(out var failure))
            {
                yield return failure;
            }
        }
    }

    public static IEnumerable<TFailure> GetFailures<TValue, TFailure>(
        this IEnumerable<Result<TValue, TFailure>> source)
    {
        Guard.NotNull(source);

        foreach (var result in source)
        {
            if (result.TryGetFailure(out var failure))
            {
                yield return failure;
            }
        }
    }

    public static IEnumerable<TValue> GetValues<TValue, TFailure>(this IEnumerable<Result<TValue, TFailure>> source)
    {
        Guard.NotNull(source);

        foreach (var result in source)
        {
            if (result.TryGetValue(out var value))
            {
                yield return value;
            }
        }
    }
}