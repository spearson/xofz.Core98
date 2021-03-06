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
            if (list == null || list.Count < 2)
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
            var c = list.Count;
            var midpoint = c / 2;
            ICollection<ManualResetEvent> finishedCollection =
                new LinkedList<ManualResetEvent>();
            for (var currentProc = 0;
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
                    var lastHalfIndex = c - multipleIndex - 1;
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
    }
}
