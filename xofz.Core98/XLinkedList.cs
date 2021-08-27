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
            if (finiteSource == null)
            {
                return ll;
            }

            foreach (var item in finiteSource)
            {
                ll.AddTail(item);
            }

            return ll;
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

        public virtual void AddHead(
            T o)
        {
            var currentHead = this.headNode;
            var newHead = new XLinkedListNode<T>
            {
                Next = currentHead,
                O = o,
            };

            if (currentHead != null)
            {
                currentHead.Previous = newHead;
            }

            var currentTail = this.tailNode;
            if (currentTail == null)
            {
                this.setTail(newHead);
            }

            this.setHead(newHead);
        }

        public virtual void AddTail(
            T o)
        {
            var currentTail = this.tailNode;
            var newTail = new XLinkedListNode<T>
            {
                Previous = currentTail,
                O = o,
            };

            if (currentTail != null)
            {
                currentTail.Next = newTail;
            }

            var currentHead = this.headNode;
            if (currentHead == null)
            {
                this.setHead(newTail);
            }

            this.setTail(newTail);
        }

        public virtual void RemoveHead()
        {
            var newHead = this.headNode?.Next;
            if (newHead != null)
            {
                newHead.Previous = null;
            }

            this.setHead(
                newHead);
        }

        public virtual void RemoveTail()
        {
            var newTail = this.tailNode?.Previous;
            if (newTail != null)
            {
                newTail.Next = null;
            }

            this.setTail(
                newTail);
        }

        public IEnumerator<T> GetEnumerator()
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

        public int Count
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

        public bool IsReadOnly => falsity;

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
