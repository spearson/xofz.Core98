namespace xofz.Framework.Computation
{
    using System.Collections.Generic;

    public class QuickSorter
    {
        public virtual T[] Sort<T>(
            IEnumerable<T> finiteSource)
            where T : System.IComparable
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
            where T : System.IComparable
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
                ll.LongCount - one);

            return ll;
        }

        public virtual void SortV2<T>(
            IndexedLinkedList<T> ll)
            where T : System.IComparable
        {
            if (ll == null)
            {
                return;
            }

            this.SortV2(
                ll,
                zero,
                ll.LongCount - one);
        }

        public virtual void SortV2<T>(
            IndexedLinkedList<T> ll,
            long left,
            long right)
            where T : System.IComparable
        {
            if (ll == null)
            {
                return;
            }

            this.SortV3(
                ll,
                (item1, item2) =>
                {
                    var oneNull = item1 == null;
                    var twoNull = item2 == null;
                    if (oneNull && twoNull)
                    {
                        return zero;
                    }

                    if (oneNull)
                    {
                        return minusOne;
                    }

                    if (twoNull)
                    {
                        return one;
                    }

                    return item1.CompareTo(item2);
                },
            
                left,
                right);
        }

        public virtual void SortV3<T>(
            IndexedLinkedList<T> ll,
            Gen<T, T, long> compare)
        {
            if (ll == null)
            {
                return;
            }

            this.SortV3(
                ll, 
                compare, 
                zero, 
                ll.LongCount - one);
        }

        public virtual void SortV3<T>(
            IndexedLinkedList<T> ll,
            Gen<T, T, long> compare,
            long left,
            long right)
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

            var length = ll.Count;
            var lengthDownOne = length - one;
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
                while (compare?.Invoke(
                           ll[lowIndex],
                           pivot) < zero)
                {
                    ++lowIndex;
                    if (lowIndex > lengthDownOne)
                    {
                        break;
                    }
                }

                while (compare?.Invoke(
                           ll[highIndex],
                           pivot) > zero)
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

        public virtual void SortV3<T>(
            IndexedLinkedList<T> ll,
            IComparer<T> comparer)
        {
            if (comparer == null)
            {
                return;
            }

            this.SortV3(
                ll,
                (item1, item2) => comparer.Compare(
                    item1,
                    item2));
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

        public virtual ICollection<System.IComparable> SortV2(
            IEnumerable<System.IComparable> finiteSource)
        {
            if (finiteSource is IndexedLinkedList<System.IComparable> ll)
            {
                this.SortV2(ll);
                return ll;
            }

            var sourceLL = IndexedLinkedList<System.IComparable>.CreateIndexed(
                finiteSource);
            this.SortV2(
                sourceLL,
                zero,
                sourceLL.Count - one);

            return sourceLL;
        }

        public virtual void SortV2(
            IndexedLinkedList<System.IComparable> ll)
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
            IndexedLinkedList<System.IComparable> ll,
            long left,
            long right)
        {
            this.SortV2<System.IComparable>(
                ll,
                left,
                right);
        }

        protected const short
            minusOne = -1;
        protected const byte
            zero = 0,
            one = 1;
    }
}
