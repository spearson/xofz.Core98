namespace xofz.Framework.Login
{
    using xofz.UI;
    using xofz.UI.Login;

    public class BackspaceKeyTappedHandler
    {
        public BackspaceKeyTappedHandler(
            MethodRunner runner)
        {
            this.runner = runner;
        }

        /// <summary>
        /// Requires a UiReaderWriter registered with the MethodWeb.
        /// </summary>
        /// <param name="ui"></param>
        public virtual void Handle(
            LoginUi ui)
        {
            var r = this.runner;
            r?.Run<SecureStringToolSet, UiReaderWriter>(
                (ssts, uiRW) =>
                {
                    const byte one = 1;
                    var currentPw = ssts.Decode(uiRW.Read(
                        ui,
                        () => ui?.CurrentPassword));
                    var newPw = StringHelpers.RemoveEndChars(
                        currentPw,
                        one);
                    var securePw = ssts.Encode(
                        newPw);
                    uiRW.Write(
                        ui,
                        () =>
                        {
                            if (ui == null)
                            {
                                return;
                            }

                            ui.CurrentPassword = securePw;
                            ui?.FocusPassword();
                        });
                });
        }

        protected readonly MethodRunner runner;
    }
}