namespace Utilities
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using JetBrains.Annotations;

    /// <summary>
    /// Contains precondition assertions.
    /// </summary>
    [DebuggerStepThrough]
    public static class Guard
    {
        /// <summary>
        /// Asserts that the <paramref name="parameter"/> is not null.
        /// </summary>
        /// <typeparam name="T">Type of the <paramref name="parameter"/>.</typeparam>
        /// <param name="parameter">Parameter entering the assertion.</param>
        /// <param name="parameterName">Name of the parameter.</param>
        /// <exception cref="ArgumentNullException">Thrown if the <paramref name="parameter"/> is null.</exception>
        [ContractAnnotation("parameter:null => halt")]
        public static void NotNull<T>([NoEnumeration] T parameter, [InvokerParameterName] string parameterName)
            where T : class
        {
            if (parameter is null)
            {
                throw new ArgumentNullException(Format(parameterName), "Parameter cannot be null.");
            }
        }

        /// <summary>
        /// Asserts that the <paramref name="parameter"/> is not equal to its default value.
        /// </summary>
        /// <typeparam name="T">Type of the <paramref name="parameter"/>.</typeparam>
        /// <param name="parameter">Parameter entering the assertion.</param>
        /// <param name="parameterName">Name of the parameter.</param>
        /// <exception cref="ArgumentException">Thrown if the <paramref name="parameter"/> is equal to its default value.</exception>
        public static void NotDefault<T>([NoEnumeration] T parameter, [InvokerParameterName] string parameterName)
            where T : struct, IEquatable<T>
        {
            if (parameter.Equals(default))
            {
                throw new ArgumentException("Parameter cannot be equal to its default value.", Format(parameterName));
            }
        }

        /// <summary>
        /// Asserts that the <paramref name="parameter"/> has value.
        /// </summary>
        /// <typeparam name="T">Type of the <paramref name="parameter"/>.</typeparam>
        /// <param name="parameter">Parameter entering the assertion.</param>
        /// <param name="parameterName">Name of the parameter.</param>
        /// <exception cref="ArgumentException">Thrown if the <paramref name="parameter"/> does not have value.</exception>
        public static void HasValue<T>([NoEnumeration] T? parameter, [InvokerParameterName] string parameterName)
            where T : struct
        {
            if (!parameter.HasValue)
            {
                throw new ArgumentException("Parameter must have a value.", Format(parameterName));
            }
        }

        /// <summary>
        /// Asserts that the <paramref name="parameter"/> collection has elements.
        /// </summary>
        /// <typeparam name="T">Type of the collection elements.</typeparam>
        /// <param name="parameter">Parameter entering the assertion.</param>
        /// <param name="parameterName">Name of the parameter.</param>
        /// <exception cref="ArgumentNullException">Thrown if the <paramref name="parameter"/> is null.</exception>
        /// <exception cref="ArgumentException">Thrown if the <paramref name="parameter"/> does not have elements.</exception>
        [ContractAnnotation("parameter:null => halt")]
        public static void HasElements<T>(ICollection<T> parameter, [InvokerParameterName] string parameterName)
        {
            if (parameter is null)
            {
                throw new ArgumentNullException(Format(parameterName), "Parameter cannot be null.");
            }

            if (parameter.Count == 0)
            {
                throw new ArgumentException("Collection must have elements.", Format(parameterName));
            }
        }

        /// <summary>
        /// Asserts that the <paramref name="parameter"/> List has elements.
        /// </summary>
        /// <typeparam name="T">Type of the List elements.</typeparam>
        /// <param name="parameter">Parameter entering the assertion.</param>
        /// <param name="parameterName">Name of the parameter.</param>
        /// <exception cref="ArgumentNullException">Thrown if the <paramref name="parameter"/> is null.</exception>
        /// <exception cref="ArgumentException">Thrown if the <paramref name="parameter"/> does not have elements.</exception>
        [ContractAnnotation("parameter:null => halt")]
        public static void HasElements<T>(List<T> parameter, [InvokerParameterName] string parameterName)
        {
            if (parameter is null)
            {
                throw new ArgumentNullException(Format(parameterName), "Parameter cannot be null.");
            }

            if (parameter.Count == 0)
            {
                throw new ArgumentException("List must have elements.", Format(parameterName));
            }
        }

        /// <summary>
        /// Asserts that the <paramref name="parameter"/> HashSet has elements.
        /// </summary>
        /// <typeparam name="T">Type of the HashSet elements.</typeparam>
        /// <param name="parameter">Parameter entering the assertion.</param>
        /// <param name="parameterName">Name of the parameter.</param>
        /// <exception cref="ArgumentNullException">Thrown if the <paramref name="parameter"/> is null.</exception>
        /// <exception cref="ArgumentException">Thrown if the <paramref name="parameter"/> does not have elements.</exception>
        [ContractAnnotation("parameter:null => halt")]
        public static void HasElements<T>(HashSet<T> parameter, [InvokerParameterName] string parameterName)
        {
            if (parameter is null)
            {
                throw new ArgumentNullException(Format(parameterName), "Parameter cannot be null.");
            }

            if (parameter.Count == 0)
            {
                throw new ArgumentException("HashSet must have elements.", Format(parameterName));
            }
        }

        /// <summary>
        /// Asserts that the <paramref name="parameter"/> Dictionary has elements.
        /// </summary>
        /// <typeparam name="TKey">Type of the Dictionary key elements.</typeparam>
        /// <typeparam name="TValue">Type of the Dictionary value elements.</typeparam>
        /// <param name="parameter">Parameter entering the assertion.</param>
        /// <param name="parameterName">Name of the parameter.</param>
        /// <exception cref="ArgumentNullException">Thrown if the <paramref name="parameter"/> is null.</exception>
        /// <exception cref="ArgumentException">Thrown if the <paramref name="parameter"/> does not have elements.</exception>
        [ContractAnnotation("parameter:null => halt")]
        public static void HasElements<TKey, TValue>(Dictionary<TKey, TValue> parameter, [InvokerParameterName] string parameterName)
        {
            if (parameter is null)
            {
                throw new ArgumentNullException(Format(parameterName), "Parameter cannot be null.");
            }

            if (parameter.Count == 0)
            {
                throw new ArgumentException("Dictionary must have elements.", Format(parameterName));
            }
        }

        /// <summary>
        /// Asserts that the <paramref name="parameter"/> Array has elements.
        /// </summary>
        /// <typeparam name="T">Type of the Array elements.</typeparam>
        /// <param name="parameter">Parameter entering the assertion.</param>
        /// <param name="parameterName">Name of the parameter.</param>
        /// <exception cref="ArgumentNullException">Thrown if the <paramref name="parameter"/> is null.</exception>
        /// <exception cref="ArgumentException">Thrown if the <paramref name="parameter"/> does not have elements.</exception>
        [ContractAnnotation("parameter:null => halt")]
        public static void HasElements<T>(T[] parameter, [InvokerParameterName] string parameterName)
        {
            if (parameter is null)
            {
                throw new ArgumentNullException(Format(parameterName), "Parameter cannot be null.");
            }

            if (parameter.Length == 0)
            {
                throw new ArgumentException("Array must have elements.", Format(parameterName));
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
        public static void NotNullOrEmpty(string parameter, [InvokerParameterName] string parameterName)
        {
            if (parameter is null)
            {
                throw new ArgumentNullException(Format(parameterName), "Parameter cannot be null.");
            }

            if (parameter.Length == 0)
            {
                throw new ArgumentException("Parameter cannot be empty.", Format(parameterName));
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
        public static void NotNullOrWhitespace(string parameter, [InvokerParameterName] string parameterName)
        {
            if (parameter is null)
            {
                throw new ArgumentNullException(Format(parameterName), "Parameter cannot be null.");
            }

            if (parameter.Trim().Length == 0)
            {
                throw new ArgumentException("Parameter cannot be empty or whitespace", Format(parameterName));
            }
        }

        private static string Format(string parameterName)
            => parameterName ?? "N/A";
    }
}
