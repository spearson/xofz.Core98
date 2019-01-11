namespace xofz.Framework.LogStatistics
{
    using xofz.UI;

    public class ResetTypeKeyTappedHandler
    {
        public ResetTypeKeyTappedHandler(
            MethodWeb web)
        {
            this.web = web;
        }

        public virtual void Handle(
            LogStatisticsUi ui)
        {
            var w = this.web;
            w.Run<UiReaderWriter>(uiRW =>
            {
                uiRW.Write(
                    ui,
                    () =>
                    {
                        ui.FilterType = null;
                    });
            });
        }

        protected readonly MethodWeb web;
    }
}
