namespace xofz.Presentation
{
    using System;
    using System.Collections.Generic;
    using System.Globalization;
    using System.Threading;
    using Framework;
    using UI;
    using xofz.Framework.Logging;

    public sealed class LogPresenter : NamedPresenter
    {
        public LogPresenter(
            LogUi ui,
            ShellUi shell,
            MethodWeb web)
            : base(ui, shell)
        {
            this.ui = ui;
            this.web = web;
            this.entriesToAddOnRefresh = new LinkedList<LogEntry>();
        }

        public void Setup()
        {
            if (Interlocked.CompareExchange(ref this.setupIf1, 1, 0) == 1)
            {
                return;
            }

            var w = this.web;
            var addKeyVisible = true;
            var clearKeyVisible = true;
            var statisticsKeyVisible = true;
            w.Run<LogSettings>(settings =>
                {
                    addKeyVisible = settings.EditLevel == AccessLevel.None;
                    clearKeyVisible = settings.ClearLevel == AccessLevel.None;
                    statisticsKeyVisible = settings.StatisticsEnabled;
                },
                this.Name);
            var today = DateTime.Today;
            var lastWeek = today.Subtract(TimeSpan.FromDays(6));
            UiHelpers.WriteSync(this.ui, () =>
            {
                this.ui.AddKeyVisible = addKeyVisible;
                this.ui.ClearKeyVisible = clearKeyVisible;
                this.ui.StatisticsKeyVisible = statisticsKeyVisible;
                this.ui.StartDate = lastWeek;
                this.ui.EndDate = today;
                this.ui.FilterType = string.Empty;
                this.ui.FilterContent = string.Empty;
            });

            var subscriberRegistered = false;
            w.Run<EventSubscriber>(subscriber =>
            {
                subscriberRegistered = true;
                subscriber.Subscribe(
                    this.ui,
                    nameof(this.ui.StartDateChanged),
                    this.ui_DateChanged);
                subscriber.Subscribe(
                    this.ui,
                    nameof(this.ui.EndDateChanged),
                    this.ui_DateChanged);
                subscriber.Subscribe(
                    this.ui,
                    nameof(this.ui.AddKeyTapped),
                    this.ui_AddKeyTapped);
                subscriber.Subscribe(
                    this.ui,
                    nameof(this.ui.ClearKeyTapped),
                    this.ui_ClearKeyTapped);
                subscriber.Subscribe(
                    this.ui,
                    nameof(this.ui.StatisticsKeyTapped),
                    this.ui_StatisticsKeyTapped);
                subscriber.Subscribe(
                    this.ui,
                    nameof(this.ui.FilterTextChanged),
                    this.ui_FilterTextChanged);
                w.Run<Log>(l =>
                        subscriber.Subscribe<LogEntry>(
                            l,
                            nameof(l.EntryWritten),
                            this.log_EntryWritten),
                    this.Name);
                w.Run<AccessController>(ac =>
                    subscriber.Subscribe<AccessLevel>(
                        ac,
                        nameof(ac.AccessLevelChanged),
                        this.accessLevelChanged));
            });

            if (!subscriberRegistered)
            {
                this.ui.StartDateChanged += this.ui_DateChanged;
                this.ui.EndDateChanged += this.ui_DateChanged;
                this.ui.AddKeyTapped += this.ui_AddKeyTapped;
                this.ui.ClearKeyTapped += this.ui_ClearKeyTapped;
                this.ui.StatisticsKeyTapped += this.ui_StatisticsKeyTapped;
                this.ui.FilterTextChanged += this.ui_FilterTextChanged;
                w.Run<Log>(
                    l => l.EntryWritten += this.log_EntryWritten,
                    this.Name);
                w.Run<AccessController>(ac =>
                    ac.AccessLevelChanged += this.accessLevelChanged);
            }
            
            w.Run<Navigator>(n => n.RegisterPresenter(this));
        }

        public override void Start()
        {
            if (Interlocked.Read(ref this.setupIf1) == 0)
            {
                return;
            }

            Interlocked.CompareExchange(ref this.startedIf1, 1, 0);
            base.Start();

            if (Interlocked.CompareExchange(
                    ref this.startedFirstTimeIf1, 1, 0) == 0)
            {
                this.reloadEntries();
                return;
            }

            if (Interlocked.CompareExchange(
                    ref this.refreshOnStartIf1, 0, 1) == 1)
            {
                this.reloadEntries();
                return;
            }

            var w = this.web;
            w.Run<LogSettings>(settings =>
                {
                    if (settings.ResetOnStart)
                    {
                        this.resetDatesAndFilters();
                    }
                },
                this.Name);

            this.insertNewEntries();
            this.entriesToAddOnRefresh.Clear();
        }

        public override void Stop()
        {
            Interlocked.CompareExchange(
                ref this.startedIf1, 0, 1);
        }

