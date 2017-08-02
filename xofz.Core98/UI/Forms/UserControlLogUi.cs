namespace xofz.UI.Forms
{
    using System;
    using System.Collections.Generic;
    using System.Threading;
    using System.Windows.Forms;

    public partial class UserControlLogUi : UserControlUi, LogUi
    {
        public UserControlLogUi(Materializer materializer)
        {
            this.materializer = materializer;

            this.InitializeComponent();
            this.activeFilterTextBox = this.filterContentTextBox;

            var h = this.Handle;
        }

        public event Action StartDateChanged;

        public event Action EndDateChanged;

        public event Action AddKeyTapped;

        public event Action StatisticsKeyTapped;

        public event Action FilterTextChanged;

        MaterializedEnumerable<Tuple<string, string, string>> LogUi.Entries
        {
            get
            {
                var ll = new LinkedList<Tuple<string, string, string>>();
                foreach (DataGridViewRow row in this.entriesGrid.Rows)
                {
                    var timestamp = row.Cells[0].Value?.ToString();
                    var type = row.Cells[1].Value?.ToString();
                    var content = row.Cells[2].Value?.ToString();
                    if (timestamp != null && type != null)
                    {
                        ll.AddLast(
                        Tuple.Create(
                            timestamp,
                            type,
                            content));
                    }
                }

                return this.materializer.Materialize(ll);
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
                this.startDatePicker.SelectionStart = value;
                this.startDatePicker.SelectionEnd = value;
            }
        }

        DateTime LogUi.EndDate
        {
            get => this.endDatePicker.SelectionStart;

            set
            {
                this.endDatePicker.SelectionStart = value;
                this.endDatePicker.SelectionEnd = value;
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

        bool LogUi.StatisticsKeyVisible
        {
            get => this.statisticsKey.Visible;
            set => this.statisticsKey.Visible = value;
        }

        private void addKey_Click(object sender, EventArgs e)
        {
            new Thread(() => this.AddKeyTapped?.Invoke()).Start();
        }

        private void startDatePicker_DateChanged(object sender, DateRangeEventArgs e)
        {
            new Thread(() => this.StartDateChanged?.Invoke()).Start();
        }

        private void endDatePicker_DateChanged(object sender, DateRangeEventArgs e)
        {
            new Thread(() => this.EndDateChanged?.Invoke()).Start();
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
            new Thread(() => this.StatisticsKeyTapped?.Invoke()).Start();
        }

        private void filterContentTextBox_TextChanged(object sender, EventArgs e)
        {
            this.activeFilterTextBox = this.filterContentTextBox;
            new Thread(() => this.FilterTextChanged?.Invoke()).Start();
        }

        private void filterTypeTextBox_TextChanged(object sender, EventArgs e)
        {
            this.activeFilterTextBox = this.filterTypeTextBox;
            new Thread(() => this.FilterTextChanged?.Invoke()).Start();
        }

        private void resetContentKey_Click(object sender, EventArgs e)
        {
            this.activeFilterTextBox = this.filterContentTextBox;
            this.filterContentTextBox.Text = string.Empty;
            this.filterContentTextBox.Focus();
        }

        private void resetTypeKey_Click(object sender, EventArgs e)
        {
            this.activeFilterTextBox = this.filterTypeTextBox;
            this.filterTypeTextBox.Text = string.Empty;
            this.filterTypeTextBox.Focus();
        }

        private TextBox activeFilterTextBox;
        private readonly Materializer materializer;
    }
}
