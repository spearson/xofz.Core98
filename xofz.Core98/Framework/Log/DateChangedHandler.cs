namespace xofz.Framework.Log
{
    using System.Collections.Generic;
    using System.Threading;
    using xofz.Framework.Logging;
    using xofz.UI;

    public class DateChangedHandler
    {
        public DateChangedHandler(
            MethodWeb web)
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
                    if (Interlocked.Read(ref holder.startedIf1) == 1)
                    {
                        w.Run<EntryReloader>(reloader =>
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

        protected readonly MethodWeb web;
    }
}
