namespace xofz.Framework.Log
{
    using xofz.Framework.Logging;
    using xofz.UI;

    public class ClearKeyTappedHandler
    {
        public ClearKeyTappedHandler(
            MethodWeb web)
        {
            this.web = web;
        }

        public virtual void Handle(
            LogUi ui,
            string name)
        {
            var w = this.web;
            var response = Response.No;
            Gen<string> computeBackupLocation = null;
            w.Run<
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
                                "Really clear the log? "
                                + "A backup will NOT be created."));
                        return;
                    }

                    response = uiRW.Read(
                        m.Subscriber,
                        () => m.Question(
                            "Clear log? "
                            + "A backup will be created."));
                },
                name);

            if (response != Response.Yes)
            {
                return;
            }

            w.Run<SettingsHolder, LogEditor>((settings, le) =>
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
                            "Information",
                            new[]
                            {
                                "The log was cleared.  A backup "
                                + "was created at " + bl + "."
                            });
                        w.Run<EntryReloader>(reloader =>
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

                    w.Run<EntryReloader>(reloader =>
                    {
                        reloader.Reload(ui, name);
                    });
                    le.AddEntry(
                        "Information",
                        new[]
                        {
                            "The log was cleared."
                        });
                },
                name,
                name);
        }

        protected readonly MethodWeb web;
    }
}
