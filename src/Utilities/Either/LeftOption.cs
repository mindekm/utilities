namespace Utilities
{
    // Used to achieve return type inference of the Left case.
    // See implicit conversion on Either<TLeft, TRight>.
    // Concept by Alexey Golub: https://tyrrrz.me/blog/return-type-inference/
    // Care should be taken when using this technique to
    // avoid unexpected behaviour such as: https://vkontech.com/avoid-conversion-operators/.
    public readonly struct LeftOption<T>
    {
        public LeftOption(T value)
        {
            Value = value;
        }

        public T Value { get; }
    }
}