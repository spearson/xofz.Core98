namespace xofz.Tests.Framework.Log
{
    using FakeItEasy;
    using Ploeh.AutoFixture;
    using xofz.Framework;
    using xofz.Framework.Log;
    using xofz.UI;
    using xofz.UI.Log;
    using Xunit;

    public class FilterTextChangedHandlerTests
    {
        public class Context
        {
            protected Context()
            {
                this.web = new MethodWeb();
                this.handler = new FilterTextChangedHandler(
                    this.web);
                this.fields = new FieldHolder();
                this.reloader = A.Fake<EntryReloader>();
                this.ui = A.Fake<LogUi>();
                this.fixture = new Fixture();
                this.name = this.fixture.Create<string>();

                var w = this.web;
                w.RegisterDependency(
                    this.fields,
                    this.name);
                w.RegisterDependency(
                    this.reloader);
            }

            protected readonly MethodWeb web;
            protected readonly FilterTextChangedHandler handler;
            protected readonly FieldHolder fields;
            protected readonly EntryReloader reloader;
            protected readonly LogUi ui;
            protected readonly string name;
            protected readonly Fixture fixture;
        }

        public class When_Handle_is_called : Context
        {
            [Fact]
            public void If_started_calls_reloader_Reload()
            {
                this.fields.startedIf1 = 1;

                this.handler.Handle(
                    this.ui,
                    this.name);

                A
                    .CallTo(() => this.reloader.Reload(
                        this.ui,
                        this.name))
                    .MustHaveHappened();
            }

            [Fact]
            public void Switches_refreshOnStartIf1_to_1()
            {
                this.fields.refreshOnStartIf1 = 0;

                this.handler.Handle(
                    this.ui,
                    this.name);

                Assert.Equal(
                    this.fields.refreshOnStartIf1,
                    1);
            }
        }
    }
}
