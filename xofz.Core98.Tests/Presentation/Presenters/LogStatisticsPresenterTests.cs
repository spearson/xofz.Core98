namespace xofz.Tests.Presentation.Presenters
{
    using FakeItEasy;
    using Ploeh.AutoFixture;
    using xofz.Framework;
    using xofz.Framework.LogStatistics;
    using xofz.Presentation;
    using xofz.Presentation.Presenters;
    using xofz.UI;
    using xofz.UI.Log;
    using xofz.UI.LogStatistics;
    using Xunit;

    public class LogStatisticsPresenterTests
    {
        public class Context
        {
            protected Context()
            {
                this.ui = A.Fake<LogStatisticsUiV2>();
                this.web = new MethodWebV2();
                this.presenter = new LogStatisticsPresenter(
                    this.ui,
                    this.web);
                this.setupHandler = A.Fake<SetupHandler>();
                this.sub = new EventSubscriber();
                this.nav = A.Fake<Navigator>();
                this.fixture = new Fixture();
                this.name = this.fixture.Create<string>();
                this.presenter.Name = this.name;
                this.startHandler = A.Fake<StartHandler>();
                this.rangeHandler = A.Fake<RangeKeyTappedHandler>();
                this.overallHandler = A.Fake<OverallKeyTappedHandler>();
                this.resetContentHandler =
                    A.Fake<ResetContentKeyTappedHandler>();
                this.resetTypeHandler = A.Fake<ResetTypeKeyTappedHandler>();

                var w = this.web;
                w.RegisterDependency(
                    this.setupHandler);
                w.RegisterDependency(
                    this.sub);
                w.RegisterDependency(
                    this.nav);
                w.RegisterDependency(
                    this.startHandler);
                w.RegisterDependency(
                    this.rangeHandler);
                w.RegisterDependency(
                    this.overallHandler);
                w.RegisterDependency(
                    this.resetContentHandler);
                w.RegisterDependency(
                    this.resetTypeHandler);
            }

            protected readonly LogStatisticsUiV2 ui;
            protected readonly MethodWebV2 web;
            protected readonly LogStatisticsPresenter presenter;
            protected readonly Fixture fixture;
            protected readonly string name;
            protected readonly SetupHandler setupHandler;
            protected EventSubscriber sub;
            protected readonly Navigator nav;
            protected readonly StartHandler startHandler;
            protected readonly RangeKeyTappedHandler rangeHandler;
            protected readonly OverallKeyTappedHandler overallHandler;
            protected readonly ResetContentKeyTappedHandler resetContentHandler;
            protected readonly ResetTypeKeyTappedHandler resetTypeHandler;
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
                        this.ui,
                        this.name))
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

            [Fact]
            public void Subscribes_to_RangeKeyTapped()
            {
                this.presenter.Setup();

                A
                    .CallTo(() => this.sub.Subscribe(
                        this.ui,
                        nameof(this.ui.RangeKeyTapped),
                        A<Do>.Ignored))
                    .MustHaveHappened();
            }

            [Fact]
            public void Subscribes_to_OverallKeyTapped()
            {
                this.presenter.Setup();

                A
                    .CallTo(() => this.sub.Subscribe(
                        this.ui,
                        nameof(this.ui.OverallKeyTapped),
                        A<Do>.Ignored))
                    .MustHaveHappened();
            }

            [Fact]
            public void Subscribes_to_HideKeyTapped()
            {
                this.presenter.Setup();

                A
                    .CallTo(() => this.sub.Subscribe(
                        this.ui,
                        nameof(this.ui.HideKeyTapped),
                        A<Do>.Ignored))
                    .MustHaveHappened();
            }

            [Fact]
            public void Subscribes_to_reset_content_key_tapped()
            {
                this.presenter.Setup();

                A
                    .CallTo(() => this.sub.Subscribe(
                        this.ui,
                        nameof(this.ui.ResetContentKeyTapped),
                        A<Do>.Ignored))
                    .MustHaveHappened();
            }

            [Fact]
            public void Subscribes_to_reset_type_key_tapped()
            {
                this.presenter.Setup();

                A
                    .CallTo(() => this.sub.Subscribe(
                        this.ui,
                        nameof(this.ui.ResetTypeKeyTapped),
                        A<Do>.Ignored))
                    .MustHaveHappened();
            }
        }

        public class When_Start_is_called : Context
        {
            [Fact]
            public void Displays_the_ui()
            {
                this.presenter.Setup();
                this.presenter.Start();

                A
                    .CallTo(() => this.ui.Display())
                    .MustHaveHappened();
            }

            [Fact]
            public void Calls_StartHandler_Handle()
            {
                this.presenter.Setup();
                this.presenter.Start();

                A
                    .CallTo(() => this.startHandler.Handle(
                        this.ui,
                        A<Gen<LogUi>>.Ignored,
                        this.name))
                    .MustHaveHappened();
            }
        }

        public class When_the_range_key_is_tapped : Context
        {
            [Fact]
            public void Calls_RangeKeyTappedHandler_Handle()
            {
                this.presenter.Setup();

                this.ui.RangeKeyTapped += Raise.FreeForm.With();

                A
                    .CallTo(() => this.rangeHandler.Handle(
                        this.ui,
                        this.name))
                    .MustHaveHappened();
            }
        }

        public class When_the_overall_key_is_tapped : Context
        {
            [Fact]
            public void Calls_OverallKeyTappedHandler_Handle()
            {
                this.presenter.Setup();

                this.ui.OverallKeyTapped += Raise.FreeForm.With();

                A
                    .CallTo(() => this.overallHandler.Handle(
                        this.ui,
                        this.name))
                    .MustHaveHappened();
            }
        }

        public class When_the_reset_content_key_is_tapped : Context
        {
            [Fact]
            public void Calls_ResetContentKeyTappedHandler_Handle()
            {
                this.presenter.Setup();

                this.ui.ResetContentKeyTapped += Raise.FreeForm.With();

                A
                    .CallTo(() => this.resetContentHandler.Handle(
                        this.ui,
                        this.name))
                    .MustHaveHappened();
            }
        }

        public class When_the_reset_type_key_is_tapped : Context
        {
            [Fact]
            public void Calls_ResetTypeKeyTappedHandler_Handle()
            {
                this.presenter.Setup();

                this.ui.ResetTypeKeyTapped += Raise.FreeForm.With();

                A
                    .CallTo(() => this.resetTypeHandler.Handle(
                        this.ui,
                        this.name))
                    .MustHaveHappened();
            }
        }
    }
}
