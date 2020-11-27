namespace xofz.Tests.Framework.Login
{
    using System;
    using System.Security;
    using FakeItEasy;
    using Ploeh.AutoFixture;
    using xofz.Framework;
    using xofz.Framework.Login;
    using xofz.UI;
    using Xunit;

    public class LoginKeyTappedHandlerTests
    {
        public class Context
        {
            protected Context()
            {
                this.web = new MethodWeb();
                this.handler = new LoginKeyTappedHandler(
                    this.web);
                this.ui = A.Fake<LoginUi>();
                this.uiRW = new UiReaderWriter();
                this.ac = A.Fake<AccessController>();
                this.settings = new SettingsHolder();
                this.timer = A.Fake<Timer>();
                this.raiser = A.Fake<EventRaiser>();
                this.stopHandler = A.Fake<StopHandler>();

                var w = this.web;
                w.RegisterDependency(
                    this.uiRW);
                w.RegisterDependency(
                    this.ac);
                w.RegisterDependency(
                    this.settings);
                w.RegisterDependency(
                    this.timer,
                    DependencyNames.Timer);
                w.RegisterDependency(
                    this.raiser);
                w.RegisterDependency(
                    this.stopHandler);
                this.fixture = new Fixture();
            }

            protected readonly MethodWeb web;
            protected readonly LoginKeyTappedHandler handler;
            protected readonly LoginUi ui;
            protected readonly UiReaderWriter uiRW;
            protected readonly AccessController ac;
            protected readonly SettingsHolder settings;
            protected readonly Timer timer;
            protected readonly EventRaiser raiser;
            protected readonly StopHandler stopHandler;
            protected readonly Fixture fixture;
        }

        public class When_Handle_is_called : Context
        {
            [Fact]
            public void Reads_ui_CurrentPassword()
            {
                this.handler.Handle(
                    this.ui);

                A
                    .CallTo(() => this.ui.CurrentPassword)
                    .MustHaveHappened();
            }

            [Fact]
            public void Calls_ac_InputPassword()
            {
                var pw = this.fixture.Create<SecureString>();
                this.settings.Duration = this.fixture.Create<TimeSpan>();
                this.ui.CurrentPassword = pw;

                this.handler.Handle(
                    this.ui);

                A
                    .CallTo(() => this.ac.InputPassword(
                        pw,
                        this.settings.Duration))
                    .MustHaveHappened();
            }

            [Fact]
            public void If_cal_no_change_raises_timer_Elapsed()
            {
                this.handler.Handle(
                    this.ui);

                A
                    .CallTo(() => this.raiser.Raise(
                        this.timer,
                        nameof(this.timer.Elapsed)))
                    .MustHaveHappened();
            }

            [Fact]
            public void If_auth_higher_than_none_sets_settings_CurrentPassword()
            {
                var pw = this.fixture.Create<SecureString>();
                this.ui.CurrentPassword = pw;
                this.settings.Duration = this.fixture.Create<TimeSpan>();

                A
                    .CallTo(() => this.ac.InputPassword(
                        pw,
                        this.settings.Duration))
                    .Invokes(() => A.CallTo(
                            () => this.ac.CurrentAccessLevel)
                        .Returns(AccessLevel.Level1));

                this.handler.Handle(
                    this.ui);

                Assert.Same(
                    pw,
                    this.settings.CurrentPassword);

            }

            [Fact]
            public void Also_calls_StopHandler_Handle()
            {
                var pw = this.fixture.Create<SecureString>();
                this.ui.CurrentPassword = pw;
                this.settings.Duration = this.fixture.Create<TimeSpan>();

                A
                    .CallTo(() => this.ac.InputPassword(
                        pw,
                        this.settings.Duration))
                    .Invokes(() => A.CallTo(
                            () => this.ac.CurrentAccessLevel)
                        .Returns(AccessLevel.Level1));

                this.handler.Handle(
                    this.ui);

                A
                    .CallTo(() => this.stopHandler.Handle(
                        this.ui))
                    .MustHaveHappened();
            }

            [Fact]
            public void Calls_ui_FocusPassword_otherwise()
            {
                this.handler.Handle(
                    this.ui);

                A
                    .CallTo(() => this.ui.FocusPassword())
                    .MustHaveHappened();
            }
        }
    }
}
