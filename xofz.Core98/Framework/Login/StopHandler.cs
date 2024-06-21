namespace xofz.Framework.Login
{
    using xofz.UI;
    using xofz.UI.Login;

    public class StopHandler
    {
        public StopHandler(
            MethodRunner runner)
        {
            this.runner = runner;
        }

        /// <summary>
        /// Requires a UiReaderWriter and a SettingsHolder
        /// Also uses a LatchHolder called LoginLatch 
        /// </summary>
        /// <param name="ui">The LoginUi to handle</param>
        public virtual void Handle(
            LoginUi ui)
        {
            var r = this.runner;
            r?.Run<
                UiReaderWriter,
                SettingsHolder>(
                (uiRW, settings) =>
                {
                    var cp = settings.CurrentPassword;
                    uiRW.WriteSync(
                        ui,
                        () =>
                        {
                            if (ui == null)
                            {
                                return;
                            }

                            ui.CurrentPassword = cp;
                            ui?.Hide();
                        });
                    r.Run<LatchHolder>(latch =>
                        {
                            latch.Latch?.Set();
                        },
                        DependencyNames.Latch);
                });
        }

        protected readonly MethodRunner runner;
    }
}