namespace xofz.Root.Commands
{
    using xofz.Framework;
    using xofz.Framework.Shutdown;
    using xofz.Presentation.Presenters;
    using xofz.UI;

    public class SetupShutdownCommand
        : Command
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
            if (w == null)
            {
                return;
            }

            w.RegisterDependency(
                this.cleanupUi,
                UiNames.Cleanup);
            w.RegisterDependency(
                this.cleanup,
                MethodNames.Cleanup);
            w.RegisterDependency(
                new StartHandler(w));
            w.RegisterDependency(
                new ProcessKiller());
        }

        protected readonly Ui cleanupUi;
        protected readonly Do cleanup;
        protected readonly MethodWeb web;
    }
}
