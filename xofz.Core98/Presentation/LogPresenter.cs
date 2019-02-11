namespace xofz.Presentation
{
    using System.Threading;
    using Framework;
    using UI;
    using xofz.Framework.Log;
    using xofz.Framework.Logging;

    public sealed class LogPresenter : NamedPresenter
    {
        public LogPresenter(
            LogUi ui,
            ShellUi shell,
            MethodWeb web)
            : base(ui, shell)
        {
            this.ui = ui;
            this.web = web;
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

            var w = this.web;
            w.Run<SetupHandler>(handler =>
            {
                handler.Handle(this.ui, this.Name);
            });

            w.Run<EventSubscriber>(subscriber =>
            {
                subscriber.Subscribe(
                    this.ui,
                    nameof(this.ui.DateRangeChanged),
                    this.ui_DateChanged);
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
                w.Run<Log>(l =>
                        subscriber.Subscribe<LogEntry>(
                            l,
                            nameof(l.EntryWritten),
                            this.log_EntryWritten),
                    this.Name);
                w.Run<AccessController>(ac =>
                    subscriber.Subscribe<AccessLevel>(
                        ac,
                        nameof(ac.AccessLevelChanged),
                        this.accessLevelChanged));
            });
            
            w.Run<Navigator>(nav => nav.RegisterPresenter(this));
        }

        public override void Start()
        {
            if (Interlocked.Read(ref this.setupIf1) != 1)
            {
                return;
            }

            base.Start();

            var w = this.web;
            Do unsubscribe = null;
            Do subscribe = null;
            w.Run<EventSubscriber>(sub =>
            {
                unsubscribe = () =>
                {
                    sub.Unsubscribe(
                        this.ui,
                        nameof(this.ui.DateRangeChanged),
                        this.ui_DateChanged);
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
                        this.ui_DateChanged);
                    sub.Subscribe(
                        this.ui,
                        nameof(this.ui.FilterTextChanged),
                        this.ui_FilterTextChanged);
                };
            });
            w.Run<StartHandler>(handler =>
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
            var w = this.web;
            w.Run<StopHandler>(handler =>
            {
                handler.Handle(this.ui, this.Name);
            });
        }

        private void ui_DateChanged()
        {
            var w = this.web;
            w.Run<DateChangedHandler>(handler =>
            {
                handler.Handle(this.ui, this.Name);
            });
        }

        private void ui_AddKeyTapped()
        {
            var w = this.web;
            Do presentEditor = null;
            w.Run<Navigator>(
                n =>
                {
                    presentEditor = () => n.PresentFluidly<LogEditorPresenter>(
                        this.Name);
                });

            w.Run<AddKeyTappedHandler>(handler =>
            {
                handler.Handle(presentEditor);
            });
        }

        private void ui_ClearKeyTapped()
        {
            var w = this.web;
            w.Run<ClearKeyTappedHandler>(handler =>
            {
                handler.Handle(this.ui, this.Name);
            });
        }

        private void ui_StatisticsKeyTapped()
        {
            var w = this.web;
            Do presentStats = () =>
            {
                w.Run<Navigator>(
                    n =>
                    {
                        n.PresentFluidly<LogStatisticsPresenter>(
                            this.Name);
                    });
            };

            w.Run<StatisticsKeyTappedHandler>(handler =>
            {
                handler.Handle(presentStats);
            });
        }

        private void ui_FilterTextChanged()
        {
            var w = this.web;
            w.Run<FilterTextChangedHandler>(handler =>
            {
                handler.Handle(this.ui, this.Name);
            });
        }

        private void log_EntryWritten(LogEntry e)
        {
            var w = this.web;
            w.Run<EntryWrittenHandler>(handler =>
            {
                handler.Handle(this.ui, this.Name, e);
            });
        }

        private void accessLevelChanged(AccessLevel newAccessLevel)
        {
            var w = this.web;
            w.Run<AccessLevelChangedHandler>(handler =>
            {
                handler.Handle(
                    this.ui,
                    newAccessLevel,
                    this.Name);
            });
        }

        private long setupIf1;
        private readonly LogUi ui;
        private readonly MethodWeb web;
    }
}
