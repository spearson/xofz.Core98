namespace xofz.Framework.Transformation
{
    using System.Collections.Generic;

    public class EnumerableSkipper
    {
        public virtual IEnumerable<T> Skip<T>(
            IEnumerable<T> source,
            long skipInterval)
        {
            if (skipInterval < one)
            {
                yield break;
            }

            var e = source?.GetEnumerator();
            if (e == null)
            {
                yield break;
            }

            long counter = zero;
            while (e.MoveNext())
            {
                ++counter;

                if (counter == skipInterval)
                {
                    counter = zero;
                    yield return e.Current;
                }
            }

            e.Dispose();
        }

        // note: if number of elements % skip point != 0,
        // will not enumerate the entire enumerable
        public virtual ICollection<T> SkipThrough<T>(
            IEnumerable<T> finiteSource,
            int skipPoint)
        {
            if (finiteSource == null)
            {
                return new XLinkedList<T>();
            }

            var ll = XLinkedList<T>.Create(finiteSource);
            var result = new XLinkedList<T>();
            for (int i = zero; i < skipPoint; ++i)
            {
                result.Append(this.Skip(ll, skipPoint));
                var o = ll.RemoveHead();
                ll.AddTail(o);
            }

            return result;
        }

        protected const byte
            zero = 0,
            one = 1;
    }
}