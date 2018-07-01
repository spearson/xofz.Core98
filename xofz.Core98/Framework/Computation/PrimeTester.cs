namespace xofz.Framework.Computation
{
    using System;
    using System.Collections.Generic;

    public class PrimeTester
    {
        public virtual bool RelativelyPrime(IEnumerable<long> numbers, bool onlyCheckLast)
        {
            LinkedList<long> orderedLinkedList;
            if (onlyCheckLast)
            {
                orderedLinkedList = new LinkedList<long>(numbers);
                var numberToCheck = orderedLinkedList.Last.Value;
                var squareRoot = (long)Math.Sqrt(numberToCheck);
                var smallLL = new LinkedList<long>();
                foreach (var number in orderedLinkedList)
                {
                    if (number <= squareRoot)
                    {
                        smallLL.AddLast(number);
                    }
                }

                foreach (var number in smallLL)
                {
                    if (numberToCheck % number == 0)
                    {
                        return false;
                    }
                }

                return true;
            }

            orderedLinkedList = new LinkedList<long>(numbers);
            while (true)
            {
                var lowestNumber = orderedLinkedList.First.Value;
                orderedLinkedList.RemoveFirst();
                if (orderedLinkedList.Count == 0)
                {
                    return true;
                }

                foreach (var number in orderedLinkedList)
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
