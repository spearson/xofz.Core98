﻿namespace xofz.Presentation
{
    using System.Threading;
    using xofz.Framework;
    using xofz.Framework.LogStatistics;
    using xofz.UI;

    public sealed class LogStatisticsPresenter 
        : PopupNamedPresenter
    {
        public LogStatisticsPresenter(
            LogStatisticsUi ui,
            MethodRunner runner)
            : base(ui)
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
            r.Run<SetupHandler>(handler =>
            {
                handler.Handle(ui, this.Name);
            });

            r.Run<EventSubscriber>(subscriber =>
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
            
            r.Run<Navigator>(nav => 
                nav.RegisterPresenter(this));
        }

        public override void Start()
        {
            if (Interlocked.Read(
                    ref this.setupIf1) != 1)
            {
                return;
            }

            base.Start();
            var r = this.runner;
            r.Run<Navigator>(nav =>
            {
                var name = this.Name;
                Gen<LogUi> readLogUi = () => nav.GetUi<LogPresenter, LogUi>(
                    name);
                r.Run<StartHandler>(handler =>
                {
                    handler.Handle(
                        this.ui,
                        readLogUi,
                        name);
                });
            });
        }

        private void ui_RangeKeyTapped()
        {
            var r = this.runner;
            r.Run<RangeKeyTappedHandler>(handler =>
            {
                handler.Handle(this.ui, this.Name);
            });
        }

        private void ui_OverallKeyTapped()
        {
            var r = this.runner;
            r.Run<OverallKeyTappedHandler>(handler =>
            {
                handler.Handle(this.ui, this.Name);
            });
        }

        private void ui_ResetContentKeyTapped()
        {
            var r = this.runner;
            r.Run<ResetContentKeyTappedHandler>(handler =>
            {
                handler.Handle(this.ui, this.Name);
            });
        }

        private void ui_ResetTypeKeyTapped()
        {
            var r = this.runner;
            r.Run<ResetTypeKeyTappedHandler>(handler =>
            {
                handler.Handle(this.ui, this.Name);
            });
        }

        private long setupIf1;
        private readonly LogStatisticsUi ui;
        private readonly MethodRunner runner;
    }
}
