namespace Utilities
{
    using System.Collections.Generic;

    public static class EitherExtensions
    {
        public static IEnumerable<TLeft> GetLeftValues<TLeft, TRight>(this IEnumerable<Either<TLeft, TRight>> source)
        {
            Guard.NotNull(source, nameof(source));

            foreach (var (isLeft, left, _, _) in source)
            {
                if (isLeft)
                {
                    yield return left;
                }
            }
        }

        public static IEnumerable<TRight> GetRightValues<TLeft, TRight>(this IEnumerable<Either<TLeft, TRight>> source)
        {
            Guard.NotNull(source, nameof(source));

            foreach (var (_, _, isRight, right) in source)
            {
                if (isRight)
                {
                    yield return right;
                }
            }
        }
    }
}