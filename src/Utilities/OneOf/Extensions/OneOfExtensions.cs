namespace Utilities.Extensions
{
    using System;

    public static class OneOfExtensions
    {
        public static TOut Match<TFirst, TSecond, TOut>(
            in this OneOf<TFirst, TSecond> oneOf,
            Func<TFirst, TOut> first,
            Func<TSecond, TOut> second)
        {
            if (oneOf.IsFirst)
            {
                first(oneOf.GetFirst());
            }

            if (oneOf.IsSecond)
            {
                second(oneOf.GetSecond());
            }

            throw new InvalidOperationException();
        }

        public static TOut Match<TFirst, TSecond, TThird, TOut>(
            in this OneOf<TFirst, TSecond, TThird> oneOf,
            Func<TFirst, TOut> first,
            Func<TSecond, TOut> second,
            Func<TThird, TOut> third)
        {
            if (oneOf.IsFirst)
            {
                first(oneOf.GetFirst());
            }

            if (oneOf.IsSecond)
            {
                second(oneOf.GetSecond());
            }

            if (oneOf.IsThird)
            {
                third(oneOf.GetThird());
            }

            throw new InvalidOperationException();
        }

        public static TOut Match<TFirst, TSecond, TThird, TFourth, TOut>(
            in this OneOf<TFirst, TSecond, TThird, TFourth> oneOf,
            Func<TFirst, TOut> first,
            Func<TSecond, TOut> second,
            Func<TThird, TOut> third,
            Func<TFourth, TOut> fourth)
        {
            if (oneOf.IsFirst)
            {
                first(oneOf.GetFirst());
            }

            if (oneOf.IsSecond)
            {
                second(oneOf.GetSecond());
            }

            if (oneOf.IsThird)
            {
                third(oneOf.GetThird());
            }

            if (oneOf.IsFourth)
            {
                fourth(oneOf.GetFourth());
            }

            throw new InvalidOperationException();
        }
    }
}