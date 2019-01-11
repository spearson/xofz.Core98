namespace xofz.Framework.Log
{
    using System.Collections.Generic;
    using System.Threading;
    using xofz.Framework.Logging;
    using xofz.UI;

    public class StartHandler
    {
        public StartHandler(
            MethodWeb web)
        {
            this.web = web;
        }

        public virtual void Handle(
            LogUi ui,
            Do unsubscribe,
            Do subscribe,
            string name)
        {
            var w = this.web;
            w.Run<FieldHolder, Log, SettingsHolder>(
                (fields, log, settings) =>
                {
                    Interlocked.CompareExchange(
                        ref fields.startedIf1,
                        1,
                        0);

                    if (Interlocked.CompareExchange(
                            ref fields.startedFirstTimeIf1,
                            1,
                            0) != 1)
                    {
                        w.Run<EntryReloader>(reloader =>
                        {
                            reloader.Reload(ui, name);
                        });
                        return;
                    }

                    if (Interlocked.CompareExchange(
                            ref fields.refreshOnStartIf1,
                            0,
                            1) == 1)
                    {
                        w.Run<EntryReloader>(reloader =>
                        {
                            reloader.Reload(ui, name);
                        });
                        return;
                    }

                    if (settings.ResetOnStart)
                    {
                        w.Run<DateAndFilterResetter>(resetter =>
                        {
                            resetter.Reset(
                                ui,
                                unsubscribe,
                                subscribe,
                                name);
                        });
                        return;
                    }

                    w.Run<
                        ICollection<LogEntry>,
                        UiReaderWriter,
                        EntryConverter>(
                        (refreshEntries, uiRW, converter) =>
                        {
                            foreach (var entry in refreshEntries)
                            {
                                var xt = converter.Convert(entry);
                                uiRW.WriteSync(
                                    ui,
                                    () => ui.AddToTop(xt));
                            }
                        },
                        name);
                },
                name,
                name,
                name);
        }

        protected readonly MethodWeb web;
    }
}
