namespace Utilities;

public static class Either
{
    public static LeftOption<TLeft> Left<TLeft>(TLeft value) =>
        new LeftOption<TLeft>(value);

    public static RightOption<TRight> Right<TRight>(TRight value) =>
        new RightOption<TRight>(value);
}
