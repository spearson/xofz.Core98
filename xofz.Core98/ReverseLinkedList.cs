namespace xofz
{
    using System.Collections.Generic;

    // this class keeps the same order as an XLinkedList,
    // it just iterates in reverse order
    public class ReverseLinkedList<T>
        : XLinkedList<T>
    {
        public static ReverseLinkedList<T> CreateReverse(
            IEnumerable<T> finiteSource)
        {
            var ll = new ReverseLinkedList<T>();
            populate(ll, finiteSource);

            return ll;
        }

        public override IEnumerator<T> GetEnumerator()
        {
            return new ReverseEnumerator(
                this.tailNode);
        }

        public override IEnumerable<XLinkedListNode<T>> GetNodes()
        {
            return this.readNodes(
                this.tailNode,
                node => node?.Previous);
        }

        protected class ReverseEnumerator
            : XLinkedListEnumerator
        {
            public ReverseEnumerator(
                XLinkedListNode<T> tail)
                : base(null)
            {
                this.tail = tail;
            }

            public override bool MoveNext()
            {
                var cn = this.currentNode;
                if (cn == null)
                {
                    cn = this.tail;
                    goto checkPrevious;
                }

                cn = cn.Previous;

                checkPrevious:
                if (cn == null)
                {
                    return falsity;
                }

                this.setCurrentNode(cn);
                return truth;
            }

            protected readonly XLinkedListNode<T> tail;
        }
    }
}