namespace xofz.Tests.Presentation.Presenters
{
    using FakeItEasy;
    using Ploeh.AutoFixture;
    using xofz.Framework;
    using xofz.Framework.LogEditor;
    using xofz.Presentation;
    using xofz.Presentation.Presenters;
    using xofz.UI;
    using Xunit;

    public class LogEditorPresenterTests
    {
        public class Context
        {
            protected Context()
            {
                this.ui = A.Fake<LogEditorUi>();
                this.web = new MethodWebV2();

                this.presenter = new LogEditorPresenter(
                    this.ui,
                    this.web);
                this.setupHandler = A.Fake<SetupHandler>();
                this.sub = new EventSubscriber();
                this.nav = A.Fake<Navigator>();
                this.typeHandler = A.Fake<TypeChangedHandler>();
                this.addHandler = A.Fake<AddKeyTappedHandler>();
                this.fixture = new Fixture();
                this.name = this.fixture.Create<string>();
                this.presenter.Name = this.name;

                var w = this.web;
                w.RegisterDependency(
                    this.setupHandler);
                w.RegisterDependency(
                    this.sub);
                w.RegisterDependency(
                    this.nav);
                w.RegisterDependency(
                    this.typeHandler);
                w.RegisterDependency(
                    this.addHandler);
            }

            protected readonly LogEditorUi ui;
            protected readonly MethodWebV2 web;
            protected readonly LogEditorPresenter presenter;
            protected readonly SetupHandler setupHandler;
            protected readonly Navigator nav;
            protected EventSubscriber sub;
            protected readonly TypeChangedHandler typeHandler;
            protected readonly AddKeyTappedHandler addHandler;
            protected readonly Fixture fixture;
            protected readonly string name;
        }

        public class When_Setup_is_called : Context
        {
            public When_Setup_is_called()
            {
                this.sub = A.Fake<EventSubscriber>();
                var w = this.web;
                w.Unregister<EventSubscriber>();
                w.RegisterDependency(
                    this.sub);
            }

            [Fact]
            public void Calls_SetupHandler_Handle()
            {
                this.presenter.Setup();

                A
                    .CallTo(() => this.setupHandler.Handle(
                        this.ui))
                    .MustHaveHappened();
            }

            [Fact]
            public void Subscribes_to_ui_TypeChanged()
            {
                this.presenter.Setup();

                A
                    .CallTo(() => this.sub.Subscribe(
                        this.ui,
                        nameof(this.ui.TypeChanged),
                        A<Do>.Ignored))
                    .MustHaveHappened();
            }

            [Fact]
            public void Subscribes_to_ui_AddKeyTapped()
            {
                this.presenter.Setup();

                A
                    .CallTo(() => this.sub.Subscribe(
                        this.ui,
                        nameof(this.ui.AddKeyTapped),
                        A<Do>.Ignored))
                    .MustHaveHappened();
            }

            [Fact]
            public void Registers_itself_with_the_navigator()
            {
                this.presenter.Setup();

                A
                    .CallTo(() => this.nav.RegisterPresenter(
                        this.presenter))
                    .MustHaveHappened();
            }
        }

        public class When_the_type_is_changed : Context
        {
            [Fact]
            public void Calls_TypeChangedHandler_Handle()
            {
                this.presenter.Setup();

                this.ui.TypeChanged += Raise.FreeForm.With();

                A
                    .CallTo(() => this.typeHandler.Handle(
                        this.ui,
                        this.presenter.Name))
                    .MustHaveHappened();
            }
        }

        public class When_the_add_key_is_tapped : Context
        {
            [Fact]
            public void Calls_AddKeyTappedHandler_Handle()
            {
                this.presenter.Setup();

                this.ui.AddKeyTapped += Raise.FreeForm.With();

                A
                    .CallTo(() => this.addHandler.Handle(
                        this.ui,
                        this.name))
                    .MustHaveHappened();
            }
        }
    }
}