        private void resetDatesAndFilters()
        {
            while (Interlocked.CompareExchange(ref this.resettingIf1, 1, 0) == 1)
            {
                Thread.Sleep(0);
            }

            var w = this.web;
            var today = DateTime.Today;
            var lastWeek = today.Subtract(TimeSpan.FromDays(6));
            var needsReload = true;
            var started = Interlocked.Read(ref this.startedIf1) == 1;
            if (UiHelpers.Read(this.ui, () => this.ui.StartDate)
                == lastWeek
                && UiHelpers.Read(this.ui, () => this.ui.EndDate)
                == today
                && UiHelpers.Read(this.ui, () => this.ui.FilterContent)
                == string.Empty
                && UiHelpers.Read(this.ui, () => this.ui.FilterType)
                == string.Empty)
            {
                if (started && Interlocked.Read(
                        ref this.startedFirstTimeIf1) == 1)
                {
                    needsReload = false;
                }
            }

            Interlocked.CompareExchange(
                ref this.refreshOnStartIf1, 0, 1);
            if (started && needsReload)
            {
                w.Run<EventSubscriber>(subscriber =>
                {
                    subscriber.Unsubscribe(
                        this.ui,
                        nameof(this.ui.StartDateChanged),
                        this.ui_DateChanged);
                    subscriber.Unsubscribe(
                        this.ui,
                        nameof(this.ui.EndDateChanged),
                        this.ui_DateChanged);
                    subscriber.Unsubscribe(
                        this.ui,
                        nameof(this.ui.FilterTextChanged),
                        this.ui_FilterTextChanged);
                });
                UiHelpers.WriteSync(this.ui, () =>
                {
                    this.ui.StartDate = lastWeek;
                    this.ui.EndDate = today;
                    this.ui.FilterType = string.Empty;
                    this.ui.FilterContent = string.Empty;
                });
                this.reloadEntries();
                w.Run<EventSubscriber>(subscriber =>
                {
                    subscriber.Subscribe(
                        this.ui,
                        nameof(this.ui.StartDateChanged),
                        this.ui_DateChanged);
                    subscriber.Subscribe(
                        this.ui,
                        nameof(this.ui.EndDateChanged),
                        this.ui_DateChanged);
                    subscriber.Subscribe(
                        this.ui,
                        nameof(this.ui.FilterTextChanged),
                        this.ui_FilterTextChanged);
                });
            }

            Interlocked.CompareExchange(ref this.resettingIf1, 0, 1);
        }

        private void ui_DateChanged()
        {
            if (Interlocked.Read(ref this.startedIf1) == 1)
            {
                this.reloadEntries();
                return;
            }

            Interlocked.CompareExchange(
                ref this.refreshOnStartIf1, 1, 0);
        }

        private void insertNewEntries()
        {
            var etaor = this.entriesToAddOnRefresh;
            foreach (var entry in etaor)
            {
                var tuple = this.createTuple(entry);
                UiHelpers.WriteSync(
                    this.ui,
                    () => this.ui.AddToTop(
                        tuple));
            }
        }

        private void reloadEntries()
        {
            var w = this.web;
            var start = UiHelpers.Read(this.ui, () => this.ui.StartDate);
            var end = UiHelpers.Read(this.ui, () => this.ui.EndDate);
            var filterContent = UiHelpers.Read(
                this.ui,
                () => this.ui.FilterContent);
            var filterType = UiHelpers.Read(
                this.ui,
                () => this.ui.FilterType);
            w.Run<Log>(l =>
                {
                    // first, begin reading all entries
                    var matchingEntries = l.ReadEntries();

                    // second, get all the entries in the date range
                    matchingEntries = EnumerableHelpers.Where(
                        matchingEntries,
                        e => e.Timestamp >= start
                             && e.Timestamp < end.AddDays(1));

                    // third, match on content
                    if (!StringHelpers.NullOrWhiteSpace(filterContent))
                    {
                        matchingEntries = EnumerableHelpers.Where(
                            matchingEntries,
                            e => EnumerableHelpers.Any(
                                e.Content,
                                s => s
                                    .ToLowerInvariant()
                                    .Contains(
                                        filterContent.ToLowerInvariant())));
                    }

                    // fourth, match on type
                    if (!StringHelpers.NullOrWhiteSpace(filterType))
                    {
                        matchingEntries = EnumerableHelpers.Where(
                            matchingEntries,
                            e => e.Type.ToLowerInvariant()
                                .Contains(filterType.ToLowerInvariant()));
                    }

                    // finally, order them by newest first
                    matchingEntries = EnumerableHelpers.OrderByDescending(
                        matchingEntries,
                        e => e.Timestamp);

                    ICollection<XTuple<string, string, string>> uiEntries
                        = new LinkedList<XTuple<string, string, string>>(
                            EnumerableHelpers.Select(
                                matchingEntries,
                                this.createTuple));

                    UiHelpers.WriteSync(
                        this.ui,
                        () => this.ui.Entries = uiEntries);
                    this.entriesToAddOnRefresh.Clear();
                },
                this.Name);
        }

