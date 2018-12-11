namespace xofz.Framework.Login
{
    using xofz.UI;

    public class AccessLevelChangedHandler
    {
        public AccessLevelChangedHandler(
            MethodWeb web)
        {
            this.web = web;
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
            var w = this.web;
            w.Run<UiReaderWriter>(rw =>
            {
                if (newAccessLevel == AccessLevel.None)
                {
                    rw.Write(
                        ui,
                        () => ui.CurrentPassword = null);
                }
            });

            w.Run<xofz.Framework.Timer, EventRaiser>(
                (t, er) =>
                {
                    er.Raise(
                        t,
                        nameof(t.Elapsed));
                },
                "LoginTimer");
        }

        protected readonly MethodWeb web;
    }
}
