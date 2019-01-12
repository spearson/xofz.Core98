namespace xofz.Framework.Login
{
    using xofz.UI;

    public class SetupHandler
    {
        public SetupHandler(
            MethodWeb web)
        {
            this.web = web;
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
            var w = this.web;
            w.Run<UiReaderWriter>(uiRW =>
            {
                uiRW.Write(
                    ui,
                    () =>
                    {
                        ui.TimeRemaining = "Not logged in";
                        ui.KeyboardKeyVisible = false;
                    });

                w.Run<KeyboardLoader>(loader =>
                {
                    uiRW.Write(
                        ui,
                        () => ui.KeyboardKeyVisible = true);
                });

                w.Run<AccessController>(ac =>
                {
                    var cal = ac.CurrentAccessLevel;
                    uiRW.Write(
                        ui,
                        () => ui.CurrentAccessLevel = cal);
                });

                w.Run<xofz.Framework.Timer>(t =>
                    {
                        t.Start(1000);
                    },
                    "LoginTimer");
            });
        }

        protected readonly MethodWeb web;
    }
}
