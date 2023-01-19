namespace xofz.Framework.Transformation
{
    using System.Collections.Generic;
    using System.Threading;

    public class Reverser
    {
        public virtual void Reverse<T>(
            IList<T> list)
        {
            if (list == null)
            {
                return;
            }

            var c = list.Count;
            if (c < two)
            {
                return;
            }

            switch (list)
            {
                case List<T> concreteList:
                    concreteList.Reverse();
                    return;
                case T[] array:
                    System.Array.Reverse(array);
                    return;
            }

            this.ReverseV2(
                i => list[(int)i],
                (item, i) => list[(int)i] = item,
                c);
        }

        public virtual void ReverseV2<T>(
            IndexedLinkedList<T> ll)
        {
            if (ll == null)
            {
                return;
            }

            var c = ll.LongCount;
            if (c < two)
            {
                return;
            }

            this.ReverseV2(
                i => ll[i],
                (item, i) => ll[i] = item,
                c);
        }

        public virtual void ReverseV2<T>(
            Gen<long, T> read,
            Do<T, long> assign,
            long count)
        {
            this.reverseAbstract(
                read,
                assign,
                count,
                System.Environment.ProcessorCount);
        }

        protected virtual void reverseAbstract<T>(
            Gen<long, T> read,
            Do<T, long> assign,
            long reverseCount,
            long concurrencyCount)
        {
            const byte zero = 0;
            const byte one = 1;
            if (read == null ||
                assign == null ||
                reverseCount < one ||
                concurrencyCount < one)
            {
                return;
            }

            var midpoint = reverseCount / two;
            ICollection<ManualResetEvent> finishedCollection =
                new XLinkedList<ManualResetEvent>();
            for (long currentProc = zero;
                 currentProc < concurrencyCount;
                 ++currentProc)
            {
                var e = new ManualResetEvent(false);
                finishedCollection.Add(
                    e);
                var multipleIndex = currentProc;
                ThreadPool.QueueUserWorkItem(o =>
                {
                    T swapItem;
                    var lastHalfIndex = reverseCount
                                        - multipleIndex
                                        - one;
                    for (var firstHalfIndex = multipleIndex;
                         firstHalfIndex < midpoint;
                         firstHalfIndex += concurrencyCount)
                    {
                        swapItem = read(firstHalfIndex);
                        assign(
                            read(lastHalfIndex),
                            firstHalfIndex);
                        assign(
                            swapItem,
                            lastHalfIndex);
                        lastHalfIndex -= concurrencyCount;
                    }

                    e.Set();
                });
            }

            foreach (var waitHandle in finishedCollection)
            {
                waitHandle.WaitOne();
            }
        }

        protected const byte two = 2;
    }
}
