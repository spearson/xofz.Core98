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
                currentNext;
            // check the head node
            if (this.nodeContains(currentNode, item))
            {
                currentNext = currentNode.Next;

                if (currentNext != null)
                {
                    currentNext.Previous = null;
                    this.setHead(currentNext);
                    goto clearNode;
                }

                this.Clear();

                clearNode:
                currentNode.Next = null;
                currentNode.Previous = null;
                return truth;
            }

            while ((currentNode = currentNode?.Next) != null)
            {
                if (!this.nodeContains(currentNode, item))
                {
                    continue;
                }

                var currentPrev = currentNode.Previous;
                currentNext = currentNode.Next;
                if (currentPrev != null)
                {
                    currentPrev.Next = currentNext;
                    if (currentNext == null)
                    {
                        this.setTail(currentPrev);
                        goto clearNextPrevious;
                    }
                }

                if (currentNext != null)
                {
                    currentNext.Previous = currentPrev;
                    if (currentPrev == null)
                    {
                        this.setHead(currentNext);
                    }

                    goto clearNextPrevious;
                }

                // tail node
                this.setTail(currentPrev);
                clearNextPrevious:
                currentNode.Next = null;
                currentNode.Previous = null;

                return truth;
            }

            return falsity;
        }

        public virtual XLinkedListNode<T> Remove(
            XLinkedListNode<T> node)
        {
            if (node == null)
            {
                return node;
            }

            var currentNode = this.headNode;
            if (currentNode == null)
            {
                return node;
            }

            if (currentNode == node)
            {
                var currentNext = currentNode.Next;
                if (currentNext == null)
                {
                    this.Clear();
                    node.Previous = null;
                    node.Next = null;
                    return node;
                }

                this.setHead(
                    currentNext);
                currentNext.Previous = null;
                node.Previous = null;
                node.Next = null;
                return node;
            }

            while ((currentNode = currentNode.Next) != null)
            {
                if (currentNode != node)
                {
                    continue;
                }

                var currentPrev = currentNode.Previous;
                var currentNext = currentNode.Next;
                var prevNull = currentPrev == null;
                var prevNotNull = currentPrev != null;
                if (currentNext == null)
                {
                    if (prevNotNull)
                    {
                        currentPrev.Next = null;
                        this.setTail(currentPrev);
                    }

                    if (prevNull)
                    {
                        this.Clear();
                    }

                    node.Previous = null;
                    node.Next = null;
                    return node;
                }

                if (prevNotNull)
                {
                    currentPrev.Next = currentNext;
                }

                currentNext.Previous = currentPrev;
                if (prevNull)
                {
                    this.setHead(currentNext);
                }

                node.Previous = null;
                node.Next = null;
                return node;
            }

            return node;
        }

        public virtual int Count
        {
            get
            {
                long result = zero;
                var currentNode = this.headNode;
                while (currentNode != null)
                {
                    checked
                    {
                        ++result;
                    }

                    currentNode = currentNode.Next;
                }

                return (int)result;
            }
        }

        public virtual bool IsReadOnly => falsity;

        public virtual IEnumerable<XLinkedListNode<T>> GetNodes()
        {
            var currentNode = this.headNode;
            if (currentNode == null)
            {
                yield break;
            }

            yield return currentNode;

            while ((currentNode = currentNode?.Next) != null)
            {
                yield return currentNode;
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
            if (currentHead == newHead)
            {
                return newHead;
            }

            if (currentHead != null)
            {
                currentHead.Previous = newHead;
            }

            var currentTail = this.tailNode;
            if (currentTail == null)
            {
                this.setTail(newHead);
            }

            var newHeadsPrevious = newHead.Previous;
            var newHeadsNext = newHead.Next;
            if (newHeadsPrevious != null)
            {
                newHeadsPrevious.Next = newHeadsNext;
            }

            if (newHeadsNext != null)
            {
                newHeadsNext.Previous = newHeadsPrevious;
            }

            if (newHead == currentTail)
            {
                var currentTailsPrevious = currentTail.Previous;
                if (currentTailsPrevious != null)
                {
                    this.setTail(
                        currentTailsPrevious);
                    currentTailsPrevious.Next = null;
                }
            }

            newHead.Previous = null;
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
            if (currentTail == newTail)
            {
                return newTail;
            }

            if (currentTail != null)
            {
                currentTail.Next = newTail;
            }

            var currentHead = this.headNode;
            if (currentHead == null)
            {
                this.setHead(newTail);
            }

            var newTailsNext = newTail.Next;
            var newTailsPrevious = newTail.Previous;

            if (newTailsNext != null)
            {
                newTailsNext.Previous = newTailsPrevious;
            }

            if (newTailsPrevious != null)
            {
                newTailsPrevious.Next = newTailsNext;
            }

            newTail.Next = null;
            newTail.Previous = currentTail;
            this.setTail(newTail);

            if (newTail == currentHead)
            {
                var currentHeadsNext = currentHead.Next;
                if (currentHeadsNext != null)
                {
                    this.setHead(
                        currentHeadsNext);
                    currentHeadsNext.Previous = null;
                }
            }

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
            if (node == null || newNode == null || node == newNode)
            {
                return newNode;
            }

            var nodeNext = node.Next;
            var newPrev = newNode.Previous;
            var newNext = newNode.Next;
            if (newPrev == node)
            {
                node.Next = newNode;

                if (nodeNext == null)
                {
                    this.setTail(newNode);
                }

                return newNode;
            }

            var currentNode = this.headNode;
            if (currentNode == null)
            {
                return newNode;
            }

            if (currentNode == node)
            {
                goto addLink;
            }

            while ((currentNode = currentNode?.Next) != null)
            {
                if (currentNode == node)
                {
                    goto addLink;
                }
            }

            return newNode;

            addLink:
            node.Next = newNode;
            if (newPrev != null)
            {
                newPrev.Next = newNext;
                if (newNext == null)
                {
                    this.setTail(newPrev);
                }
            }

            if (newNext != null)
            {
                newNext.Previous = newPrev;
                if (newPrev == null)
                {
                    this.setHead(newNext);
                }
            }

            newNode.Previous = node;
            newNode.Next = nodeNext;
            if (nodeNext == null)
            {
                this.setTail(newNode);
                return newNode;
            }

            nodeNext.Previous = newNode;
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
            if (node == null || newNode == null || node == newNode)
            {
                return newNode;
            }

            var nodePrev = node.Previous;
            var newNext = newNode.Next;
            var newPrev = newNode.Previous;
            if (newNext == node)
            {
                node.Previous = newNode;

                if (nodePrev == null)
                {
                    this.setHead(newNode);
                }

                return newNode;
            }

            var currentNode = this.headNode;
            if (currentNode == null)
            {
                return newNode;
            }

            if (currentNode == node)
            {
                goto addLink;
            }

            while ((currentNode = currentNode?.Next) != null)
            {
                if (currentNode == node)
                {
                    goto addLink;
                }
            }

            return newNode;

            addLink:
            node.Previous = newNode;
            if (newNext != null)
            {
                newNext.Previous = newPrev;
                if (newPrev == null)
                {
                    this.setHead(newNext);
                }
            }

            if (newPrev != null)
            {
                newPrev.Next = newNext;
                if (newNext == null)
                {
                    this.setTail(newPrev);
                }
            }

            newNode.Next = node;
            newNode.Previous = nodePrev;
            if (nodePrev == null)
            {
                this.setHead(newNode);
                return newNode;
            }

            nodePrev.Next = newNode;
            return newNode;
        }

        public virtual XLinkedListNode<T> RemoveHead()
        {
            var oldHead = this.headNode;
            var newHead = oldHead?.Next;
            var newHeadNull = newHead == null;
            if (!newHeadNull)
            {
                newHead.Previous = null;
            }

            this.setHead(
                newHead);
            if (newHeadNull)
            {
                this.setTail(
                    newHead);
            }

            if (oldHead != null)
            {
                oldHead.Next = null;
                oldHead.Previous = null;
            }


            return oldHead;
        }

        public virtual XLinkedListNode<T> RemoveTail()
        {
            var oldTail = this.tailNode;
            var newTail = oldTail?.Previous;
            var newTailNull = newTail == null;
            if (!newTailNull)
            {
                newTail.Next = null;
            }

            this.setTail(
                newTail);
            if (newTailNull)
            {
                this.setHead(
                    newTail);
            }

            if (oldTail != null)
            {
                oldTail.Next = null;
                oldTail.Previous = null;
            }

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

        protected virtual bool nodeContains(
            XLinkedListNode<T> node,
            T item)
        {
            if (node == null)
            {
                return falsity;
            }

            return node.O?.Equals(item) ?? item == null;
        }

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

        protected XLinkedListNode<T>
            headNode,
            tailNode;

        protected const byte zero = 0;
        protected const bool
            truth = true,
            falsity = false;
    }

    public class XLinkedListNode<T>
    {
        public virtual T O { get; set; }

        public virtual XLinkedListNode<T> Previous { get; set; }

        public virtual XLinkedListNode<T> Next { get; set; }
    }
}