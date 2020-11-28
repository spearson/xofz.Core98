namespace xofz.Framework.LogStatistics
{
    using xofz.Framework.Logging;
    using xofz.UI;

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
                        setter => setter.Set(
                            ui, name));
                    stats.ComputeOverall();
                    r.Run<UiReaderWriter, Labels>(
                        (uiRW, labels) =>
                        {
                            uiRW.Write(
                                ui,
                                () =>
                                {
                                    ui.Title = labels.Overall;
                                });
                        });
                    r.Run<StatsDisplayer>(sd =>
                    {
                        sd.Display(ui, stats, false);
                    });
                },
                name);
        }

        protected readonly MethodRunner runner;
    }
}
