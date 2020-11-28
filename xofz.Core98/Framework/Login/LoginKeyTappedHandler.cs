namespace xofz.Framework.Login
{
    using xofz.UI;

    public class LoginKeyTappedHandler
    {
        public LoginKeyTappedHandler(
            MethodRunner runner)
        {
            this.runner = runner;
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
            var r = this.runner;
            r?.Run<
                UiReaderWriter,
                AccessController,
                SettingsHolder>(
                (uiRW, ac, settings) =>
                {
                    var securePw = uiRW.Read(
                        ui,
                        () => ui.CurrentPassword);
                    var previousCal = ac.CurrentAccessLevel;
                    ac.InputPassword(
                        securePw,
                        settings.Duration);
                    var newCal = ac.CurrentAccessLevel;
                    if (previousCal == newCal)
                    {
                        r.Run<xofz.Framework.Timer, EventRaiser>(
                            (t, er) =>
                            {
                                er.Raise(t, nameof(t.Elapsed));
                            },
                            DependencyNames.Timer);
                    }

                    if (newCal > AccessLevel.None)
                    {
                        settings.CurrentPassword = securePw;
                        r.Run<StopHandler>(handler =>
                        {
                            handler.Handle(ui);
                        });
                        return;
                    }

                    uiRW.Write(
                        ui,
                        ui.FocusPassword);
                });
        }

        protected readonly MethodRunner runner;
    }
}
