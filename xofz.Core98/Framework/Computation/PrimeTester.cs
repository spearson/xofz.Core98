namespace xofz.Framework.Computation
{
    using System;
    using System.Collections.Generic;

    public class PrimeTester
    {
        public virtual bool RelativelyPrime(
            IEnumerable<long> numbers, 
            bool onlyCheckLast)
        {
            var ll = new LinkedList<long>(numbers);
            if (ll.Count == 0)
            {
                return true;
            }

            if (onlyCheckLast)
            {
                var numberToCheck = ll.Last.Value;
                var squareRoot = (long)Math.Sqrt(numberToCheck);
                foreach (var number in EnumerableHelpers.Where(
                    ll,
                    n => n <= squareRoot))
                {
                    if (numberToCheck % number == 0)
                    {
                        return false;
                    }
                }

                return true;
            }

            while (true)
            {
                var lowestNumber = ll.First.Value;
                ll.RemoveFirst();
                if (ll.Count == 0)
                {
                    return true;
                }

                foreach (var number in ll)
                {
                    if (number % lowestNumber == 0)
                    {
                        return false;
                    }
                }
            }
        }
    }
}
