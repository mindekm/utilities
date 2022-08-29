namespace Utilities;

public static class Result
{
    public static Result<FailureMessage> Success() => Result<FailureMessage>.Success();

    public static Result<FailureMessage> Failure(FailureMessage failureMessage)
        => Result<FailureMessage>.Failure(failureMessage);

    public static Result<FailureMessage> Failure() => Failure(FailureMessage.Unspecified);

    public static Result<TValue, FailureMessage> Success<TValue>(TValue value)
        => Result<TValue, FailureMessage>.Success(value);

    public static Result<TValue, FailureMessage> Failure<TValue>(FailureMessage failureMessage)
        => Result<TValue, FailureMessage>.Failure(failureMessage);

    public static Result<TValue, FailureMessage> Failure<TValue>() => Failure<TValue>(FailureMessage.Unspecified);
}
