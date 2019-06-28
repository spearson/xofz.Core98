﻿namespace xofz.Root.Commands
{
    using System;
    using System.Threading;
    using xofz.Framework;
    using xofz.Framework.Login;
    using xofz.Presentation;
    using xofz.UI;

    public class SetupLoginCommand : Command
    {
        public SetupLoginCommand(
            LoginUi ui,
            MethodWeb web,
            int loginDurationMinutes = 15)
            : this(
                ui,
                web,
                TimeSpan.FromMinutes(loginDurationMinutes))
        {
        }

        public SetupLoginCommand(
            LoginUi ui,
            MethodWeb web,
            TimeSpan loginDuration)
        {
            this.ui = ui;
            this.web = web;
            this.loginDuration = loginDuration;
        }

        public override void Execute()
        {
            this.registerDependencies();
            new LoginPresenter(
                    this.ui,
                    this.web)
                .Setup();
        }

        protected virtual void registerDependencies()
        {
            var w = this.web;
            w.RegisterDependency(
                new xofz.Framework.Timer(),
                DependencyNames.Timer);
            w.RegisterDependency(
                new SettingsHolder
                {
                    Duration = this.loginDuration
                });
            w.RegisterDependency(
                new LatchHolder
                {
                    Latch = new ManualResetEvent(true)
                },
                DependencyNames.Latch);
            w.RegisterDependency(
                new KeyboardLoader());
            w.RegisterDependency(
                new SetupHandler(w));
            w.RegisterDependency(
                new StartHandler(w));
            w.RegisterDependency(
                new StopHandler(w));
            w.RegisterDependency(
                new BackspaceKeyTappedHandler(w));
            w.RegisterDependency(
                new LoginKeyTappedHandler(w));
            w.RegisterDependency(
                new TimerHandler(w));
            w.RegisterDependency(
                new AccessLevelChangedHandler(w));
            w.RegisterDependency(
                new KeyboardKeyTappedHandler(w));
            w.RegisterDependency(
                new LabelApplier(w));
            var registered = false;
            w.Run<Labels>(labels =>
            {
                registered = true;
            });
            if (!registered)
            {
                w.RegisterDependency(
                    new Labels());
            }
        }

        protected readonly LoginUi ui;
        protected readonly MethodWeb web;
        protected readonly TimeSpan loginDuration;
    }
}
