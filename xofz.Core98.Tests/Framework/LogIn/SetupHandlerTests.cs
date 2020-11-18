namespace xofz.Tests.Framework.LogIn
{
    using FakeItEasy;
    using Ploeh.AutoFixture;
    using xofz.Framework;
    using xofz.Framework.Login;
    using xofz.UI;
    using Xunit;

    public class SetupHandlerTests
    {
        public class Context
        {
            protected Context()
            {
                this.web = new MethodWebV2();
                this.handler = new SetupHandler(
                    this.web);
                this.ui = A.Fake<LoginUiV2>();
                this.labels = new Labels();
                this.uiRW = new UiReaderWriter();
                this.loader = A.Fake<KeyboardLoader>();
                this.ac = A.Fake<AccessController>();
                this.applier = A.Fake<LabelApplier>();
                this.timer = A.Fake<Timer>();
                this.fixture = new Fixture();

                var w = this.web;
                w.RegisterDependency(
                    this.labels);
                w.RegisterDependency(
                    this.uiRW);
                w.RegisterDependency(
                    this.loader);
                w.RegisterDependency(
                    this.ac);
                w.RegisterDependency(
                    this.applier);
                w.RegisterDependency(
                    this.timer,
                    DependencyNames.Timer);
            }

            protected readonly MethodWebV2 web;
            protected readonly SetupHandler handler;
            protected readonly LoginUi ui;
            protected readonly Labels labels;
            protected readonly UiReaderWriter uiRW;
            protected readonly KeyboardLoader loader;
            protected readonly AccessController ac;
            protected readonly LabelApplier applier;
            protected readonly Timer timer;
            protected readonly Fixture fixture;
        }

        public class When_Handle_is_called : Context
        {
            [Fact]
            public void Sets_ui_TimeRemaining_to_labels_NotLoggedIn()
            {
                this.labels.NotLoggedIn = this.fixture.Create<string>();
                this.ui.TimeRemaining = null;

                this.handler.Handle(
                    this.ui);

                Assert.Equal(
                    this.labels.NotLoggedIn,
                    this.ui.TimeRemaining);
            }

            [Fact]
            public void If_KeyboardLoader_sets_KeyboardKeyVisible_to_true()
            {
                this.ui.KeyboardKeyVisible = false;

                this.handler.Handle(
                    this.ui);

                Assert.True(
                    this.ui.KeyboardKeyVisible);
            }

            [Fact]
            public void Otherwise_false()
            {
                var w = this.web;
                w.Unregister<KeyboardLoader>();
                this.ui.KeyboardKeyVisible = true;

                this.handler.Handle(
                    this.ui);

                Assert.False(
                    this.ui.KeyboardKeyVisible);
            }

            [Fact]
            public void Calls_applier_Apply()
            {
                this.handler.Handle(
                    this.ui);

                A
                    .CallTo(() => this.applier.Apply(
                        this.ui as LoginUiV2))
                    .MustHaveHappened();
            }

            [Fact]
            public void Calls_timer_Start()
            {
                this.handler.Handle(
                    this.ui);

                A
                    .CallTo(() => this.timer.Start(
                        A<long>.Ignored))
                    .MustHaveHappened();
            }
        }
    }
}
