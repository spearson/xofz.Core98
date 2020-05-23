namespace xofz.Framework.Log
{
    using xofz.UI;

    public class SetupHandler
    {
        public SetupHandler(
            MethodRunner runner)
        {
            this.runner = runner;
        }

        public virtual void Handle(
            LogUi ui,
            string name)
        {
            var r = this.runner;
            r.Run<SettingsHolder, UiReaderWriter>(
                (settings, uiRW) =>
                {
                    var addKeyVisible = settings.EditLevel == AccessLevel.None;
                    var clearKeyVisible = settings.ClearLevel == AccessLevel.None;
                    var statisticsKeyVisible = settings.StatisticsEnabled;

                    uiRW.WriteSync(ui, () =>
                    {
                        ui.AddKeyVisible = addKeyVisible;
                        ui.ClearKeyVisible = clearKeyVisible;
                        ui.StatisticsKeyVisible = statisticsKeyVisible;
                        ui.FilterType = string.Empty;
                        ui.FilterContent = string.Empty;
                    });

                    r.Run<TimeProvider>(provider =>
                    {
                        var today = provider.Now().Date;
                        var lastWeek = today.AddDays(-6).Date;

                        uiRW.WriteSync(
                            ui,
                            () =>
                            {
                                ui.EndDate = today;
                                ui.StartDate = lastWeek;
                            });
                    });
                },
                name);

            if (ui is LogUiV2 v2)
            {
                r.Run<LabelApplier>(applier =>
                {
                    applier.Apply(v2);
                });
            }

            if (ui is LogUiV3 v3)
            {
                r.Run<CurrentWeekKeyTappedHandler>(handler =>
                {
                    handler.Handle(
                        v3,
                        name,
                        null,
                        null);
                });
            }
        }

        protected readonly MethodRunner runner;
    }
}
