namespace xofz.Framework.LogStatistics
{
    using xofz.UI;

    public class ResetContentKeyTappedHandler
    {
        public ResetContentKeyTappedHandler(
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
                        ui.FilterContent = null;
                    });
            });
        }

        protected readonly MethodWeb web;
    }
}
