namespace xofz.Tests.Framework.Main
{
    using FakeItEasy;
    using xofz.Framework;
    using xofz.Framework.Main;
    using xofz.UI;
    using Xunit;

    public class ShutdownRequestedHandlerTests
    {
        public class Context
        {
            protected Context()
            {
                this.web = new MethodWeb();
                this.handler = new ShutdownRequestedHandler(
                    this.web);
                this.ui = A.Fake<MainUi>();
                this.logIn = A.Fake<Do>();
                this.shutdown = A.Fake<Do>();
                this.accessController = A.Fake<AccessController>();
                this.settings = new SettingsHolder();

                var w = this.web;
                w.RegisterDependency(
                    this.accessController);
                w.RegisterDependency(
                    this.settings);
            }

            protected readonly MethodWeb web;
            protected readonly ShutdownRequestedHandler handler;
            protected readonly MainUi ui;
            protected readonly Do logIn;
            protected readonly Do shutdown;
            protected readonly AccessController accessController;
            protected readonly SettingsHolder settings;
        }

        public class When_Handle_is_called : Context
        {
            [Fact]
            public void Reads_ac_CurrentAccessLevel()
            {
                this.handler.Handle(
                    this.ui,
                    this.logIn,
                    this.shutdown);

                A
                    .CallTo(() => this.accessController.CurrentAccessLevel)
                    .MustHaveHappened();
            }

            [Fact]
            public void If_current_level_geq_shutdown_level_invokes_shutdown()
            {
                this.handler.Handle(
                    this.ui,
                    this.logIn,
                    this.shutdown);

                A
                    .CallTo(() => this.shutdown.Invoke())
                    .MustHaveHappened();
            }

            [Fact]
            public void Otherwise_logs_in()
            {
                this.settings.ShutdownLevel = AccessLevel.Level1;

                this.handler.Handle(
                    this.ui,
                    this.logIn,
                    this.shutdown);

                A
                    .CallTo(() => this.logIn.Invoke())
                    .MustHaveHappened();
            }

            [Fact]
            public void If_authed_after_login_invokes_shutdown()
            {
                const AccessLevel shutdownLevel = AccessLevel.Level1;
                this.settings.ShutdownLevel = shutdownLevel;
                A
                    .CallTo(() => this.logIn.Invoke())
                    .Invokes(
                        () => A
                            .CallTo(() => this.accessController.CurrentAccessLevel)
                            .Returns(shutdownLevel));

                this.handler.Handle(
                    this.ui,
                    this.logIn,
                    this.shutdown);

                A
                    .CallTo(() => this.shutdown.Invoke())
                    .MustHaveHappened();
            }
        }
    }
}
