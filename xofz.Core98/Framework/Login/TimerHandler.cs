namespace xofz.Framework.Login
{
    using System.Text;
    using xofz.UI;

    public class TimerHandler
    {
        public TimerHandler(
            MethodRunner runner)
        {
            this.runner = runner;
        }

        /// <summary>
        /// Requires an AccessController and UiReaderWriter
        /// Also requires a SettingsHolder, but
        /// this is registered by the SetupLoginCommand
        /// </summary>
        /// <param name="ui">The LoginUi on which to handle the event</param>
        public virtual void Handle(
            LoginUi ui)
        {
            var r = this.runner;
            r.Run<
                AccessController, 
                SettingsHolder,
                Labels,
                UiReaderWriter>(
                (ac, settings, labels, uiRW) =>
            {
                var cal = ac.CurrentAccessLevel;
                string timeRemaining;
                if (cal < AccessLevel.Level1)
                {
                    timeRemaining = labels.NotLoggedIn;
                    goto checkAccess;
                }

                var tr = ac.TimeRemaining;
                var sb = new StringBuilder();
                sb.Append((int)tr.TotalHours)
                    .Append(':')
                    .Append(tr
                        .Minutes
                        .ToString()
                        .PadLeft(2, '0'))
                    .Append(':')
                    .Append(tr
                        .Seconds
                        .ToString()
                        .PadLeft(2, '0'))
                    .Append('.')
                    .Append(tr
                        .Milliseconds
                        .ToString()
                        .PadLeft(3, '0'));
                timeRemaining = sb.ToString();

                checkAccess:
                var noAccess = cal == AccessLevel.None;
                if (noAccess)
                {
                    settings.CurrentPassword = null;
                }

                uiRW.Write(
                    ui,
                    () =>
                    {
                        ui.CurrentAccessLevel = cal;
                        ui.TimeRemaining = timeRemaining;
                    });
            });
        }

        protected readonly MethodRunner runner;
    }
}
