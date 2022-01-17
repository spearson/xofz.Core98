namespace xofz.Root.Commands
{
    using xofz.Framework;
    using xofz.Framework.Main;
    using xofz.Presentation.Presenters;
    using xofz.UI;

    public class SetupMainCommand 
        : Command
    {
        public SetupMainCommand(
            MainUi ui,
            MethodWeb web,
            SettingsHolder settings)
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
                w?.RegisterDependency(
                    new SettingsHolder());
                goto registerHandlers;
            }

            w?.RegisterDependency(
                s);

            registerHandlers:
            w?.RegisterDependency(
                new ShutdownRequestedHandler(w));
        }

        protected readonly MainUi ui;
        protected readonly MethodWeb web;
        protected readonly SettingsHolder settings;
    }
}
