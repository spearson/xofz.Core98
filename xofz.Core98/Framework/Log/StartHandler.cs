namespace xofz.Framework.Log
{
    using System.Collections.Generic;
    using System.Threading;
    using xofz.Framework.Logging;
    using xofz.UI;

    public class StartHandler
    {
        public StartHandler(
            MethodRunner runner)
        {
            this.runner = runner;
        }

        public virtual void Handle(
            LogUi ui,
            Do unsubscribe,
            Do subscribe,
            string name)
        {
            var r = this.runner;
            r.Run<FieldHolder, Log, SettingsHolder>(
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
                        r.Run<EntryReloader>(reloader =>
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
                        r.Run<EntryReloader>(reloader =>
                        {
                            reloader.Reload(ui, name);
                        });
                        return;
                    }

                    if (settings.ResetOnStart)
                    {
                        r.Run<DateAndFilterResetter>(resetter =>
                        {
                            resetter.Reset(
                                ui,
                                unsubscribe,
                                subscribe,
                                name);
                        });
                        return;
                    }

                    r.Run<
                        ICollection<LogEntry>,
                        UiReaderWriter,
                        EntryConverter>(
                        (refreshEntries, uiRW, converter) =>
                        {
                            foreach (var entry in refreshEntries)
                            {
                                var xt = converter.Convert(entry, name);
                                uiRW.WriteSync(
                                    ui,
                                    () =>
                                    {
                                        ui.AddToTop(xt);
                                    });
                            }
                        },
                        name);
                },
                name,
                name,
                name);
        }

        protected readonly MethodRunner runner;
    }
}
