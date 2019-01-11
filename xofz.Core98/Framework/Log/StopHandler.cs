namespace xofz.Framework.Log
{
    using System.Threading;
    using xofz.UI;

    public class StopHandler
    {
        public StopHandler(MethodWeb web)
        {
            this.web = web;
        }

        public virtual void Handle(
            LogUi ui,
            string name)
        {
            var w = this.web;
            w.Run<FieldHolder>(holder =>
                {
                    Interlocked.CompareExchange(
                        ref holder.startedIf1,
                        0,
                        1);
                },
                name);
        }

        protected readonly MethodWeb web;
    }
}
