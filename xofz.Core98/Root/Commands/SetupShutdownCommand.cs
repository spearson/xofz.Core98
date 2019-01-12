namespace xofz.Root.Commands
{
    using xofz.Framework;
    using xofz.Presentation;
    using xofz.UI;

    public class SetupShutdownCommand : Command
    {
        public SetupShutdownCommand(
            MethodWeb web)
            : this(() => { }, web)
        {
        }

        public SetupShutdownCommand(
            Ui mainUi,
            MethodWeb web)
            : this(mainUi, () => { }, web)
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
            new ShutdownPresenter(
                    this.mainUi,
                    this.cleanup,
                    this.web)
                .Setup();
        }

        protected readonly Ui mainUi;
        protected readonly Do cleanup;
        protected readonly MethodWeb web;
    }
}
