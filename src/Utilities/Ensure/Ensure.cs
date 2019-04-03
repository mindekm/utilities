namespace Utilities
{
    public static class Ensure
    {
        public static That<T> That<T>(T item)
        {
            return new That<T>(item);
        }
    }
}
