namespace Utilities;

using System.Collections.Generic;

public static class EitherExtensions
{
    public static IEnumerable<TLeft> GetLeftValues<TLeft, TRight>(this IEnumerable<Either<TLeft, TRight>> source)
    {
        Guard.NotNull(source);

        foreach (var either in source)
        {
            if (either.TryUnwrapLeft(out var left))
            {
                yield return left;
            }
        }
    }

    public static IEnumerable<TRight> GetRightValues<TLeft, TRight>(this IEnumerable<Either<TLeft, TRight>> source)
    {
        Guard.NotNull(source);

        foreach (var either in source)
        {
            if (either.TryUnwrapRight(out var right))
            {
                yield return right;
            }
        }
    }
}