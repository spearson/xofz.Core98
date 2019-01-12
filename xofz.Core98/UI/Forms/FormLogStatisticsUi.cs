namespace xofz.UI.Forms
{
    using System;
    using System.Drawing;
    using System.Threading;
    using System.Windows.Forms;

    public partial class FormLogStatisticsUi : FormUi, LogStatisticsUi
    {
        public FormLogStatisticsUi(Form shell)
        {
            this.shell = shell;

            this.InitializeComponent();

            var h = this.Handle;
        }

        public event Do OverallKeyTapped;

        public event Do RangeKeyTapped;

        public event Do HideKeyTapped;

        public event Do ResetContentKeyTapped;

        public event Do ResetTypeKeyTapped;

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
            get => this.groupBox.Text;

            set => this.groupBox.Text = value;
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
            this.Location = new Point(
                s.Location.X,
                s.Location.Y);
            this.Visible = true;
        }

        private void overallKey_Click(object sender, EventArgs e)
        {
            var okt = this.OverallKeyTapped;
            if (okt == null)
            {
                return;
            }

            ThreadPool.QueueUserWorkItem(
                o => okt.Invoke());
        }

        private void rangeKey_Click(object sender, EventArgs e)
        {
            var rkt = this.RangeKeyTapped;
            if (rkt == null)
            {
                return;
            }

            ThreadPool.QueueUserWorkItem(
                o => rkt.Invoke());
        }

        private void hideKey_Click(object sender, EventArgs e)
        {
            var hkt = this.HideKeyTapped;
            if (hkt == null)
            {
                return;
            }

            ThreadPool.QueueUserWorkItem(
                o => hkt.Invoke());
        }

        private void resetContentKey_Click(object sender, EventArgs e)
        {
            var rckt = this.ResetContentKeyTapped;
            if (rckt == null)
            {
                return;
            }

            ThreadPool.QueueUserWorkItem(
                o => rckt.Invoke());
        }

        private void resetTypeKey_Click(object sender, EventArgs e)
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
