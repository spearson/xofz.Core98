namespace xofz.Presentation
{
    using System.Threading;
    using xofz.Framework;
    using xofz.Framework.Logging;
    using xofz.Framework.LogStatistics;
    using xofz.UI;

    public sealed class LogStatisticsPresenter : PopupNamedPresenter
    {
        public LogStatisticsPresenter(
            LogStatisticsUi ui,
            MethodWeb web)
            : base(ui)
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
                handler.Handle(ui, this.Name);
            });

            w.Run<EventSubscriber>(subscriber =>
            {
                subscriber.Subscribe(
                    this.ui,
                    nameof(this.ui.RangeKeyTapped),
                    this.ui_RangeKeyTapped);
                subscriber.Subscribe(
                    this.ui,
                    nameof(this.ui.OverallKeyTapped),
                    this.ui_OverallKeyTapped);
                subscriber.Subscribe(
                    this.ui,
                    nameof(this.ui.HideKeyTapped),
                    this.Stop);
                subscriber.Subscribe(
                    this.ui,
                    nameof(this.ui.ResetContentKeyTapped),
                    this.ui_ResetContentKeyTapped);
                subscriber.Subscribe(
                    this.ui,
                    nameof(this.ui.ResetTypeKeyTapped),
                    this.ui_ResetTypeKeyTapped);
            });
            
            w.Run<Navigator>(n => n.RegisterPresenter(this));
        }

        public override void Start()
        {
            if (Interlocked.Read(ref this.setupIf1) != 1)
            {
                return;
            }

            base.Start();
            var w = this.web;
            Gen<LogUi> readLogUi = null;
            var name = this.Name;
            w.Run<Navigator>(
                n =>
                {
                    readLogUi = () => n.GetUi<LogPresenter, LogUi>(
                        name);
                });
            w.Run<StartHandler>(handler =>
            {
                handler.Handle(
                    this.ui,
                    readLogUi,
                    name);
            });
        }

        private void ui_RangeKeyTapped()
        {
            var w = this.web;
            w.Run<RangeKeyTappedHandler>(handler =>
            {
                handler.Handle(this.ui, this.Name);
            });
        }

        private void ui_OverallKeyTapped()
        {
            var w = this.web;
            w.Run<OverallKeyTappedHandler>(handler =>
            {
                handler.Handle(this.ui, this.Name);
            });
        }

        private void ui_ResetContentKeyTapped()
        {
            var w = this.web;
            w.Run<ResetContentKeyTappedHandler>(handler =>
            {
                handler.Handle(this.ui);
            });
        }

        private void ui_ResetTypeKeyTapped()
        {
            var w = this.web;
            w.Run<ResetTypeKeyTappedHandler>(handler =>
            {
                handler.Handle(this.ui);
            });
        }

        private long setupIf1;
        private readonly LogStatisticsUi ui;
        private readonly MethodWeb web;
    }
}
