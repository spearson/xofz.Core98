namespace xofz.Presentation
{
    using System;
    using System.Collections.Generic;
    using System.Globalization;
    using System.Threading;
    using Framework;
    using Framework.Materialization;
    using UI;

    public sealed class LogPresenter : Presenter
    {
        public LogPresenter(
            LogUi ui, 
            ShellUi shell,
            MethodWeb web)
            : base(ui, shell)
        {
            this.ui = ui;
            this.web = web;
            this.locker = new object();
        }

        public void Setup(
            AccessLevel editLevel, 
            bool resetOnStart = false,
            bool statisticsEnabled = false)
        {
            if (Interlocked.CompareExchange(ref this.setupIf1, 1, 0) == 1)
            {
                return;
            }

            var w = this.web;
            this.editLevel = editLevel;
            this.resetOnStart = resetOnStart;
            this.ui.StartDateChanged += this.ui_DateChanged;
            this.ui.EndDateChanged += this.ui_DateChanged;
            this.ui.AddKeyTapped += this.ui_AddKeyTapped;
            this.ui.StatisticsKeyTapped += this.ui_StatisticsKeyTapped;
            this.ui.FilterTextChanged += this.ui_FilterTextChanged;
            this.resetDatesAndFilters();
            UiHelpers.Write(this.ui, () =>
            {
                this.ui.AddKeyVisible = false;
                this.ui.StatisticsKeyVisible = statisticsEnabled;
            });
            this.ui.WriteFinished.WaitOne();

            w.Run<Log>(l => l.EntryWritten += this.log_EntryWritten);
            new Thread(this.timer_Elapsed).Start();

            w.Run<xofz.Framework.Timer>(
                t =>
                {
                    t.Elapsed += this.timer_Elapsed;
                    t.Start(1000);
                },
                "LogTimer");
            w.Run<Navigator>(n => n.RegisterPresenter(this));
        }

        public override void Start()
        {
            if (Interlocked.Read(ref this.setupIf1) == 0)
            {
                return;
            }

            if (this.resetOnStart)
            {
                this.resetDatesAndFilters();
            }

            base.Start();
        }

        private void resetDatesAndFilters()
        {
            var today = DateTime.Today;
            var lastWeek = today.Subtract(TimeSpan.FromDays(6));
            UiHelpers.Write(this.ui, () =>
            {
                this.ui.StartDate = lastWeek;
                this.ui.EndDate = today;
                this.ui.FilterContent = string.Empty;
                this.ui.FilterType = string.Empty;
            });
            this.ui.WriteFinished.WaitOne();
        }

        private void ui_DateChanged()
        {
            this.reloadEntries();
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
                var ll = new LinkedList<LogEntry>();
                foreach (var entry in l.ReadEntries())
                {
                    if (entry.Timestamp < start
                        || entry.Timestamp >= end.AddDays(1))
                    {
                        continue;
                    }

                    if (!string.IsNullOrEmpty(filterContent))
                    {
                        var contains = false;
                        foreach (var line in entry.Content)
                        {
                            if (line.Contains(filterContent))
                            {
                                contains = true;
                                break;
                            }
                        }

                        if (!contains)
                        {
                            continue;
                        }
                    }

                    if (!string.IsNullOrEmpty(filterType))
                    {
                        if (!entry.Type.Contains(filterType))
                        {
                            continue;
                        }
                    }

                    ll.AddLast(entry);
                }

                var array = new LogEntry[ll.Count];
                ll.CopyTo(array, 0);
                Array.Reverse(array);
                var ll2 = new LinkedList<
                    Tuple<string, string, string>>();
                foreach (var entry in array)
                {
                    ll2.AddLast(this.createTuple(entry));
                }

                var uiEntries = new LinkedListMaterializedEnumerable<
                    Tuple<string, string, string>>(ll2);

                UiHelpers.Write(
                    this.ui, 
                    () => this.ui.Entries = uiEntries);
                this.ui.WriteFinished.WaitOne();
            });
        }

        private void ui_AddKeyTapped()
        {
            var w = this.web;
            w.Run<Navigator>(
                n => n.PresentFluidly<LogEditorPresenter>());
        }

        private void ui_StatisticsKeyTapped()
        {
            var w = this.web;
            w.Run<Navigator>(
                n => n.PresentFluidly<LogStatisticsPresenter>());
        }

        private void ui_FilterTextChanged()
        {
            this.reloadEntries();
        }

        private void log_EntryWritten(LogEntry e)
        {
            if (UiHelpers.Read(this.ui, () => this.ui.EndDate) < DateTime.Today)
            {
                return;
            }

            lock (this.locker)
            {
                var newEntries = new LinkedList<Tuple<string, string, string>>(
                    UiHelpers.Read(this.ui, () => this.ui.Entries));
                newEntries.AddFirst(this.createTuple(e));

                UiHelpers.Write(this.ui, () => this.ui.Entries =
                    new LinkedListMaterializedEnumerable<
                        Tuple<string, string, string>>(newEntries));
                this.ui.WriteFinished.WaitOne();
            }
        }

        private Tuple<string, string, string> createTuple(LogEntry e)
        {
            var contentArray = new string[e.Content.Count];
            var en = e.Content.GetEnumerator();
            for (var i = 0; i < contentArray.Length; ++i)
            {
                en.MoveNext();
                contentArray[i] = en.Current;
            }

            en.Dispose();
            return Tuple.Create(
                e.Timestamp.ToString("yyyy/MM/dd HH:mm.ss", CultureInfo.CurrentCulture),
                e.Type,
                string.Join(
                    Environment.NewLine, 
                    contentArray));
        }

        private void timer_Elapsed()
        {
            var w = this.web;
            var cal = w.Run<AccessController, AccessLevel>(
                ac => ac.CurrentAccessLevel);
            var visible = cal >= this.editLevel;
            UiHelpers.Write(this.ui, () => this.ui.AddKeyVisible = visible);
        }

        private long setupIf1;
        private bool resetOnStart;
        private AccessLevel editLevel;
        private readonly LogUi ui;
        private readonly MethodWeb web;
        private readonly object locker;
    }
}
