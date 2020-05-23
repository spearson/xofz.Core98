namespace xofz.Framework.Login
{
    using xofz.UI;

    public class KeyboardKeyTappedHandler
    {
        public KeyboardKeyTappedHandler(
            MethodRunner runner)
        {
            this.runner = runner;
        }

        public virtual void Handle(
            LoginUi ui)
        {
            var r = this.runner;
            r.Run<KeyboardLoader>(loader =>
            {
                try
                {
                    loader.Load();
                }
                catch
                {
                    // swallow
                }
            });

            r.Run<UiReaderWriter>(uiRW =>
            {
                uiRW.Write(
                    ui,
                    ui.FocusPassword);
            });
        }

        protected readonly MethodRunner runner;
    }
}
