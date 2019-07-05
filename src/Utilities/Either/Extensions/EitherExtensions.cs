namespace Utilities.Extensions
{
    using System;
    using System.Collections.Generic;

    public static class EitherExtensions
    {
        public static TOut Match<TLeft, TRight, TOut>(this in Either<TLeft, TRight> either, TOut left, TOut right)
        {
            return either.IsLeft ? left : right;
        }

        public static TOut Match<TLeft, TRight, TOut>(
            this in Either<TLeft, TRight> either,
            Func<TOut> left,
            Func<TOut> right)
        {
            Guard.NotNull(left, nameof(left));
            Guard.NotNull(right, nameof(right));

            return either.IsLeft ? left() : right();
        }

        public static TOut Match<TLeft, TRight, TOut>(
            this in Either<TLeft, TRight> either,
            Func<TLeft, TOut> left,
            Func<TRight, TOut> right)
        {
            Guard.NotNull(left, nameof(left));
            Guard.NotNull(right, nameof(right));

            return either.IsLeft ? left(either.GetLeft()) : right(either.GetRight());
        }

        public static void Do<TLeft, TRight>(this in Either<TLeft, TRight> either, Action left, Action right)
        {
            Guard.NotNull(left, nameof(left));
            Guard.NotNull(right, nameof(right));

            if (either.IsLeft)
            {
                left();
            }
            else
            {
                right();
            }
        }

        public static void Do<TLeft, TRight>(
            this in Either<TLeft, TRight> either,
            Action<TLeft> left,
            Action<TRight> right)
        {
            Guard.NotNull(left, nameof(left));
            Guard.NotNull(right, nameof(right));

            if (either.IsLeft)
            {
                left(either.GetLeft());
            }
            else
            {
                right(either.GetRight());
            }
        }

        public static Either<TOutLeft, TOutRight> Bind<TInLeft, TInRight, TOutLeft, TOutRight>(
            this in Either<TInLeft, TInRight> either,
            Func<TInLeft, Either<TOutLeft, TOutRight>> leftBinder,
            Func<TInRight, Either<TOutLeft, TOutRight>> rightBinder)
        {
            Guard.NotNull(leftBinder, nameof(leftBinder));
            Guard.NotNull(rightBinder, nameof(rightBinder));

            return either.IsLeft ? leftBinder(either.GetLeft()) : rightBinder(either.GetRight());
        }

        public static IEnumerable<TLeft> AsLeftEnumerable<TLeft, TRight>(this Either<TLeft, TRight> either)
        {
            if (either.IsLeft)
            {
                yield return either.GetLeft();
            }
        }

        public static IEnumerable<TRight> AsRightEnumerable<TLeft, TRight>(this Either<TLeft, TRight> either)
        {
            if (either.IsRight)
            {
                yield return either.GetRight();
            }
        }

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