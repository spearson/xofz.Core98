﻿namespace xofz.Framework.Login
{
    using xofz.UI;

    public class LoginKeyTappedHandler
    {
        public LoginKeyTappedHandler(
            MethodWeb web)
        {
            this.web = web;
        }

        /// <summary>
        /// Requires a UiReaderWriter, an AccessController, and an EventRaiser
        /// Also requires a Timer called LoginTimer and a StopHandler
        /// The timer and handler are registered by the SetupLoginCommand
        /// </summary>
        /// <param name="ui"></param>
        /// 
        public virtual void Handle(
            LoginUi ui)
        {
            var w = this.web;
            w.Run<
                UiReaderWriter,
                AccessController,
                SettingsHolder>(
                (rw, ac, settings) =>
                {
                    var password = rw.Read(
                        ui,
                        () => ui.CurrentPassword);
                    var previousCal = ac.CurrentAccessLevel;
                    ac.InputPassword(
                        password,
                        settings.Duration);
                    var newCal = ac.CurrentAccessLevel;
                    if (previousCal == newCal)
                    {
                        w.Run<xofz.Framework.Timer, EventRaiser>(
                            (t, er) =>
                            {
                                er.Raise(t, nameof(t.Elapsed));
                            },
                            "LoginTimer");
                    }

                    if (newCal > AccessLevel.None)
                    {
                        settings.CurrentPassword = password;
                        w.Run<StopHandler>(handler => handler.Handle(ui));
                        return;
                    }

                    rw.Write(
                        ui,
                        ui.FocusPassword);
                });
        }

        protected readonly MethodWeb web;
    }
}