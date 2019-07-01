namespace xofz.UI.Forms
{
    using System;
    using System.Threading;
    using System.Windows.Forms;

    public partial class FormLogStatisticsUi 
        : FormUi, LogStatisticsUiV2
    {
        public FormLogStatisticsUi(
            Form shell)
        {
            this.shell = shell;

            this.InitializeComponent();

            var h = this.Handle;
        }

        private FormLogStatisticsUi()
        {
            this.InitializeComponent();
        }

        public event Do OverallKeyTapped;

        public event Do RangeKeyTapped;

        public event Do HideKeyTapped;

        public event Do ResetContentKeyTapped;

        public event Do ResetTypeKeyTapped;

        string LogStatisticsUiV2.StartLabelLabel
        {
            get => this.startLabel.Text;

            set => this.startLabel.Text = value;
        }

        string LogStatisticsUiV2.EndLabelLabel
        {
            get => this.endLabel.Text;

            set => this.endLabel.Text = value;
        }

        string LogStatisticsUiV2.HideKeyLabel
        {
            get => this.hideKey.Text;

            set => this.hideKey.Text = value;
        }

        string LogStatisticsUiV2.OverallKeyLabel
        {
            get => this.overallKey.Text;

            set => this.overallKey.Text = value;
        }

        string LogStatisticsUiV2.RangeKeyLabel
        {
            get => this.rangeKey.Text;

            set => this.rangeKey.Text = value;
        }

        string LogStatisticsUiV2.FilterContentLabelLabel
        {
            get => this.filterContentLabel.Text;

            set => this.filterContentLabel.Text = value;
        }

        string LogStatisticsUiV2.FilterTypeLabelLabel
        {
            get => this.filterTypeLabel.Text;

            set => this.filterTypeLabel.Text = value;
        }

        string LogStatisticsUiV2.ResetContentKeyLabel
        {
            get => this.resetContentKey.Text;

            set => this.resetContentKey.Text = value;
        }

        string LogStatisticsUiV2.ResetTypeKeyLabel
        {
            get => this.resetTypeKey.Text;

            set => this.resetTypeKey.Text = value;
        }

        string LogStatisticsUiV2.StatsContainerLabel
        {
            get => this.statsContainer.Text;

            set => this.statsContainer.Text = value;
        }

        string LogStatisticsUiV2.TotalEntryCountLabelLabelLabel
        {
            get => this.totalEntryCountLabelLabel.Text;

            set => this.totalEntryCountLabelLabel.Text = value;
        }

        string LogStatisticsUiV2.AvgEntriesPerDayLabelLabelLabel
        {
            get => this.avgEntriesPerDayLabelLabel.Text;

            set => this.avgEntriesPerDayLabelLabel.Text = value;
        }

        string LogStatisticsUiV2.OldestTimestampLabelLabelLabel
        {
            get => this.oldestTimestampLabelLabel.Text;

            set => this.oldestTimestampLabelLabel.Text = value;
        }

        string LogStatisticsUiV2.NewestTimestampLabelLabelLabel
        {
            get => this.newestTimestampLabelLabel.Text;

            set => this.newestTimestampLabelLabel.Text = value;
        }

        string LogStatisticsUiV2.EarliestTimestampLabelLabelLabel
        {
            get => this.earliestTimestampLabelLabel.Text;

            set => this.earliestTimestampLabelLabel.Text = value;
        }

        string LogStatisticsUiV2.LatestTimestampLabelLabelLabel
        {
            get => this.latestTimestampLabelLabel.Text;

            set => this.latestTimestampLabelLabel.Text = value;
        }

        DateTime LogStatisticsUi.StartDate
        {
            get => this.startDatePicker.SelectionStart;

            set
            {
                var sdp = this.startDatePicker;
                sdp.SelectionStart = value;
                sdp.SelectionEnd = value;
            }
        }

        DateTime LogStatisticsUi.EndDate
        {
            get => this.endDatePicker.SelectionStart;

            set
            {
                var edp = this.endDatePicker;
                edp.SelectionStart = value;
                edp.SelectionEnd = value;
            }
        }

        string LogStatisticsUi.FilterContent
        {
            get => this.filterContentTextBox.Text;

            set
            {
                var fctb = this.filterContentTextBox;
                fctb.Text = value;
                fctb.Focus();
            }
        }

        string LogStatisticsUi.FilterType
        {
            get => this.filterTypeTextBox.Text;

            set
            {
                var fttb = this.filterTypeTextBox;
                fttb.Text = value;
                fttb.Focus();
            }
        }

        string LogStatisticsUi.Title
        {
            get => this.statsContainer.Text;

            set => this.statsContainer.Text = value;
        }

        string LogStatisticsUi.TotalEntryCount
        {
            get => this.totalEntryCountLabel.Text;

            set => this.totalEntryCountLabel.Text = value;
        }

        string LogStatisticsUi.AvgEntriesPerDay
        {
            get => this.avgEntriesPerDayLabel.Text;

            set => this.avgEntriesPerDayLabel.Text = value;
        }

        string LogStatisticsUi.OldestTimestamp
        {
            get => this.oldestTimestampLabel.Text;

            set => this.oldestTimestampLabel.Text = value;
        }

        string LogStatisticsUi.NewestTimestamp
        {
            get => this.newestTimestampLabel.Text;

            set => this.newestTimestampLabel.Text = value;
        }

        string LogStatisticsUi.EarliestTimestamp
        {
            get => this.earliestTimestampLabel.Text;

            set => this.earliestTimestampLabel.Text = value;
        }

        string LogStatisticsUi.LatestTimestamp
        {
            get => this.latestTimestampLabel.Text;

            set => this.latestTimestampLabel.Text = value;
        }

        string LabeledUi.Label
        {
            get => this.Text;

            set => this.Text = value;
        }

        private void this_FormClosing(
            object sender, 
            FormClosingEventArgs e)
        {
            e.Cancel = true;
            var hkt = this.HideKeyTapped;
            if (hkt == null)
            {
                return;
            }

            ThreadPool.QueueUserWorkItem(
                o => hkt.Invoke());
        }

        void PopupUi.Display()
        {
            var s = this.shell;
            if (s == null)
            {
                this.Show();
                return;
            }

            this.Location = s.Location;
            this.Show(s);
        }

        private void overallKey_Click(
            object sender, 
            EventArgs e)
        {
            var okt = this.OverallKeyTapped;
            if (okt == null)
            {
                return;
            }

            ThreadPool.QueueUserWorkItem(
                o => okt.Invoke());
        }

        private void rangeKey_Click(
            object sender, 
            EventArgs e)
        {
            var rkt = this.RangeKeyTapped;
            if (rkt == null)
            {
                return;
            }

            ThreadPool.QueueUserWorkItem(
                o => rkt.Invoke());
        }

        private void hideKey_Click(
            object sender, 
            EventArgs e)
        {
            var hkt = this.HideKeyTapped;
            if (hkt == null)
            {
                return;
            }

            ThreadPool.QueueUserWorkItem(
                o => hkt.Invoke());
        }

        private void resetContentKey_Click(
            object sender, 
            EventArgs e)
        {
            var rckt = this.ResetContentKeyTapped;
            if (rckt == null)
            {
                return;
            }

            ThreadPool.QueueUserWorkItem(
                o => rckt.Invoke());
        }

        private void resetTypeKey_Click(
            object sender,
            EventArgs e)
        {
            var rtkt = this.ResetTypeKeyTapped;
            if (rtkt == null)
            {
                return;
            }

            ThreadPool.QueueUserWorkItem(
                o => rtkt.Invoke());
        }

        protected readonly Form shell;
    }
}
