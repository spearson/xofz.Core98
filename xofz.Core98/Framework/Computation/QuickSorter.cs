namespace xofz.Framework.Computation
{
    using System;
    using System.Collections.Generic;

    public class QuickSorter
    {
        public virtual IComparable[] Sort(
            IEnumerable<IComparable> finiteSource)
        {
            if (finiteSource is IComparable[] array)
            {
                this.Sort(array);
                return array;
            }

            var sourceArray = EnumerableHelpers.ToArray(
                finiteSource);
            this.Sort(sourceArray, 0, sourceArray.Length - 1);

            return sourceArray;
        }

        public virtual void Sort(
            IComparable[] array)
        {
            if (array == null)
            {
                return;
            }

            this.Sort(array, 0, array.Length - 1);
        }

        public virtual void Sort(
            IComparable[] array,
            long left,
            long right)
        {
            if (left < 0)
            {
                return;
            }

            if (right < 0)
            {
                return;
            }

            if (left >= right)
            {
                return;
            }

            if (left > array.Length - 1)
            {
                return;
            }

            if (right > array.Length - 1)
            {
                return;
            }

            ICollection<long> closedRights
                = new LinkedList<long>();

            beginSort:
            long lowIndex = left, highIndex = right;
            long pivotIndex;
            checked
            {
                pivotIndex = left + right;
            }

            pivotIndex /= 2;
            var pivot = array[pivotIndex];
            while (lowIndex <= highIndex)
            {
                while (array[lowIndex].CompareTo(pivot) < 0)
                {
                    ++lowIndex;
                }

                while (array[highIndex].CompareTo(pivot) > 0)
                {
                    --highIndex;
                }

                if (lowIndex > highIndex)
                {
                    break;
                }

                var lowSwap = array[lowIndex];
                array[lowIndex] = array[highIndex];
                array[highIndex] = lowSwap;

                ++lowIndex;
                --highIndex;
            }

            if (left < highIndex)
            {
                closedRights.Add(right);
                right = highIndex;
                goto beginSort;
            }

            foreach (var closedRight in closedRights)
            {
                if (lowIndex < closedRight)
                {
                    left = lowIndex;
                    right = closedRight;
                    goto beginSort;
                }
            }
        }
    }
}
