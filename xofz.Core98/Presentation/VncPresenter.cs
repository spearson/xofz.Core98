namespace xofz.Presentation
{
    using System;
    using System.Net.Sockets;
    using System.Threading;
    using xofz.Framework;
    using xofz.UI;

    public sealed class VncPresenter : Presenter
    {
        public VncPresenter(
            VncUi ui,
            ShellUi shell,
            MethodWeb web)
            : base(ui, shell)
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
            this.ui.ErrorConnecting += this.ui_ErrorConnecting;
            w.Run<Navigator>(n => n.RegisterPresenter(this));
        }

        public override void Start()
        {
            if (Interlocked.CompareExchange(ref this.startedIf1, 1, 0) == 1)
            {
                return;
            }

            base.Start();
            this.connect();
        }

        public override void Stop()
        {
            Interlocked.CompareExchange(ref this.startedIf1, 0, 1);
            var w = this.web;
            w.Run<VncSettingsHolder>(
                holder =>
                {
                    if (holder.MaintainConnection)
                    {
                        return;
                    }

                    UiHelpers.Write(
                        this.ui,
                        () => this.ui.Disconnect());
                    this.ui.WriteFinished.WaitOne();
                    Interlocked.CompareExchange(
                        ref this.connectedIf1,
                        0,
                        1);
                });
        }

        private void connect()
        {
            if (Interlocked.CompareExchange(ref this.connectedIf1, 1, 0) == 1)
            {
                return;
            }

            var w = this.web;
            w.Run<VncSettingsHolder>(
                holder =>
                {
                    using (var client = new TcpClient())
                    {
                        client.BeginConnect(
                                holder.Hostname,
                                holder.Port,
                                null,
                                null)
                            .AsyncWaitHandle
                            .WaitOne(TimeSpan.FromSeconds(2));

                        if (!client.Connected)
                        {
                            w.Run<EventRaiser>(
                                er => er.Raise(
                                    this.ui,
                                    nameof(this.ui.ErrorConnecting),
                                    "Could not connect to "
                                    + holder.Hostname
                                    + ":"
                                    + holder.Port));
                            return;
                        }
                    }

                    UiHelpers.Write(
                        this.ui,
                        () => this.ui.Connect(
                            holder.Hostname,
                            holder.Password,
                            holder.ScaleToWindowSize,
                            holder.Port,
                            holder.ViewOnly));
                    this.ui.WriteFinished.WaitOne();
                    this.ui.ConnectionLost += this.ui_ConnectionLost;
                });
        }

        private void ui_ErrorConnecting(string errorMessage)
        {
            Interlocked.CompareExchange(ref this.connectedIf1, 0, 1);
            var w = this.web;
            w.Run<Messenger>(
                m => UiHelpers.Write(
                    m.Subscriber,
                    () => m.GiveError(
                        "Error connecting to VNC server."
                        + Environment.NewLine
                        + errorMessage)));
        }

        private void ui_ConnectionLost()
        {
            Interlocked.CompareExchange(ref this.connectedIf1, 0, 1);
            var w = this.web;
            w.Run<Messenger>(
                m => UiHelpers.Write(
                    m.Subscriber,
                    () => m.GiveError(
                        "The connection to the VNC server was lost. "
                        + "A new connection attempt may be made by "
                        + "navigating away from the VNC screen and back "
                        + "to it.")));
            w.Run<VncSettingsHolder>(
                holder =>
                    this.ui.ConnectionLost -= this.ui_ConnectionLost);
        }

        private int setupIf1, startedIf1;
        private long connectedIf1;
        private readonly VncUi ui;
        private readonly MethodWeb web;
    }
}
