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
            r?.Run<SettingsHolder, UiReaderWriter>(
                (settings, uiRW) =>
                {
                    const AccessLevel zeroAccess = AccessLevel.None;
                    var addKeyVisible = settings.EditLevel == zeroAccess;
                    var clearKeyVisible = settings.ClearLevel == zeroAccess;
                    var statisticsKeyVisible = settings.StatisticsEnabled;

                    const string emptyString = @"";
                    uiRW.WriteSync(ui, () =>
                    {
                        if (ui == null)
                        {
                            return;
                        }

                        ui.AddKeyVisible = addKeyVisible;
                        ui.ClearKeyVisible = clearKeyVisible;
                        ui.StatisticsKeyVisible = statisticsKeyVisible;
                        ui.FilterType = emptyString;
                        ui.FilterContent = emptyString;
                    });

                    r.Run<TimeProvider>(provider =>
                    {
                        const short daysToAdd = -6;
                        var today = provider.Now().Date;
                        var lastWeek = today
                            .AddDays(daysToAdd)
                            .Date;

                        uiRW.WriteSync(
                            ui,
                            () =>
                            {
                                if (ui == null)
                                {
                                    return;
                                }

                                ui.EndDate = today;
                                ui.StartDate = lastWeek;
                            });
                    });
                },
                name);

            if (ui is LogUiV2 v2)
            {
                r?.Run<LabelApplier>(applier =>
                {
                    applier.Apply(v2);
                });
            }

            if (ui is LogUiV3 v3)
            {
                r?.Run<CurrentWeekKeyTappedHandler>(handler =>
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
