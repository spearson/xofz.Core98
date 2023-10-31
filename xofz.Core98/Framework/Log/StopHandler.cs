namespace xofz.Framework.Log
{
    using System.Threading;
    using xofz.UI.Log;

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
                    const byte zero = 0;
                    Interlocked.Exchange(
                        ref fields.startedIf1,
                        zero);
                },
                name);
        }

        protected readonly MethodRunner runner;
    }
}
