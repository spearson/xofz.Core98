namespace xofz.Framework.Log
{
    using System.Threading;
    using xofz.UI;

    public class DateRangeChangedHandler
    {
        public DateRangeChangedHandler(
            MethodRunner runner)
        {
            this.runner = runner;
        }

        public virtual void Handle(
            LogUi ui,
            string name)
        {
            var r = this.runner;
            r.Run<FieldHolder>(holder =>
                {
                    if (Interlocked.Read(ref holder.startedIf1) == 1)
                    {
                        r.Run<EntryReloader>(reloader =>
                        {
                            reloader.Reload(ui, name);
                        });
                        return;
                    }

                    Interlocked.CompareExchange(
                        ref holder.refreshOnStartIf1,
                        1,
                        0);
                },
                name);
        }

        protected readonly MethodRunner runner;
    }
}
