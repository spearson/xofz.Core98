namespace xofz.Framework.LogStatistics
{
    using xofz.Framework.Logging;
    using xofz.UI;
    using xofz.UI.LogStatistics;

    public class OverallKeyTappedHandler
    {
        public OverallKeyTappedHandler(
            MethodRunner runner)
        {
            this.runner = runner;
        }

        public virtual void Handle(
            LogStatisticsUi ui,
            string name)
        {
            var r = this.runner;
            r?.Run<LogStatistics>(stats =>
                {
                    r.Run<FilterSetter>(
                        setter =>
                        {
                            setter.Set(
                                ui,
                                name);
                        });

                    stats.ComputeOverall();

                    r.Run<UiReaderWriter, Labels>(
                        (uiRW, labels) =>
                        {
                            uiRW.Write(
                                ui,
                                () =>
                                {
                                    if (ui == null)
                                    {
                                        return;
                                    }

                                    ui.Title = labels.Overall;
                                });
                        });

                    r.Run<StatsDisplayer>(sd =>
                    {
                        const bool falsity = false;
                        sd.Display(
                            ui,
                            stats,
                            falsity);
                    });
                },
                name);
        }

        protected readonly MethodRunner runner;
    }
}