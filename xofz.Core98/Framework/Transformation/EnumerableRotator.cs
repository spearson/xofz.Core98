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

            const byte
                zero = 0,
                one = 1;
            Lot<T> lot = ll;
            if (lot.Count < one)
            {
                return lot;
            }

            if (goRight)
            {
                for (int i = zero; i < cycles; ++i)
                {
                    var node = ll.Last;
                    ll.RemoveLast();
                    ll.AddFirst(node);
                }

                return lot;
            }

            for (int i = zero; i < cycles; ++i)
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
