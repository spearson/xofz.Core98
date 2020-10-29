namespace xofz.Tests.Framework.Log
{
    using FakeItEasy;
    using Ploeh.AutoFixture;
    using xofz.Framework;
    using xofz.Framework.Log;
    using xofz.UI;
    using Xunit;

    public class StopHandlerTests
    {
        public class Context
        {
            protected Context()
            {
                this.web = new MethodWeb();
                this.handler = new StopHandler(
                    this.web);
                this.fields = new FieldHolder();
                this.fixture = new Fixture();
                this.name = this.fixture.Create<string>();
                this.ui = A.Fake<LogUi>();

                var w = this.web;
                w.RegisterDependency(
                    this.fields,
                    this.name);
            }

            protected readonly MethodWeb web;
            protected readonly StopHandler handler;
            protected readonly FieldHolder fields;
            protected readonly Fixture fixture;
            protected readonly string name;
            protected readonly LogUi ui;
        }

        public class When_Handle_is_called : Context
        {
            [Fact]
            public void Sets_fields_startIf1_to_0()
            {
                this.fields.startedIf1 = 1;

                this.handler.Handle(
                    this.ui,
                    this.name);

                Assert.Equal(
                    0,
                    this.fields.startedIf1);
            }
        }
    }
}
