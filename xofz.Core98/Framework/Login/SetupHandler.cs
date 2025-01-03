﻿namespace xofz.Framework.Login
{
    using xofz.UI;
    using xofz.UI.Login;

    public class SetupHandler
    {
        public SetupHandler(
            MethodRunner runner)
        {
            this.runner = runner;
        }

        /// <summary>
        /// Requires a UiReaderWriter, an AccessController, and
        /// a timer called LoginTimer registered with the MethodWeb
        /// </summary>
        /// <param name="ui">
        /// The LoginUi to set up</param>
        public virtual void Handle(
            LoginUi ui)
        {
            var r = this.runner;

            const bool 
                truth = true,
                falsity = false;
            r?.Run<Labels, UiReaderWriter>(
                (labels, uiRW) =>
                {
                    uiRW.Write(
                        ui,
                        () =>
                        {
                            if (ui == null)
                            {
                                return;
                            }

                            ui.TimeRemaining = labels.NotLoggedIn;
                            ui.KeyboardKeyVisible = falsity;
                        });

                    r.Run<KeyboardLoader>(loader =>
                    {
                        uiRW.Write(
                            ui,
                            () =>
                            {
                                if (ui == null)
                                {
                                    return;
                                }

                                ui.KeyboardKeyVisible = truth;
                            });
                    });

                    r.Run<AccessController>(ac =>
                    {
                        var cal = ac.CurrentAccessLevel;
                        uiRW.Write(
                            ui,
                            () =>
                            {
                                if (ui == null)
                                {
                                    return;
                                }

                                ui.CurrentAccessLevel = cal;
                            });
                    });

                    if (ui is LoginUiV2 v2)
                    {
                        r.Run<LabelApplier>(applier =>
                        {
                            applier.Apply(
                                v2);
                        });
                    }

                    r.Run<xofz.Framework.Timer>(t =>
                        {
                            const short timerInterval = 1000;
                            t.Start(timerInterval);
                        },
                        DependencyNames.Timer);
                });
        }

        protected readonly MethodRunner runner;
    }
}