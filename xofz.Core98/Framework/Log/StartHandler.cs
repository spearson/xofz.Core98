namespace xofz.Framework.Log
{
    using System.Collections.Generic;
    using System.Threading;
    using xofz.Framework.Logging;
    using xofz.UI;
    using xofz.UI.Log;

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
            r?.Run<FieldHolder, Log, SettingsHolder>(
                (fields, log, settings) =>
                {
                    const byte one = 1;
                    Interlocked.Exchange(
                        ref fields.startedIf1,
                        one);
                    
                    if (Interlocked.Exchange(
                            ref fields.startedFirstTimeIf1,
                            one) != one)
                    {
                        r.Run<EntryReloader>(reloader =>
                        {
                            reloader.Reload(ui, name);
                        });
                        return;
                    }

                    const byte zero = 0;
                    if (Interlocked.Exchange(
                            ref fields.refreshOnStartIf1,
                            zero) == one)
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
                                        ui?.AddToTop(xt);
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
