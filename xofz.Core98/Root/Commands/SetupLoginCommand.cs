namespace xofz.Root.Commands
{
    using System.Threading;
    using xofz.Framework;
    using xofz.Presentation;
    using xofz.UI;

    public class SetupLoginCommand : Command
    {
        public SetupLoginCommand(
            LoginUi ui,
            MethodWeb web,
            bool support98 = false,
            int loginDurationMinutes = 15)
        {
            this.ui = ui;
            this.web = web;
            this.support98 = support98;
            this.loginDurationMinutes = loginDurationMinutes;
        }

        public override void Execute()
        {
            this.registerDependencies();
            new LoginPresenter(
                    this.ui,
                    this.web)
                .Setup(
                    this.loginDurationMinutes);
        }

        private void registerDependencies()
        {
            var w = this.web;
            w.RegisterDependency(
                this.support98
                    ? new xofz.Framework.Timer()
                    : new xofz.Framework.Timer(),
                "LoginTimer");
            w.RegisterDependency(
                new LatchHolder
                {
                    Latch = new ManualResetEvent(true)
                },
                "LoginLatch");
        }

        private readonly LoginUi ui;
        private readonly MethodWeb web;
        private readonly bool support98;
        private readonly int loginDurationMinutes;
    }
}
