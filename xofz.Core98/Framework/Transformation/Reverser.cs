namespace xofz.Framework.Transformation
{
    using System;
    using System.Collections.Generic;
    using System.Threading;

    public class Reverser
    {
        public virtual void Reverse<T>(
            IList<T> list)
        {
            if (list == null || list.Count < two)
            {
                return;
            }

            switch (list)
            {
                case List<T> concreteList:
                    concreteList.Reverse();
                    return;
                case T[] array:
                    Array.Reverse(array);
                    return;
            }

            this.reverseProtected(
                list,
                Environment.ProcessorCount);

        }

        protected virtual void reverseProtected<T>(
            IList<T> list,
            int parallelizationCount)
        {
            const byte zero = 0;
            const byte one = 1;
            if (list == null || parallelizationCount < one)
            {
                return;
            }
            
            var c = list.Count;
            var midpoint = c / two;
            ICollection<ManualResetEvent> finishedCollection =
                new XLinkedList<ManualResetEvent>();
            for (int currentProc = zero;
                currentProc < parallelizationCount;
                ++currentProc)
            {
                var e = new ManualResetEvent(false);
                finishedCollection.Add(
                    e);
                var multipleIndex = currentProc;
                ThreadPool.QueueUserWorkItem(o =>
                {
                    T swapItem;
                    var lastHalfIndex = c - multipleIndex - one;
                    for (var firstHalfIndex = multipleIndex;
                        firstHalfIndex < midpoint;
                        firstHalfIndex += parallelizationCount)
                    {
                        swapItem = list[firstHalfIndex];
                        list[firstHalfIndex] = list[lastHalfIndex];
                        list[lastHalfIndex] = swapItem;
                        lastHalfIndex -= parallelizationCount;
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
