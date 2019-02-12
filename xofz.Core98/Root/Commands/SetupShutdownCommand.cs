namespace xofz.Root.Commands
{
    using xofz.Framework;
    using xofz.Framework.Shutdown;
    using xofz.Presentation;
    using xofz.UI;

    public class SetupShutdownCommand : Command
    {
        public SetupShutdownCommand(
            MethodWeb web)
            : this(null, null, web)
        {
        }

        public SetupShutdownCommand(
            Do cleanup,
            MethodWeb web)
            : this(null, cleanup, web)
        {
        }

        public SetupShutdownCommand(
            Ui cleanupUi,
            Do cleanup,
            MethodWeb web)
        {
            this.cleanupUi = cleanupUi;
            this.cleanup = cleanup;
            this.web = web;
        }

        public override void Execute()
        {
            this.registerDependencies();

            new ShutdownPresenter(
                    this.web)
                .Setup();
        }

        protected virtual void registerDependencies()
        {
            var w = this.web;
            var ui = this.cleanupUi;
            if (ui != null)
            {
                w.RegisterDependency(
                    ui,
                    UiNames.Cleanup);
            }

            var c = this.cleanup;
            if (c != null)
            {
                w.RegisterDependency(
                    c,
                    MethodNames.Cleanup);
            }

            w.RegisterDependency(
                new StartHandler(w));
        }

        protected readonly Ui cleanupUi;
        protected readonly Do cleanup;
        protected readonly MethodWeb web;
    }
}
