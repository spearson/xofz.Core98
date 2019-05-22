namespace xofz.Framework.Login
{
    using xofz.UI;

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
            r.Run<UiReaderWriter>(uiRW =>
            {
                if (newAccessLevel == AccessLevel.None)
                {
                    uiRW.Write(
                        ui,
                        () => ui.CurrentPassword = null);
                }
            });

            r.Run<xofz.Framework.Timer, EventRaiser>(
                (t, er) =>
                {
                    er.Raise(
                        t,
                        nameof(t.Elapsed));
                },
                DependencyNames.Timer);
        }

        protected readonly MethodRunner runner;
    }
}
