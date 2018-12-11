namespace xofz.Framework.Login
{
    using xofz.UI;

    public class KeyboardKeyTappedHandler
    {
        public KeyboardKeyTappedHandler(
            MethodWeb web)
        {
            this.web = web;
        }

        public virtual void Handle(
            LoginUi ui)
        {
            var w = this.web;
            w.Run<KeyboardLoader>(loader => loader.Load());
            w.Run<UiReaderWriter>(rw =>
            {
                rw.Write(
                    ui,
                    ui.FocusPassword);
            });
        }

        protected readonly MethodWeb web;
    }
}
