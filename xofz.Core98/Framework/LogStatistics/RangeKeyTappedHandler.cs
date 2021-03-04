namespace xofz.Framework.LogStatistics
{
    using xofz.Framework.Logging;
    using xofz.UI;

    public class RangeKeyTappedHandler
    {
        public RangeKeyTappedHandler(
            MethodRunner runner)
        {
            this.runner = runner;
        }

        public virtual void Handle(
            LogStatisticsUi ui,
            string name)
        {
            var r = this.runner;
            r?.Run<UiReaderWriter>(uiRW =>
            {
                r.Run<FilterSetter>(fs =>
                {
                    fs.Set(ui, name);
                });

                r.Run<
                    LogStatistics,
                    SettingsHolder,
                    Labels>(
                    (stats, settings, labels) =>
                    {
                        var startNullable = uiRW.Read(
                            ui,
                            () => ui?.StartDate);
                        var endNullable = uiRW.Read(
                            ui,
                            () => ui?.EndDate);
                        if (startNullable == null || endNullable == null)
                        {
                            return;
                        }

                        var start = startNullable.Value;
                        var end = endNullable.Value;
                        stats.ComputeRange(
                            start, end);

                        var df = settings.DateFormat;
                        var typeInfo = labels.Range(
                            start,
                            end,
                            df);
                        uiRW.Write(
                            ui,
                            () =>
                            {
                                if (ui == null)
                                {
                                    return;
                                }

                                ui.Title = typeInfo;
                            });

                        r.Run<StatsDisplayer>(sd =>
                        {
                            sd.Display(ui, stats, false);
                        });
                    },
                    name,
                    name);
            });
        }

        protected readonly MethodRunner runner;
    }
}
