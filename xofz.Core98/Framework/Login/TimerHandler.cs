namespace xofz.Framework.Login
{
    using System.Text;
    using xofz.UI;

    public class TimerHandler
    {
        public TimerHandler(
            MethodWeb web)
        {
            this.web = web;
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
            var w = this.web;
            w.Run<
                AccessController, 
                SettingsHolder,
                UiReaderWriter>(
                (ac, settings, rw) =>
            {
                var cal = ac.CurrentAccessLevel;
                string timeRemaining;
                if (cal < AccessLevel.Level1)
                {
                    timeRemaining = "Not logged in";
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

                rw.Write(
                    ui,
                    () =>
                    {
                        ui.CurrentAccessLevel = cal;
                        ui.TimeRemaining = timeRemaining;
                    });
            });
        }

        protected readonly MethodWeb web;
    }
}
