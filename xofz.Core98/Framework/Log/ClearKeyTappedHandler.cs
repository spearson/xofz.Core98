namespace xofz.Framework.Log
{
    using xofz.Framework.Logging;
    using xofz.UI;

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
            r.Run<
                SettingsHolder,
                Messenger,
                UiReaderWriter>((settings, m, uiRW) =>
                {
                    computeBackupLocation = settings.ComputeBackupLocation;
                    if (computeBackupLocation == null)
                    {
                        response = uiRW.Read(
                            m.Subscriber,
                            () => m.Question(
                                @"Really clear the log? "
                                + @"A backup will NOT be created."));
                        return;
                    }

                    response = uiRW.Read(
                        m.Subscriber,
                        () => m.Question(
                            @"Clear log? "
                            + @"A backup will be created."));
                },
                name);

            if (response != Response.Yes)
            {
                return;
            }

            r.Run<SettingsHolder, LogEditor>((settings, le) =>
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
                                @"The log was cleared.  A backup "
                                + @"was created at " + bl + '.'
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
                            @"The log was cleared."
                        });
                },
                name,
                name);
        }

        protected readonly MethodRunner runner;
    }
}
