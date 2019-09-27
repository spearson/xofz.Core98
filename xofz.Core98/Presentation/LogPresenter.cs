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
            if (Interlocked.CompareExchange(
                    ref this.setupIf1, 
                    1, 
                    0) == 1)
            {
                return;
            }

            var r = this.runner;
            var name = this.Name;
            r.Run<SetupHandler>(handler =>
            {
                handler.Handle(this.ui, name);
            });

            r.Run<EventSubscriber>(subscriber =>
            {
                subscriber.Subscribe(
                    this.ui,
                    nameof(this.ui.DateRangeChanged),
                    this.ui_DateRangeChanged);
                subscriber.Subscribe(
                    this.ui,
                    nameof(this.ui.AddKeyTapped),
                    this.ui_AddKeyTapped);
                subscriber.Subscribe(
                    this.ui,
                    nameof(this.ui.ClearKeyTapped),
                    this.ui_ClearKeyTapped);
                subscriber.Subscribe(
                    this.ui,
                    nameof(this.ui.StatisticsKeyTapped),
                    this.ui_StatisticsKeyTapped);
                subscriber.Subscribe(
                    this.ui,
                    nameof(this.ui.FilterTextChanged),
                    this.ui_FilterTextChanged);
                r.Run<Log>(l =>
                        subscriber.Subscribe<LogEntry>(
                            l,
                            nameof(l.EntryWritten),
                            this.log_EntryWritten),
                    name);
                r.Run<AccessController>(ac =>
                    subscriber.Subscribe<AccessLevel>(
                        ac,
                        nameof(ac.AccessLevelChanged),
                        this.accessLevelChanged));
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
            Do unsubscribe = null;
            Do subscribe = null;
            r.Run<EventSubscriber>(sub =>
            {
                unsubscribe = () =>
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
                subscribe = () =>
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
            });
            r.Run<StartHandler>(handler =>
            {
                handler.Handle(
                    this.ui, 
                    unsubscribe,
                    subscribe,
                    this.Name);
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
            Do presentEditor = null;
            r.Run<Navigator>(
                nav =>
                {
                    presentEditor = () =>
                    {
                        nav.PresentFluidly<LogEditorPresenter>(
                            this.Name);
                    };
                });

            r.Run<AddKeyTappedHandler>(handler =>
            {
                handler.Handle(presentEditor);
            });
        }

        private void ui_ClearKeyTapped()
        {
            var r = this.runner;
            r.Run<ClearKeyTappedHandler>(handler =>
            {
                handler.Handle(this.ui, this.Name);
            });
        }

        private void ui_StatisticsKeyTapped()
        {
            var r = this.runner;
            Do presentStats = () =>
            {
                r.Run<Navigator>(
                    nav =>
                    {
                        nav.PresentFluidly<LogStatisticsPresenter>(
                            this.Name);
                    });
            };

            r.Run<StatisticsKeyTappedHandler>(handler =>
            {
                handler.Handle(presentStats);
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

        private void log_EntryWritten(LogEntry e)
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

        private void accessLevelChanged(AccessLevel newAccessLevel)
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

        private long setupIf1;
        private readonly LogUi ui;
        private readonly MethodRunner runner;
    }
}
