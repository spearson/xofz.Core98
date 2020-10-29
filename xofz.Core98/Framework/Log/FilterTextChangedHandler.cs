namespace xofz.Framework.Log
{
    using System.Threading;
    using xofz.UI;

    public class FilterTextChangedHandler
    {
        public FilterTextChangedHandler(
            MethodRunner runner)
        {
            this.runner = runner;
        }

        public virtual void Handle(
            LogUi ui,
            string name)
        {
            var r = this.runner;
            r.Run<FieldHolder>(fields =>
                {
                    if (Interlocked.Read(ref fields.startedIf1) == 1)
                    {
                        r.Run<EntryReloader>(reloader =>
                        {
                            reloader.Reload(ui, name);
                        });
                        return;
                    }

                    Interlocked.CompareExchange(
                        ref fields.refreshOnStartIf1,
                        1,
                        0);
                },
                name);
        }

        protected readonly MethodRunner runner;
    }
}
