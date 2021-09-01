namespace xofz
{
    using System.Collections;
    using System.Collections.Generic;

    public class XLinkedList<T>
        : ICollection<T>
    {
        public static XLinkedList<T> Create(
            IEnumerable<T> finiteSource)
        {
            var ll = new XLinkedList<T>();
            populate(ll, finiteSource);

            return ll;
        }

        protected static void populate(
            XLinkedList<T> ll,
            IEnumerable<T> finiteSource)
        {
            if (finiteSource == null || ll == null)
            {
                return;
            }

            foreach (var item in finiteSource)
            {
                ll.AddTail(item);
            }
        }

        public virtual XLinkedListNode<T> HeadN => this.headNode;

        public virtual XLinkedListNode<T> TailN => this.tailNode;

        public virtual T Head
        {
            get
            {
                var head = this.headNode;
                if (head == null)
                {
                    return default;
                }

                return head.O;
            }
        }

        public virtual T Tail
        {
            get
            {
                var tail = this.tailNode;
                if (tail == null)
                {
                    return default;
                }

                return tail.O;
            }
        }

        public virtual XLinkedListNode<T> AddHead(
            T o)
        {
            return this.AddHead(
                new XLinkedListNode<T>
                {
                    O = o
                });
        }

        public virtual XLinkedListNode<T> AddHead(
            XLinkedListNode<T> newHead)
        {
            if (newHead == null)
            {
                return newHead;
            }

            var currentHead = this.headNode;
            if (currentHead != null)
            {
                currentHead.Previous = newHead;
            }

            var currentTail = this.tailNode;
            if (currentTail == null)
            {
                this.setTail(newHead);
            }

            newHead.Next = currentHead;
            this.setHead(newHead);

            return newHead;
        }

        public virtual XLinkedListNode<T> AddTail(
            T o)
        {
            return this.AddTail(
                new XLinkedListNode<T>
                {
                    O = o
                });
        }

        public virtual XLinkedListNode<T> AddTail(
            XLinkedListNode<T> newTail)
        {
            if (newTail == null)
            {
                return newTail;
            }

            var currentTail = this.tailNode;
            if (currentTail != null)
            {
                currentTail.Next= newTail;
            }

            var currentHead = this.headNode;
            if (currentHead == null)
            {
                this.setHead(newTail);
            }

            newTail.Previous = currentTail;
            this.setTail(newTail);

            return newTail;
        }

        public virtual XLinkedListNode<T> AddAfter(
            XLinkedListNode<T> node,
            T o)
        {
            var newNode = new XLinkedListNode<T>
            {
                O = o
            };

            return this.AddAfter(
                node,
                newNode);
        }

        public virtual XLinkedListNode<T> AddAfter(
            XLinkedListNode<T> node,
            XLinkedListNode<T> newNode)
        {
            if (node == null || newNode == null)
            {
                return newNode;
            }

            var currentNode = this.headNode;
            if (currentNode == null)
            {
                return newNode;
            }

            if (currentNode.Equals(node))
            {
                goto addLink;
            }

            while ((currentNode = currentNode?.Next) != null)
            {
                if (currentNode.Equals(node))
                {
                    goto addLink;
                }
            }

            return newNode;

            addLink:
            var newNext = currentNode.Next;
            newNode.Previous = currentNode;
            newNode.Next = newNext;
            if (newNext != null)
            {
                newNext.Previous = newNode;
            }

            currentNode.Next = newNode;
            if (currentNode == this.tailNode)
            {
                this.setTail(newNode);
            }

            return newNode;
        }

        public virtual XLinkedListNode<T> AddBefore(
            XLinkedListNode<T> node,
            T o)
        {
            var newNode = new XLinkedListNode<T>
            {
                O = o
            };

            return this.AddBefore(
                node,
                newNode);
        }

        public virtual XLinkedListNode<T> AddBefore(
            XLinkedListNode<T> node,
            XLinkedListNode<T> newNode)
        {
            if (node == null || newNode == null)
            {
                return newNode;
            }

            var currentNode = this.headNode;
            if (currentNode == null)
            {
                return newNode;
            }

            if (currentNode.Equals(node))
            {
                goto addLink;
            }

            while ((currentNode = currentNode?.Next) != null)
            {
                if (currentNode.Equals(node))
                {
                    goto addLink;
                }
            }

            return newNode;

            addLink:
            var newPrev = currentNode.Previous;
            newNode.Next = currentNode;
            newNode.Previous = newPrev;
            if (newPrev != null)
            {
                newPrev.Next = newNode;
            }

            currentNode.Previous = newNode;
            if (currentNode == this.headNode)
            {
                this.setHead(
                    newNode);
            }

            return newNode;
        }

        public virtual XLinkedListNode<T> RemoveHead()
        {
            var oldHead = this.headNode;
            var newHead = oldHead?.Next;
            if (newHead != null)
            {
                newHead.Previous = null;
            }

            this.setHead(
                newHead);
            return oldHead;
        }

        public virtual XLinkedListNode<T> RemoveTail()
        {
            var oldTail = this.tailNode;
            var newTail = oldTail?.Previous;
            if (newTail != null)
            {
                newTail.Next = null;
            }

            this.setTail(
                newTail);
            return oldTail;
        }

        public virtual XLinkedListNode<T> Find(
            T o)
        {
            var currentNode = this.headNode;
            if (currentNode == null)
            {
                return null;
            }

            bool oIsNull = o == null;
            if (currentNode.O?.Equals(o) ?? oIsNull)
            {
                return currentNode;
            }

            while ((currentNode = currentNode?.Next) != null)
            {
                if (currentNode.O?.Equals(o) ?? oIsNull)
                {
                    return currentNode;
                }
            }

            return currentNode;
        }

        public virtual XLinkedListNode<T> FindLast(
            T o)
        {
            var currentNode = this.tailNode;
            if (currentNode == null)
            {
                return null;
            }

            bool oIsNull = o == null;
            if (currentNode.O?.Equals(o) ?? oIsNull)
            {
                return currentNode;
            }

            while ((currentNode = currentNode?.Previous) != null)
            {
                if (currentNode.O?.Equals(o) ?? oIsNull)
                {
                    return currentNode;
                }
            }

            return currentNode;
        }

        public virtual IEnumerator<T> GetEnumerator()
        {
            return new XLinkedListEnumerator(
                this.headNode);
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }

        public virtual void Add(
            T item)
        {
            this.AddTail(item);
        }

        protected virtual void setHead(
            XLinkedListNode<T> head)
        {
            this.headNode = head;
        }

        protected virtual void setTail(
            XLinkedListNode<T> tail)
        {
            this.tailNode = tail;
        }

        public virtual void Clear()
        {
            this.setHead(null);
            this.setTail(null);
        }

        public virtual bool Contains(
            T item)
        {
            var currentNode = this.headNode;
            if (this.nodeContains(currentNode, item))
            {
                return truth;
            }

            while ((currentNode = currentNode?.Next) != null)
            {
                if (this.nodeContains(currentNode, item))
                {
                    return truth;
                }
            }

            return falsity;
        }

        public virtual void CopyTo(
            T[] array,
            int arrayIndex)
        {
            var l = array.Length;
            var e = this.GetEnumerator();
            for (long i = arrayIndex; i < l; ++i)
            {
                if (!e.MoveNext())
                {
                    break;
                }

                array[i] = e.Current;
            }

            e.Dispose();
        }

        public virtual bool Remove(
            T item)
        {
            var currentNode = this.headNode;
            if (currentNode == null)
            {
                return falsity;
            }

            XLinkedListNode<T>
                newNext;
            // check the head node
            if (this.nodeContains(currentNode, item))
            {
                newNext = currentNode.Next;

                if (newNext != null)
                {
                    newNext.Previous = null;
                }

                this.setHead(newNext);
                return truth;
            }

            while ((currentNode = currentNode?.Next) != null)
            {
                if (this.nodeContains(currentNode, item))
                {
                    var newPrevious = currentNode.Previous;
                    newNext = currentNode.Next;
                    if (newPrevious != null)
                    {
                        newPrevious.Next = newNext;
                    }

                    if (newNext != null)
                    {
                        newNext.Previous = newPrevious;
                        return truth;
                    }

                    // tail node
                    this.setTail(newPrevious);
                    return truth;
                }
            }

            return falsity;
        }

        protected virtual bool nodeContains(
            XLinkedListNode<T> node,
            T item)
        {
            return node?.O?.Equals(item) ?? item == null;
        }

        public virtual int Count
        {
            get
            {
                long result = zero;
                var currentNode = this.headNode;
                while (currentNode != null)
                {
                    ++result;

                    currentNode = currentNode.Next;
                }

                return (int)result;
            }
        }

        public virtual bool IsReadOnly => falsity;

        protected XLinkedListNode<T>
            headNode,
            tailNode;
        protected const bool
            truth = true,
            falsity = false;
        protected const byte zero = 0;

        protected class XLinkedListEnumerator
            : IEnumerator<T>
        {
            public XLinkedListEnumerator(
                XLinkedListNode<T> head)
            {
                this.head = head;
            }

            public virtual bool MoveNext()
            {
                var cn = this.currentNode;
                if (cn == null && !this.movedOnce)
                {
                    cn = this.head;
                    this.setCurrentNode(
                        cn);
                    this.setMoved(
                        truth);

                    return cn != null;
                }

                cn = cn?.Next;
                this.setCurrentNode(cn);
                return cn != null;
            }

            public virtual void Reset()
            {
                this.currentNode = null;
                this.setMoved(falsity);
            }

            public virtual T Current
            {
                get
                {
                    var c = this.currentNode;
                    if (c == null)
                    {
                        return default;
                    }

                    return c.O;
                }
            }

            object IEnumerator.Current => this.Current;

            public virtual void Dispose()
            {
                this.currentNode = null;
            }

            protected virtual void setCurrentNode(
                XLinkedListNode<T> currentNode)
            {
                this.currentNode = currentNode;
            }

            protected virtual void setMoved(
                bool movedOnce)
            {
                this.movedOnce = movedOnce;
            }

            protected bool movedOnce;
            protected XLinkedListNode<T> currentNode;
            protected readonly XLinkedListNode<T> head;
        }
    }

    public class XLinkedListNode<T>
    {
        public virtual T O { get; set; }

        public virtual XLinkedListNode<T> Previous { get; set; }

        public virtual XLinkedListNode<T> Next { get; set; }
    }
}
