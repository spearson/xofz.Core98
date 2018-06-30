namespace xofz.Root.Commands
{
    using System;
    using System.Threading;
    using xofz.Framework;
    using xofz.Presentation;
    using xofz.UI;

    public class SetupLoginCommand : Command
    {
        public SetupLoginCommand(
            LoginUi ui,
            MethodWeb web,
            int loginDurationMinutes = 15)
            : this(
                ui,
                web,
                TimeSpan.FromMinutes(loginDurationMinutes))
        {
        }

        public SetupLoginCommand(
            LoginUi ui,
            MethodWeb web,
            TimeSpan loginDuration)
        {
            this.ui = ui;
            this.web = web;
            this.loginDuration = loginDuration;
        }

        public override void Execute()
        {
            this.registerDependencies();
            new LoginPresenter(
                    this.ui,
                    this.web)
                .Setup(
                    this.loginDuration);
        }

        private void registerDependencies()
        {
            var w = this.web;
            w.RegisterDependency(
                new xofz.Framework.Timer(),
                "LoginTimer");
            w.RegisterDependency(
                new LatchHolder
                {
                    Latch = new ManualResetEvent(true)
                },
                "LoginLatch");
            w.RegisterDependency(
                new KeyboardLoader());
        }

        private readonly LoginUi ui;
        private readonly MethodWeb web;
        private readonly TimeSpan loginDuration;
    }
}
