namespace xofz.Framework.Log
{
    using System.Threading;
    using xofz.UI.Log;

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
            r?.Run<FieldHolder>(fields =>
                {
                    const byte one = 1;
                    if (Interlocked.Read(ref fields.startedIf1) == one)
                    {
                        r.Run<EntryReloader>(reloader =>
                        {
                            reloader.Reload(ui, name);
                        });
                        return;
                    }

                    Interlocked.Exchange(
                        ref fields.refreshOnStartIf1,
                        one);
                },
                name);
        }

        protected readonly MethodRunner runner;
    }
}
