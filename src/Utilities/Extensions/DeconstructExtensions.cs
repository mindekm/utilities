namespace Utilities;

using System.ComponentModel;

[EditorBrowsable(EditorBrowsableState.Never)]
public static class DeconstructExtensions
{
    public static void Deconstruct<T>(this T? nullable, out bool hasValue, out T? value)
        where T : struct => (hasValue, value) = (nullable.HasValue, nullable.GetValueOrDefault());
}
