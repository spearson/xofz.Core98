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

                var h = this.headNode;
                long currentIndex = zero;
                if (currentIndex == index)
                {
                    return h == null
                        ? default
                        : h.O;
                }

                var result = h;
                while (currentIndex < index)
                {
                    ++currentIndex;
                    result = result?.Next;
                }

                return result == null
                    ? default
                    : result.O;
            }

            set
            {
                var h = this.headNode;
                if (h == null || index < zero)
                {
                    return;
                }

                long currentIndex = zero;
                if (currentIndex == index)
                {
                    h.O = value;
                    return;
                }

                var target = h;
                while (currentIndex < index)
                {
                    ++currentIndex;
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

            var currentNode = this.headNode;
            if (currentNode == null)
            {
                return currentNode;
            }

            long currentIndex = zero;
            if (index == currentIndex)
            {
                return currentNode;
            }

            while ((currentNode = currentNode.Next) != null)
            {
                ++currentIndex;
                if (currentIndex == index)
                {
                    return currentNode;
                }
            }

            return default;
        }
    }
}
