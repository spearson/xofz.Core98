namespace xofz.Tests.Framework.Transformation
{
    using xofz.Framework.Transformation;
    using Xunit;

    public class EnumerableFluxerTests
    {
        public class Context
        {
            protected Context()
            {
                this.fluxer = new EnumerableFluxer();
            }

            protected readonly EnumerableFluxer fluxer;
        }

        public class When_Flux_is_called : Context
        {
            [Fact]
            public void Fluxes_correctly()
            {
                var items = new long[]
                {
                    0x11,
                    0x22,
                    0x33,
                    0x44,
                    0x55,
                    0x66,
                    0x77,
                    0x88,
                    0x99
                };

                byte min = 1, max = 4;
                var totalItemCount = 0;

                foreach (var collection in this.fluxer.Flux(
                    items,
                    min,
                    max))
                {
                    var c = collection.Count;
                    totalItemCount += c;
                    Assert.InRange(
                        c,
                        min,
                        max);
                }

                Assert.Equal(
                    totalItemCount,
                    items.Length);
            }
        }
    }
}
