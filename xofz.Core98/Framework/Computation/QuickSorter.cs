namespace xofz.Framework.Computation
{
    using System;
    using System.Collections.Generic;

    public class QuickSorter
    {
        public virtual T[] Sort<T>(
            IEnumerable<T> finiteSource)
            where T : IComparable
        {
            if (finiteSource == null)
            {
                return new T[0];
            }

            if (finiteSource is T[] array)
            {
                this.Sort(array, 0, array.Length - 1);
                return array;
            }

            var sourceArray = EnumerableHelpers.ToArray(
                finiteSource);
            this.Sort(sourceArray, 0, sourceArray.Length - 1);

            return sourceArray;
        }

        public virtual void Sort<T>(
            T[] array) where T : IComparable
        {
            if (array == null)
            {
                return;
            }

            this.Sort(
                array,
                0,
                array.Length - 1);
        }

        public virtual void Sort<T>(
            T[] array,
            long left,
            long right)
            where T : IComparable
        {
            if (array == null)
            {
                return;
            }

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

            pivotIndex >>= 1;
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

            if (lowIndex < right)
            {
                left = lowIndex;
                goto beginSort;
            }

            foreach (var closedRight in closedRights)
            {
                if (lowIndex >= closedRight)
                {
                    continue;
                }

                left = lowIndex;
                right = closedRight;
                closedRights.Remove(closedRight);
                goto beginSort;
            }
        }

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
            if (array == null)
            {
                return;
            }

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

            pivotIndex >>= 1;
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

            if (lowIndex < right)
            {
                left = lowIndex;
                goto beginSort;
            }

            foreach (var closedRight in closedRights)
            {
                if (lowIndex >= closedRight)
                {
                    continue;
                }

                left = lowIndex;
                right = closedRight;
                closedRights.Remove(closedRight);
                goto beginSort;
            }
        }
    }
}