        private bool passesDatesAndFilters(LogEntry e)
        {
            var startDate = UiHelpers.Read(
                this.ui,
                () => this.ui.StartDate);
            var endDate = UiHelpers.Read(
                this.ui,
                () => this.ui.EndDate);
            if (e.Timestamp < startDate)
            {
                return false;
            }

            if (e.Timestamp > endDate.AddDays(1))
            {
                return false;
            }

            var filterType = UiHelpers.Read(
                this.ui,
                () => this.ui.FilterType);
            if (!(e.Type?.Contains(filterType)
                  ?? false))
            {
                return false;
            }

            var filterContent = UiHelpers.Read(
                this.ui,
                () => this.ui.FilterContent);
            if (EnumerableHelpers.All(
                e.Content,
                s => !s.ToLowerInvariant().Contains(
                    filterContent.ToLowerInvariant())))
            {
                return false;
            }

            return true;
        }

        private void ui_AddKeyTapped()
        {
            var w = this.web;
            w.Run<Navigator>(
                n => n.PresentFluidly<LogEditorPresenter>(
                    this.Name));
        }

        private void ui_ClearKeyTapped()
        {
            var w = this.web;
            var response = Response.No;
            w.Run<LogSettings, Messenger>((settings, m) =>
                {
                    if (settings.ComputeBackupLocation ==
                        default(Gen<string>))
                    {
                        response = UiHelpers.Read(
                            m.Subscriber,
                            () => m.Question(
                                "Really clear the log? "
                                + "A backup will not be created."));
                        return;
                    }

                    response = UiHelpers.Read(
                        m.Subscriber,
                        () => m.Question(
                            "Clear log? "
                            + "A backup will be created."));
                },
                this.Name);

            if (response != Response.Yes)
            {
                return;
            }

            w.Run<LogSettings, LogEditor>((settings, le) =>
                {
                    var cbl = settings.ComputeBackupLocation;
                    if (cbl != default(Gen<string>))
                    {
                        var bl = cbl();
                        try
                        {
                            le.Clear(bl);
                        }
                        catch
                        {
                            return;
                        }

                        this.reloadEntries();
                        le.AddEntry(
                            "Information",
                            new[]
                            {
                                    "The log was cleared.  A backup "
                                    + "was created at " + bl + "."
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

                    this.reloadEntries();
                    le.AddEntry(
                        "Information",
                        new[]
                        {
                                "The log was cleared."
                        });
                },
                this.Name,
                this.Name);
        }

        private void ui_StatisticsKeyTapped()
        {
            var w = this.web;
            w.Run<Navigator>(
                n => n.PresentFluidly<LogStatisticsPresenter>(
                    this.Name));
        }

        private void ui_FilterTextChanged()
        {
            if (Interlocked.Read(ref this.startedIf1) == 1)
            {
                this.reloadEntries();
                return;
            }

            Interlocked.CompareExchange(
                ref this.refreshOnStartIf1, 1, 0);
        }

        private void log_EntryWritten(LogEntry e)
        {
            if (UiHelpers.Read(this.ui, () => this.ui.EndDate) < DateTime.Today)
            {
                return;
            }

            if (Interlocked.Read(ref this.startedIf1) == 0)
            {
                if (Interlocked.Read(
                        ref this.startedFirstTimeIf1) == 1 &&
                    this.passesDatesAndFilters(e))
                {
                    this.entriesToAddOnRefresh.Add(e);
                }

                return;
            }

            if (this.passesDatesAndFilters(e))
            {
                var tuple = this.createTuple(e);
                UiHelpers.Write(
                    this.ui,
                    () => this.ui.AddToTop(tuple));
            }
        }

        private XTuple<string, string, string> createTuple(LogEntry e)
        {
            return XTuple.Create(
                e.Timestamp.ToString(
                    "yyyy/MM/dd HH:mm.ss.fffffff",
                    CultureInfo.CurrentCulture),
                e.Type,
                string.Join(
                    Environment.NewLine, 
                    EnumerableHelpers.ToArray(e.Content)));
        }

        private void accessLevelChanged(AccessLevel newAccessLevel)
        {
            var w = this.web;
            var addKeyVisible = false;
            var clearKeyVisible = false;
            w.Run<LogSettings>(settings =>
                {
                    addKeyVisible = newAccessLevel >= settings.EditLevel;
                    clearKeyVisible = newAccessLevel >= settings.ClearLevel;
                },
                this.Name);
            UiHelpers.Write(
                this.ui,
                () =>
                {
                    this.ui.AddKeyVisible = addKeyVisible;
                    this.ui.ClearKeyVisible = clearKeyVisible;
                });
        }

        private long
            setupIf1,
            startedIf1,
            refreshOnStartIf1,
            startedFirstTimeIf1,
            resettingIf1;
        private readonly LogUi ui;
        private readonly MethodWeb web;
        private readonly ICollection<LogEntry> entriesToAddOnRefresh;
    }
}
