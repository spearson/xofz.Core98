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

        public event Action OverallKeyTapped;

        public event Action RangeKeyTapped;

        public event Action HideKeyTapped;

        public event Action ResetContentKeyTapped;

        public event Action ResetTypeKeyTapped;

        DateTime LogStatisticsUi.StartDate
        {
            get => this.startDatePicker.SelectionStart;

            set
            {
                this.startDatePicker.SelectionStart = value;
                this.startDatePicker.SelectionEnd = value;
            }
        }

        DateTime LogStatisticsUi.EndDate
        {
            get => this.endDatePicker.SelectionStart;

            set
            {
                this.endDatePicker.SelectionStart = value;
                this.endDatePicker.SelectionEnd = value;
            }
        }

        string LogStatisticsUi.FilterContent
        {
            get => this.filterContentTextBox.Text;

            set
            {
                this.filterContentTextBox.Text = value;
                this.filterContentTextBox.Focus();
            }
        }

        string LogStatisticsUi.FilterType
        {
            get => this.filterTypeTextBox.Text;

            set
            {
                this.filterTypeTextBox.Text = value;
                this.filterTypeTextBox.Focus();
            }
        }

        string LogStatisticsUi.Header
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
            ThreadPool.QueueUserWorkItem(
                o => this.HideKeyTapped?.Invoke());
        }

        void PopupUi.Display()
        {
            this.Location = new Point(
                this.shell.Location.X,
                this.shell.Location.Y);
            this.Visible = true;
        }

        private void overallKey_Click(object sender, EventArgs e)
        {
            ThreadPool.QueueUserWorkItem(
                o => this.OverallKeyTapped?.Invoke());
        }

        private void rangeKey_Click(object sender, EventArgs e)
        {
            ThreadPool.QueueUserWorkItem(
                o => this.RangeKeyTapped?.Invoke());
        }

        private void hideKey_Click(object sender, EventArgs e)
        {
            ThreadPool.QueueUserWorkItem(
                o => this.HideKeyTapped?.Invoke());
        }

        private void resetContentKey_Click(object sender, EventArgs e)
        {
            ThreadPool.QueueUserWorkItem(
                o => this.ResetContentKeyTapped?.Invoke());
        }

        private void resetTypeKey_Click(object sender, EventArgs e)
        {
            ThreadPool.QueueUserWorkItem(
                o => this.ResetTypeKeyTapped?.Invoke());
        }

        private readonly Form shell;
    }
}
