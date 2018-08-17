namespace xofz.Tests.Framework
{
    using xofz.Framework;
    using Xunit;

    public class EventRaiserTests
    {
        public class Context
        {
            protected Context()
            {
                this.eventRaiser = new EventRaiser();
            }

            protected readonly EventRaiser eventRaiser;
        }

        public class TestEventer
        {
            public event Action AnEvent;
        }

        public class When_Raise_is_called : Context
        {
            [Fact]
            public void Raises_the_event()
            {
                var te = new TestEventer();
                var raised = false;
                te.AnEvent += () => raised = true;

                this.eventRaiser.Raise(
                    te,
                    nameof(te.AnEvent));

                Assert.True(raised);
            }

            public class TE2 : TestEventer
            {
            }

            [Fact]
            public void Raises_the_event_even_if_there_is_some_inheritance()
            {
                var te2 = new TE2();
                var raised = false;
                te2.AnEvent += () => raised = true;

                this.eventRaiser.Raise(
                    te2,
                    nameof(te2.AnEvent));

                Assert.True(raised);
            }

            public class TE3 : TE2
            {
            }

            public class TE4 : TE3
            {
            }

            [Fact]
            public void Supports_multiple_levels_of_inheritance()
            {
                var er = this.eventRaiser;
                var raised = false;
                var te3 = new TE3();
                te3.AnEvent += () => raised = true;

                er.Raise(
                    te3,
                    nameof(te3.AnEvent));

                Assert.True(raised);

                raised = false;
                var te4 = new TE4();
                te4.AnEvent += () => raised = true;

                er.Raise(
                    te4,
                    nameof(te4.AnEvent));

                Assert.True(raised);

            }
        }
    }
}
