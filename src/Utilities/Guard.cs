namespace Utilities;

using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;
using JetBrains.Annotations;
using NotNullAttribute = System.Diagnostics.CodeAnalysis.NotNullAttribute;

/// <summary>
/// Contains precondition assertions.
/// </summary>
[DebuggerStepThrough]
public static class Guard
{
    /// <summary>
    /// Asserts that the <paramref name="parameter"/> is not null.
    /// </summary>
    /// <param name="parameter">Parameter entering the assertion.</param>
    /// <param name="parameterName">Name of the parameter.</param>
    /// <exception cref="ArgumentNullException">Thrown if the <paramref name="parameter"/> is null.</exception>
    [ContractAnnotation("parameter:null => halt")]
    public static void NotNull(
        [NoEnumeration, NotNull] object? parameter,
        [InvokerParameterName, CallerArgumentExpression("parameter")] string? parameterName = default)
    {
        if (parameter is null)
        {
            ThrowWhenParameterIsNull(parameterName);
        }
    }

    /// <summary>
    /// Asserts that the <paramref name="parameter"/> is not equal to its default value.
    /// </summary>
    /// <param name="parameter">Parameter entering the assertion.</param>
    /// <param name="parameterName">Name of the parameter.</param>
    /// <exception cref="ArgumentException">Thrown if the <paramref name="parameter"/> is equal to its default value.</exception>
    public static void NotDefault<T>(
        [NoEnumeration] T parameter,
        [InvokerParameterName, CallerArgumentExpression("parameter")] string? parameterName = default)
        where T : struct, IEquatable<T>
    {
        if (parameter.Equals(default))
        {
            ThrowWhenParameterIsDefault(parameterName);
        }
    }

    /// <summary>
    /// Asserts that the <paramref name="parameter"/> has value.
    /// </summary>
    /// <param name="parameter">Parameter entering the assertion.</param>
    /// <param name="parameterName">Name of the parameter.</param>
    /// <exception cref="ArgumentException">Thrown if the <paramref name="parameter"/> does not have value.</exception>
    public static void HasValue<T>(
        [NoEnumeration] T? parameter,
        [InvokerParameterName, CallerArgumentExpression("parameter")] string? parameterName = default)
        where T : struct
    {
        if (!parameter.HasValue)
        {
            ThrowWhenNullableParameterHasNoValue(parameterName);
        }
    }

    /// <summary>
    /// Asserts that the <paramref name="parameter"/> collection has elements.
    /// </summary>
    /// <param name="parameter">Parameter entering the assertion.</param>
    /// <param name="parameterName">Name of the parameter.</param>
    /// <exception cref="ArgumentNullException">Thrown if the <paramref name="parameter"/> is null.</exception>
    /// <exception cref="ArgumentException">Thrown if the <paramref name="parameter"/> does not have elements.</exception>
    [ContractAnnotation("parameter:null => halt")]
    public static void HasElements<T>(
        [NotNull] ICollection<T>? parameter,
        [InvokerParameterName, CallerArgumentExpression("parameter")] string? parameterName = default)
    {
        if (parameter is null)
        {
            ThrowWhenParameterIsNull(parameterName);
        }

        if (parameter.Count == 0)
        {
            ThrowWhenCollectionHasNoElements(parameterName);
        }
    }

    /// <summary>
    /// Asserts that the <paramref name="parameter"/> List has elements.
    /// </summary>
    /// <param name="parameter">Parameter entering the assertion.</param>
    /// <param name="parameterName">Name of the parameter.</param>
    /// <exception cref="ArgumentNullException">Thrown if the <paramref name="parameter"/> is null.</exception>
    /// <exception cref="ArgumentException">Thrown if the <paramref name="parameter"/> does not have elements.</exception>
    [ContractAnnotation("parameter:null => halt")]
    public static void HasElements<T>(
        [NotNull] List<T>? parameter,
        [InvokerParameterName, CallerArgumentExpression("parameter")] string? parameterName = default)
    {
        if (parameter is null)
        {
            ThrowWhenParameterIsNull(parameterName);
        }

        if (parameter.Count == 0)
        {
            ThrowWhenCollectionHasNoElements(parameterName);
        }
    }

    /// <summary>
    /// Asserts that the <paramref name="parameter"/> HashSet has elements.
    /// </summary>
    /// <param name="parameter">Parameter entering the assertion.</param>
    /// <param name="parameterName">Name of the parameter.</param>
    /// <exception cref="ArgumentNullException">Thrown if the <paramref name="parameter"/> is null.</exception>
    /// <exception cref="ArgumentException">Thrown if the <paramref name="parameter"/> does not have elements.</exception>
    [ContractAnnotation("parameter:null => halt")]
    public static void HasElements<T>(
        [NotNull] HashSet<T>? parameter,
        [InvokerParameterName, CallerArgumentExpression("parameter")] string? parameterName = default)
    {
        if (parameter is null)
        {
            ThrowWhenParameterIsNull(parameterName);
        }

        if (parameter.Count == 0)
        {
            ThrowWhenCollectionHasNoElements(parameterName);
        }
    }

