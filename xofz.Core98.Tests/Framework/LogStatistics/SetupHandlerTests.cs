namespace xofz.Tests.Framework.LogStatistics
{
    using FakeItEasy;
    using Ploeh.AutoFixture;
    using xofz.Framework;
    using xofz.Framework.LogStatistics;
    using xofz.UI;
    using Xunit;

    public class SetupHandlerTests
    {
        public class Context
        {
            protected Context()
            {
                this.web = new MethodWeb();
                this.handler = new SetupHandler(
                    this.web);
                this.ui = A.Fake<LogStatisticsUi>();
                this.fixture = new Fixture();
                this.name = this.fixture.Create<string>();
                this.resetter = A.Fake<DateResetter>();
                this.applier = A.Fake<LabelApplier>();

                var w = this.web;
                w.RegisterDependency(
                    this.resetter);
                w.RegisterDependency(
                    this.applier);
            }

            protected readonly MethodWeb web;
            protected readonly SetupHandler handler;
            protected readonly LogStatisticsUi ui;
            protected readonly Fixture fixture;
            protected readonly string name;
            protected readonly DateResetter resetter;
            protected readonly LabelApplier applier;
        }

        public class When_Handle_is_called : Context
        {
            [Fact]
            public void Calls_resetter_Reset()
            {
                this.handler.Handle(
                    this.ui,
                    this.name);

                A
                    .CallTo(() => this.resetter.Reset(
                        this.ui,
                        this.name))
                    .MustHaveHappened();
            }

            [Fact]
            public void If_ui_is_v2_calls_applier_Apply()
            {
                var v2 = A.Fake<LogStatisticsUiV2>();

                this.handler.Handle(
                    v2,
                    this.name);

                A
                    .CallTo(() => this.applier.Apply(
                        v2))
                    .MustHaveHappened();
            }
        }
    }
}
