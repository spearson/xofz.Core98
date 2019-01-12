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
            w.Run<SecureStringToolSet, UiReaderWriter>(
                (ssts, uiRW) =>
            {
                var currentPw = ssts.Decode(uiRW.Read(
                    ui,
                    () => ui.CurrentPassword));
                var newPw = StringHelpers.RemoveEndChars(
                    currentPw,
                    1);
                var securePw = ssts.Encode(
                    newPw);
                uiRW.Write(ui,
                    () =>
                    {
                        ui.CurrentPassword = securePw;
                        ui.FocusPassword();
                    });
            });
        }

        protected readonly MethodWeb web;
    }
}
