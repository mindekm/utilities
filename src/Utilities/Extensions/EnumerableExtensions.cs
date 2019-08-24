namespace Utilities.Extensions
{
    using System.Collections.Generic;

    public static class EnumerableExtensions
    {
        public static IEnumerable<(T, int)> EnumerateWithIndex<T>(this IEnumerable<T> source)
        {
            var index = -1;

            foreach (var item in source)
            {
                checked
                {
                    index++;
                }

                yield return (item, index);
            }
        }

        public static IEnumerable<(T, int)> EnumerateWithProgress<T>(this IEnumerable<T> source)
        {
            var index = 0;

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