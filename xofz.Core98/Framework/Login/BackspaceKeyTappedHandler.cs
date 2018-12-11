namespace xofz.Framework.Login
{
    using xofz.UI;

    public class BackspaceKeyTappedHandler
    {
        public BackspaceKeyTappedHandler(
            MethodWeb web)
        {
            this.web = web;
        }

        /// <summary>
        /// Requires a UiReaderWriter registered with the MethodWeb.
        /// </summary>
        /// <param name="ui"></param>
        public virtual void Handle(
            LoginUi ui)
        {
            var w = this.web;
            w.Run<UiReaderWriter>(rw =>
            {
                var currentPw = rw.Read(
                    ui,
                    () => ui.CurrentPassword);
                var newPw = StringHelpers.RemoveEndChars(
                    currentPw,
                    1);
                rw.Write(ui,
                    () =>
                    {
                        ui.CurrentPassword = newPw;
                        ui.FocusPassword();
                    });
            });
        }

        protected readonly MethodWeb web;
    }
}
