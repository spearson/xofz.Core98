namespace xofz.Root.Commands
{
    using xofz.Framework;
    using xofz.Presentation;
    using xofz.UI;

    public class SetupMainCommand : Command
    {
        public SetupMainCommand(
            MainUi ui,
            MethodWeb web,
            AccessLevel shutdownLevel = AccessLevel.None)
        {
            this.ui = ui;
            this.web = web;
            this.shutdownLevel = shutdownLevel;
        }

        public override void Execute()
        {
            this.registerDependencies();
            new MainPresenter(
                    this.ui,
                    this.web)
                .Setup();
        }

        private void registerDependencies()
        {
            var w = this.web;
            w.RegisterDependency(
                new MainUiSettings
                {
                    ShutdownLevel = this.shutdownLevel
                });
        }

        private readonly MainUi ui;
        private readonly MethodWeb web;
        private readonly AccessLevel shutdownLevel;
    }
}
