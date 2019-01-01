namespace Utilities.Extensions
{
    using System;
    using System.Collections.Generic;

    // Enumerable extension methods based on https://referencesource.microsoft.com/#system.core/system/linq/Enumerable.cs.html
    public static class MaybeEnumerableExtensions
    {
        /// <summary>
        /// Returns the first element of a sequence as Some, or None if the sequence contains no elements.
        /// </summary>
        /// <typeparam name="TSource">The type of the elements source.</typeparam>
        /// <param name="source">An <see cref="T:System.Collections.Generic.IEnumerable`1"></see> to return the first element of.</param>
        /// <returns>None&lt;<typeparamref name="TSource"/>&gt; if <paramref name="source">source</paramref> is empty;
        /// otherwise, the first element in <paramref name="source">source</paramref> as Some&lt;<typeparamref name="TSource"/>&gt;.</returns>
        public static Maybe<TSource> FirstOrNone<TSource>(this IEnumerable<TSource> source)
        {
            Guard.NotNull(source, nameof(source));

            switch (source)
            {
                case IList<TSource> list:
                    if (list.Count > 0)
                    {
                        return Maybe.Some(list[0]);
                    }

                    break;
                case IReadOnlyList<TSource> readOnlyList:
                    if (readOnlyList.Count > 0)
                    {
                        return Maybe.Some(readOnlyList[0]);
                    }

                    break;
                default:
                    using (var enumerator = source.GetEnumerator())
                    {
                        if (enumerator.MoveNext())
                        {
                            return Maybe.Some(enumerator.Current);
                        }
                    }

                    break;
            }

            return Maybe.None<TSource>();
        }

        /// <summary>
        /// Returns the first element of a sequence that satisfies a condition as Some, or None if no such element is found.
        /// </summary>
        /// <typeparam name="TSource">The type of the elements source.</typeparam>
        /// <param name="source">An <see cref="T:System.Collections.Generic.IEnumerable`1"></see> to return the first element of.</param>
        /// <param name="predicate">A function to test each element for condition.</param>
        /// <returns>None&lt;<typeparamref name="TSource"/>&gt; if <paramref name="source">source</paramref> is empty or if no
        /// element passes the test specified by <paramref name="predicate"/>; otherwise, the first element in <paramref name="source">source</paramref>
        /// that passes the test specified by <paramref name="predicate"/> as Some&lt;<typeparamref name="TSource"/>&gt;.</returns>
        public static Maybe<TSource> FirstOrNone<TSource>(this IEnumerable<TSource> source, Func<TSource, bool> predicate)
        {
            Guard.NotNull(source, nameof(source));
            Guard.NotNull(predicate, nameof(predicate));

            foreach (var element in source)
            {
                if (predicate(element))
                {
                    return Maybe.Some(element);
                }
            }

            return Maybe.None<TSource>();
        }

        /// <summary>
        /// Returns the last element of a sequence as Some, or None if the sequence contains no elements.
        /// </summary>
        /// <typeparam name="TSource">The type of the elements of source.</typeparam>
        /// <param name="source">An <see cref="T:System.Collections.Generic.IEnumerable`1"></see> to return the last element of.</param>
        /// <returns>None&lt;<typeparamref name="TSource"/>&gt; if the source sequence is empty;
        /// otherwise, the last element in <paramref name="source"/> as Some&lt;<typeparamref name="TSource"/>&gt;.</returns>
        public static Maybe<TSource> LastOrNone<TSource>(this IEnumerable<TSource> source)
        {
            Guard.NotNull(source, nameof(source));

            int count;
            switch (source)
            {
                case IList<TSource> list:
                    count = list.Count;
                    if (count > 0)
                    {
                        return Maybe.Some(list[count - 1]);
                    }

                    break;
                case IReadOnlyList<TSource> readOnlyList:
                    count = readOnlyList.Count;
                    if (count > 0)
                    {
                        return Maybe.Some(readOnlyList[count - 1]);
                    }

                    break;
                default:
                    using (var enumerator = source.GetEnumerator())
                    {
                        if (enumerator.MoveNext())
                        {
                            Maybe<TSource> result;
                            do
                            {
                                result = Maybe.Some(enumerator.Current);
                            }
                            while (enumerator.MoveNext());

                            return result;
                        }
                    }

                    break;
            }

            return Maybe.None<TSource>();
        }

        /// <summary>
        /// Returns the last element of a sequence that satisfies a condition as Some or None if no such element is found.
        /// </summary>
        /// <typeparam name="TSource">The type of the elements of source.</typeparam>
        /// <param name="source">An <see cref="T:System.Collections.Generic.IEnumerable`1"></see> to return the last element of.</param>
        /// <param name="predicate">A function to test each element for a condition.</param>
        /// <returns>None&lt;<typeparamref name="TSource"/>&gt; if the sequence is empty or if not elements pass the test in the predicate function;
        /// otherwise, the last element that passes the test in the predicate function as Some&lt;<typeparamref name="TSource"/>&gt;.</returns>
        public static Maybe<TSource> LastOrNone<TSource>(this IEnumerable<TSource> source, Func<TSource, bool> predicate)
        {
            Guard.NotNull(source, nameof(source));
            Guard.NotNull(predicate, nameof(predicate));

            var result = Maybe.None<TSource>();
            foreach (var element in source)
            {
                if (predicate(element))
                {
                    result = Maybe.Some(element);
                }
            }

            return result;
        }

        /// <summary>
        /// Returns the only element of a sequence as Some, or None if the sequence is empty; this method throws an exception
        /// if there is more than one element in the sequence.
        /// </summary>
        /// <typeparam name="TSource">The type of the elements of source.</typeparam>
        /// <param name="source">An <see cref="T:System.Collections.Generic.IEnumerable`1"></see> to return the single element of.</param>
        /// <returns>The single element of the input sequence as Some&lt;<typeparamref name="TSource"/>&gt;, or None&lt;<typeparamref name="TSource"/>&gt;
        /// if the sequence contains no elements.</returns>
        public static Maybe<TSource> SingleOrNone<TSource>(this IEnumerable<TSource> source)
        {
            Guard.NotNull(source, nameof(source));

            switch (source)
            {
                case IList<TSource> list:
                    switch (list.Count)
                    {
                        case 0: return Maybe.None<TSource>();
                        case 1: return Maybe.Some(list[0]);
                    }

                    break;
                case IReadOnlyList<TSource> readOnlyList:
                    switch (readOnlyList.Count)
                    {
                        case 0: return Maybe.None<TSource>();
                        case 1: return Maybe.Some(readOnlyList[0]);
                    }

                    break;
                default:
                    using (var enumerable = source.GetEnumerator())
                    {
                        if (!enumerable.MoveNext())
                        {
                            return Maybe.None<TSource>();
                        }

                        var result = Maybe.Some(enumerable.Current);
                        if (!enumerable.MoveNext())
                        {
                            return result;
                        }
                    }

                    break;
            }

            throw Error.TooManyElements();
        }

        /// <summary>
        /// Returns the only element of a sequence that satisfies a specified condition as Some, or None if no such element exists;
        /// this method throws an exception if more than one element satisfies the condition.
        /// </summary>
        /// <typeparam name="TSource">The type of the elements of source.</typeparam>
        /// <param name="source">An <see cref="T:System.Collections.Generic.IEnumerable`1"></see> to return a single element from.</param>
        /// <param name="predicate">A function to test an element for a condition.</param>
        /// <returns>The single element of the input sequence that satisfies the condition as Some&lt;<typeparamref name="TSource"/>&gt;, or None&lt;<typeparamref name="TSource"/>&gt;
        /// if no such element is found.</returns>
        public static Maybe<TSource> SingleOrNone<TSource>(this IEnumerable<TSource> source, Func<TSource, bool> predicate)
        {
            Guard.NotNull(source, nameof(source));
            Guard.NotNull(predicate, nameof(source));

            var result = Maybe.None<TSource>();
            long count = 0;
            foreach (var element in source)
            {
                if (predicate(element))
                {
                    result = Maybe.Some(element);
                    checked
                    {
                        count++;
                    }
                }
            }

            switch (count)
            {
                case 0: return Maybe.None<TSource>();
                case 1: return result;
            }

            throw Error.TooManyElements();
        }

        /// <summary>
        /// Returns the element as a specified index in a sequence as Some or None if the index is out of range.
        /// </summary>
        /// <typeparam name="TSource">The type of the elements of source.</typeparam>
        /// <param name="source">An <see cref="T:System.Collections.Generic.IEnumerable`1"></see> to return an element from.</param>
        /// <param name="index">The zero-based index of the element to retrieve.</param>
        /// <returns>None&lt;<typeparamref name="TSource"/>&gt; if the index is outside the bounds of the source sequence; otherwise,
        /// the element at the specified position in the source sequence as Some&lt;<typeparamref name="TSource"/>&gt;.</returns>
        public static Maybe<TSource> ElementAtOrNone<TSource>(this IEnumerable<TSource> source, int index)
        {
            Guard.NotNull(source, nameof(source));

            if (index >= 0)
            {
                switch (source)
                {
                    case IList<TSource> list:
                        if (index < list.Count)
                        {
                            return Maybe.Some(list[index]);
                        }

                        break;
                    case IReadOnlyList<TSource> readOnlyList:
                        if (index < readOnlyList.Count)
                        {
                            return Maybe.Some(readOnlyList[index]);
                        }

                        break;
                    default:
                        foreach (var item in source)
                        {
                            if (index == 0)
                            {
                                return Maybe.Some(item);
                            }

                            index--;
                        }

                        break;
                }
            }

            return Maybe.None<TSource>();
        }
    }
}
