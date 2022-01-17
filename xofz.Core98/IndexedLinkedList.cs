namespace xofz
{
    using System.Collections.Generic;

    public class IndexedLinkedList<T>
        : XLinkedList<T>
    {
        public static IndexedLinkedList<T> CreateIndexed(
            IEnumerable<T> finiteSource)
        {
            var ll = new IndexedLinkedList<T>();
            populate(ll, finiteSource);

            return ll;
        }

        public virtual T this[long index]
        {
            get
            {
                if (index < zero)
                {
                    return default;
                }

                var result = this.headNode;
                long nodeIndex = zero;
                while (nodeIndex < index)
                {
                    ++nodeIndex;
                    result = result?.Next;
                }

                return result == null
                    ? default
                    : result.O;
            }

            set
            {
                if (index < zero)
                {
                    return;
                }

                var target = this.headNode;
                long nodeIndex = zero;
                while (nodeIndex < index)
                {
                    ++nodeIndex;
                    target = target?.Next;
                }

                if (target == null)
                {
                    return;
                }

                target.O = value;
            }
        }

        public virtual XLinkedListNode<T> GetNode(
            long index)
        {
            if (index < zero)
            {
                return default;
            }

            var result = this.headNode;
            long nodeIndex = zero;
            while (nodeIndex < index)
            {
                ++nodeIndex;
                result = result?.Next;
            }

            return result;
        }
    }
}
