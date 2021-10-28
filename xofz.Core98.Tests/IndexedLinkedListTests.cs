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
    }
}
