namespace xofz.UI.Forms
{
    using System;
    using System.Collections.Generic;
    using System.Threading;
    using System.Windows.Forms;

    public partial class UserControlLogUi 
        : UserControlUi, LogUi
    {
        public UserControlLogUi()
        {
            this.InitializeComponent();
            this.activeFilterTextBox = this.filterContentTextBox;

            var h = this.Handle;
        }

        public event Do StartDateChanged;

        public event Do EndDateChanged;

        public event Do AddKeyTapped;

        public event Do ClearKeyTapped;

        public event Do StatisticsKeyTapped;

        public event Do FilterTextChanged;

        ICollection<XTuple<string, string, string>> LogUi.Entries
        {
            get
            {
                ICollection<XTuple<string, string, string>>
                    entriesCollection = new LinkedList<XTuple<string, string, string>>();
                foreach (DataGridViewRow row in this.entriesGrid.Rows)
                {
                    var timestamp = row.Cells[0].Value?.ToString();
                    var type = row.Cells[1].Value?.ToString();
                    var content = row.Cells[2].Value?.ToString();
                    if (timestamp != null && type != null)
                    {
                        entriesCollection.Add(
                        XTuple.Create(
                            timestamp,
                            type,
                            content));
                    }
                }

                return entriesCollection;
            }

            set
            {
                var eg = this.entriesGrid;
                eg.Rows.Clear();
                foreach (var entry in value)
                {
                    eg.Rows.Add(entry.Item1, entry.Item2, entry.Item3);
                }

                this.activeFilterTextBox.Focus();
            }
        }

        DateTime LogUi.StartDate
        {
            get => this.startDatePicker.SelectionStart;

            set
            {
                var sdp = this.startDatePicker;
                sdp.SelectionStart = value;
                sdp.SelectionEnd = value;
            }
        }

        DateTime LogUi.EndDate
        {
            get => this.endDatePicker.SelectionStart;

            set
            {
                var edp = this.endDatePicker;
                edp.SelectionStart = value;
                edp.SelectionEnd = value;
            }
        }

        string LogUi.FilterContent
        {
            get => this.filterContentTextBox.Text;

            set => this.filterContentTextBox.Text = value;
        }

        string LogUi.FilterType
        {
            get => this.filterTypeTextBox.Text;

            set => this.filterTypeTextBox.Text = value;
        }

        bool LogUi.AddKeyVisible
        {
            get => this.addKey.Visible;

            set => this.addKey.Visible = value;
        }

        bool LogUi.ClearKeyVisible
        {
            get => this.clearKey.Visible;

            set => this.clearKey.Visible = value;
        }

        bool LogUi.StatisticsKeyVisible
        {
            get => this.statisticsKey.Visible;

            set => this.statisticsKey.Visible = value;
        }

        void LogUi.AddToTop(
            XTuple<string, string, string> entry)
        {
            this.entriesGrid.Rows.Insert(0,
                entry.Item1,
                entry.Item2,
                entry.Item3);
        }

        private void addKey_Click(object sender, EventArgs e)
        {
            var akt = this.AddKeyTapped;
            if (akt == null)
            {
                return;
            }

            ThreadPool.QueueUserWorkItem(o => akt.Invoke());
        }

        private void clearKey_Click(object sender, EventArgs e)
        {
            var ckt = this.ClearKeyTapped;
            if (ckt == null)
            {
                return;
            }

            ThreadPool.QueueUserWorkItem(o => ckt.Invoke());
        }

        private void startDatePicker_DateSelected(object sender, DateRangeEventArgs e)
        {
            var sdc = this.StartDateChanged;
            if (sdc == null)
            {
                return;
            }

            ThreadPool.QueueUserWorkItem(o => sdc.Invoke());
        }

        private void endDatePicker_DateSelected(object sender, DateRangeEventArgs e)
        {
            var edc = this.EndDateChanged;
            if (edc == null)
            {
                return;
            }

            ThreadPool.QueueUserWorkItem(o => edc.Invoke());
        }

        private void downKey_Click(object sender, EventArgs e)
        {
            this.entriesGrid.Focus();
            
            SendKeys.Send("{PGDN}");
        }

        private void upKey_Click(object sender, EventArgs e)
        {
            this.entriesGrid.Focus();

            SendKeys.Send("{PGUP}");
        }

        private void statisticsKey_Click(object sender, EventArgs e)
        {
            var skt = this.StatisticsKeyTapped;
            if (skt == null)
            {
                return;
            }

            ThreadPool.QueueUserWorkItem(o => skt.Invoke());
        }

        private void filterContentTextBox_TextChanged(object sender, EventArgs e)
        {
            this.activeFilterTextBox = this.filterContentTextBox;
            var ftc = this.FilterTextChanged;
            if (ftc == null)
            {
                return;
            }

            ThreadPool.QueueUserWorkItem(o => ftc.Invoke());
        }

        private void filterTypeTextBox_TextChanged(object sender, EventArgs e)
        {
            this.activeFilterTextBox = this.filterTypeTextBox;
            var ftc = this.FilterTextChanged;
            if (ftc == null)
            {
                return;
            }

            ThreadPool.QueueUserWorkItem(o => ftc.Invoke());
        }

        private void resetContentKey_Click(object sender, EventArgs e)
        {
            var fctb = this.filterContentTextBox;
            this.activeFilterTextBox = fctb;
            fctb.Text = string.Empty;
            fctb.Focus();
        }

        private void resetTypeKey_Click(object sender, EventArgs e)
        {
            var fttb = this.filterTypeTextBox;
            this.activeFilterTextBox = fttb;
            fttb.Text = string.Empty;
            fttb.Focus();
        }

        private TextBox activeFilterTextBox;
    }
}
