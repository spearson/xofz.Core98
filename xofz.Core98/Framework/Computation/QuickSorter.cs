namespace xofz.Framework.Computation
{
    using System.Collections.Generic;

    public class QuickSorter
    {
        public virtual void Sort<T>(
            T[] array)
            where T : System.IComparable
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
            where T : System.IComparable
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

            var l = array.Length;
            var lengthDownOne = l - one;
            if (left > lengthDownOne)
            {
                return;
            }

            if (right > lengthDownOne)
            {
                return;
            }

            ICollection<long> closedRights
                = new XLinkedList<long>();

            beginSort:
            long lowIndex = left,
                highIndex = right;
            long pivotIndex;
            checked
            {
                pivotIndex = left + right;
            }

            pivotIndex >>= one;
            var pivot = array[pivotIndex];
            while (lowIndex <= highIndex)
            {
                while (array[lowIndex]
                           ?.CompareTo(pivot) < zero)
                {
                    ++lowIndex;
                    if (lowIndex > lengthDownOne)
                    {
                        break;
                    }
                }

                while (array[highIndex]
                           ?.CompareTo(pivot) > zero)
                {
                    --highIndex;
                    if (highIndex < zero)
                    {
                        break;
                    }
                }

                if (lowIndex > highIndex || lowIndex > lengthDownOne ||
                    highIndex < zero)
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

        public virtual T[] Sort<T>(
            IEnumerable<T> finiteSource)
            where T : System.IComparable
        {
            switch (finiteSource)
            {
                case null:
                    return new T[zero];
                case T[] array:
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

        public virtual System.IComparable[] Sort(
            IEnumerable<System.IComparable> finiteSource)
        {
            if (finiteSource is System.IComparable[] array)
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
            System.IComparable[] array)
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
            System.IComparable[] array,
            long left,
            long right)
        {
            this.Sort<System.IComparable>(
                array,
                left,
                right);
        }

        protected const byte
            zero = 0,
            one = 1;
    }
}