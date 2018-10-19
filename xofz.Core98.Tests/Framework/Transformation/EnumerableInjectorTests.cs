namespace xofz.Tests.Framework.Transformation
{
    using xofz.Framework.Lots;
    using xofz.Framework.Transformation;
    using Xunit;

    public class EnumerableInjectorTests
    {
        public class Context
        {
            protected Context()
            {
                this.injector = new EnumerableInjector();
            }

            protected readonly EnumerableInjector injector;
        }

        public class When_Inject_is_called : Context
        {
            [Fact]
            public void If_source_is_null_and_injections_is_null_yield_breaks()
            {
                var noItems = true;
                var injectionPoints = new ArrayLot<long>(
                    new long[] { 0xFF, 0xFFF });
                foreach (var item in this.injector.Inject<object>(
                    null,
                    injectionPoints,
                    null))
                {
                    noItems = false;
                }

                Assert.True(noItems);
            }

            [Fact]
            public void If_source_is_null_yield_returns_each_injection_and_then_breaks()
            {
                var injections = new object[] { "test string", 0xAF, 4.56 };
                byte counter = 0;
                foreach (var item in this.injector.Inject<object>(
                    null,
                    null,
                    injections))
                {
                    Assert.Same(injections[counter], item);
                    ++counter;
                }
            }

            [Fact]
            public void If_injections_is_null_yield_returns_each_item_in_source_and_then_breaks()
            {
                object[] injections = null;
                var source = new object[] { "testzorz", "another string", 0.00, 0xFAF };
                byte counter = 0;
                foreach (var item in this.injector.Inject<object>(
                    source,
                    new ArrayLot<long>(new long[] { 2, 4 }),
                    injections))
                {
                    Assert.Same(
                        source[counter], item);
                    ++counter;
                }
            }

            [Fact]
            public void If_injectionPoints_is_null_yield_returns_each_item_in_source_and_then_breaks()
            {
                Lot<long> injectionPoints = null;
                var source = new object[] { "testzorz", "another string", 0.00, 0xFAF };
                byte counter = 0;
                foreach (var item in this.injector.Inject<object>(
                    source,
                    injectionPoints,
                    new object(),
                    new object(),
                    new object()))
                {
                    Assert.Same(
                        source[counter], item);
                    ++counter;
                }
            }

            [Fact]
            public void Otherwise_injects_the_injections_at_the_injection_points_in_the_enumerable()
            {
                var source = new object[]
                {
                    0xFFABF,
                    3.567,
                    "testing 1 2 3...",
                    999,
                    0,
                    "more test data"
                };
                byte counter = 0;
                Lot<long> injectionPoints = new ArrayLot<long>(
                    new long[] { 1, 3, 4 });
                var injections = new object[]
                {
                    "injection 1",
                    "injection 2",
                    "injection 3"
                };
                foreach (var item in this.injector.Inject(
                    source,
                    injectionPoints,
                    injections))
                {
                    object currentObject = null;
                    switch (counter)
                    {
                        case 0:
                            currentObject = source[0];
                            break;
                        case 1:
                            currentObject = injections[0];
                            break;
                        case 2:
                            currentObject = source[1];
                            break;
                        case 3:
                            currentObject = source[2];
                            break;
                        case 4:
                            currentObject = injections[1];
                            break;
                        case 5:
                            currentObject = source[3];
                            break;
                        case 6:
                            currentObject = injections[2];
                            break;
                        case 7:
                            currentObject = source[4];
                            break;
                        case 8:
                            currentObject = source[5];
                            break;
                    }

                    Assert.Same(item, currentObject);
                    ++counter;
                }
            }
        }
    }
}
