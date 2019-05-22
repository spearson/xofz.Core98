namespace xofz.Framework.Log
{
    using xofz.UI;

    public class AccessLevelChangedHandler
    {
        public AccessLevelChangedHandler(
            MethodRunner runner)
        {
            this.runner = runner;
        }

        public virtual void Handle(
            LogUi ui,
            AccessLevel newLevel,
            string name)
        {
            var r = this.runner;
            r.Run<SettingsHolder, UiReaderWriter>(
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

        protected readonly MethodRunner runner;
    }
}
