namespace xofz.Root.Commands
{
    using System;
    using xofz.Framework;
    using xofz.Presentation;
    using xofz.UI;

    public class SetupShutdownCommand : Command
    {
        public SetupShutdownCommand(
            Ui mainUi,
            MethodWeb web)
            : this(mainUi, () => { }, web)
        {
        }

        public SetupShutdownCommand(
            Ui mainUi,
            Action cleanup,
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

        private readonly Ui mainUi;
        private readonly Action cleanup;
        private readonly MethodWeb web;
    }
}
