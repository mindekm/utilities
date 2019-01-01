namespace Utilities
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using Extensions;
    using JetBrains.Annotations;

    /// <summary>
    /// Contains precondition assertions.
    /// </summary>
    [DebuggerStepThrough]
    public static class Guard
    {
        /// <summary>
        /// Asserts that the <paramref name="item"/> is not null. Used to validate public method calls.
        /// </summary>
        /// <typeparam name="T">Type of item.</typeparam>
        /// <param name="item">The item.</param>
        /// <param name="parameterName">Name of the parameter.</param>
        /// <exception cref="ArgumentNullException">Thrown if the <paramref name="item"/> is null.</exception>
        [ContractAnnotation("item:null => halt")]
        public static void NotNull<T>([NoEnumeration] T item, [InvokerParameterName] [NotNull] string parameterName)
            where T : class
        {
            Ensure.That(item).IsNotNull(Error.NullArgument(parameterName));
        }

        /// <summary>
        /// Asserts that the <paramref name="item"/> is not null. Used to validate public method calls.
        /// </summary>
        /// <typeparam name="T">Type of item.</typeparam>
        /// <param name="item">The item.</param>
        /// <param name="parameterName">Name of the parameter.</param>
        /// <exception cref="ArgumentNullException">Thrown if the <paramref name="item"/> is null.</exception>
        [ContractAnnotation("item:null => halt")]
        public static void NotNull<T>([NoEnumeration] T? item, [InvokerParameterName] [NotNull] string parameterName)
            where T : struct
        {
            Ensure.That(item).IsNotNull(() => Error.NullArgument(parameterName));
        }

        [ContractAnnotation("source:null => halt")]
        public static void NotNullOrEmpty<T>(ICollection<T> source, [InvokerParameterName] [NotNull] string parameterName)
        {
            Ensure.That(source).IsNotNull(Error.NullArgument(parameterName));
            Ensure.That(source).Fails(c => c.Count == 0, new ArgumentException(/* TODO */));
        }

        /// <summary>
        /// Asserts that the <paramref name="item"/> is not null, empty or whitespace.
        /// Used to validate public method calls.
        /// </summary>
        /// <param name="item">The item.</param>
        /// <param name="parameterName">Name of the parameter.</param>
        /// <exception cref="ArgumentNullException">Thrown if the <paramref name="item"/> is null.</exception>
        /// <exception cref="ArgumentException">Thrown if the <paramref name="item"/> is
        /// empty or whitespace.</exception>
        [ContractAnnotation("item:null => halt")]
        public static void NotEmpty(string item, [InvokerParameterName] [NotNull] string parameterName)
        {
            Ensure.That(item).IsNotNull(Error.NullArgument(parameterName));
            Ensure.That(item).Fails(s => s.Trim().Length == 0, Error.EmptyStringArgument(parameterName));
        }
    }
}
