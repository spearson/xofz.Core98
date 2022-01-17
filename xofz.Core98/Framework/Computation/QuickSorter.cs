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
            T[] array)
            where T : IComparable
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
                while (array[lowIndex]?.
                    CompareTo(pivot) < zero)
                {
                    ++lowIndex;
                    if (lowIndex > lengthDownOne)
                    {
                        break;
                    }
                }

                while (array[highIndex]?.
                    CompareTo(pivot) > zero)
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

        public virtual ICollection<T> SortV2<T>(
            IEnumerable<T> finiteSource)
            where T : IComparable
        {
            var ll = IndexedLinkedList<T>.CreateIndexed(
                finiteSource);
            if (ll == null)
            {
                return ll;
            }

            this.SortV2(
                ll,
                zero,
                ll.Count - one);

            return ll;
        }

        public virtual void SortV2<T>(
            IndexedLinkedList<T> ll)
            where T : IComparable
        {
            this.SortV2(
                ll,
                zero,
                ll.Count - one);
        }

        public virtual void SortV2<T>(
            IndexedLinkedList<T> ll,
            long left,
            long right)
            where T : IComparable
        {
            if (ll == null)
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

            var l = ll.Count;
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
            var pivot = ll[pivotIndex];
            while (lowIndex <= highIndex)
            {
                while (ll[lowIndex]?.
                    CompareTo(pivot) < zero)
                {
                    ++lowIndex;
                    if (lowIndex > lengthDownOne)
                    {
                        break;
                    }
                }

                while (ll[highIndex]?.
                    CompareTo(pivot) > zero)
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

                var lowSwap = ll[lowIndex];
                ll[lowIndex] = ll[highIndex];
                ll[highIndex] = lowSwap;

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
            this.Sort<IComparable>(
                array,
                left,
                right);
        }

        public virtual ICollection<IComparable> SortV2(
            IEnumerable<IComparable> finiteSource)
        {
            if (finiteSource is IndexedLinkedList<IComparable> ll)
            {
                this.SortV2(ll);
                return ll;
            }

            var sourceLL = IndexedLinkedList<IComparable>.CreateIndexed(
                finiteSource);
            this.SortV2(
                sourceLL,
                zero,
                sourceLL.Count - one);

            return sourceLL;
        }

        public virtual void SortV2(
            IndexedLinkedList<IComparable> ll)
        {
            if (ll == null)
            {
                return;
            }

            this.SortV2(
                ll,
                zero,
                ll.Count - one);
        }

        public virtual void SortV2(
            IndexedLinkedList<IComparable> ll,
            long left,
            long right)
        {
            this.SortV2<IComparable>(
                ll,
                left,
                right);
        }

        protected const byte
            zero = 0,
            one = 1;
    }
}
