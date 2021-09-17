namespace xofz.Tests
{
    using System;
    using Xunit;
    using Xunit.Abstractions;

    public class XLinkedListTests
    {
        public class Context
        {
            public Context(
                ITestOutputHelper helper)
            {
                var xll = new XLinkedList<object>();
                this.ll = xll;
                this.node1 = new XLinkedListNode<object>
                {
                    O = 1
                };
                this.node2 = new XLinkedListNode<object>
                {
                    O = 2
                };
                this.node3 = new XLinkedListNode<object>
                {
                    O = 3
                };
                this.node4 = new XLinkedListNode<object>
                {
                    O = 4
                };
                this.node5 = new XLinkedListNode<object>
                {
                    O = 5
                };

                xll.AddTail(this.node1);
                xll.AddTail(this.node2);
                xll.AddTail(this.node3);
                xll.AddTail(this.node4);
                xll.AddTail(this.node5);
                this.helper = helper;
            }

            protected readonly XLinkedList<object> ll;
            protected readonly XLinkedListNode<object> node1;
            protected readonly XLinkedListNode<object> node2;
            protected readonly XLinkedListNode<object> node3;
            protected readonly XLinkedListNode<object> node4;
            protected readonly XLinkedListNode<object> node5;
            protected readonly ITestOutputHelper helper;
        }

        public class When_AddHead_is_called : Context
        {
            public When_AddHead_is_called(
                ITestOutputHelper helper)
                : base(helper)
            {
            }

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
                this.ll.Clear();
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

            [Fact]
            public void Rearranging_test()
            {
                var ll2 = new XLinkedList<object>();
                ll2.AddHead(
                    1);
                ll2.AddTail(
                    ll2.HeadN);
                byte indexer = 0xFF;
                foreach (var item in ll2)
                {
                    ++indexer;
                    switch (indexer)
                    {
                        case 0:
                            Assert.Equal(1, item);
                            break;
                        case 1:
                            throw new Exception(
                                @"Should not reach here.");
                    }
                }

                ll2.AddTail(2); // { 1, 2 }
                ll2.AddHead(
                    ll2.TailN); // { 2, 1 }
                ll2.AddTail(3); // { 2, 1, 3 }
                ll2.AddHead(ll2.TailN.Previous); // { 1, 2, 3 }
                indexer = 0xFF;
                foreach (var item in ll2)
                {
                    this.helper.WriteLine(
                        item.ToString());
                    ++indexer;
                    switch (indexer)
                    {
                        case 0:
                            Assert.Equal(
                                1, 
                                item);
                            break;
                        case 1:
                            Assert.Equal(
                                2,
                                item);
                            break;
                        case 2:
                            Assert.Equal(3,
                                item);
                            break;
                        case 3:
                            throw new Exception(
                                @"Should not reach here.");
                    }
                }
            }
        }

        public class When_AddTail_is_called : Context
        {
            public When_AddTail_is_called(
                ITestOutputHelper helper)
                : base(helper)
            {
            }

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
                this.ll.Clear();
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

            [Fact]
            public void Rearranging_test()
            {
                var ll2 = new XLinkedList<object>();
                ll2.AddTail(
                    1);
                ll2.AddHead(
                    ll2.TailN);
                byte indexer = 0xFF;
                foreach (var item in ll2)
                {
                    this.helper.WriteLine(
                        item.ToString());

                    ++indexer;
                    switch (indexer)
                    {
                        case 0:
                            Assert.Equal(1, item);
                            break;
                        case 1:
                            throw new Exception(
                                @"Should not reach here.");
                    }
                }

                this.helper.WriteLine(string.Empty);

                ll2.AddTail(2); // { 1, 2 }
                ll2.AddHead(
                    ll2.TailN); // { 2, 1 }
                ll2.AddHead(3); // { 3, 2, 1 }
                ll2.AddTail(ll2.HeadN.Next); // { 3, 1, 2 }
                indexer = 0xFF;
                foreach (var item in ll2)
                {
                    this.helper.WriteLine(
                        item.ToString());
                    ++indexer;
                    switch (indexer)
                    {
                        case 0:
                            Assert.Equal(
                                3,
                                item);
                            break;
                        case 1:
                            Assert.Equal(
                                1,
                                item);
                            break;
                        case 2:
                            Assert.Equal(
                                2,
                                item);
                            break;
                        case 3:
                            throw new Exception(
                                @"Should not reach here.");
                    }
                }
            }
        }

        public class When_AddAfter_is_called : Context
        {
            [Fact]
            public void Adds_item_after_node()
            {
                var targetNode = new XLinkedListNode<object>();
                targetNode.O = 3;
                this.ll.Clear();
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

            [Fact]
            public void Rearranging_test()
            {
                var ll2 = new XLinkedList<object>();

                ll2.AddTail(this.node1);
                ll2.AddTail(this.node2);
                ll2.AddTail(this.node4);
                ll2.AddTail(this.node5);
                ll2.AddTail(this.node3); // { 1, 2, 4, 5, 3 }

                foreach (var item in ll2)
                {
                    this.helper.WriteLine(item?.ToString());
                }

                ll2.AddAfter(
                    this.node3, 
                    this.node4); // { 1, 2, 5, 3, 4 }
                // node = this.node3
                // newNode = this.node4
                // newPrev.Next = newNode.Next;
                // newNext.Previous = newNode.Previous
                this.helper.WriteLine(string.Empty);

                foreach (var item in ll2)
                {
                    this.helper.WriteLine(item?.ToString());
                }

                ll2.AddAfter(
                    this.node4, 
                    this.node5); // { 1, 2, 3, 4, 5 }

                // node = this.node4
                // newNode = this.node5
                // turn { 1, 2, 5, 3, 4 } into { 1, 2, 3, 4, 5 }
                // node.Next = newNode
                // newNode.Previous = node
                // newNode.Next = nodeNext

                this.helper.WriteLine(string.Empty);
                foreach (var item in ll2)
                {
                    this.helper.WriteLine(item?.ToString());
                }

                ll2.AddAfter(
                    ll2.HeadN,
                    ll2.TailN); // { 1, 5, 2, 3, 4 }
                // node = head node
                // newNode = tail node


                this.helper.WriteLine(string.Empty);
                foreach (var item in ll2)
                {
                    this.helper.WriteLine(item?.ToString());
                }

                ll2.AddAfter(
                    ll2.TailN,
                    ll2.HeadN); // { 5, 2, 3, 4, 1 }

                this.helper.WriteLine(string.Empty);
                foreach (var item in ll2)
                {
                    this.helper.WriteLine(item?.ToString());
                }

                ll2.AddAfter(
                    node3,
                    node4); // { 5, 2, 3, 4, 1 }
                this.helper.WriteLine(string.Empty);
                foreach (var item in ll2)
                {
                    this.helper.WriteLine(item?.ToString());
                }
            }

            public When_AddAfter_is_called(ITestOutputHelper helper)
                : base(helper)
            {
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
                this.ll.Clear();
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

            [Fact]
            public void Rearranging_test()
            {
                var ll2 = new XLinkedList<object>();

                ll2.AddTail(this.node1);
                ll2.AddTail(this.node2);
                ll2.AddTail(this.node3);
                ll2.AddTail(this.node4);
                ll2.AddTail(this.node5); // { 1, 2, 3, 4, 5 }

                foreach (var item in ll2)
                {
                    this.helper.WriteLine(item?.ToString());
                }

                ll2.AddBefore(
                    this.node1, 
                    this.node4); // { 4, 1, 2, 3, 5 }
                this.helper.WriteLine(string.Empty);

                foreach (var item in ll2)
                {
                    this.helper.WriteLine(item?.ToString());
                }

                ll2.AddBefore(
                    this.node4, 
                    this.node5); // { 5, 4, 1, 2, 3 }

                this.helper.WriteLine(string.Empty);
                foreach (var item in ll2)
                {
                    this.helper.WriteLine(item?.ToString());
                }

                ll2.AddBefore(
                    ll2.TailN,
                    ll2.HeadN); // { 4, 1, 2, 5, 3 }

                this.helper.WriteLine(string.Empty);
                foreach (var item in ll2)
                {
                    this.helper.WriteLine(item?.ToString());
                }

                ll2.AddBefore(
                    ll2.HeadN,
                    ll2.TailN); // { 3, 4, 1, 2, 5 }

                this.helper.WriteLine(string.Empty);
                foreach (var item in ll2)
                {
                    this.helper.WriteLine(item?.ToString());
                }

                ll2.AddBefore(
                    node4,
                    node3); // { 3, 4, 1, 2, 5 }
                this.helper.WriteLine(string.Empty);
                foreach (var item in ll2)
                {
                    this.helper.WriteLine(item?.ToString());
                }
            }

            [Fact]
            public void Same_place_test()
            {
                var ll2 = new XLinkedList<object>();
                var node2 = new XLinkedListNode<object>
                {
                    O = 2
                };
                ll2.AddTail(1);
                ll2.AddTail(node2);
                ll2.AddTail(3);
                ll2.AddAfter(
                    node2,
                    node2);

                foreach (var item in ll2)
                {
                    this.helper.WriteLine(item.ToString());
                }

            }

            public When_AddBefore_is_called(ITestOutputHelper helper)
                : base(helper)
            {
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

            public When_Find_is_called(ITestOutputHelper helper)
                : base(helper)
            {
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

            public When_FindLast_is_called(ITestOutputHelper helper)
                : base(helper)
            {
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

            public When_GetNodes_is_called(ITestOutputHelper helper)
                : base(helper)
            {
            }
        }

        public class When_Remove_is_called : Context
        {
            [Fact]
            public void Works()
            {
                this.ll.Remove(3);
                foreach (var item in this.ll)
                {
                    this.helper.WriteLine(item.ToString()); // 1, 2, 4, 5 should be
                }

                this.ll.AddAfter(
                    this.node2, 
                    this.node3);
                // to make { 1, 2, 4, 5 } into { 1, 2, 3, 4, 5 }
                this.helper.WriteLine(string.Empty);
                foreach (var item in this.ll)
                {
                    this.helper.WriteLine(item.ToString());
                }

                this.ll.Remove(1);
                this.helper.WriteLine(string.Empty);
                foreach (var item in this.ll)
                {
                    this.helper.WriteLine(item.ToString()); // 2, 3, 4, 5 should be
                }

                this.ll.AddBefore(
                    this.node2,
                    this.node1); 
                this.helper.WriteLine(string.Empty);
                foreach (var item in this.ll)
                {
                    this.helper.WriteLine(item.ToString()); // { 1, 2, 3, 4, 5}
                }
                this.ll.Remove(5);
                this.helper.WriteLine(string.Empty);
                foreach (var item in this.ll)
                {
                    this.helper.WriteLine(item.ToString()); // 1, 2, 3, 4 should be
                }
            }

            public When_Remove_is_called(ITestOutputHelper helper)
                : base(helper)
            {
            }
        }

        public class When_Remove_node_is_called : Context
        {
            public When_Remove_node_is_called(ITestOutputHelper helper)
                : base(helper)
            {
            }

            [Fact]
            public void Ices_the_head()
            {
                this.ll.Remove(
                    this.ll.HeadN);
                foreach (var item in this.ll)
                {
                    this.helper.WriteLine(item.ToString()); // { 2, 3, 4, 5 }
                }
            }

            [Fact]
            public void Ices_the_tail()
            {
                this.ll.Remove(
                    this.ll.TailN);
                foreach (var item in this.ll)
                {
                    this.helper.WriteLine(item.ToString()); // { 1, 2, 3, 4 }
                }
            }

            [Fact]
            public void Ices_the_head_and_the_tail()
            {
                this.ll.Remove(
                    this.ll.TailN);
                this.ll.Remove(
                    this.ll.HeadN);
                foreach (var item in this.ll)
                {
                    this.helper.WriteLine(item.ToString()); // { 2, 3, 4 }
                }
            }

            [Fact]
            public void Ices_all_the_middle_ones()
            {
                this.ll.Remove(
                    this.node2);
                this.ll.Remove(
                    this.node4);
                this.ll.Remove(
                    this.node3);
                foreach (var item in this.ll)
                {
                    this.helper.WriteLine(item.ToString()); // { 1, 5 }
                }
            }
        }
    }
}
