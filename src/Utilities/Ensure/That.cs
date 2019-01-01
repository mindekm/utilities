namespace Utilities
{
    public readonly struct That<T>
    {
        public readonly T Item;

        public That(T item)
        {
            Item = item;
        }
    }
}