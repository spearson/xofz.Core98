namespace xofz.Framework.LogStatistics
{
    using xofz.UI;
    using xofz.UI.LogStatistics;

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
            r?.Run<UiReaderWriter>(uiRW =>
            {
                uiRW.Write(
                    ui,
                    () =>
                    {
                        if (ui == null)
                        {
                            return;
                        }

                        ui.FilterType = null;
                    });
            });
        }

        public virtual void Handle(
            LogStatisticsUi ui,
            string name)
        {
            var r = this.runner;
            r?.Run<UiReaderWriter>(uiRW =>
            {
                uiRW.Write(
                    ui,
                    () =>
                    {
                        if (ui == null)
                        {
                            return;
                        }

                        ui.FilterType = null;
                    });
            });
        }

        protected readonly MethodRunner runner;
    }
}
