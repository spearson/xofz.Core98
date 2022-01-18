namespace xofz.Tests.Presentation.Presenters
{
    using FakeItEasy;
    using Ploeh.AutoFixture;
    using xofz.Framework;
    using xofz.Framework.Login;
    using xofz.Presentation;
    using xofz.Presentation.Presenters;
    using xofz.UI;
    using xofz.UI.Login;
    using Xunit;

    public class LoginPresenterTests
    {
        public class Context
        {
            protected Context()
            {
                this.ui = A.Fake<LoginUi>();
                this.web = new MethodWeb();
                this.presenter = new LoginPresenter(
                    this.ui, 
                    this.web);
                this.fixture = new Fixture();

                this.subscriber = new EventSubscriber();
                this.setupHandler = A.Fake<SetupHandler>();
                this.navigator = A.Fake<Navigator>();
                this.startHandler = A.Fake<StartHandler>();
                this.stopHandler = A.Fake<StopHandler>();
                this.bsKeyTappedHandler = A.Fake<BackspaceKeyTappedHandler>();
                this.loginKeyTappedHandler = A.Fake<LoginKeyTappedHandler>();
                this.timerHandler = A.Fake<TimerHandler>();
                this.accessChangedHandler = A.Fake<AccessLevelChangedHandler>();
                this.kbKeyTappedHandler = A.Fake<KeyboardKeyTappedHandler>();
                this.accessController = A.Fake<AccessController>();
                this.timer = A.Fake<Timer>();

                var w = this.web;
                w.RegisterDependency(this.setupHandler);
                w.RegisterDependency(this.navigator);
                w.RegisterDependency(this.startHandler);
                w.RegisterDependency(this.stopHandler);
                w.RegisterDependency(this.bsKeyTappedHandler);
                w.RegisterDependency(this.loginKeyTappedHandler);
                w.RegisterDependency(this.timerHandler);
                w.RegisterDependency(this.accessChangedHandler);
                w.RegisterDependency(this.kbKeyTappedHandler);
                w.RegisterDependency(this.accessController);
                w.RegisterDependency(this.timer, DependencyNames.Timer);
            }

            protected readonly LoginUi ui;
            protected readonly MethodWeb web;
            protected readonly LoginPresenter presenter;
            protected readonly Fixture fixture;

            protected EventSubscriber subscriber;
            protected readonly SetupHandler setupHandler;
            protected readonly AccessController accessController;
            protected readonly xofz.Framework.Timer timer;
            protected readonly Navigator navigator;
            protected readonly StartHandler startHandler;
            protected readonly StopHandler stopHandler;
            protected readonly BackspaceKeyTappedHandler bsKeyTappedHandler;
            protected readonly LoginKeyTappedHandler loginKeyTappedHandler;
            protected readonly TimerHandler timerHandler;
            protected readonly AccessLevelChangedHandler accessChangedHandler;
            protected readonly KeyboardKeyTappedHandler kbKeyTappedHandler;
        }

        public class When_Setup_is_called : Context
        {
            [Fact]
            public void Subscribes_to_ui_LoginKeyTapped_event()
            {
                this.subscriber = A.Fake<EventSubscriber>();
                this.web.RegisterDependency(this.subscriber);

                this.presenter.Setup();

                A
                    .CallTo(() => this.subscriber.Subscribe(
                        this.ui,
                        nameof(this.ui.LoginKeyTapped),
                        A<Do>.Ignored))
                    .MustHaveHappened();
            }

            [Fact]
            public void Subscribes_to_CancelKeyTapped_event()
            {
                this.subscriber = A.Fake<EventSubscriber>();
                this.web.RegisterDependency(this.subscriber);

                this.presenter.Setup();

                A
                    .CallTo(() => this.subscriber.Subscribe(
                        this.ui,
                        nameof(this.ui.CancelKeyTapped),
                        A<Do>.Ignored))
                    .MustHaveHappened();
            }

            [Fact]
            public void Subscribes_to_BackspaceKeyTapped_event()
            {
                this.subscriber = A.Fake<EventSubscriber>();
                this.web.RegisterDependency(this.subscriber);

                this.presenter.Setup();

                A
                    .CallTo(() => this.subscriber.Subscribe(
                        this.ui,
                        nameof(this.ui.BackspaceKeyTapped),
                        A<Do>.Ignored))
                    .MustHaveHappened();
            }

            [Fact]
            public void Subscribes_to_KeyboardKeyTapped_event()
            {
                this.subscriber = A.Fake<EventSubscriber>();
                this.web.RegisterDependency(this.subscriber);

                this.presenter.Setup();

                A
                    .CallTo(() => this.subscriber.Subscribe(
                        this.ui,
                        nameof(this.ui.KeyboardKeyTapped),
                        A<Do>.Ignored))
                    .MustHaveHappened();
            }

            [Fact]
            public void Subscribes_to_AccessLevelChanged_event()
            {
                this.subscriber = A.Fake<EventSubscriber>();
                this.web.RegisterDependency(this.subscriber);

                this.presenter.Setup();

                var ac = this.accessController;
                A
                    .CallTo(() => this.subscriber.Subscribe(
                        ac,
                        nameof(ac.AccessLevelChanged),
                        A<Do<AccessLevel>>.Ignored))
                    .MustHaveHappened();
            }

            [Fact]
            public void Subscribes_to_Timer_Elapsed_event()
            {
                this.subscriber = A.Fake<EventSubscriber>();
                this.web.RegisterDependency(this.subscriber);

                this.presenter.Setup();

                var t = this.timer;
                A
                    .CallTo(() => this.subscriber.Subscribe(
                        t,
                        nameof(t.Elapsed),
                        A<Do>.Ignored))
                    .MustHaveHappened();
            }

            [Fact]
            public void Calls_SetupHandler_Handle()
            {
                this.presenter.Setup();

                A.CallTo(() => this.setupHandler.Handle(this.ui))
                    .MustHaveHappened();
            }

            [Fact]
            public void Registers_itself_with_the_Navigator()
            {
                var p = this.presenter;
                p.Setup();

                A.CallTo(() => this.navigator.RegisterPresenter(p))
                    .MustHaveHappened();
            }
         }

        public class When_Start_is_called : Context
        {
            [Fact]
            public void If_setup_calls_StartHandler_Handle()
            {
                var p = this.presenter;
                p.Setup();

                p.Start();

                A.CallTo(() => this.startHandler.Handle(this.ui))
                    .MustHaveHappened();
            }
        }

        public class When_Stop_is_called : Context
        {
            [Fact]
            public void If_setup_calls_StopHandler_Handle()
            {
                var p = this.presenter;
                p.Setup();

                p.Stop();

                A.CallTo(() => this.stopHandler.Handle(this.ui))
                    .MustHaveHappened();
            }
        }

        public class When_the_backspace_key_is_tapped : Context
        {
            [Fact]
            public void Calls_BackspaceKeyTappedHandler_Handle()
            {
                this.subscriber = new EventSubscriber();
                this.web.RegisterDependency(this.subscriber);
                this.presenter.Setup();

                this.ui.BackspaceKeyTapped += Raise.FreeForm<Do>.With();

                A.CallTo(() => this.bsKeyTappedHandler.Handle(
                        this.ui))
                    .MustHaveHappened();
            }
        }

        public class When_the_login_key_is_tapped : Context
        {
            [Fact]
            public void Calls_LoginKeyTappedHandler_Handle()
            {
                this.subscriber = new EventSubscriber();
                this.web.RegisterDependency(this.subscriber);
                this.presenter.Setup();

                this.ui.LoginKeyTapped += Raise.FreeForm<Do>.With();

                A.CallTo(() => this.loginKeyTappedHandler.Handle(
                        this.ui))
                    .MustHaveHappened();
            }
        }

        public class When_the_timer_elapses : Context
        {
            [Fact]
            public void Calls_TimerHandler_Handle()
            {
                this.subscriber = new EventSubscriber();
                this.web.RegisterDependency(this.subscriber);
                this.presenter.Setup();

                this.timer.Elapsed += Raise.FreeForm<Do>.With();

                A.CallTo(() => this.timerHandler.Handle(
                        this.ui))
                    .MustHaveHappened();
            }
        }

        public class When_the_access_level_changes : Context
        {
            [Fact]
            public void Calls_AccessLevelChangedHandler_Handle()
            {
                this.subscriber = new EventSubscriber();
                this.web.RegisterDependency(this.subscriber);
                this.presenter.Setup();
                var newLevel = this.fixture.Create<AccessLevel>();

                this.accessController.AccessLevelChanged
                    += Raise.FreeForm<Do<AccessLevel>>.With(newLevel);

                A.CallTo(() => this.accessChangedHandler.Handle(
                        this.ui, newLevel))
                    .MustHaveHappened();
            }
        }
    }
}
