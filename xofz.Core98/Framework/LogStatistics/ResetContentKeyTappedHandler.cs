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

                        ui.FilterContent = null;
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

                        ui.FilterContent = null;
                    });
            });
        }

        protected readonly MethodRunner runner;
    }
}
