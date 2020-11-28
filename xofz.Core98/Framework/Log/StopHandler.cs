namespace xofz.Framework.Log
{
    using System.Threading;
    using xofz.UI;

    public class StopHandler
    {
        public StopHandler(
            MethodRunner runner)
        {
            this.runner = runner;
        }

        public virtual void Handle(
            LogUi ui,
            string name)
        {
            var r = this.runner;
            r?.Run<FieldHolder>(fields =>
                {
                    Interlocked.Exchange(
                        ref fields.startedIf1,
                        0);
                },
                name);
        }

        protected readonly MethodRunner runner;
    }
}
