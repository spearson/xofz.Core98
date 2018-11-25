namespace xofz.Tests.Framework.Transformation
{
    using xofz.Framework.Transformation;
    using Ploeh.AutoFixture;
    using Xunit;

    public class EnumerableDecoratorTests
    {
        public class Context
        {
            protected Context()
            {
                this.decorator = new EnumerableDecorator();
                this.fixture = new Fixture();
            }

            protected readonly EnumerableDecorator decorator;
            protected readonly Fixture fixture;
        }

        public class When_Decorate_is_called : Context
        {
            [Fact]
            public void If_source_is_null_yield_breaks()
            {
                var noItems = true;
                foreach (var item in this.decorator.Decorate<object>(
                    null, o => o.GetHashCode()))
                {
                    noItems = false;
                }

                Assert.True(noItems);
            }

            [Fact]
            public void If_action_is_null_yield_returns_each_item_from_the_source_unchanged()
            {
                var source = new[] { 1, 4, 7, 10 };
                foreach (var item in this.decorator.Decorate(
                    source, null))
                {
                    Assert.Contains(item, source);
                }
            }

            public class ToBeDecorated
            {
                public virtual int Value { get; set; }
            }

            [Fact]
            public void Otherwise_decorates_the_items()
            {
                var f = this.fixture;
                var source = new[]
                {
                    new ToBeDecorated(),
                    new ToBeDecorated(),
                    new ToBeDecorated(),
                    new ToBeDecorated(),
                    new ToBeDecorated(),
                    new ToBeDecorated(),
                    new ToBeDecorated(),
                    new ToBeDecorated(),
                };
                foreach (var item in source)
                {
                    Assert.Equal(0, item.Value);
                }

                Do<ToBeDecorated> decoration
                    = tbd => tbd.Value = f.Create<int>();
                foreach (var item in this.decorator.Decorate(
                    source,
                    decoration))
                {
                    Assert.NotEqual(0, item.Value);
                }
            }
        }
    }
}
