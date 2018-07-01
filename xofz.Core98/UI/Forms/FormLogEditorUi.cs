namespace xofz.UI.Forms
{
    using System;
    using System.Collections.Generic;
    using System.Threading;
    using System.Windows.Forms;

    public partial class FormLogEditorUi : FormUi, LogEditorUi
    {
        public FormLogEditorUi(
            Form shell,
            Materializer materializer)
        {
            this.shell = shell;
            this.materializer = materializer;

            this.InitializeComponent();
            this.FormClosing += this.this_FormClosing;

            var h = this.Handle;
        }

        void PopupUi.Display()
        {
            this.Location = this.shell.Location;
            this.Visible = true;
            this.contentTextBox.Focus();
        }

        public event Action AddKeyTapped;

        public event Action TypeChanged;

        MaterializedEnumerable<string> LogEditorUi.Types
        {
            get
            {
                var ll = new LinkedList<string>();
                foreach (string item in this.entryTypeComboBox.Items)
                {
                    ll.AddLast(item);
                }

                return this.materializer.Materialize(ll);
            }

            set
            {
                this.entryTypeComboBox.Items.Clear();
                foreach (var type in value)
                {
                    this.entryTypeComboBox.Items.Add(type);
                }
            }
        }

        string LogEditorUi.SelectedType
        {
            get => this.entryTypeComboBox.Text;

            set => this.entryTypeComboBox.Text = value;
        }

        string LogEditorUi.CustomType
        {
            get => this.customTypeTextBox.Text;

            set => this.customTypeTextBox.Text = value;
        }

        bool LogEditorUi.CustomTypeVisible
        {
            get => this.customTypeTextBox.Visible;

            set => this.customTypeTextBox.Visible = value;
        }

        public MaterializedEnumerable<string> Content
        {
            get
            {
                var ctb = this.contentTextBox;
                if (ctb.Lines == null)
                {
                    return this.materializer.Materialize(
                        new LinkedList<string>());
                }

                var ll = new LinkedList<string>();
                foreach (var line in ctb.Lines)
                {
                    ll.AddLast(line);
                }

                return this.materializer.Materialize(ll);
            }

            set
            {
                this.contentTextBox.Clear();
                if (value == null)
                {
                    return;
                }

                var array = new string[value.Count];
                var e = value.GetEnumerator();
                for (var i = 0; i < value.Count; ++i)
                {
                    e.MoveNext();
                    array[i] = e.Current;
                }

                e.Dispose();

                this.contentTextBox.Lines = array;
            }
        }

        private void this_FormClosing(object sender, FormClosingEventArgs e)
        {
            this.Hide();
            e.Cancel = true;
        }

        private void entryTypeComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            ThreadPool.QueueUserWorkItem(
                o => this.TypeChanged?.Invoke());
        }

        private void addKey_Click(object sender, EventArgs e)
        {
            ThreadPool.QueueUserWorkItem(
                o => this.AddKeyTapped?.Invoke());
        }

        private readonly Form shell;
        private readonly Materializer materializer;
    }
}
