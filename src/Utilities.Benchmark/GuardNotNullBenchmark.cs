namespace Utilities.Benchmark;

using System;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;
using BenchmarkDotNet.Attributes;

[MemoryDiagnoser]
public class GuardNotNullGenericsBenchmark
{
    [Benchmark(Baseline = true)]
    public string Generic()
    {
        var input = Guid.NewGuid().ToString();
        
        Guard.NotNullGeneric(input);

        return input;
    }

    [Benchmark]
    public string NonGeneric()
    {
        var input = Guid.NewGuid().ToString();
        
        Guard.NotNull(input);

        return input;
    }

    private class Guard
    {
        public static void NotNullGeneric<T>(T parameter, [CallerArgumentExpression("parameter")] string parameterName = default)
            where T : class
        {
            if (parameter is null)
            {
                ThrowArgumentNullException(parameterName);
            }
        }

        public static void NotNull(object parameter, [CallerArgumentExpression("parameter")] string parameterName = default)
        {
            if (parameter is null)
            {
                ThrowArgumentNullException(parameterName);
            }
        }

        [DoesNotReturn]
        private static void ThrowArgumentNullException(string parameterName)
            => throw new ArgumentNullException(parameterName);
    }
}
