namespace xofz.Framework.Main
{
    using xofz.UI;

    public class ShutdownRequestedHandler
    {
        public ShutdownRequestedHandler(
            MethodWeb web)
        {
            this.web = web;
        }

        public virtual void Handle(
            MainUi ui,
            Do logIn,
            Do shutdown)
        {
            var w = this.web;
            var cal = AccessLevel.None;
            var shutdownLevel = AccessLevel.None;
            w.Run<AccessController, SettingsHolder>(
                (ac, s) =>
                {
                    cal = ac.CurrentAccessLevel;
                    shutdownLevel = s.ShutdownLevel;
                });

            if (cal >= shutdownLevel)
            {
                shutdown?.Invoke();
                return;
            }

            logIn?.Invoke();
            w.Run<AccessController>(ac =>
            {
                cal = ac.CurrentAccessLevel;
                if (cal >= shutdownLevel)
                {
                    shutdown?.Invoke();
                }
            });
        }

        protected readonly MethodWeb web;
    }
}
