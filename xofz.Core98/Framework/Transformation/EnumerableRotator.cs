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
            var ll = new XLinkedListLot<T>();
            if (finiteSource == null)
            {
                return ll;
            }

            foreach (var item in finiteSource)
            {
                ll.AddTail(item);
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
                    ll.AddHead(
                        ll.RemoveTail());
                }

                return lot;
            }

            for (int i = zero; i < cycles; ++i)
            {
                ll.AddTail(
                    ll.RemoveHead());
            }

            return lot;
        }

        protected const bool truth = true;
    }
}
