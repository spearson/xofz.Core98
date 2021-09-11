namespace xofz.Tests
{
    using Xunit;

    public class XLinkedListTests
    {
        public class Context
        {
            protected Context()
            {
                this.ll = new XLinkedList<object>();
            }

            protected readonly XLinkedList<object> ll;
        }

        public class When_AddHead_is_called : Context
        {
            [Fact]
            public void Adds_a_head_node()
            {
                this.ll.AddHead(
                    (object)null);

                Assert.NotNull(
                    this.ll.HeadN);
            }

            [Fact]
            public void Add_the_item_to_the_front()
            {
                this.ll.AddTail(4);
                this.ll.AddTail(5);
                this.ll.AddTail(6);

                this.ll.AddHead(3);

                byte indexer = 0xFF;
                foreach (var o in this.ll)
                {
                    ++indexer;
                    switch (indexer)
                    {
                        case 0:
                            Assert.Equal(3, (int)o);
                            break;
                        case 1:
                            Assert.Equal(4, (int)o);
                            break;
                        case 2:
                            Assert.Equal(5, (int)o);
                            break;
                        case 3:
                            Assert.Equal(6, (int)o);
                            break;
                    }
                }
            }

            [Fact]
            public void Returns_the_new_head_node()
            {
                this.ll.AddTail(0);
                var node = this.ll.AddHead(1);

                Assert.Same(
                    node,
                    this.ll.HeadN);
            }
        }

        public class When_AddTail_is_called : Context
        {
            [Fact]
            public void Adds_a_tail_node()
            {
                this.ll.AddTail(
                    (object)null);

                Assert.NotNull(
                    this.ll.TailN);
            }

            [Fact]
            public void Adds_the_item_to_the_back()
            {
                this.ll.AddTail(3);
                this.ll.AddTail(4);
                this.ll.AddTail(5);
                this.ll.AddTail(6);

                byte indexer = 0xFF;
                foreach (var o in this.ll)
                {
                    ++indexer;
                    switch (indexer)
                    {
                        case 0:
                            Assert.Equal(3, (int)o);
                            break;
                        case 1:
                            Assert.Equal(4, (int)o);
                            break;
                        case 2:
                            Assert.Equal(5, (int)o);
                            break;
                        case 3:
                            Assert.Equal(6, (int)o);
                            break;
                    }
                }
            }

            [Fact]
            public void Returns_the_new_tail_node()
            {
                this.ll.AddHead(0);
                var node = this.ll.AddTail(1);

                Assert.Same(
                    node,
                    this.ll.TailN);
            }
        }

        public class When_AddAfter_is_called : Context
        {
            [Fact]
            public void Adds_item_after_node()
            {
                var targetNode = new XLinkedListNode<object>();
                targetNode.O = 3;
                this.ll.AddTail(
                    1);
                this.ll.AddTail(
                    2);
                this.ll.AddTail(
                    targetNode);

                this.ll.AddAfter(
                    targetNode,
                    4);

                byte indexer = 0xFF;
                bool assertedAll = false;
                foreach (var o in this.ll)
                {
                    ++indexer;
                    switch (indexer)
                    {
                        case 0:
                            Assert.Equal(o, 1);
                            break;
                        case 1:
                            Assert.Equal(o, 2);
                            break;
                        case 2:
                            Assert.Equal(o, 3);
                            break;
                        case 3:
                            Assert.Equal(o, 4);
                            assertedAll = true;
                            break;
                    }
                }

                Assert.True(assertedAll);

                targetNode = new XLinkedListNode<object>();
                targetNode.O = 5;
                this.ll.AddTail(targetNode);
                this.ll.AddAfter(
                    targetNode,
                    6);

                indexer = 0xFF;
                assertedAll = false;
                foreach (var o in ll)
                {
                    ++indexer;
                    switch (indexer)
                    {
                        case 0:
                            Assert.Equal(o, 1);
                            break;
                        case 1:
                            Assert.Equal(o, 2);
                            break;
                        case 2:
                            Assert.Equal(o, 3);
                            break;
                        case 3:
                            Assert.Equal(o, 4);
                            break;
                        case 4:
                            Assert.Equal(o, 5);
                            break;
                        case 5:
                            Assert.Equal(o, 6);
                            assertedAll = true;
                            break;
                    }
                }

                Assert.True(assertedAll);
            }

            [Fact]
            public void Test2()
            {
                var ll2 = new XLinkedList<object>();
                var o1 = new object();
                var o2 = new object();
                var o3 = new object();
                ll2.AddTail(o1);
                ll2.AddTail(o2);
                ll2.AddTail(o3);
                ll2.AddTail(4);
                byte indexer = 0xFF;
                var node = ll2.TailN;
                ll2.AddBefore(
                    ll2.HeadN,
                    node);
                var assertedAll = false;
                
                foreach (var n in ll2.GetNodes())
                {
                    ++indexer;

                    switch (indexer)
                    {
                        case 0:
                            Assert.Equal(
                                node,
                                n);
                            break;
                        case 3:
                            Assert.NotEqual(
                                node,
                                n);
                            assertedAll = true;
                            break;
                    }
                }

                Assert.True(assertedAll);

                indexer = 0xFF;
                foreach (var item in ll2)
                {
                    ++indexer;
                    switch (indexer)
                    {
                        case 0:
                            Assert.Equal(
                                4,
                                item);
                            break;
                        case 1:
                            Assert.Equal(
                                o1,
                                item);
                            break;
                        case 2:
                            Assert.Equal(
                                o2,
                                item);
                            break;
                        case 3:
                            Assert.Equal(
                                o3,
                                item);
                            break;
                        case 4:
                            throw new System.Exception(
                                @"Should not reach here");
                    }
                }

                node = ll2.HeadN;
                ll2.AddAfter(
                    ll2.TailN,
                    node);
            }
        }

        public class When_AddBefore_is_called : Context
        {
            [Fact]
            public void Adds_item_before_node()
            {
                var targetNode = new XLinkedListNode<object>
                {
                    O = 2
                };
                this.ll.AddTail(
                    targetNode);
                this.ll.AddTail(
                    3);
                this.ll.AddTail(
                    4);

                this.ll.AddBefore(
                    targetNode,
                    1);

                byte indexer = 0xFF;
                bool assertedAll = false;
                foreach (var o in this.ll)
                {
                    ++indexer;
                    switch (indexer)
                    {
                        case 0:
                            Assert.Equal(o, 1);
                            break;
                        case 1:
                            Assert.Equal(o, 2);
                            break;
                        case 2:
                            Assert.Equal(o, 3);
                            break;
                        case 3:
                            Assert.Equal(o, 4);
                            assertedAll = true;
                            break;
                    }
                }

                Assert.True(assertedAll);

                targetNode = new XLinkedListNode<object>();
                targetNode.O = 6;
                this.ll.AddTail(targetNode);
                this.ll.AddBefore(
                    targetNode,
                    5);

                indexer = 0xFF;
                assertedAll = false;
                foreach (var o in ll)
                {
                    ++indexer;
                    switch (indexer)
                    {
                        case 0:
                            Assert.Equal(o, 1);
                            break;
                        case 1:
                            Assert.Equal(o, 2);
                            break;
                        case 2:
                            Assert.Equal(o, 3);
                            break;
                        case 3:
                            Assert.Equal(o, 4);
                            break;
                        case 4:
                            Assert.Equal(o, 5);
                            break;
                        case 5:
                            Assert.Equal(o, 6);
                            assertedAll = true;
                            break;
                    }
                }

                Assert.True(assertedAll);
            }
        }

        public class When_Find_is_called : Context
        {
            [Fact]
            public void Finds_the_element()
            {
                var ll2 = new XLinkedList<int>();
                ll2.AddTail(1);
                ll2.AddTail(3);
                ll2.AddTail(5);

                var node = ll2.Find(3);
                Assert.True(
                    node.O == 3);
                Assert.True(
                    node.Previous.O == 1);
                Assert.True(
                    node.Next.O == 5);
            }
        }

        public class When_FindLast_is_called : Context
        {
            [Fact]
            public void Finds_the_last_occurrence_of_the_element()
            {
                var ll2 = new XLinkedList<byte>();
                ll2.AddHead(0);
                ll2.AddHead(1);
                ll2.AddTail(2);
                ll2.AddTail(3);
                ll2.AddTail(0);
                ll2.AddTail(4);

                var node = ll2.FindLast(0);
                Assert.True(
                    node.O == 0);
                Assert.True(
                    node.Previous.O == 3);
                Assert.True(
                    node.Next.O == 4);
            }
        }

        public class When_GetNodes_is_called : Context
        {
            [Fact]
            public void Reads_the_nodes_out()
            {
                var ll2 = new XLinkedList<object>();
                var node1 = new XLinkedListNode<object>();
                var node2 = new XLinkedListNode<object>();
                var node3 = new XLinkedListNode<object>();
                ll2.AddTail(
                    node1);
                ll2.AddTail(
                    node2);
                ll2.AddTail(
                    node3);

                var e = ll2.GetNodes()
                    ?.GetEnumerator();
                if (e == null)
                {
                    Assert.True(
                        false);
                    return;
                }

                e.MoveNext();
                Assert.Same(
                    node1,
                    e.Current);

                e.MoveNext();
                Assert.Same(
                    node2,
                    e.Current);

                e.MoveNext();
                Assert.Same(
                    node3,
                    e.Current);

                Assert.False(
                    e.MoveNext());

                e.Dispose();
            }

            [Fact]
            public void Returns_one_item_for_one()
            {
                var ll2 = new XLinkedList<byte>();
                var node = new XLinkedListNode<byte>();
                ll2.AddTail(node);

                var e = ll2.GetNodes()
                    ?.GetEnumerator();
                if (e == null)
                {
                    Assert.True(
                        false);
                    return;
                }

                Assert.True(
                    e.MoveNext());
                Assert.Same(
                    e.Current,
                    node);

                e.Dispose();
            }

            [Fact]
            public void If_no_items_returns_empty()
            {
                Assert.Empty(
                    new XLinkedList<object>().GetNodes());
            }

            [Fact]
            public void If_two_items_returns_both()
            {
                var ll2 = new XLinkedList<object>();
                var node1 = new XLinkedListNode<object>();
                var node2 = new XLinkedListNode<object>();
                ll2.AddTail(
                    node1);
                ll2.AddTail(
                    node2);

                var e = ll2.GetNodes()
                    ?.GetEnumerator();
                if (e == null)
                {
                    Assert.True(
                        false);
                    return;
                }

                e.MoveNext();
                Assert.Same(
                    node1,
                    e.Current);

                e.MoveNext();
                Assert.Same(
                    node2,
                    e.Current);

                Assert.False(
                    e.MoveNext());

                e.Dispose();
            }
        }
    }
}
