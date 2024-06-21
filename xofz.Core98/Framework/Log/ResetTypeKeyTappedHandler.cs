namespace xofz.Framework.Log
{
    using xofz.UI;
    using xofz.UI.Log;

    public class ResetTypeKeyTappedHandler
    {
        public ResetTypeKeyTappedHandler(
            MethodRunner runner)
        {
            this.runner = runner;
        }

        public virtual void Handle(
            LogUiV3 ui,
            string name)
        {
            var r = this.runner;
            r?.Run<UiReaderWriter>(uiRW =>
            {
                uiRW.Write(
                    ui,
                    () =>
                    {
                        ui?.ResetTypeFilter();
                    });
            });
        }

        protected readonly MethodRunner runner;
    }
}