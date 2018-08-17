namespace xofz.Tests.Framework.Transformation
{
    using xofz.Framework.Transformation;
    using Ploeh.AutoFixture;
    using Xunit;

    public class EnumerableDisplacerTests
    {
        public class Context
        {
            protected Context()
            {
                this.displacer = new EnumerableDisplacer();
            }

            protected readonly EnumerableDisplacer displacer;
            protected readonly Fixture fixture;
        }

        public class When_Displace_is_called : Context
        {
            [Fact]
            public void If_source_is_null_yield_breaks()
            {
                var noItems = true;
                foreach (var item in this.displacer.Displace<object>(
                    null, 0xFFFF))
                {
                    noItems = false;
                }

                Assert.True(noItems);
            }

            [Fact]
            public void If_displace_count_less_than_1_yield_returns_each_item_in_source()
            {
                var source = new[]
                {
                    1,2,3,4,5,6,7,8
                };

                var counter = 0;
                foreach (var item in this.displacer.Displace(source, 0))
                {
                    ++counter;
                    Assert.Equal(counter, item);
                }
            }

            [Fact]
            public void Otherwise_displaces_the_first_items()
            {
                var source = new[]
                {
                    1,2,3,4,5,6,7,8,9,10
                };

                var displaceCount = 4;
                var array = EnumerableHelpers.ToArray(
                    this.displacer.Displace(
                    source,
                    displaceCount));

                Assert.Equal(array[0], 5);
                Assert.Equal(array[1], 6);
                Assert.Equal(array[2], 7);
                Assert.Equal(array[3], 8);
                Assert.Equal(array[4], 1);
                Assert.Equal(array[5], 2);
                Assert.Equal(array[6], 3);
                Assert.Equal(array[7], 4);
                Assert.Equal(array[8], 9);
                Assert.Equal(array[9], 10);
            }
        }
    }
}