    /// <summary>
    /// Asserts that the <paramref name="parameter"/> Dictionary has elements.
    /// </summary>
    /// <param name="parameter">Parameter entering the assertion.</param>
    /// <param name="parameterName">Name of the parameter.</param>
    /// <exception cref="ArgumentNullException">Thrown if the <paramref name="parameter"/> is null.</exception>
    /// <exception cref="ArgumentException">Thrown if the <paramref name="parameter"/> does not have elements.</exception>
    [ContractAnnotation("parameter:null => halt")]
    public static void HasElements<TKey, TValue>(
        [NotNull] Dictionary<TKey, TValue>? parameter,
        [InvokerParameterName, CallerArgumentExpression("parameter")] string? parameterName = default)
        where TKey : notnull
    {
        if (parameter is null)
        {
            ThrowWhenParameterIsNull(parameterName);
        }

        if (parameter.Count == 0)
        {
            ThrowWhenCollectionHasNoElements(parameterName);
        }
    }

    /// <summary>
    /// Asserts that the <paramref name="parameter"/> Array has elements.
    /// </summary>
    /// <param name="parameter">Parameter entering the assertion.</param>
    /// <param name="parameterName">Name of the parameter.</param>
    /// <exception cref="ArgumentNullException">Thrown if the <paramref name="parameter"/> is null.</exception>
    /// <exception cref="ArgumentException">Thrown if the <paramref name="parameter"/> does not have elements.</exception>
    [ContractAnnotation("parameter:null => halt")]
    public static void HasElements<T>(
        [NotNull] T[]? parameter,
        [InvokerParameterName, CallerArgumentExpression("parameter")] string? parameterName = default)
    {
        if (parameter is null)
        {
            ThrowWhenParameterIsNull(parameterName);
        }

        if (parameter.Length == 0)
        {
            ThrowWhenArrayHasNoElements(parameterName);
        }
    }

    /// <summary>
    /// Asserts that the <paramref name="parameter"/> is not null or empty.
    /// </summary>
    /// <param name="parameter">Parameter entering the assertion.</param>
    /// <param name="parameterName">Name of the parameter.</param>
    /// <exception cref="ArgumentNullException">Thrown if the <paramref name="parameter"/> is null.</exception>
    /// <exception cref="ArgumentException">Thrown is the <paramref name="parameter"/> is empty.</exception>
    [ContractAnnotation("parameter:null => halt")]
    public static void NotNullOrEmpty(
        [NotNull] string? parameter,
        [InvokerParameterName, CallerArgumentExpression("parameter")] string? parameterName = default)
    {
        if (parameter is null)
        {
            ThrowWhenParameterIsNull(parameterName);
        }

        if (parameter.Length == 0)
        {
            ThrowWhenStringIsEmpty(parameterName);
        }
    }

    /// <summary>
    /// Asserts that the <paramref name="parameter"/> is not null, empty or whitespace.
    /// </summary>
    /// <param name="parameter">Parameter entering the assertion.</param>
    /// <param name="parameterName">Name of the parameter.</param>
    /// <exception cref="ArgumentNullException">Thrown if the <paramref name="parameter"/> is null.</exception>
    /// <exception cref="ArgumentException">Thrown is the <paramref name="parameter"/> is empty or whitespace.</exception>
    [ContractAnnotation("parameter:null => halt")]
    public static void NotNullOrWhitespace(
        [NotNull] string? parameter,
        [InvokerParameterName, CallerArgumentExpression("parameter")] string? parameterName = default)
    {
        if (parameter is null)
        {
            ThrowWhenParameterIsNull(parameterName);
        }

        if (IsEmptyOrWhitespace(parameter))
        {
            ThrowWhenStringIsEmptyOrWhitespace(parameterName);
        }
    }

    /// <summary>
    /// Asserts that the <paramref name="parameter"/> is not None.
    /// </summary>
    /// <param name="parameter">Parameter entering the assertion.</param>
    /// <param name="parameterName">Name of the parameter.</param>
    /// <exception cref="ArgumentException">Thrown if the <paramref name="parameter"/> is None.</exception>
    public static void NotNone<T>(
        Maybe<T> parameter,
        [InvokerParameterName, CallerArgumentExpression("parameter")] string? parameterName = default)
    {
        if (parameter.IsNone)
        {
            ThrowWhenParameterIsNone(parameterName);
        }
    }

