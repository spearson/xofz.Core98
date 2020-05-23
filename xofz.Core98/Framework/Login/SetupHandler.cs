namespace xofz.Framework.Login
{
    using xofz.UI;

    public class SetupHandler
    {
        public SetupHandler(
            MethodRunner runner)
        {
            this.runner = runner;
        }
        
        /// <summary>
        /// Requires a UiReaderWriter, an AccessController, and
        /// a timer called LoginTimer registered with the MethodWeb
        /// </summary>
        /// <param name="ui">
        /// The LoginUi to set up</param>
        public virtual void Handle(
            LoginUi ui)
        {
            var r = this.runner;
            r.Run<Labels, UiReaderWriter>(
                (labels, uiRW) =>
            {
                uiRW.Write(
                    ui,
                    () =>
                    {
                        ui.TimeRemaining = labels.NotLoggedIn;
                        ui.KeyboardKeyVisible = false;
                    });

                r.Run<KeyboardLoader>(loader =>
                {
                    uiRW.Write(
                        ui,
                        () =>
                        {
                            ui.KeyboardKeyVisible = true;
                        });
                });

                r.Run<AccessController>(ac =>
                {
                    var cal = ac.CurrentAccessLevel;
                    uiRW.Write(
                        ui,
                        () =>
                        {
                            ui.CurrentAccessLevel = cal;
                        });
                });

                if (ui is LoginUiV2 v2)
                {
                    r.Run<LabelApplier>(applier =>
                    {
                        applier.Apply(
                            v2);
                    });
                }

                r.Run<xofz.Framework.Timer>(t =>
                    {
                        t.Start(1000);
                    },
                    DependencyNames.Timer);
            });
        }

        protected readonly MethodRunner runner;
    }
}
