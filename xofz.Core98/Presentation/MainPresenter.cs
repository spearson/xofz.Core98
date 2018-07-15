﻿namespace xofz.Presentation
{
    using System.Threading;
    using xofz.Framework;
    using xofz.UI;

    public sealed class MainPresenter : Presenter
    {
        public MainPresenter(
            MainUi ui,
            MethodWeb web)
            : base(ui, null)
        {
            this.ui = ui;
            this.web = web;
        }

        public void Setup()
        {
            if (Interlocked.CompareExchange(ref this.setupIf1, 1, 0) == 1)
            {
                return;
            }

            var w = this.web;
            var subscriberRegistered = false;
            w.Run<EventSubscriber>(subscriber =>
            {
                subscriberRegistered = true;
                subscriber.Subscribe(
                    this.ui,
                    nameof(this.ui.ShutdownRequested),
                    this.ui_ShutdownRequested);
            });

            if (!subscriberRegistered)
            {
                this.ui.ShutdownRequested += this.ui_ShutdownRequested;
            }
            
            w.Run<Navigator>(n => n.RegisterPresenter(this));
        }

        public override void Start()
        {
        }

        private void ui_ShutdownRequested()
        {
            var w = this.web;
            var cal = AccessLevel.None;
            var shutdownLevel = AccessLevel.None;
            w.Run<AccessController, MainUiSettings>(
                (ac, s) =>
                {
                    cal = ac.CurrentAccessLevel;
                    shutdownLevel = s.ShutdownLevel;
                });

            if (cal >= shutdownLevel)
            {
                w.Run<Navigator>(n => n.Present<ShutdownPresenter>());
                return;
            }

            w.Run<Navigator>(n => n.LoginFluidly());
            w.Run<AccessController, EventRaiser>((ac, er) =>
            {
                cal = ac.CurrentAccessLevel;
                if (cal >= shutdownLevel)
                {
                    er.Raise(
                        this.ui,
                        nameof(this.ui.ShutdownRequested));
                }
            });
        }

        private long setupIf1;
        private readonly MainUi ui;
        private readonly MethodWeb web;
    }
}
