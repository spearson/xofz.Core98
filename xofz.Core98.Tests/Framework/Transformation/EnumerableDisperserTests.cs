namespace xofz.Tests.Framework.Transformation
{
    using System.Collections.Generic;
    using xofz.Framework.Lots;
    using xofz.Framework.Transformation;
    using Xunit;

    public class EnumerableDisperserTests
    {
        public class Context
        {
            protected Context()
            {
                this.disperser = new EnumerableDisperser();
            }

            protected readonly EnumerableDisperser disperser;
        }

        public class When_Disperse_is_called : Context
        {
            [Fact]
            public void If_source_is_null_yield_breaks()
            {
                var noItems = true;
                foreach (var item in this.disperser.Disperse<object>(
                    null,
                    null,
                    null))
                {
                    noItems = false;
                }

                Assert.True(noItems);
            }

            [Fact]
            public void If_dispersion_is_null_yield_returns_all_items_in_source()
            {
                var source = new[] { 1, 2, 2, 2, 3, 4, 77, -45 };
                long currentCount = 0;
                foreach (var item in this.disperser.Disperse(
                    source,
                    null,
                    null))
                {
                    ++currentCount;
                    Assert.Contains(item, source);
                }

                Assert.Equal(currentCount, source.Length);
            }

            [Fact]
            public void If_dispersionPoints_is_null_yield_returns_all_items_in_source()
            {
                var source = new[] { -125, 512, -512, 0, 46, 733 };
                var dispersion = new[] { 0, 0xFF };
                long currentCount = 0;
                foreach (var item in this.disperser.Disperse(
                    source,
                    dispersion,
                    null))
                {
                    ++currentCount;
                    Assert.Contains(item, source);
                }

                Assert.Equal(currentCount, source.Length);
            }

            [Fact]
            public void Otherwise_disperses_all_items_amongst_the_enumerable()
            {
                var source = new[] { -125, 512, -512, 0, 46, 733 };
                var dispersion = new[] { 0, 0xFF };
                Lot<long> dispersionPoints = new ArrayLot<long>(
                    new long[] { 1, 4 });
                long currentCount = 0;
                foreach (var item in this.disperser.Disperse(
                    source,
                    dispersion,
                    dispersionPoints))
                {
                    ++currentCount;
                    if (currentCount == 2) // dispersion point of 1
                    {
                        Assert.Equal(0, item);
                        continue;
                    }

                    if (currentCount == 6) // dispersion point of 4
                    {
                        Assert.Equal(0xFF, item);
                        continue;
                    }

                    Assert.Contains(item, source);
                }

                Assert.Equal(
                    currentCount,
                    source.Length + dispersionPoints.Count);
            }
        }
    }
}
