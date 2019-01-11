namespace xofz.Framework.Log
{
    using xofz.UI;

    public class AccessLevelChangedHandler
    {
        public AccessLevelChangedHandler(
            MethodWeb web)
        {
            this.web = web;
        }
        public virtual void Handle(
            LogUi ui,
            AccessLevel newLevel,
            string name)
        {
            var w = this.web;
            w.Run<SettingsHolder, UiReaderWriter>(
                (settings, uiRW) =>
                {
                    var akv = newLevel >= settings.EditLevel;
                    var ckv = newLevel >= settings.ClearLevel;
                    uiRW.Write(
                        ui,
                        () =>
                        {
                            ui.AddKeyVisible = akv;
                            ui.ClearKeyVisible = ckv;
                        });
                },
                name);
        }

        protected readonly MethodWeb web;
    }
}
