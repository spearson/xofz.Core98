namespace xofz.Root.Commands
{
    using xofz.Framework;
    using xofz.Framework.Access;

    public class SetupAccessCommand : Command
    {
        public SetupAccessCommand(
            PasswordHolder passwords,
            MethodWeb web)
        {
            this.passwords = passwords;
            this.web = web;
        }

        public override void Execute()
        {
            this.registerDependencies();

            new AccessController(this.web)
                .Setup();
        }

        protected virtual void registerDependencies()
        {
            var w = this.web;
            w.RegisterDependency(
                this.passwords);
            w.RegisterDependency(
                new xofz.Framework.Timer(),
                @"AccessTimer");
            w.RegisterDependency(
                new SecureStringToolSet());
            w.RegisterDependency(
                new SettingsHolder());
        }

        protected readonly PasswordHolder passwords;
        protected readonly MethodWeb web;
    }
}
