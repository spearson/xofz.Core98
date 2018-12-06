namespace xofz.Tests
{
    using Xunit;

    public class XTupleTests
    {
        public class XTuple_2_Equality
        {
            [Fact]
            public void Go()
            {
                var item1 = new object();
                var item2 = new object();

                Assert.Equal(
                    XTuple.Create(item1, item2),
                    XTuple.Create(item1, item2));
            }
        }

        public class XTuple_3_Equality
        {
            [Fact]
            public void Go()
            {
                var item1 = new object();
                var item2 = new object();
                var item3 = new object();

                Assert.Equal(
                    XTuple.Create(item1, item2, item3),
                    XTuple.Create(item1, item2, item3));
            }
        }

        public class XTuple_4_Equality
        {
            [Fact]
            public void Go()
            {
                var item1 = new object();
                var item2 = new object();
                var item3 = new object();
                var item4 = new object();

                Assert.Equal(
                    XTuple.Create(item1, item2, item3, item4),
                    XTuple.Create(item1, item2, item3, item4));
            }
        }
    }
}
