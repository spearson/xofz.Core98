namespace xofz.Framework.LogStatistics
{
    using xofz.UI;

    public class ResetTypeKeyTappedHandler
    {
        public ResetTypeKeyTappedHandler(
            MethodRunner runner)
        {
            this.runner = runner;
        }

        public virtual void Handle(
            LogStatisticsUi ui)
        {
            var r = this.runner;
            r.Run<UiReaderWriter>(uiRW =>
            {
                uiRW.Write(
                    ui,
                    () =>
                    {
                        ui.FilterType = null;
                    });
            });
        }

        protected readonly MethodRunner runner;
    }
}
