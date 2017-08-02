namespace xofz
{
    using System.Collections.Generic;

    public interface MaterializedEnumerable<T> : IEnumerable<T>
    {
        long Count { get; }
    }

    public class MEHelpers
    {
        public static T[] ToArray<T>(MaterializedEnumerable<T> me)
        {
            if (me == default(MaterializedEnumerable<T>))
            {
                return new T[0];
            }

            var array = new T[me.Count];
            var e = me.GetEnumerator();
            for (var i = 0; i < array.Length; ++i)
            {
                e.MoveNext();
                array[i] = e.Current;
            }

            e.Dispose();
            return array;
        }
    }
}