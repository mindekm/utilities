namespace Utilities
{
    using System.Collections;
    using System.Collections.Generic;

    public class NonEmptyList<T> : IReadOnlyList<T>
    {
        private readonly List<T> list;

        internal NonEmptyList(List<T> list)
        {
            this.list = list;
        }

        public int Count => list.Count;

        public T this[int index] => list[index];

        public bool Contains(T item) => list.Contains(item);

        public IEnumerator<T> GetEnumerator() => list.GetEnumerator();

        IEnumerator IEnumerable.GetEnumerator() => ((IEnumerable)list).GetEnumerator();
    }
}
