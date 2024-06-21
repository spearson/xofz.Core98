namespace xofz.Framework.Log
{
    using xofz.Framework.Logging;
    using xofz.UI;
    using xofz.UI.Log;

    public class ClearKeyTappedHandler
    {
        public ClearKeyTappedHandler(
            MethodRunner runner)
        {
            this.runner = runner;
        }

        public virtual void Handle(
            LogUi ui,
            string name)
        {
            var r = this.runner;
            var response = Response.No;
            Gen<string> computeBackupLocation = null;
            r?.Run<
                SettingsHolder,
                Messenger,
                Labels,
                UiReaderWriter>(
                (settings, m, labels, uiRW) =>
                {
                    computeBackupLocation = settings.ComputeBackupLocation;
                    if (computeBackupLocation == null)
                    {
                        response = uiRW.Read(
                            m.Subscriber,
                            () => m.Question(
                                labels.ClearNoBackupQuestion));
                        return;
                    }

                    response = uiRW.Read(
                        m.Subscriber,
                        () => m.Question(
                            labels.ClearWithBackupQuestion));
                },
                name);

            if (response != Response.Yes)
            {
                return;
            }

            r?.Run<
                SettingsHolder,
                LogEditor,
                Labels>((settings, le, labels) =>
                {
                    if (computeBackupLocation != null)
                    {
                        var bl = computeBackupLocation();
                        try
                        {
                            le.Clear(bl);
                        }
                        catch
                        {
                            return;
                        }

                        le.AddEntry(
                            DefaultEntryTypes.Information,
                            new[]
                            {
                                labels.ClearedWithBackup(bl)
                            });
                        r.Run<EntryReloader>(reloader =>
                        {
                            reloader.Reload(ui, name);
                        });
                        return;
                    }

                    try
                    {
                        le.Clear();
                    }
                    catch
                    {
                        return;
                    }

                    r.Run<EntryReloader>(reloader =>
                    {
                        reloader.Reload(ui, name);
                    });
                    le.AddEntry(
                        DefaultEntryTypes.Information,
                        new[]
                        {
                            labels.ClearedNoBackup
                        });
                },
                name,
                name);
        }

        protected readonly MethodRunner runner;
    }
}