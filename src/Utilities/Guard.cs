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
            if (item is null)
            {
                throw Error.NullArgument(parameterName);
            }
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
            if (!item.HasValue)
            {
                throw Error.NullArgument(parameterName);
            }
        }

        /// <summary>
        /// Asserts that the <paramref name="source"/> collection is not null or empty.
        /// Used to validate public method calls.
        /// </summary>
        /// <typeparam name="T">Type of the collection items.</typeparam>
        /// <param name="source">The collection.</param>
        /// <param name="parameterName">Name of the parameter.</param>
        /// <exception cref="ArgumentNullException">Thrown if the <paramref name="source"/> is null.</exception>
        /// <exception cref="ArgumentException">Thrown if the <paramref name="source"/> is empty.</exception>
        [ContractAnnotation("source:null => halt")]
        public static void NotNullOrEmpty<T>(ICollection<T> source, [InvokerParameterName] [NotNull] string parameterName)
        {
            Ensure.That(source).IsNotNull(Error.NullArgument(parameterName));
            Ensure.That(source).IsNotEmpty(Error.EmptyCollectionArgument(parameterName));
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
            Ensure.That(item).IsNotEmpty(Error.EmptyStringArgument(parameterName));
        }
    }
}
