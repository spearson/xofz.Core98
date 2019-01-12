namespace xofz.Root.Commands
{
    using xofz.Framework;
    using xofz.Framework.Main;
    using xofz.Presentation;
    using xofz.UI;

    public class SetupMainCommand : Command
    {
        public SetupMainCommand(
            MainUi ui,
            MethodWeb web,
            AccessLevel shutdownLevel)
            : this(ui, web, new MainUiSettings
            {
                ShutdownLevel = shutdownLevel
            })
        {
        }

        public SetupMainCommand(
            MainUi ui,
            MethodWeb web,
            MainUiSettings settings)
        {
            this.ui = ui;
            this.web = web;
            this.settings = settings;
        }

        public override void Execute()
        {
            this.registerDependencies();
            new MainPresenter(
                    this.ui,
                    this.web)
                .Setup();
        }

        protected virtual void registerDependencies()
        {
            var w = this.web;
            var s = this.settings;
            if (s == null)
            {
                w.RegisterDependency(
                    new MainUiSettings
                    {
                        ShutdownLevel = AccessLevel.None
                    });
                goto registerHandlers;
            }

            w.RegisterDependency(
                s);

            registerHandlers:
            w.RegisterDependency(
                new ShutdownRequestedHandler(w));
        }

        protected readonly MainUi ui;
        protected readonly MethodWeb web;
        protected readonly MainUiSettings settings;
    }
}
