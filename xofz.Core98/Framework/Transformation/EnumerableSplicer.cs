namespace xofz.Framework.Transformation
{
    using System.Collections.Generic;
    using xofz.Framework.Lots;

    public class EnumerableSplicer
    {
        public virtual Lot<T> Splice<T>(
            ICollection<T>[] collections)
        {
            var result = new LinkedListLot<T>();
            if (collections == null)
            {
                return result;
            }

            var source = this.SpliceV2(
                collections);
            if (source == null)
            {
                return result;
            }

            foreach (var item in source)
            {
                result.AddLast(item);
            }

            return result;
        }

        public virtual Lot<T> Splice<T>(
            Lot<T>[] lots)
        {
            var result = new LinkedListLot<T>();
            if (lots == null)
            {
                return result;
            }

            var source = this.SpliceV2(
                lots);
            if (source == null)
            {
                return result;
            }

            foreach (var item in source)
            {
                result.AddLast(item);
            }

            return result;
        }

        public virtual Lot<T> Splice<T>(
            IEnumerable<T>[] finiteSources)
        {
            var result = new LinkedListLot<T>();
            if (finiteSources == null)
            {
                return result;
            }

            var source = this.SpliceV2(
                finiteSources);
            if (source == null)
            {
                return result;
            }

            foreach (var item in source)
            {
                result.AddLast(item);
            }

            return result;
        }

        public virtual IEnumerable<T> SpliceV2<T>(
            IEnumerable<IEnumerable<T>> sources)
        {
            if (sources == null)
            {
                yield break;
            }

            ICollection<IEnumerator<T>> enumerators
                = new XLinkedList<IEnumerator<T>>();
            foreach (var source in sources)
            {
                var enumerator = source?.GetEnumerator();
                if (enumerator == null)
                {
                    continue;
                }

                enumerators.Add(enumerator);
            }

            bool reachedOne;
            const bool truth = true;

            splice:
            reachedOne = false;
            foreach (var enumerator in enumerators)
            {
                if (!enumerator.MoveNext())
                {
                    continue;
                }

                reachedOne = truth;
                yield return enumerator.Current;
            }

            if (reachedOne)
            {
                goto splice;
            }

            foreach (var enumerator in enumerators)
            {
                enumerator.Dispose();
            }
        }

        protected const byte
            zero = 0,
            one = 1;
    }
}
