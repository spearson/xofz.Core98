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
            SettingsHolder settings)
        {
            var w = this.web;
            var addKeyVisible = settings.EditLevel == AccessLevel.None;
            var clearKeyVisible = settings.ClearLevel == AccessLevel.None;
            var statisticsKeyVisible = settings.StatisticsEnabled;
            var today = DateTime.Today;
            var lastWeek = today.Subtract(TimeSpan.FromDays(6));
            w.Run<UiReaderWriter>(uiRW =>
            {
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
            });
        }

        protected readonly MethodWeb web;
    }
}
