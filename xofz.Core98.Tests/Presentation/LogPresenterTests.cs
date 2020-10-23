namespace xofz.Tests.Presentation
{
    using FakeItEasy;
    using Ploeh.AutoFixture;
    using xofz.Framework;
    using xofz.Framework.Log;
    using xofz.Framework.Logging;
    using xofz.Presentation;
    using xofz.UI;
    using Xunit;

    public class LogPresenterTests
    {
        public class Context
        {
            protected Context()
            {
                this.web = new MethodWebV2();
                this.ui = A.Fake<LogUiV3>();
                this.shell = A.Fake<ShellUi>();
                this.fixture = new Fixture();
                this.presenter = new LogPresenter(
                    this.ui,
                    this.shell,
                    this.web)
                {
                    Name = this.fixture.Create<string>()
                };
                this.nav = A.Fake<Navigator>();

                this.setupHandler = A.Fake<SetupHandler>();
                this.sub = new EventSubscriber();
                this.accessController = A.Fake<AccessController>();
                this.startHandler = A.Fake<StartHandler>();
                this.stopHandler = A.Fake<StopHandler>();
                this.dateChangedHandler = A.Fake<DateRangeChangedHandler>();
                this.addHandler = A.Fake<AddKeyTappedHandler>();
                this.clearHandler = A.Fake<ClearKeyTappedHandler>();
                this.statsHandler = A.Fake<StatisticsKeyTappedHandler>();
                this.filterHandler = A.Fake<FilterTextChangedHandler>();
                this.writeHandler = A.Fake<EntryWrittenHandler>();
                this.accessHandler = A.Fake<AccessLevelChangedHandler>();
                this.previousHandler = A.Fake<PreviousWeekKeyTappedHandler>();
                this.currentHandler = A.Fake<CurrentWeekKeyTappedHandler>();
                this.nextHandler = A.Fake<NextWeekKeyTappedHandler>();
                this.downHandler = A.Fake<DownKeyTappedHandler>();
                this.upHandler = A.Fake<UpKeyTappedHandler>();
                this.resetContentHandler =
                    A.Fake<ResetContentKeyTappedHandler>();
                this.resetTypeHandler = A.Fake<ResetTypeKeyTappedHandler>();
                this.log = A.Fake<Log>();

                var w = this.web;
                w.RegisterDependency(
                    this.nav);
                w.RegisterDependency(
                    this.setupHandler);
                w.RegisterDependency(
                    this.sub);
                w.RegisterDependency(
                    this.accessController);
                w.RegisterDependency(
                    this.startHandler);
                w.RegisterDependency(
                    this.stopHandler);
                w.RegisterDependency(
                    this.dateChangedHandler);
                w.RegisterDependency(
                    this.addHandler);
                w.RegisterDependency(
                    this.clearHandler);
                w.RegisterDependency(
                    this.statsHandler);
                w.RegisterDependency(
                    this.filterHandler);
                w.RegisterDependency(
                    this.writeHandler);
                w.RegisterDependency(
                    this.accessHandler);
                w.RegisterDependency(
                    this.previousHandler);
                w.RegisterDependency(
                    this.currentHandler);
                w.RegisterDependency(
                    this.nextHandler);
                w.RegisterDependency(
                    this.downHandler);
                w.RegisterDependency(
                    this.upHandler);
                w.RegisterDependency(
                    this.resetContentHandler);
                w.RegisterDependency(
                    this.resetTypeHandler);
                w.RegisterDependency(
                    this.log,
                    this.presenter.Name);
            }

            protected readonly MethodWebV2 web;
            protected readonly LogPresenter presenter;
            protected readonly Navigator nav;
            protected readonly LogUiV3 ui;
            protected readonly ShellUi shell;
            protected readonly SetupHandler setupHandler;
            protected EventSubscriber sub;
            protected readonly Fixture fixture;
            protected readonly AccessController accessController;
            protected readonly StartHandler startHandler;
            protected readonly StopHandler stopHandler;
            protected readonly DateRangeChangedHandler dateChangedHandler;
            protected readonly AddKeyTappedHandler addHandler;
            protected readonly ClearKeyTappedHandler clearHandler;
            protected readonly StatisticsKeyTappedHandler statsHandler;
            protected readonly FilterTextChangedHandler filterHandler;
            protected readonly EntryWrittenHandler writeHandler;
            protected readonly AccessLevelChangedHandler accessHandler;
            protected readonly PreviousWeekKeyTappedHandler previousHandler;
            protected readonly CurrentWeekKeyTappedHandler currentHandler;
            protected readonly NextWeekKeyTappedHandler nextHandler;
            protected readonly DownKeyTappedHandler downHandler;
            protected readonly UpKeyTappedHandler upHandler;
            protected readonly ResetContentKeyTappedHandler resetContentHandler;
            protected readonly ResetTypeKeyTappedHandler resetTypeHandler;
            protected readonly Log log;
        }

        public class When_Setup_is_called : Context
        {
            public When_Setup_is_called()
            {
                var w = this.web;
                w.Unregister<EventSubscriber>();
                this.sub = A.Fake<EventSubscriber>();
                w.RegisterDependency(this.sub);
            }

            [Fact]
            public void Calls_SetupHandler_Handle()
            {
                this.presenter.Setup();

                A
                    .CallTo(() => this.setupHandler.Handle(
                        this.ui,
                        this.presenter.Name))
                    .MustHaveHappened();
            }

            [Fact]
            public void Subscribes_to_DateRangeChanged()
            {
                this.presenter.Setup();

                A
                    .CallTo(() => this.sub.Subscribe(
                        this.ui,
                        nameof(this.ui.DateRangeChanged),
                        A<Do>.Ignored))
                    .MustHaveHappened();
            }

            [Fact]
            public void Subscribes_to_AddKeyTapped()
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
            public void Subscribes_to_ClearKeyTapped()
            {
                this.presenter.Setup();

                A
                    .CallTo(() => this.sub.Subscribe(
                        this.ui,
                        nameof(this.ui.ClearKeyTapped),
                        A<Do>.Ignored))
                    .MustHaveHappened();
            }

            [Fact]
            public void Subscribes_to_StatisticsKeyTapped()
            {
                this.presenter.Setup();

                A
                    .CallTo(() => this.sub.Subscribe(
                        this.ui,
                        nameof(this.ui.StatisticsKeyTapped),
                        A<Do>.Ignored))
                    .MustHaveHappened();
            }

            [Fact]
            public void Subscribes_to_FilterTextChanged()
            {
                this.presenter.Setup();

                A
                    .CallTo(() => this.sub.Subscribe(
                        this.ui,
                        nameof(this.ui.FilterTextChanged),
                        A<Do>.Ignored))
                    .MustHaveHappened();
            }

            [Fact]
            public void Subscribes_to_log_EntryWritten()
            {
                this.presenter.Setup();

                A
                    .CallTo(() => this.sub.Subscribe(
                        this.log,
                        nameof(this.log.EntryWritten),
                        A<Do<LogEntry>>.Ignored))
                    .MustHaveHappened();
            }

            [Fact]
            public void Subscribes_to_access_controller_AccessLevelChanged()
            {
                this.presenter.Setup();

                A
                    .CallTo(() => this.sub.Subscribe(
                        this.accessController,
                        nameof(this.accessController.AccessLevelChanged),
                        A<Do<AccessLevel>>.Ignored))
                    .MustHaveHappened();
            }

            [Fact]
            public void Subscribes_to_ui_PreviousWeekKeyTapped()
            {
                this.presenter.Setup();

                A
                    .CallTo(() => this.sub.Subscribe(
                        this.ui,
                        nameof(this.ui.PreviousWeekKeyTapped),
                        A<Do>.Ignored))
                    .MustHaveHappened();
            }

            [Fact]
            public void Subscribes_to_ui_CurrentWeekKeyTapped()
            {
                this.presenter.Setup();

                A
                    .CallTo(() => this.sub.Subscribe(
                        this.ui,
                        nameof(this.ui.CurrentWeekKeyTapped),
                        A<Do>.Ignored))
                    .MustHaveHappened();
            }

            [Fact]
            public void Subscribes_to_NextWeekKeyTapped()
            {
                this.presenter.Setup();

                A
                    .CallTo(() => this.sub.Subscribe(
                        this.ui,
                        nameof(this.ui.NextWeekKeyTapped),
                        A<Do>.Ignored))
                    .MustHaveHappened();
            }

            [Fact]
            public void Subscribes_to_DownKeyTapped()
            {
                this.presenter.Setup();

                A
                    .CallTo(() => this.sub.Subscribe(
                        this.ui,
                        nameof(this.ui.DownKeyTapped),
                        A<Do>.Ignored))
                    .MustHaveHappened();
            }

            [Fact]
            public void Subscribes_to_UpKeyTapped()
            {
                this.presenter.Setup();

                A
                    .CallTo(() => this.sub.Subscribe(
                        this.ui,
                        nameof(this.ui.UpKeyTapped),
                        A<Do>.Ignored))
                    .MustHaveHappened();
            }

            [Fact]
            public void Subscribes_to_ResetContentKeyTapped()
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
            public void Subscribes_to_ResetTypeKeyTapped()
            {
                this.presenter.Setup();

                A
                    .CallTo(() => this.sub.Subscribe(
                        this.ui,
                        nameof(this.ui.ResetTypeKeyTapped),
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

        public class When_Start_is_called : Context
        {
            [Fact]
            public void Calls_startHandler_Handle()
            {
                this.presenter.Setup();

                this.presenter.Start();

                A
                    .CallTo(() => this.startHandler.Handle(
                        this.ui,
                        A<Do>.Ignored,
                        A<Do>.Ignored,
                        this.presenter.Name))
                    .MustHaveHappened();
            }
        }

        public class When_Stop_is_called : Context
        {
            [Fact]
            public void Calls_StopHandler_Handle()
            {
                this.presenter.Setup();

                this.presenter.Stop();

                A
                    .CallTo(() => this.stopHandler.Handle(
                        this.ui,
                        this.presenter.Name))
                    .MustHaveHappened();
            }
        }

        public class When_the_date_range_is_changed : Context
        {
            [Fact]
            public void Calls_DateRangeChangedHandler_Handle()
            {
                this.presenter.Setup();

                this.ui.DateRangeChanged += Raise.FreeForm.With();

                A
                    .CallTo(() => this.dateChangedHandler.Handle(
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
                        A<Do<string>>.Ignored, 
                        this.ui, 
                        this.presenter.Name))
                    .MustHaveHappened();
            }
        }

        public class When_the_clear_key_is_tapped : Context
        {
            [Fact]
            public void Calls_ClearKeyTappedHandler_Handle()
            {
                this.presenter.Setup();

                this.ui.ClearKeyTapped += Raise.FreeForm.With();

                A
                    .CallTo(() => this.clearHandler.Handle(
                        this.ui,
                        this.presenter.Name))
                    .MustHaveHappened();
            }
        }

        public class When_the_statistics_key_is_tapped : Context
        {
            [Fact]
            public void Calls_StatisticsKeyTappedHandler_Handle()
            {
                this.presenter.Setup();

                this.ui.StatisticsKeyTapped += Raise.FreeForm.With();

                A
                    .CallTo(() => this.statsHandler.Handle(
                        A<Do<string>>.Ignored,
                        this.ui,
                        this.presenter.Name))
                    .MustHaveHappened();
            }
        }

        public class When_the_filter_text_changes : Context
        {
            [Fact]
            public void Calls_FilterTextChangedHandler_Handle()
            {
                this.presenter.Setup();

                this.ui.FilterTextChanged += Raise.FreeForm.With();

                A
                    .CallTo(() => this.filterHandler.Handle(
                        this.ui,
                        this.presenter.Name))
                    .MustHaveHappened();
            }
        }

        public class When_an_entry_is_written : Context
        {
            [Fact]
            public void Calls_EntryWrittenHandler_Handle()
            {
                this.presenter.Setup();
                var e = new LogEntry(
                    this.fixture.Create<string>(),
                    new[]
                    {
                        this.fixture.Create<string>()
                    });

                this.log.EntryWritten += Raise.FreeForm.With(e);

                A
                    .CallTo(() => this.writeHandler.Handle(
                        this.ui,
                        this.presenter.Name,
                        e))
                    .MustHaveHappened();
            }
        }

        public class When_the_access_level_changes : Context
        {
            [Fact]
            public void Calls_AccessLevelChangedHandler_Handle()
            {
                this.presenter.Setup();
                var newLevel = this.fixture.Create<AccessLevel>();

                this.accessController.AccessLevelChanged +=
                    Raise.FreeForm.With(newLevel);

                A
                    .CallTo(() => this.accessHandler.Handle(
                        this.ui,
                        newLevel,
                        this.presenter.Name))
                    .MustHaveHappened();
            }
        }

        public class When_the_previous_week_key_is_tapped : Context
        {
            [Fact]
            public void Calls_PreviousWeekKeyTappedHandler_Handle()
            {
                this.presenter.Setup();

                this.ui.PreviousWeekKeyTapped += Raise.FreeForm.With();

                A
                    .CallTo(() => this.previousHandler.Handle(
                        this.ui,
                        this.presenter.Name,
                        A<Do>.Ignored,
                        A<Do>.Ignored))
                    .MustHaveHappened();
            }
        }

        public class When_the_current_week_key_is_tapped : Context
        {
            [Fact]
            public void Calls_CurrentWeekKeyTappedHandler_Handle()
            {
                this.presenter.Setup();

                this.ui.CurrentWeekKeyTapped += Raise.FreeForm.With();

                A
                    .CallTo(() => this.currentHandler.Handle(
                        this.ui,
                        this.presenter.Name,
                        A<Do>.Ignored,
                        A<Do>.Ignored))
                    .MustHaveHappened();
            }
        }

        public class When_the_next_week_key_is_tapped : Context
        {
            [Fact]
            public void Calls_NextWeekKeyTappedHandler_Handle()
            {
                this.presenter.Setup();

                this.ui.NextWeekKeyTapped += Raise.FreeForm.With();

                A
                    .CallTo(() => this.nextHandler.Handle(
                        this.ui,
                        this.presenter.Name,
                        A<Do>.Ignored,
                        A<Do>.Ignored))
                    .MustHaveHappened();
            }
        }

        public class When_the_down_key_is_tapped : Context
        {
            [Fact]
            public void Calls_DownKeyTappedHandler_Handle()
            {
                this.presenter.Setup();

                this.ui.DownKeyTapped += Raise.FreeForm.With();

                A
                    .CallTo(() => this.downHandler.Handle(
                        this.ui,
                        this.presenter.Name))
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
                        this.presenter.Name))
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
                        this.presenter.Name))
                    .MustHaveHappened();
            }
        }
    }
}
