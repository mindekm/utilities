namespace Utilities;

using System.ComponentModel;

public class FailureDetails<T>
{
    public FailureDetails(T details, FailureLevel level = FailureLevel.Error)
    {
        Details = details;
        Level = level;
    }

    public T Details { get; }

    public FailureLevel Level { get; }

    public override string ToString()
    {
        return $"[{Level}] {Details}";
    }

    [EditorBrowsable(EditorBrowsableState.Never)]
    public void Deconstruct(out FailureLevel level, out T details) => (level, details) = (Level, Details);
}
