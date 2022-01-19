namespace xofz
{
    using System.Collections.Generic;

    public class IndexedLinkedList<T>
        : XLinkedList<T>, IList<T>
    {
        public static IndexedLinkedList<T> CreateIndexed(
            IEnumerable<T> finiteSource)
        {
            var ll = new IndexedLinkedList<T>();
            populate(
                ll,
                finiteSource);

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

        public virtual int IndexOf(
            T item)
        {
            const short minusOne = -1;
            int index = minusOne;
            foreach (var node in this.readNodes(
                         this.headNode,
                         n => n.Next))
            {
                checked
                {
                    ++index;
                }

                if (node == null)
                {
                    break;
                }

                if (node.O?.Equals(item) ?? item == null)
                {
                    return index;
                }
            }

            return minusOne;
        }

        public virtual void Insert(
            int index,
            T item)
        {
            this.AddBefore(
                this.GetNode(index),
                item);
        }

        public virtual void RemoveAt(
            int index)
        {
            this.Remove(
                this.GetNode(index));
        }

        public virtual T this[int index]
        {
            get => this[(long) index];

            set => this[(long) index] = value;
        }
    }
}
