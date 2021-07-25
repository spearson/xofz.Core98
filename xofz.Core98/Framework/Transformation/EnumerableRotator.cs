namespace xofz.Framework.Transformation
{
    using System.Collections.Generic;
    using xofz.Framework.Lots;

    public class EnumerableRotator
    {
        public virtual Lot<T> Rotate<T>(
            IEnumerable<T> finiteSource, 
            int cycles, 
            bool goRight = truth)
        {
            var ll = new LinkedListLot<T>();
            if (finiteSource == null)
            {
                return ll;
            }

            foreach (var item in finiteSource)
            {
                ll.AddLast(item);
            }

            Lot<T> lot = ll;
            if (lot.Count < 1)
            {
                return lot;
            }

            if (goRight)
            {
                for (var i = 0; i < cycles; ++i)
                {
                    var node = ll.Last;
                    ll.RemoveLast();
                    ll.AddFirst(node);
                }

                return lot;
            }

            for (var i = 0; i < cycles; ++i)
            {
                var node = ll.First;
                ll.RemoveFirst();
                ll.AddLast(node);
            }

            return lot;
        }

        protected const bool truth = true;
    }
}
