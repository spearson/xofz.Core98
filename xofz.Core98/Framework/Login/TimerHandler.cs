namespace xofz.Framework.Login
{
    using System.Text;
    using xofz.UI;
    using xofz.UI.Login;

    public class TimerHandler
    {
        public TimerHandler(
            MethodRunner runner)
        {
            this.runner = runner;
        }

        /// <summary>
        /// Requires an AccessController and UiReaderWriter
        /// Also requires a SettingsHolder and Labels, but
        /// this is registered by the SetupLoginCommand
        /// </summary>
        /// <param name="ui">The LoginUi on which to handle the event</param>
        public virtual void Handle(
            LoginUi ui)
        {
            var r = this.runner;
            r?.Run<
                AccessController,
                SettingsHolder,
                Labels>(
                (ac, settings, labels) =>
                {
                    var cal = ac.CurrentAccessLevel;
                    string timeRemaining;
                    var noAccess = cal == AccessLevel.None;
                    if (noAccess)
                    {
                        timeRemaining = labels.NotLoggedIn;
                        goto checkAccess;
                    }

                    var tr = ac.TimeRemaining;
                    var sb = new StringBuilder();
                    const byte
                        two = 2,
                        three = 3;
                    const char
                        colon = ':',
                        period = '.',
                        zeroC = '0';

                    sb.Append((int)tr.TotalHours).
                        Append(colon).
                        Append(tr.Minutes.ToString().
                            PadLeft(two, zeroC)).
                        Append(colon).
                        Append(tr.Seconds.ToString().
                            PadLeft(two, zeroC)).
                        Append(period).
                        Append(tr.Milliseconds.ToString().
                            PadLeft(three, zeroC));
                    timeRemaining = sb.ToString();

                    checkAccess:

                    if (noAccess)
                    {
                        settings.CurrentPassword = null;
                    }

                    r.Run<UiReaderWriter>(uiRW =>
                    {
                        uiRW.Write(
                            ui,
                            () =>
                            {
                                if (ui == null)
                                {
                                    return;
                                }

                                ui.CurrentAccessLevel = cal;
                                ui.TimeRemaining = timeRemaining;
                            });
                    });
                });
        }

        protected readonly MethodRunner runner;
    }
}
