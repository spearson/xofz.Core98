namespace xofz.Framework.Login
{
    using xofz.UI;
    using xofz.UI.Login;

    public class AccessLevelChangedHandler
    {
        public AccessLevelChangedHandler(
            MethodRunner runner)
        {
            this.runner = runner;
        }

        /// <summary>
        ///  Needs a UiReaderWriter, a timer named LoginTimer, and an EventRaiser
        /// </summary>
        /// <param name="ui"></param>
        /// <param name="newAccessLevel"></param>
        public virtual void Handle(
            LoginUi ui,
            AccessLevel newAccessLevel)
        {
            var r = this.runner;
            r?.Run<UiReaderWriter>(uiRW =>
            {
                const AccessLevel zeroAccess = AccessLevel.None;
                if (newAccessLevel == zeroAccess)
                {
                    uiRW.Write(
                        ui,
                        () =>
                        {
                            if (ui == null)
                            {
                                return;
                            }

                            ui.CurrentPassword = null;
                        });
                }
            });

            r?.Run<xofz.Framework.Timer, EventRaiser>(
                (timer, er) =>
                {
                    er.Raise(
                        timer,
                        nameof(timer.Elapsed));
                },
                DependencyNames.Timer);
        }

        protected readonly MethodRunner runner;
    }
}