﻿namespace xofz.Framework.Login
{
    using xofz.UI;

    public class StopHandler
    {
        public StopHandler(
            MethodWeb web)
        {
            this.web = web;
        }

        /// <summary>
        /// Requires a UiReaderWriter and a SettingsHolder
        /// Also uses a LatchHolder called LoginLatch 
        /// </summary>
        /// <param name="ui">The LoginUi to handle</param>
        public virtual void Handle(
            LoginUi ui)
        {
            var w = this.web;
            w.Run<UiReaderWriter, SettingsHolder>(
                (rw, settings) =>
                {
                    var cp = settings.CurrentPassword;
                    rw.WriteSync(ui,
                        () =>
                        {
                            ui.CurrentPassword = cp;
                            ui.Hide();
                        });
                    w.Run<LatchHolder>(latch =>
                        {
                            latch.Latch.Set();
                        },
                        "LoginLatch");
                });
        }

        protected readonly MethodWeb web;
    }
}