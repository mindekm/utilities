namespace Utilities.Extensions
{
    using System.Collections.Generic;

    public static class DeconstructExtensions
    {
        public static void Deconstruct<T>(this T? nullable, out bool hasValue, out T value)
            where T : struct => (hasValue, value) = (nullable.HasValue, nullable.GetValueOrDefault());

        public static void Deconstruct<TKey, TValue>(this KeyValuePair<TKey, TValue> keyValuePair, out TKey key, out TValue value)
            => (key, value) = (keyValuePair.Key, keyValuePair.Value);
    }
}
