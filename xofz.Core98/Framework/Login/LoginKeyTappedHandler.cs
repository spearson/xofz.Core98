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
                        () => ui?.CurrentPassword);
                    var previousLevel = ac.CurrentAccessLevel;
                    ac.InputPassword(
                        securePw,
                        settings.Duration);
                    var newLevel = ac.CurrentAccessLevel;
                    if (previousLevel == newLevel)
                    {
                        r.Run<xofz.Framework.Timer, EventRaiser>(
                            (t, er) =>
                            {
                                er.Raise(t, nameof(t.Elapsed));
                            },
                            DependencyNames.Timer);
                    }

                    const AccessLevel zeroAccess = AccessLevel.None;
                    if (newLevel > zeroAccess)
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
                        () =>
                        {
                            ui?.FocusPassword();
                        });
                });
        }

        protected readonly MethodRunner runner;
    }
}
