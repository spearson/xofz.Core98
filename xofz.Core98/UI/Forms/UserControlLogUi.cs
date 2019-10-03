namespace xofz.UI.Forms
{
    using System;
    using System.Collections.Generic;
    using System.Threading;
    using System.Windows.Forms;

    public partial class UserControlLogUi 
        : UserControlUi, LogUiV2
    {
        public UserControlLogUi()
        {
            this.InitializeComponent();
            this.activeFilterTextBox = this.filterContentTextBox;

            var h = this.Handle;
        }

        public virtual event Do DateRangeChanged;

        public virtual event Do AddKeyTapped;

        public virtual event Do ClearKeyTapped;

        public virtual event Do StatisticsKeyTapped;

        public virtual event Do FilterTextChanged;

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
                if (value == null)
                {
                    return;
                }

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

        string LogUiV2.StartLabelLabel
        {
            get => this.startLabel.Text;

            set => this.startLabel.Text = value;
        }

        string LogUiV2.EndLabelLabel
        {
            get => this.endLabel.Text;

            set => this.endLabel.Text = value;
        }

        string LogUiV2.ClearKeyLabel
        {
            get => this.clearKey.Text;

            set => this.clearKey.Text = value;
        }

        string LogUiV2.StatsKeyLabel
        {
            get => this.statisticsKey.Text;

            set => this.statisticsKey.Text = value;
        }

        string LogUiV2.AddKeyLabel
        {
            get => this.addKey.Text;

            set => this.addKey.Text = value;
        }

        string LogUiV2.PreviousWeekKeyLabel
        {
            get => this.previousWeekKey.Text;

            set => this.previousWeekKey.Text = value;
        }

        string LogUiV2.NextWeekKeyLabel
        {
            get => this.nextWeekKey.Text;

            set => this.nextWeekKey.Text = value;
        }

        string LogUiV2.CurrentWeekKeyLabel
        {
            get => this.currentWeekKey.Text;

            set => this.currentWeekKey.Text = value;
        }

        string LogUiV2.FilterContentLabelLabel
        {
            get => this.filterContentLabel.Text;

            set => this.filterContentLabel.Text = value;
        }

        string LogUiV2.FilterTypeLabelLabel
        {
            get => this.filterTypeLabel.Text;

            set => this.filterTypeLabel.Text = value;
        }

        string LogUiV2.ResetContentKeyLabel
        {
            get => this.resetContentKey.Text;

            set => this.resetContentKey.Text = value;
        }

        string LogUiV2.ResetTypeKeyLabel
        {
            get => this.resetTypeKey.Text;

            set => this.resetTypeKey.Text = value;
        }

        string LogUiV2.TimestampColumnHeaderLabel
        {
            get
            {
                var columns = this.entriesGrid.Columns;
                if (columns.Count < 1)
                {
                    return null;
                }

                return columns[0].HeaderText;
            }

            set
            {
                var columns = this.entriesGrid.Columns;
                if (columns.Count < 1)
                {
                    return;
                }

                columns[0].HeaderText = value;

            }
        }

        string LogUiV2.TypeColumnHeaderLabel
        {
            get
            {
                var columns = this.entriesGrid.Columns;
                if (columns.Count < 2)
                {
                    return null;
                }

                return columns[1].HeaderText;
            }

            set
            {
                var columns = this.entriesGrid.Columns;
                if (columns.Count < 2)
                {
                    return;
                }

                columns[1].HeaderText = value;

            }
        }

        string LogUiV2.ContentColumnHeaderLabel
        {
            get
            {
                var columns = this.entriesGrid.Columns;
                if (columns.Count < 3)
                {
                    return null;
                }

                return columns[2].HeaderText;
            }

            set
            {
                var columns = this.entriesGrid.Columns;
                if (columns.Count < 3)
                {
                    return;
                }

                columns[2].HeaderText = value;

            }
        }

        protected virtual void addKey_Click(
            object sender, 
            EventArgs e)
        {
            var akt = this.AddKeyTapped;
            if (akt == null)
            {
                return;
            }

            ThreadPool.QueueUserWorkItem(
                o => akt.Invoke());
        }

        protected virtual void clearKey_Click(
            object sender, 
            EventArgs e)
        {
            var ckt = this.ClearKeyTapped;
            if (ckt == null)
            {
                return;
            }

            ThreadPool.QueueUserWorkItem(
                o => ckt.Invoke());
        }

        protected virtual void startDatePicker_DateSelected(
            object sender, 
            DateRangeEventArgs e)
        {
            var drc = this.DateRangeChanged;
            if (drc == null)
            {
                return;
            }

            ThreadPool.QueueUserWorkItem(
                o => drc.Invoke());
        }

        protected virtual void endDatePicker_DateSelected(
            object sender, 
            DateRangeEventArgs e)
        {
            var drc = this.DateRangeChanged;
            if (drc == null)
            {
                return;
            }

            ThreadPool.QueueUserWorkItem(
                o => drc.Invoke());
        }

        protected virtual void downKey_Click(
            object sender, 
            EventArgs e)
        {
            this.entriesGrid.Focus();
            
            SendKeys.Send(@"{PGDN}");
        }

        protected virtual void upKey_Click(
            object sender, 
            EventArgs e)
        {
            this.entriesGrid.Focus();

            SendKeys.Send(@"{PGUP}");
        }

        protected virtual void statisticsKey_Click(
            object sender, 
            EventArgs e)
        {
            var skt = this.StatisticsKeyTapped;
            if (skt == null)
            {
                return;
            }

            ThreadPool.QueueUserWorkItem(
                o => skt.Invoke());
        }

        protected virtual void filterContentTextBox_TextChanged(
            object sender, 
            EventArgs e)
        {
            this.activeFilterTextBox = this.filterContentTextBox;
            var ftc = this.FilterTextChanged;
            if (ftc == null)
            {
                return;
            }

            ThreadPool.QueueUserWorkItem(
                o => ftc.Invoke());
        }

        protected virtual void filterTypeTextBox_TextChanged(
            object sender, 
            EventArgs e)
        {
            this.activeFilterTextBox = this.filterTypeTextBox;
            var ftc = this.FilterTextChanged;
            if (ftc == null)
            {
                return;
            }

            ThreadPool.QueueUserWorkItem(
                o => ftc.Invoke());
        }

        protected virtual void resetContentKey_Click(
            object sender, 
            EventArgs e)
        {
            var fctb = this.filterContentTextBox;
            this.activeFilterTextBox = fctb;
            fctb.Text = string.Empty;
            fctb.Focus();
        }

        protected virtual void resetTypeKey_Click(
            object sender, 
            EventArgs e)
        {
            var fttb = this.filterTypeTextBox;
            this.activeFilterTextBox = fttb;
            fttb.Text = string.Empty;
            fttb.Focus();
        }

        protected virtual void nextWeekKey_Click(
            object sender, 
            EventArgs e)
        {
            var sdp = this.startDatePicker;
            var newStartDate = sdp.SelectionStart.AddDays(7);

            sdp.DateSelected -= this.startDatePicker_DateSelected;            
            sdp.SelectionStart = newStartDate;
            sdp.SelectionEnd = newStartDate;
            sdp.DateSelected += this.startDatePicker_DateSelected;


            var edp = this.endDatePicker;
            var newEndDate = edp.SelectionStart.AddDays(7);

            edp.DateSelected -= this.endDatePicker_DateSelected;            
            edp.SelectionStart = newEndDate;
            edp.SelectionEnd = newEndDate;
            edp.DateSelected += this.endDatePicker_DateSelected;

            var drc = this.DateRangeChanged;
            if (drc == null)
            {
                return;
            }

            ThreadPool.QueueUserWorkItem(
                o => drc.Invoke());
        }

        protected virtual void previousWeekKey_Click(
            object sender, 
            EventArgs e)
        {
            var sdp = this.startDatePicker;
            var newStartDate = sdp.SelectionStart.AddDays(-7);

            sdp.DateSelected -= this.startDatePicker_DateSelected;
            sdp.SelectionStart = newStartDate;
            sdp.SelectionEnd = newStartDate;
            sdp.DateSelected += this.startDatePicker_DateSelected;


            var edp = this.endDatePicker;
            var newEndDate = edp.SelectionStart.AddDays(-7);

            edp.DateSelected -= this.endDatePicker_DateSelected;
            edp.SelectionStart = newEndDate;
            edp.SelectionEnd = newEndDate;
            edp.DateSelected += this.endDatePicker_DateSelected;

            var drc = this.DateRangeChanged;
            if (drc == null)
            {
                return;
            }

            ThreadPool.QueueUserWorkItem(
                o => drc.Invoke());
        }

        protected virtual void currentWeekKey_Click(
            object sender, 
            EventArgs e)
        {
            var edp = this.endDatePicker;
            var newEndDate = DateTime.Today;
            edp.DateSelected -= this.endDatePicker_DateSelected;
            edp.SelectionStart = newEndDate;
            edp.SelectionEnd = newEndDate;
            edp.DateSelected += this.endDatePicker_DateSelected;

            var sdp = this.startDatePicker;
            var newStartDate = newEndDate.AddDays(-7);
            sdp.DateSelected -= this.startDatePicker_DateSelected;
            sdp.SelectionStart = newStartDate;
            sdp.SelectionEnd = newStartDate;
            sdp.DateSelected += this.startDatePicker_DateSelected;            

            var drc = this.DateRangeChanged;
            if (drc == null)
            {
                return;
            }

            ThreadPool.QueueUserWorkItem(
                o => drc.Invoke());
        }

        protected TextBox activeFilterTextBox;
    }
}
