namespace xofz.Tests.Framework.Axioses
{
    using xofz.Framework;
    using xofz.Framework.Axioses;
    using Xunit;

    public class RaiserTests
    {
        public class Context
        {
            protected Context()
            {
                this.raiser = new Raiser();
                this.er = new EventRaiser();
            }

            protected readonly Raiser raiser;
            protected readonly EventRaiser er;
        }

        public class When_Process_is_called 
            : Context
        {
            [Fact]
            public void Subscribes_behavior_to_Raised()
            {
                bool subscribed = false;
                this.raiser.Process(
                    () =>
                    {
                        subscribed = true;
                    });

                this.er.Raise(
                    this.raiser,
                    nameof(this.raiser.Raised));

                Assert.True(
                    subscribed);
            }
        }

        public class When_Unprocess_is_called : Context
        {
            [Fact]
            public void Unsubscribes_behavior_from_Raised()
            {
                bool unsubscribed = true;
                Do behavior = () =>
                {
                    unsubscribed = false;
                };
                this.raiser.Process(
                    behavior);
                this.raiser.Unprocess(
                    behavior);

                this.er.Raise(
                    this.raiser,
                    nameof(this.raiser.Raised));

                Assert.True(
                    unsubscribed);
            }
        }

        public class When_Raise_is_called : Context
        {
            [Fact]
            public void Raises_Raised()
            {
                bool raised = false;
                this.raiser.Process(
                    () =>
                    {
                        raised = true;
                    });

                this.raiser.Raise();

                Assert.True(
                    raised);
            }
        }
    }
}
