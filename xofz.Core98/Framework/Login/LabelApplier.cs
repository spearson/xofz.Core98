namespace xofz.Framework.Login
{
    using xofz.UI;

    public class LabelApplier
    {
        public LabelApplier(
            MethodRunner runner)
        {
            this.runner = runner;
        }

        public virtual void Apply(
            LoginUiV2 ui)
        {
            var r = this.runner;
            r?.Run<UiReaderWriter, Labels>((uiRW, labels) =>
            {
                uiRW.Write(
                    ui,
                    () =>
                    {
                        if (ui == null)
                        {
                            return;
                        }

                        ui.PasswordLabel = labels.Password;
                        ui.TimeRemainingLabel = labels.TimeRemaining;
                        ui.BackspaceKeyLabel = labels.BackspaceKey;
                        ui.ClearKeyLabel = labels.ClearKey;
                        ui.LogInKeyLabel = labels.LogInKey;
                        ui.CancelKeyLabel = labels.CancelKey;
                        ui.KeyboardKeyLabel = labels.KeyboardKey;
                        ui.Title = labels.Title;
                    });
            });
        }

        protected readonly MethodRunner runner;
    }
}
