namespace xofz.UI.Forms
{
    using System;
    using System.Collections.Generic;
    using System.Threading;
    using System.Windows.Forms;

    public partial class FormLogEditorUi 
        : FormUi, LogEditorUiV2
    {
        public FormLogEditorUi(
            Form shell)
        {
            this.shell = shell;

            this.InitializeComponent();

            var h = this.Handle;
        }

        private FormLogEditorUi()
        {
            this.InitializeComponent();
        }

        public virtual event Do AddKeyTapped;

        public virtual event Do TypeChanged;

        void PopupUi.Display()
        {
            var s = this.shell;
            if (s == null)
            {
                this.Show();
                goto focus;
            }

            this.Location = s.Location;
            this.Show(s);

            focus:
            this.contentTextBox.Focus();
        }

        ICollection<string> LogEditorUi.Types
        {
            get
            {
                ICollection<string> typesCollection = new XLinkedList<string>();
                foreach (string item in this.entryTypeComboBox.Items)
                {
                    typesCollection.Add(item);
                }

                return typesCollection;
            }

            set
            {
                var etcbi = this.entryTypeComboBox.Items;
                etcbi.Clear();
                if (value == null)
                {
                    return;
                }

                foreach (var type in value)
                {
                    etcbi.Add(type);
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

        ICollection<string> LogEditorUi.Content
        {
            get
            {
                ICollection<string> contentCollection =
                    new XLinkedList<string>();
                var lines = this.contentTextBox.Lines;
                if (lines == null)
                {
                    return contentCollection;
                }

                foreach (var line in lines)
                {
                    contentCollection.Add(line);
                }

                return contentCollection;
            }

            set
            {
                var ctb = this.contentTextBox;
                ctb.Clear();
                if (value == null)
                {
                    return;
                }

                var c = value.Count;
                var array = new string[c];
                var e = value.GetEnumerator();
                // ReSharper disable once ConditionIsAlwaysTrueOrFalse
                if (e == null)
                {
                    return;
                }

                const byte zero = 0;
                for (int i = zero; i < c; ++i)
                {
                    e.MoveNext();
                    array[i] = e.Current;
                }

                e.Dispose();
                ctb.Lines = array;
            }
        }

        string LogEditorUiV2.EntryTypeLabelLabel
        {
            get => this.typeLabel.Text;

            set => this.typeLabel.Text = value;
        }

        string LogEditorUiV2.EntryContentLabelLabel
        {
            get => this.entryContentLabel.Text;

            set => this.entryContentLabel.Text = value;
        }

        string LogEditorUiV2.AddKeyLabel
        {
            get => this.addKey.Text;

            set => this.addKey.Text = value;
        }

        string LogEditorUiV2.TitleLabel
        {
            get => this.Text;

            set => this.Text = value;
        }

        protected virtual void this_FormClosing(
            object sender,
            FormClosingEventArgs e)
        {
            e.Cancel = true;

            this.Hide();            
        }

        protected virtual void entryTypeComboBox_SelectedIndexChanged(
            object sender, 
            EventArgs e)
        {
            var tc = this.TypeChanged;
            if (tc == null)
            {
                return;
            }

            ThreadPool.QueueUserWorkItem(
                o => tc.Invoke());
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

        protected readonly Form shell;
    }
}
