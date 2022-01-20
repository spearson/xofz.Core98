namespace xofz.Tests
{
    using Xunit;

    public class IndexedLinkedListTests
    {
        public class Context
        {
            protected Context()
            {
                this.ll = new IndexedLinkedList<object>();
            }

            protected readonly IndexedLinkedList<object> ll;
        }

        public class When_GetNode_is_called : Context
        {
            [Fact]
            public void Returns_target_node()
            {
                var targetNode = new XLinkedListNode<object>();
                this.ll.AddTail(new object());
                this.ll.AddTail(new object());
                this.ll.AddTail(new object());
                this.ll.AddTail(targetNode);
                this.ll.AddTail(new object());
                this.ll.AddTail(new object());

                Assert.Same(
                    targetNode,
                    this.ll.GetNode(3));
            }
        }

        public class Indexing_test : Context
        {
            [Fact]
            public void Go()
            {
                this.ll.AddTail(new object());
                this.ll.AddTail(new object());
                this.ll.AddTail(new object());
                this.ll.AddTail(new object());

                var newObj1 = new object();
                var newObj2 = new object();
                this.ll[1] = newObj2;
                this.ll[3] = newObj1;

                Assert.Same(
                    ll[1],
                    newObj2);
                Assert.Same(
                    ll[3],
                    newObj1);
            }
        }

        public class IList_test : Context
        {
            [Fact]
            public void Go()
            {
                var newObj1 = new object();
                var newObj2 = new object();
                var newObj3 = new object();
                this.ll.AddTail(newObj1);
                this.ll.AddTail(newObj2);
                this.ll.AddTail(newObj3);

                Assert.Equal(
                    1,
                    this.ll.IndexOf(newObj2));

                var newObj4 = new object();
                this.ll[0] = newObj4;

                Assert.Equal(
                    newObj4,
                    this.ll[0]);
                Assert.Equal(
                    0,
                    this.ll.IndexOf(newObj4));

                this.ll.Insert(1, newObj1);
                Assert.Equal(
                    1,
                    this.ll.IndexOf(newObj1));

                this.ll.RemoveAt(0);
                Assert.Equal(
                    0,
                    this.ll.IndexOf(newObj1));

                this.ll.AddTail(newObj4);
                Assert.Equal(
                    0,
                    this.ll.IndexOf(newObj1));
                Assert.Equal(
                    1,
                    this.ll.IndexOf(newObj2));
                Assert.Equal(
                    2,
                    this.ll.IndexOf(newObj3));
                Assert.Equal(
                    3,
                    this.ll.IndexOf(newObj4));

                var newObj5 = new object();
                this.ll.Insert(
                    4,
                    newObj5);
                Assert.Equal(
                    4,
                    this.ll.IndexOf(newObj5));
                Assert.Same(
                    newObj5,
                    this.ll.TailN.O);
            }
        }
    }
}
