namespace xofz.Tests.Framework.Transformation
{
    using xofz.Framework.Transformation;
    using Xunit;

    public class EnumerableFrontBackLoaderTests
    {
        public class Context
        {
            protected Context()
            {
                this.frontBackLoader = new EnumerableFrontBackLoader();
            }

            protected readonly EnumerableFrontBackLoader frontBackLoader;
        }

        public class When_FrontLoad_is_called : Context
        {
            [Fact]
            public void If_frontItems_null_yield_returns_source()
            {
                var source = new[] { 0, 45, 676, 0xFFF };
                byte counter = 0;
                foreach (var item in this.frontBackLoader.FrontLoad(
                    source, null))
                {
                    Assert.Equal(source[counter], item);
                    ++counter;
                }
            }

            [Fact]
            public void If_source_and_frontItems_null_yield_breaks()
            {
                var noItems = true;
                foreach (var item in this.frontBackLoader.FrontLoad<object>(
                    null, null))
                {
                    noItems = false;
                }

                Assert.True(noItems);
            }

            [Fact]
            public void If_source_null_yield_returns_front_items_only()
            {
                var frontItems = new[] { 0, 0xFF, 32, 64, 0xFFFF };
                byte counter = 0;
                foreach(var item in this.frontBackLoader.FrontLoad(
                    null, frontItems))
                {
                    Assert.Equal(frontItems[counter], item);
                    ++counter;
                }
            }

            [Fact]
            public void Otherwise_yield_returns_front_items_and_then_the_source()
            {
                var frontItems = new long[] { 0xA, 0xD, 0xDEADBEEF, 0xD34DB33F };
                var source = new long[] { 0, 0xF, 0xFF, 0xFFF, 0xFFFF, 0xFFFFF, 0xFFFFFF };
                byte counter = 0;
                foreach (var item in this.frontBackLoader.FrontLoad(
                    source, frontItems))
                {
                    if (counter < frontItems.Length)
                    {
                        Assert.Equal(item, frontItems[counter]);
                        ++counter;
                        continue;
                    }

                    var fil = frontItems.Length;
                    Assert.Equal(item, source[counter - fil]);
                    ++counter;
                }
            }
        }

        public class When_BackLoad_is_called : Context
        {
            [Fact]
            public void If_backItems_null_yield_returns_source()
            {
                var source = new[] { 0, 45, 676, 0xFFF };
                byte counter = 0;
                foreach (var item in this.frontBackLoader.BackLoad(
                    source, null))
                {
                    Assert.Equal(source[counter], item);
                    ++counter;
                }
            }

            [Fact]
            public void If_source_and_backItems_null_yield_breaks()
            {
                var noItems = true;
                foreach (var item in this.frontBackLoader.BackLoad<object>(
                    null, null))
                {
                    noItems = false;
                }

                Assert.True(noItems);
            }

            [Fact]
            public void If_source_null_yield_returns_back_items_only()
            {
                var backItems = new[] { 0, 0xFF, 32, 64, 0xFFFF };
                byte counter = 0;
                foreach (var item in this.frontBackLoader.BackLoad(
                    null, backItems))
                {
                    Assert.Equal(backItems[counter], item);
                    ++counter;
                }
            }

            [Fact]
            public void Otherwise_yield_returns_source_and_then_the_back_items()
            {
                var backItems = new long[] { 0xA, 0xD, 0xDEADBEEF, 0xD34DB33F };
                var source = new long[] { 0, 0xF, 0xFF, 0xFFF, 0xFFFF, 0xFFFFF, 0xFFFFFF };
                byte counter = 0;
                foreach (var item in this.frontBackLoader.BackLoad(
                    source, backItems))
                {
                    if (counter < source.Length)
                    {
                        Assert.Equal(item, source[counter]);
                        ++counter;
                        continue;
                    }

                    var sl = source.Length;
                    Assert.Equal(item, backItems[counter - sl]);
                    ++counter;
                }
            }
        }
    }
}