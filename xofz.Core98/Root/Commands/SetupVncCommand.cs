namespace xofz.Root.Commands
{
    using xofz.Framework;
    using xofz.Presentation;
    using xofz.UI;

    public class SetupVncCommand : Command
    {
        public SetupVncCommand(
            VncUi ui,
            ShellUi shell,
            MethodWeb web,
            string hostname,
            string password,
            bool viewOnly = false,
            bool scaleToWindowSize = true,
            int port = 5900,
            bool maintainConnection = true)
        {
            this.ui = ui;
            this.shell = shell;
            this.web = web;
            this.hostname = hostname;
            this.password = password;
            this.viewOnly = viewOnly;
            this.scaleToWindowSize = scaleToWindowSize;
            this.port = port;
            this.maintainConnection = maintainConnection;
        }

        public override void Execute()
        {
            this.registerDependencies();
            new VncPresenter(
                    this.ui,
                    this.shell,
                    this.web)
                .Setup();
        }

        private void registerDependencies()
        {
            var w = this.web;
            w.RegisterDependency(
                new VncSettingsHolder
                {
                    Hostname = this.hostname,
                    Password = this.password,
                    ViewOnly = this.viewOnly,
                    Port = this.port,
                    ScaleToWindowSize = this.scaleToWindowSize,
                    MaintainConnection = this.maintainConnection
                });
        }

        private readonly VncUi ui;
        private readonly ShellUi shell;
        private readonly MethodWeb web;
        private readonly string hostname;
        private readonly string password;
        private readonly bool viewOnly;
        private readonly bool scaleToWindowSize;
        private readonly int port;
        private readonly bool maintainConnection;
    }
}
