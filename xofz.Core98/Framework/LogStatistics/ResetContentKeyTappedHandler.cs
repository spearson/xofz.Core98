namespace xofz.Framework.LogStatistics
{
    using xofz.UI;

    public class ResetContentKeyTappedHandler
    {
        public ResetContentKeyTappedHandler(
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
                        ui.FilterContent = null;
                    });
            });
        }

        protected readonly MethodRunner runner;
    }
}
