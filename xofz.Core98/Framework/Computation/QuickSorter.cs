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
                return new T[zero];
            }

            if (finiteSource is T[] array)
            {
                this.Sort(
                    array, 
                    zero, 
                    array.Length - one);
                return array;
            }

            var sourceArray = EnumerableHelpers.ToArray(
                finiteSource);
            this.Sort(
                sourceArray, 
                zero, 
                sourceArray.Length - one);

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
                zero,
                array.Length - one);
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

            if (left < zero)
            {
                return;
            }

            if (right < zero)
            {
                return;
            }

            if (left >= right)
            {
                return;
            }

            if (left > array.Length - one)
            {
                return;
            }

            if (right > array.Length - one)
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

            pivotIndex >>= one;
            var pivot = array[pivotIndex];
            while (lowIndex <= highIndex)
            {
                while (array[lowIndex].CompareTo(pivot) < zero)
                {
                    ++lowIndex;
                }

                while (array[highIndex].CompareTo(pivot) > zero)
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
            this.Sort(
                sourceArray, 
                zero, 
                sourceArray.Length - one);

            return sourceArray;
        }

        public virtual void Sort(
            IComparable[] array)
        {
            if (array == null)
            {
                return;
            }

            this.Sort(
                array, 
                zero, 
                array.Length - one);
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

            if (left < zero)
            {
                return;
            }

            if (right < zero)
            {
                return;
            }

            if (left >= right)
            {
                return;
            }

            if (left > array.Length - one)
            {
                return;
            }

            if (right > array.Length - one)
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

            pivotIndex >>= one;
            var pivot = array[pivotIndex];
            while (lowIndex <= highIndex)
            {
                while (array[lowIndex].CompareTo(pivot) < zero)
                {
                    ++lowIndex;
                }

                while (array[highIndex].CompareTo(pivot) > zero)
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

        protected const byte
            zero = 0,
            one = 1;
    }
}
