﻿namespace xofz
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

            var newPrev = newNode.Previous;
            if (newPrev == node)
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
            var currentNext = currentNode.Next;
            var newNext = newNode.Next;
            newNode.Previous = currentNode;
            newNode.Next = currentNext;
            if (currentNext != null)
            {
                currentNext.Previous = newNode;
            }

            if (newNext != null)
            {
                newNext.Previous = newPrev;
            }

            if (newPrev != null)
            {
                newPrev.Next = newNext;
            }

            currentNode.Next = newNode;
            if (currentNode != this.tailNode)
            {
                if (newNode == this.tailNode)
                {
                    this.setTail(newPrev);
                }

                return newNode;
            }

            this.setTail(newNode);
            if (newNode != this.headNode || newNext == null)
            {
                return newNode;
            }

            this.setHead(
                newNext);
            newNext.Previous = null;

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

            var newNext = newNode.Next;
            if (newNext == node)
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
            var currentPrev = currentNode.Previous;
            var newPrev = newNode.Previous;
            newNode.Next = currentNode;
            newNode.Previous = currentPrev;
            if (currentPrev != null)
            {
                currentPrev.Next = newNode;
            }

            if (newPrev != null)
            {
                newPrev.Next = newNext;
            }

            if (newNext != null)
            {
                newNext.Previous = newPrev;
            }

            currentNode.Previous = newNode;
            if (currentNode != this.headNode)
            {
                if (newNode == this.headNode)
                {
                    this.setHead(newNext);
                }

                return newNode;
            }

            this.setHead(newNode);
            if (newNode != this.tailNode || newPrev == null)
            {
                return newNode;
            }

            this.setTail(
                newPrev);
            newPrev.Next = null;

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