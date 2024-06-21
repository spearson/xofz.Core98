namespace xofz.Framework.Log
{
    using xofz.UI;
    using xofz.UI.Log;

    public class DownKeyTappedHandler
    {
        public DownKeyTappedHandler(
            MethodRunner runner)
        {
            this.runner = runner;
        }

        public virtual void Handle(
            LogUiV3 ui,
            string name)
        {
            var r = this.runner;
            r?.Run<UiReaderWriter, KeyPresser>(
                (uiRW, presser) =>
                {
                    const string keySymbolToPress = @"{PGDN}";
                    uiRW.Write(
                        ui,
                        () =>
                        {
                            ui?.FocusEntries();

                            presser.Press(keySymbolToPress);
                        });
                });
        }

        protected readonly MethodRunner runner;
    }
}