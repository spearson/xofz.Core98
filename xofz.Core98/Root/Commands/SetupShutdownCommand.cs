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
            Ui mainUi,
            Do cleanup,
            MethodWeb web)
        {
            this.mainUi = mainUi;
            this.cleanup = cleanup;
            this.web = web;
        }

        public override void Execute()
        {
            this.registerDependencies();
            new ShutdownPresenter(
                    this.mainUi,
                    this.cleanup,
                    this.web)
                .Setup();
        }

        protected virtual void registerDependencies()
        {
            var w = this.web;
            w.RegisterDependency(
                new StartHandler(w));
        }

        protected readonly Ui mainUi;
        protected readonly Do cleanup;
        protected readonly MethodWeb web;
    }
}
