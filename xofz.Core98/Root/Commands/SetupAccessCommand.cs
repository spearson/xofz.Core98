﻿namespace xofz.Root.Commands
{
    using xofz.Framework;
    using xofz.Framework.Access;

    public class SetupAccessCommand
        : Command
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
            var w = this.web;
            if (w == null)
            {
                return;
            }

            this.registerDependencies();

            var ac = new AccessController(
                w);
            ac.Setup();
            w.RegisterDependency(
                ac);
        }

        protected virtual void registerDependencies()
        {
            var w = this.web;
            if (w == null)
            {
                return;
            }

            w.RegisterDependency(
                this.passwords);
            w.RegisterDependency(
                new xofz.Framework.Timer(),
                DependencyNames.Timer);
            w.RegisterDependency(
                new SecureStringToolSet());
            w.RegisterDependency(
                new SettingsHolder());
            w.RegisterDependency(
                new TimeProvider());
        }

        protected readonly PasswordHolder passwords;
        protected readonly MethodWeb web;
    }
}