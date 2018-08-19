namespace xofz.Tests.Framework.Transformation
{
    using xofz.Framework.Transformation;
    using Xunit;

    public class EnumerableHeartbeaterTests
    {
        public class Context
        {
            protected Context()
            {
                this.heartbeater = new EnumerableHeartbeater();
            }

            protected readonly EnumerableHeartbeater heartbeater;
        }

        public class When_AddHeartbeat_is_called : Context
        {
            [Fact]
            public void If_source_is_null_yield_breaks()
            {
                var noItems = true;
                foreach (var item in this.heartbeater.AddHeartbeat(
                    null, 0xFFFF, 0xFF))
                {
                    noItems = false;
                }

                Assert.True(noItems);
            }

            [Fact]
            public void If_interval_is_less_than_one_yield_returns_each_item_in_source_and_then_breaks()
            {
                var source = new[] { 0x00, 0x0F, 0xA0, 0xAA, 0xAF, 0xFF };
                byte counter = 0;
                foreach (var item in this.heartbeater.AddHeartbeat(
                    source, 0xFAF, 0))
                {
                    Assert.Equal(
                        source[counter], item);
                    ++counter;
                }
            }

            [Fact]
            public void Otherwise_adds_a_heartbeat_at_the_appropriate_interval()
            {
                var source = new object[] { 0x00, 0x0F, 0xA0, 0xAA, 0xAF, 0xFF, 0xFAF };

                byte intervalCounter = 0, arrayCounter = 0;
                var heartbeat = (object)0xFFFF;
                byte interval = 3;
                foreach (var item in this.heartbeater.AddHeartbeat(
                    source,
                    heartbeat,
                    interval))
                {
                    if (intervalCounter < interval)
                    {
                        Assert.Same(
                            source[arrayCounter], item);
                        ++intervalCounter;
                        ++arrayCounter;
                        continue;
                    }

                    Assert.Same(
                        heartbeat, item);
                    intervalCounter = 0;                    
                }
            }
        }
    }
}
