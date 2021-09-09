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
            r?.Run<FieldHolder>(holder =>
                {
                    const byte one = 1;
                    if (Interlocked.Read(ref holder.startedIf1)
                        == one)
                    {
                        r.Run<EntryReloader>(reloader =>
                        {
                            reloader.Reload(ui, name);
                        });
                        return;
                    }

                    Interlocked.Exchange(
                        ref holder.refreshOnStartIf1,
                        1);
                },
                name);
        }

        protected readonly MethodRunner runner;
    }
}
