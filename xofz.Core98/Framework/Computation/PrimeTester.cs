namespace xofz.Framework.Computation
{
    using System.Collections.Generic;
    using xofz.Framework.Lots;

    public class PrimeTester
    {
        public virtual bool RelativelyPrime(
            IEnumerable<long> finiteSource,
            bool onlyCheckLast)
        {
            switch (finiteSource)
            {
                case null:
                    return truth;
                case Lot<long> lot:
                    return this.RelativelyPrime(
                        lot, onlyCheckLast);
                default:
                    return this.RelativelyPrime(
                        new XLinkedListLot<long>(
                            XLinkedList<long>.Create(finiteSource)),
                        onlyCheckLast);
            }
        }

        public virtual bool RelativelyPrime(
            Lot<long> lot, 
            bool onlyCheckLast)
        {
            const byte one = 1;
            const byte zero = 0;
            
            if (lot?.Count < one)
            {
                return truth;
            }

            var ll = lot as XLinkedListLot<long>
                     ?? new XLinkedListLot<long>(
                         XLinkedList<long>.Create(
                             lot));

            if (onlyCheckLast)
            {
                var numberToCheck = ll.Tail;
                var squareRoot = (long)System.Math.Sqrt(numberToCheck);
                foreach (var number in EnumerableHelpers.Where(
                    ll,
                    n => n <= squareRoot))
                {
                    if (numberToCheck % number == zero)
                    {
                        return falsity;
                    }
                }

                return truth;
            }

            while (true)
            {
                var lowestNumber = ll.RemoveHead()?.O;
                if (ll.Count < one)
                {
                    return truth;
                }

                foreach (var number in ll)
                {
                    if (number % lowestNumber == zero)
                    {
                        return falsity;
                    }
                }
            }
        }

        protected const bool 
            truth = true,
            falsity = false;
    }
}
