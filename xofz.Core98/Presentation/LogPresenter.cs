namespace xofz.Presentation
{
    using System.Threading;
    using Framework;
    using UI;
    using xofz.Framework.Log;
    using xofz.Framework.Logging;

    public sealed class LogPresenter
        : NamedPresenter
    {
        public LogPresenter(
            LogUi ui,
            ShellUi shell,
            MethodRunner runner)
            : base(ui, shell)
        {
            this.ui = ui;
            this.runner = runner;
        }

        public void Setup()
        {
            if (Interlocked.Exchange(
                ref this.setupIf1,
                1) == 1)
            {
                return;
            }

            var r = this.runner;
            var name = this.Name;
            r.Run<SetupHandler>(handler =>
            {
                handler.Handle(this.ui, name);
            });

            r.Run<EventSubscriber>(sub =>
            {
                sub.Subscribe(
                    this.ui,
                    nameof(this.ui.DateRangeChanged),
                    this.ui_DateRangeChanged);
                sub.Subscribe(
                    this.ui,
                    nameof(this.ui.AddKeyTapped),
                    this.ui_AddKeyTapped);
                sub.Subscribe(
                    this.ui,
                    nameof(this.ui.ClearKeyTapped),
                    this.ui_ClearKeyTapped);
                sub.Subscribe(
                    this.ui,
                    nameof(this.ui.StatisticsKeyTapped),
                    this.ui_StatisticsKeyTapped);
                sub.Subscribe(
                    this.ui,
                    nameof(this.ui.FilterTextChanged),
                    this.ui_FilterTextChanged);
                r.Run<Log>(l =>
                        sub.Subscribe<LogEntry>(
                            l,
                            nameof(l.EntryWritten),
                            this.log_EntryWritten),
                    name);
                r.Run<AccessController>(ac =>
                    sub.Subscribe<AccessLevel>(
                        ac,
                        nameof(ac.AccessLevelChanged),
                        this.accessLevelChanged));

                if (this.ui is LogUiV3 v3)
                {
                    sub.Subscribe(
                        v3,
                        nameof(v3.PreviousWeekKeyTapped),
                        this.v3_PreviousWeekKeyTapped);
                    sub.Subscribe(
                        v3,
                        nameof(v3.CurrentWeekKeyTapped),
                        this.v3_CurrentWeekKeyTapped);
                    sub.Subscribe(
                        v3,
                        nameof(v3.NextWeekKeyTapped),
                        this.v3_NextWeekKeyTapped);
                    sub.Subscribe(
                        v3,
                        nameof(v3.DownKeyTapped),
                        this.v3_DownKeyTapped);
                    sub.Subscribe(
                        v3,
                        nameof(v3.UpKeyTapped),
                        this.v3_UpKeyTapped);
                    sub.Subscribe(
                        v3,
                        nameof(v3.ResetContentKeyTapped),
                        this.v3_ResetContentKeyTapped);
                    sub.Subscribe(
                        v3,
                        nameof(v3.ResetTypeKeyTapped),
                        this.v3_ResetTypeKeyTapped);
                }
            });

            r.Run<Navigator>(nav =>
                nav.RegisterPresenter(this));
        }

        public override void Start()
        {
            if (Interlocked.Read(ref this.setupIf1) != 1)
            {
                return;
            }

            base.Start();

            var r = this.runner;
            r.Run<EventSubscriber>(sub =>
            {
                Do unsubscribe = () =>
                {
                    sub.Unsubscribe(
                        this.ui,
                        nameof(this.ui.DateRangeChanged),
                        this.ui_DateRangeChanged);
                    sub.Unsubscribe(
                        this.ui,
                        nameof(this.ui.FilterTextChanged),
                        this.ui_FilterTextChanged);
                };
                Do subscribe = () =>
                {
                    sub.Subscribe(
                        this.ui,
                        nameof(this.ui.DateRangeChanged),
                        this.ui_DateRangeChanged);
                    sub.Subscribe(
                        this.ui,
                        nameof(this.ui.FilterTextChanged),
                        this.ui_FilterTextChanged);
                };

                r.Run<StartHandler>(handler =>
                {
                    handler.Handle(
                        this.ui,
                        unsubscribe,
                        subscribe,
                        this.Name);
                });
            });
        }

        public override void Stop()
        {
            var r = this.runner;
            r.Run<StopHandler>(handler =>
            {
                handler.Handle(this.ui, this.Name);
            });
        }

        private void ui_DateRangeChanged()
        {
            var r = this.runner;
            r.Run<DateRangeChangedHandler>(handler =>
            {
                handler.Handle(this.ui, this.Name);
            });
        }

        private void ui_AddKeyTapped()
        {
            var r = this.runner;
            r.Run<Navigator>(
                nav =>
                {
                    Do<string> presentEditor =
                        nav.PresentFluidly<LogEditorPresenter>;

                    r.Run<AddKeyTappedHandler>(handler =>
                    {
                        handler.Handle(
                            presentEditor,
                            this.ui,
                            this.Name);
                    });
                });
        }

        private void ui_ClearKeyTapped()
        {
            var r = this.runner;
            r.Run<ClearKeyTappedHandler>(handler =>
            {
                handler.Handle(
                    this.ui,
                    this.Name);
            });
        }

        private void ui_StatisticsKeyTapped()
        {
            var r = this.runner;
            r.Run<Navigator>(nav =>
            {
                Do<string> presentStats =
                    nav.PresentFluidly<LogStatisticsPresenter>;

                r.Run<StatisticsKeyTappedHandler>(handler =>
                {
                    handler.Handle(
                        presentStats,
                        this.ui,
                        this.Name);
                });
            });
        }

        private void ui_FilterTextChanged()
        {
            var r = this.runner;
            r.Run<FilterTextChangedHandler>(handler =>
            {
                handler.Handle(
                    this.ui,
                    this.Name);
            });
        }

        private void log_EntryWritten(
            LogEntry e)
        {
            var r = this.runner;
            r.Run<EntryWrittenHandler>(handler =>
            {
                handler.Handle(
                    this.ui,
                    this.Name,
                    e);
            });
        }

        private void accessLevelChanged(
            AccessLevel newAccessLevel)
        {
            var r = this.runner;
            r.Run<AccessLevelChangedHandler>(handler =>
            {
                handler.Handle(
                    this.ui,
                    newAccessLevel,
                    this.Name);
            });
        }

        private void v3_PreviousWeekKeyTapped()
        {
            var r = this.runner;
            r.Run<EventSubscriber>(sub =>
            {
                Do unsubscribe = () =>
                {
                    sub.Unsubscribe(
                        this.ui,
                        nameof(this.ui.DateRangeChanged),
                        this.ui_DateRangeChanged);
                };

                Do subscribe = () =>
                {
                    sub.Subscribe(
                        this.ui,
                        nameof(this.ui.DateRangeChanged),
                        this.ui_DateRangeChanged);
                };

                r.Run<PreviousWeekKeyTappedHandler>(handler =>
                {
                    handler.Handle(
                        this.ui as LogUiV3,
                        this.Name,
                        unsubscribe,
                        subscribe);
                });
            });
        }

        private void v3_CurrentWeekKeyTapped()
        {
            var r = this.runner;
            r.Run<EventSubscriber>(sub =>
            {
                Do unsubscribe = () =>
                {
                    sub.Unsubscribe(
                        this.ui,
                        nameof(this.ui.DateRangeChanged),
                        this.ui_DateRangeChanged);
                };

                Do subscribe = () =>
                {
                    sub.Subscribe(
                        this.ui,
                        nameof(this.ui.DateRangeChanged),
                        this.ui_DateRangeChanged);
                };

                r.Run<CurrentWeekKeyTappedHandler>(handler =>
                {
                    handler.Handle(
                        this.ui as LogUiV3,
                        this.Name,
                        unsubscribe,
                        subscribe);
                });
            });
        }

        private void v3_NextWeekKeyTapped()
        {
            var r = this.runner;
            r.Run<EventSubscriber>(sub =>
            {
                Do unsubscribe = () =>
                {
                    sub.Unsubscribe(
                        this.ui,
                        nameof(this.ui.DateRangeChanged),
                        this.ui_DateRangeChanged);
                };

                Do subscribe = () =>
                {
                    sub.Subscribe(
                        this.ui,
                        nameof(this.ui.DateRangeChanged),
                        this.ui_DateRangeChanged);
                };

                r.Run<NextWeekKeyTappedHandler>(handler =>
                {
                    handler.Handle(
                        this.ui as LogUiV3,
                        this.Name,
                        unsubscribe,
                        subscribe);
                });
            });
        }

        private void v3_DownKeyTapped()
        {
            var r = this.runner;
            r.Run<DownKeyTappedHandler>(handler =>
            {
                handler.Handle(
                    this.ui as LogUiV3,
                    this.Name);
            });
        }

        private void v3_UpKeyTapped()
        {
            var r = this.runner;
            r.Run<UpKeyTappedHandler>(handler =>
            {
                handler.Handle(
                    this.ui as LogUiV3,
                    this.Name);
            });
        }

        private void v3_ResetContentKeyTapped()
        {
            var r = this.runner;
            r.Run<ResetContentKeyTappedHandler>(handler =>
            {
                handler.Handle(
                    this.ui as LogUiV3,
                    this.Name);
            });
        }

        private void v3_ResetTypeKeyTapped()
        {
            var r = this.runner;
            r.Run<ResetTypeKeyTappedHandler>(handler =>
            {
                handler.Handle(
                    this.ui as LogUiV3,
                    this.Name);
            });
        }

        private long setupIf1;
        private readonly LogUi ui;
        private readonly MethodRunner runner;
    }
}