    /// <summary>
    /// Asserts that the <paramref name="parameter"/> is not Left.
    /// </summary>
    /// <param name="parameter">Parameter entering the assertion.</param>
    /// <param name="parameterName">Name of the parameter.</param>
    /// <exception cref="ArgumentException">Thrown if the <paramref name="parameter"/> is Left.</exception>
    public static void NotLeft<TLeft, TRight>(
        Either<TLeft, TRight> parameter,
        [InvokerParameterName, CallerArgumentExpression("parameter")] string? parameterName = default)
    {
        if (parameter.IsLeft)
        {
            ThrowWhenParameterIsLeft(parameterName);
        }
    }

    /// <summary>
    /// Asserts that the <paramref name="parameter"/> is not Failure.
    /// </summary>
    /// <param name="parameter">Parameter entering the assertion.</param>
    /// <param name="parameterName">Name of the parameter.</param>
    /// <exception cref="ArgumentNullException">Thrown if the <paramref name="parameter"/> is null.</exception>
    /// <exception cref="ArgumentException">Thrown if the <paramref name="parameter"/> is Failure.</exception>
    [ContractAnnotation("parameter:null => halt")]
    public static void NotFailure<TPayload, TFailure>(
        [NotNull] Result<TPayload, TFailure>? parameter,
        [InvokerParameterName, CallerArgumentExpression("parameter")] string? parameterName = default)
    {
        if (parameter is null)
        {
            ThrowWhenParameterIsNull(parameterName);
        }

        if (parameter.IsFailure)
        {
            ThrowWhenParameterIsFailure(parameterName);
        }
    }

    /// <summary>
    /// Asserts that the <paramref name="parameter"/> is not Failure.
    /// </summary>
    /// <param name="parameter">Parameter entering the assertion.</param>
    /// <param name="parameterName">Name of the parameter.</param>
    /// <exception cref="ArgumentNullException">Thrown if the <paramref name="parameter"/> is null.</exception>
    /// <exception cref="ArgumentException">Thrown if the <paramref name="parameter"/> is Failure.</exception>
    [ContractAnnotation("parameter:null => halt")]
    public static void NotFailure<TFailure>(
        [NotNull] Result<TFailure>? parameter,
        [InvokerParameterName, CallerArgumentExpression("parameter")] string? parameterName = default)
    {
        if (parameter is null)
        {
            ThrowWhenParameterIsNull(parameterName);
        }

        if (parameter.IsFailure)
        {
            ThrowWhenParameterIsFailure(parameterName);
        }
    }

    [DoesNotReturn]
    private static void ThrowWhenParameterIsNull(string? parameterName)
        => throw new ArgumentNullException(parameterName, "Parameter cannot be null.");

    [DoesNotReturn]
    private static void ThrowWhenParameterIsDefault(string? parameterName)
        => throw new ArgumentException("Parameter cannot be equal to its default value.", parameterName);

    [DoesNotReturn]
    private static void ThrowWhenNullableParameterHasNoValue(string? parameterName)
        => throw new ArgumentException("Parameter must have a value.", parameterName);

    [DoesNotReturn]
    private static void ThrowWhenCollectionHasNoElements(string? parameterName)
        => throw new ArgumentException("Collection must have elements.", parameterName);

    [DoesNotReturn]
    private static void ThrowWhenArrayHasNoElements(string? parameterName)
        => throw new ArgumentException("Array must have elements.", parameterName);

    [DoesNotReturn]
    private static void ThrowWhenStringIsEmpty(string? parameterName)
        => throw new ArgumentException("String cannot be empty.", parameterName);

    [DoesNotReturn]
    private static void ThrowWhenStringIsEmptyOrWhitespace(string? parameterName)
        => throw new ArgumentException("String cannot be empty or whitespace.", parameterName);

    [DoesNotReturn]
    private static void ThrowWhenParameterIsNone(string? parameterName)
        => throw new ArgumentException("Parameter cannot be None.", parameterName);

    [DoesNotReturn]
    private static void ThrowWhenParameterIsLeft(string? parameterName)
        => throw new ArgumentException("Parameter cannot be Left.", parameterName);

    [DoesNotReturn]
    private static void ThrowWhenParameterIsFailure(string? parameterName)
        => throw new ArgumentException("Parameter cannot be Failure.", parameterName);

    private static bool IsEmptyOrWhitespace(string value)
    {
        for (var i = 0; i < value.Length; i++)
        {
            if (!char.IsWhiteSpace(value[i]))
            {
                return false;
            }
        }

        return true;
    }
}
