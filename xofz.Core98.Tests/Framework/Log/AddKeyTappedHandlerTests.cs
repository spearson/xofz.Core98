namespace xofz.Tests.Framework.Log
{
    using FakeItEasy;
    using Ploeh.AutoFixture;
    using xofz.Framework;
    using xofz.Framework.Log;
    using xofz.UI;
    using Xunit;

    public class AddKeyTappedHandlerTests
    {
        public class Context
        {
            protected Context()
            {
                this.web = new MethodWeb();
                this.handler = new AddKeyTappedHandler(
                    this.web);
                this.present1 = A.Fake<Do>();
                this.present2 = A.Fake<Do<string>>();
                this.ui = A.Fake<LogUi>();
                this.fixture = new Fixture();
                this.name = this.fixture.Create<string>();
            }

            protected readonly MethodWeb web;
            protected readonly AddKeyTappedHandler handler;
            protected readonly Do present1;
            protected readonly Do<string> present2;
            protected readonly LogUi ui;
            protected readonly Fixture fixture;
            protected readonly string name;
        }

        public class When_Handle1_is_called : Context
        {
            [Fact]
            public void Calls_present()
            {
                this.handler.Handle(
                    this.present1);

                A
                    .CallTo(() => this.present1.Invoke())
                    .MustHaveHappened();
            }
        }

        public class When_Handle2_is_called : Context
        {
            [Fact]
            public void Calls_present()
            {
                this.handler.Handle(
                    this.present2,
                    this.name);

                A
                    .CallTo(() => this.present2.Invoke(
                        this.name))
                    .MustHaveHappened();
            }
        }

        public class When_Handle3_is_called : Context
        {
            [Fact]
            public void Calls_present()
            {
                this.handler.Handle(
                    this.present2,
                    this.ui,
                    this.name);

                A
                    .CallTo(() => this.present2.Invoke(
                        this.name))
                    .MustHaveHappened();
            }
        }
    }
}
