namespace Utilities
{
    using System.Collections.Generic;

    public static class EitherExtensions
    {
        public static LeftOption<T> AsLeft<T>(this T value)
            => Either.Left(value);

        public static RightOption<T> AsRight<T>(this T value)
            => Either.Right(value);

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