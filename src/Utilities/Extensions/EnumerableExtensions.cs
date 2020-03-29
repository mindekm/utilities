namespace Utilities
{
    using System.Collections.Generic;

    public static class EnumerableExtensions
    {
        public static IEnumerable<(T element, int index)> EnumerateWithIndex<T>(this IEnumerable<T> source)
            => Enumerate(source, -1);

        public static IEnumerable<(T element, int index)> EnumerateWithProgress<T>(this IEnumerable<T> source)
            => Enumerate(source, 0);

        private static IEnumerable<(T element, int index)> Enumerate<T>(IEnumerable<T> source, int startIndex)
        {
            var index = startIndex;

            foreach (var item in source)
            {
                checked
                {
                    index++;
                }

                yield return (item, index);
            }
        }
    }
}