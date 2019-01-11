namespace xofz.Framework.Log
{
    using System;
    using xofz.UI;

    public class SetupHandler
    {
        public SetupHandler(
            MethodWeb web)
        {
            this.web = web;
        }

        public virtual void Handle(
            LogUi ui,
            string name)
        {
            var w = this.web;
            w.Run<SettingsHolder, UiReaderWriter>((settings, uiRW) =>
                {
                    var addKeyVisible = settings.EditLevel == AccessLevel.None;
                    var clearKeyVisible = settings.ClearLevel == AccessLevel.None;
                    var statisticsKeyVisible = settings.StatisticsEnabled;
                    var today = DateTime.Today;
                    var lastWeek = today.Subtract(TimeSpan.FromDays(6));

                    uiRW.WriteSync(ui, () =>
                    {
                        ui.AddKeyVisible = addKeyVisible;
                        ui.ClearKeyVisible = clearKeyVisible;
                        ui.StatisticsKeyVisible = statisticsKeyVisible;
                        ui.StartDate = lastWeek;
                        ui.EndDate = today;
                        ui.FilterType = string.Empty;
                        ui.FilterContent = string.Empty;
                    });
                },
                name);
        }

        protected readonly MethodWeb web;
    }
}
