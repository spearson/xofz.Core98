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
                return head == null 
                    ? default 
                    : head.O;
            }
        }

        public virtual T Tail
        {
            get
            {
                var tail = this.tailNode;
                return tail == null 
                    ? default 
                    : tail.O;
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
            var target = this.headNode;
            if (target == null)
            {
                return falsity;
            }

            if (this.nodeContains(target, item))
            {
                return truth;
            }

            while ((target = target.Next) != null)
            {
                if (this.nodeContains(target, item))
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
            var target = this.headNode;
            if (target == null)
            {
                return falsity;
            }

            if (this.nodeContains(target, item))
            {
                goto finish;
            }

            while ((target = target.Next) != null)
            {
                if (this.nodeContains(target, item))
                {
                    goto finish;
                }
            }

            return falsity;

            finish:
            return this.Remove(target) != null;
        }

        public virtual XLinkedListNode<T> Remove(
            XLinkedListNode<T> node)
        {
            if (node == null)
            {
                return node;
            }

            var nodeNext = node.Next;
            var nodePrev = node.Previous;
            if (nodeNext != null)
            {
                nodeNext.Previous = nodePrev;
                goto checkPrev;
            }

            this.setTail(nodePrev);

            checkPrev:
            if (nodePrev != null)
            {
                nodePrev.Next = nodeNext;
                goto finish;
            }

            this.setHead(nodeNext);

            finish:
            node.Next = null;
            node.Previous = null;

            return node;
        }

        public virtual int Count => (int)this.LongCount;

        public virtual bool IsReadOnly => falsity;

        public virtual long LongCount
        {
            get
            {
                long result = zero;
                var currentNode = this.headNode;
                while (currentNode != null)
                {
                    ++result;
                    if (result == long.MaxValue)
                    {
                        break;
                    }

                    currentNode = currentNode.Next;
                }

                return result;
            }
        }

        public virtual IEnumerable<XLinkedListNode<T>> GetNodes()
        {
            return this.readNodes(
                this.headNode,
                node => node?.Next);
        }

        public virtual XLinkedListNode<T> GetNode(
            T item)
        {
            return this.readNode(
                this.readNodes(
                    this.headNode,
                    node => node?.Next),
                item);

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

            var newPrev = newHead.Previous;
            var newNext = newHead.Next;
            if (newPrev != null)
            {
                newPrev.Next = newNext;
            }

            if (newNext != null)
            {
                newNext.Previous = newPrev;
            }

            if (newHead == currentTail)
            {
                var tailPrev = currentTail.Previous;
                if (tailPrev != null)
                {
                    tailPrev.Next = null;
                    this.setTail(
                        tailPrev);
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

            var newNext = newTail.Next;
            var newPrev = newTail.Previous;
            if (newNext != null)
            {
                newNext.Previous = newPrev;
            }

            if (newPrev != null)
            {
                newPrev.Next = newNext;
            }

            if (newTail == currentHead)
            {
                var headNext = currentHead.Next;
                if (headNext != null)
                {
                    headNext.Previous = null;
                    this.setHead(
                        headNext);
                    
                }
            }

            newTail.Next = null;
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
            if (node == null || newNode == null || node == newNode)
            {
                return newNode;
            }

            var target = this.headNode;
            if (target == null)
            {
                return newNode;
            }

            if (target == node)
            {
                goto addLink;
            }

            while ((target = target.Next) != null)
            {
                if (target == node)
                {
                    goto addLink;
                }
            }

            return newNode;

            addLink:
            var newNext = newNode.Next;
            var newPrev = newNode.Previous;
            var nodeNext = node.Next;
            if (nodeNext == newNode)
            {
                return newNode;
            }

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

            node.Next = newNode;
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

            var target = this.headNode;
            if (target == null)
            {
                return newNode;
            }

            if (target == node)
            {
                goto addLink;
            }

            while ((target = target.Next) != null)
            {
                if (target == node)
                {
                    goto addLink;
                }
            }

            return newNode;

            addLink:
            var newNext = newNode.Next;
            var newPrev = newNode.Previous;
            var nodePrev = node.Previous;
            if (nodePrev == newNode)
            {
                return newNode;
            }

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

            node.Previous = newNode;
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
            if (oldHead == null)
            {
                return null;
            }

            var newHead = oldHead.Next;
            if (newHead == null)
            {
                this.Clear();

                return oldHead;
            }

            newHead.Previous = null;
            this.setHead(newHead);

            oldHead.Next = null;
            oldHead.Previous = null;

            return oldHead;
        }

        public virtual XLinkedListNode<T> RemoveTail()
        {
            var oldTail = this.tailNode;
            if (oldTail == null)
            {
                return null;
            }

            var newTail = oldTail.Previous;
            if (newTail == null)
            {
                this.Clear();

                return oldTail;
            }

            newTail.Next = null;
            this.setTail(newTail);

            oldTail.Next = null;
            oldTail.Previous = null;

            return oldTail;
        }

        public virtual XLinkedListNode<T> Find(
            T o)
        {
            var target = this.headNode;
            if (target == null)
            {
                return target;
            }

            if (this.nodeContains(target, o))
            {
                return target;
            }

            while ((target = target.Next) != null)
            {
                if (this.nodeContains(target, o))
                {
                    return target;
                }
            }

            return target;
        }

        public virtual XLinkedListNode<T> FindLast(
            T o)
        {
            var target = this.tailNode;
            if (target == null)
            {
                return target;
            }

            if (this.nodeContains(target, o))
            {
                return target;
            }

            while ((target = target.Previous) != null)
            {
                if (this.nodeContains(target, o))
                {
                    return target;
                }
            }

            return target;
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

        protected virtual XLinkedListNode<T> readNode(
            IEnumerable<XLinkedListNode<T>> nodes,
            T item)
        {
            if (nodes == null)
            {
                return default;
            }

            foreach (var node in nodes)
            {
                if (this.nodeContains(node, item))
                {
                    return node;
                }
            }

            return default;
        }

        protected virtual IEnumerable<XLinkedListNode<T>> readNodes(
            XLinkedListNode<T> node,
            Gen<XLinkedListNode<T>, XLinkedListNode<T>> nextReader)
        {
            if (node == null)
            {
                yield break;
            }

            yield return node;

            while ((node = nextReader?.Invoke(node)) != null)
            {
                yield return node;
            }
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
                    this.setMoved(truth);
                    this.setCurrentNode(cn);

                    return cn != null;
                }

                cn = cn?.Next;
                this.setCurrentNode(cn);

                return cn != null;
            }

            public virtual void Reset()
            {
                this.setCurrentNode(null);
                this.setMoved(falsity);
            }

            public virtual T Current
            {
                get
                {
                    var c = this.currentNode;
                    return c == null 
                        ? default 
                        : c.O;
                }
            }

            object IEnumerator.Current => this.Current;

            public virtual void Dispose()
            {
                this.setCurrentNode(null);
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

        protected const byte 
            zero = 0;
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