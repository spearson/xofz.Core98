namespace xofz.UI.Forms
{
    partial class UserControlLogUi
    {
        /// <summary> 
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            this.startDatePicker = new System.Windows.Forms.MonthCalendar();
            this.endDatePicker = new System.Windows.Forms.MonthCalendar();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.entriesGrid = new System.Windows.Forms.DataGridView();
            this.timestampColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.typeColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.contentColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.addKey = new System.Windows.Forms.Button();
            this.downKey = new System.Windows.Forms.Button();
            this.upKey = new System.Windows.Forms.Button();
            this.statisticsKey = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.filterTypeTextBox = new System.Windows.Forms.TextBox();
            this.filterContentTextBox = new System.Windows.Forms.TextBox();
            this.resetContentKey = new System.Windows.Forms.Button();
            this.resetTypeKey = new System.Windows.Forms.Button();
            this.clearKey = new System.Windows.Forms.Button();
            this.previousWeekKey = new System.Windows.Forms.Button();
            this.nextWeekKey = new System.Windows.Forms.Button();
            this.currentWeekKey = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.entriesGrid)).BeginInit();
            this.SuspendLayout();
            // 
            // startDatePicker
            // 
            this.startDatePicker.Location = new System.Drawing.Point(1, 20);
            this.startDatePicker.Margin = new System.Windows.Forms.Padding(0);
            this.startDatePicker.Name = "startDatePicker";
            this.startDatePicker.TabIndex = 15;
            this.startDatePicker.DateSelected += new System.Windows.Forms.DateRangeEventHandler(this.startDatePicker_DateSelected);
            // 
            // endDatePicker
            // 
            this.endDatePicker.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.endDatePicker.Location = new System.Drawing.Point(373, 20);
            this.endDatePicker.Margin = new System.Windows.Forms.Padding(0);
            this.endDatePicker.Name = "endDatePicker";
            this.endDatePicker.TabIndex = 16;
            this.endDatePicker.DateSelected += new System.Windows.Forms.DateRangeEventHandler(this.endDatePicker_DateSelected);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(-3, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(54, 20);
            this.label1.TabIndex = 2;
            this.label1.Text = "Start:";
            // 
            // label2
            // 
            this.label2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(369, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(46, 20);
            this.label2.TabIndex = 3;
            this.label2.Text = "End:";
            // 
            // entriesGrid
            // 
            this.entriesGrid.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.entriesGrid.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCells;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.entriesGrid.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.entriesGrid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.entriesGrid.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.timestampColumn,
            this.typeColumn,
            this.contentColumn});
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("Consolas", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle3.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle3.Padding = new System.Windows.Forms.Padding(2);
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.entriesGrid.DefaultCellStyle = dataGridViewCellStyle3;
            this.entriesGrid.Location = new System.Drawing.Point(0, 278);
            this.entriesGrid.Margin = new System.Windows.Forms.Padding(0);
            this.entriesGrid.Name = "entriesGrid";
            this.entriesGrid.ReadOnly = true;
            this.entriesGrid.RowHeadersVisible = false;
            this.entriesGrid.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.entriesGrid.Size = new System.Drawing.Size(600, 222);
            this.entriesGrid.TabIndex = 4;
            // 
            // timestampColumn
            // 
            this.timestampColumn.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.timestampColumn.HeaderText = "Timestamp";
            this.timestampColumn.Name = "timestampColumn";
            this.timestampColumn.ReadOnly = true;
            this.timestampColumn.Width = 220;
            // 
            // typeColumn
            // 
            this.typeColumn.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.typeColumn.HeaderText = "Type";
            this.typeColumn.Name = "typeColumn";
            this.typeColumn.ReadOnly = true;
            this.typeColumn.Width = 135;
            // 
            // contentColumn
            // 
            this.contentColumn.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Consolas", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.contentColumn.DefaultCellStyle = dataGridViewCellStyle2;
            this.contentColumn.HeaderText = "Content";
            this.contentColumn.Name = "contentColumn";
            this.contentColumn.ReadOnly = true;
            // 
            // addKey
            // 
            this.addKey.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.addKey.AutoSize = true;
            this.addKey.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.addKey.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Lime;
            this.addKey.FlatAppearance.MouseOverBackColor = System.Drawing.Color.White;
            this.addKey.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.addKey.Font = new System.Drawing.Font("Consolas", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.addKey.Location = new System.Drawing.Point(228, 150);
            this.addKey.Margin = new System.Windows.Forms.Padding(0);
            this.addKey.Name = "addKey";
            this.addKey.Size = new System.Drawing.Size(102, 31);
            this.addKey.TabIndex = 5;
            this.addKey.Text = "Add Entry";
            this.addKey.UseVisualStyleBackColor = true;
            this.addKey.Click += new System.EventHandler(this.addKey_Click);
            // 
            // downKey
            // 
            this.downKey.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.downKey.AutoSize = true;
            this.downKey.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.downKey.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Lime;
            this.downKey.FlatAppearance.MouseOverBackColor = System.Drawing.Color.White;
            this.downKey.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.downKey.Font = new System.Drawing.Font("Consolas", 27.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.downKey.Location = new System.Drawing.Point(322, 21);
            this.downKey.Margin = new System.Windows.Forms.Padding(0);
            this.downKey.Name = "downKey";
            this.downKey.Size = new System.Drawing.Size(51, 55);
            this.downKey.TabIndex = 6;
            this.downKey.Text = "_";
            this.downKey.UseVisualStyleBackColor = true;
            this.downKey.Click += new System.EventHandler(this.downKey_Click);
            // 
            // upKey
            // 
            this.upKey.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.upKey.AutoSize = true;
            this.upKey.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.upKey.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Lime;
            this.upKey.FlatAppearance.MouseOverBackColor = System.Drawing.Color.White;
            this.upKey.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.upKey.Font = new System.Drawing.Font("Consolas", 27.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.upKey.Location = new System.Drawing.Point(228, 20);
            this.upKey.Margin = new System.Windows.Forms.Padding(0);
            this.upKey.Name = "upKey";
            this.upKey.Size = new System.Drawing.Size(51, 55);
            this.upKey.TabIndex = 7;
            this.upKey.Text = "^";
            this.upKey.UseVisualStyleBackColor = true;
            this.upKey.Click += new System.EventHandler(this.upKey_Click);
            // 
            // statisticsKey
            // 
            this.statisticsKey.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.statisticsKey.AutoSize = true;
            this.statisticsKey.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.statisticsKey.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Lime;
            this.statisticsKey.FlatAppearance.MouseOverBackColor = System.Drawing.Color.White;
            this.statisticsKey.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.statisticsKey.Font = new System.Drawing.Font("Consolas", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.statisticsKey.Location = new System.Drawing.Point(228, 112);
            this.statisticsKey.Margin = new System.Windows.Forms.Padding(0);
            this.statisticsKey.Name = "statisticsKey";
            this.statisticsKey.Size = new System.Drawing.Size(111, 31);
            this.statisticsKey.TabIndex = 8;
            this.statisticsKey.Text = "Statistics";
            this.statisticsKey.UseVisualStyleBackColor = true;
            this.statisticsKey.Click += new System.EventHandler(this.statisticsKey_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(3, 220);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(149, 20);
            this.label3.TabIndex = 9;
            this.label3.Text = "Filter on Content:";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(29, 252);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(123, 20);
            this.label4.TabIndex = 10;
            this.label4.Text = "Filter on Type:";
            // 
            // filterTypeTextBox
            // 
            this.filterTypeTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.filterTypeTextBox.Font = new System.Drawing.Font("Consolas", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.filterTypeTextBox.Location = new System.Drawing.Point(158, 249);
            this.filterTypeTextBox.Name = "filterTypeTextBox";
            this.filterTypeTextBox.Size = new System.Drawing.Size(204, 26);
            this.filterTypeTextBox.TabIndex = 1;
            this.filterTypeTextBox.TextChanged += new System.EventHandler(this.filterTypeTextBox_TextChanged);
            // 
            // filterContentTextBox
            // 
            this.filterContentTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.filterContentTextBox.Font = new System.Drawing.Font("Consolas", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.filterContentTextBox.Location = new System.Drawing.Point(158, 217);
            this.filterContentTextBox.Name = "filterContentTextBox";
            this.filterContentTextBox.Size = new System.Drawing.Size(373, 26);
            this.filterContentTextBox.TabIndex = 0;
            this.filterContentTextBox.TextChanged += new System.EventHandler(this.filterContentTextBox_TextChanged);
            // 
            // resetContentKey
            // 
            this.resetContentKey.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.resetContentKey.AutoSize = true;
            this.resetContentKey.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.resetContentKey.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Lime;
            this.resetContentKey.FlatAppearance.MouseOverBackColor = System.Drawing.Color.White;
            this.resetContentKey.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.resetContentKey.Font = new System.Drawing.Font("Consolas", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.resetContentKey.Location = new System.Drawing.Point(534, 214);
            this.resetContentKey.Margin = new System.Windows.Forms.Padding(0);
            this.resetContentKey.Name = "resetContentKey";
            this.resetContentKey.Size = new System.Drawing.Size(66, 31);
            this.resetContentKey.TabIndex = 13;
            this.resetContentKey.Text = "Reset";
            this.resetContentKey.UseVisualStyleBackColor = true;
            this.resetContentKey.Click += new System.EventHandler(this.resetContentKey_Click);
            // 
            // resetTypeKey
            // 
            this.resetTypeKey.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.resetTypeKey.AutoSize = true;
            this.resetTypeKey.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.resetTypeKey.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Lime;
            this.resetTypeKey.FlatAppearance.MouseOverBackColor = System.Drawing.Color.White;
            this.resetTypeKey.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.resetTypeKey.Font = new System.Drawing.Font("Consolas", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.resetTypeKey.Location = new System.Drawing.Point(365, 246);
            this.resetTypeKey.Margin = new System.Windows.Forms.Padding(0);
            this.resetTypeKey.Name = "resetTypeKey";
            this.resetTypeKey.Size = new System.Drawing.Size(66, 31);
            this.resetTypeKey.TabIndex = 14;
            this.resetTypeKey.Text = "Reset";
            this.resetTypeKey.UseVisualStyleBackColor = true;
            this.resetTypeKey.Click += new System.EventHandler(this.resetTypeKey_Click);
            // 
            // clearKey
            // 
            this.clearKey.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.clearKey.AutoSize = true;
            this.clearKey.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.clearKey.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Lime;
            this.clearKey.FlatAppearance.MouseOverBackColor = System.Drawing.Color.White;
            this.clearKey.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.clearKey.Font = new System.Drawing.Font("Consolas", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.clearKey.Location = new System.Drawing.Point(228, 76);
            this.clearKey.Margin = new System.Windows.Forms.Padding(0);
            this.clearKey.Name = "clearKey";
            this.clearKey.Size = new System.Drawing.Size(102, 31);
            this.clearKey.TabIndex = 17;
            this.clearKey.Text = "Clear Log";
            this.clearKey.UseVisualStyleBackColor = true;
            this.clearKey.Click += new System.EventHandler(this.clearKey_Click);
            // 
            // previousWeekKey
            // 
            this.previousWeekKey.AutoSize = true;
            this.previousWeekKey.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.previousWeekKey.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Lime;
            this.previousWeekKey.FlatAppearance.MouseOverBackColor = System.Drawing.Color.White;
            this.previousWeekKey.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.previousWeekKey.Font = new System.Drawing.Font("Consolas", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.previousWeekKey.Location = new System.Drawing.Point(63, 183);
            this.previousWeekKey.Margin = new System.Windows.Forms.Padding(0);
            this.previousWeekKey.Name = "previousWeekKey";
            this.previousWeekKey.Size = new System.Drawing.Size(165, 31);
            this.previousWeekKey.TabIndex = 18;
            this.previousWeekKey.Text = "<< Previous Week";
            this.previousWeekKey.UseVisualStyleBackColor = true;
            this.previousWeekKey.Click += new System.EventHandler(this.previousWeekKey_Click);
            // 
            // nextWeekKey
            // 
            this.nextWeekKey.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.nextWeekKey.AutoSize = true;
            this.nextWeekKey.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.nextWeekKey.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Lime;
            this.nextWeekKey.FlatAppearance.MouseOverBackColor = System.Drawing.Color.White;
            this.nextWeekKey.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.nextWeekKey.Font = new System.Drawing.Font("Consolas", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.nextWeekKey.Location = new System.Drawing.Point(373, 183);
            this.nextWeekKey.Margin = new System.Windows.Forms.Padding(0);
            this.nextWeekKey.Name = "nextWeekKey";
            this.nextWeekKey.Size = new System.Drawing.Size(129, 31);
            this.nextWeekKey.TabIndex = 19;
            this.nextWeekKey.Text = "Next Week >>";
            this.nextWeekKey.UseVisualStyleBackColor = true;
            this.nextWeekKey.Click += new System.EventHandler(this.nextWeekKey_Click);
            // 
            // currentWeekKey
            // 
            this.currentWeekKey.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.currentWeekKey.AutoSize = true;
            this.currentWeekKey.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.currentWeekKey.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Lime;
            this.currentWeekKey.FlatAppearance.MouseOverBackColor = System.Drawing.Color.White;
            this.currentWeekKey.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.currentWeekKey.Font = new System.Drawing.Font("Consolas", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.currentWeekKey.Location = new System.Drawing.Point(255, 183);
            this.currentWeekKey.Margin = new System.Windows.Forms.Padding(0);
            this.currentWeekKey.Name = "currentWeekKey";
            this.currentWeekKey.Size = new System.Drawing.Size(84, 31);
            this.currentWeekKey.TabIndex = 20;
            this.currentWeekKey.Text = "Current";
            this.currentWeekKey.UseVisualStyleBackColor = true;
            this.currentWeekKey.Click += new System.EventHandler(this.currentWeekKey_Click);
            // 
            // UserControlLogUi
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.Controls.Add(this.currentWeekKey);
            this.Controls.Add(this.nextWeekKey);
            this.Controls.Add(this.previousWeekKey);
            this.Controls.Add(this.clearKey);
            this.Controls.Add(this.resetTypeKey);
            this.Controls.Add(this.resetContentKey);
            this.Controls.Add(this.filterContentTextBox);
            this.Controls.Add(this.filterTypeTextBox);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.statisticsKey);
            this.Controls.Add(this.upKey);
            this.Controls.Add(this.downKey);
            this.Controls.Add(this.addKey);
            this.Controls.Add(this.entriesGrid);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.endDatePicker);
            this.Controls.Add(this.startDatePicker);
            this.Name = "UserControlLogUi";
            this.Size = new System.Drawing.Size(600, 500);
            ((System.ComponentModel.ISupportInitialize)(this.entriesGrid)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MonthCalendar startDatePicker;
        private System.Windows.Forms.MonthCalendar endDatePicker;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.DataGridView entriesGrid;
        private System.Windows.Forms.Button addKey;
        private System.Windows.Forms.Button downKey;
        private System.Windows.Forms.Button upKey;
        private System.Windows.Forms.Button statisticsKey;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox filterTypeTextBox;
        private System.Windows.Forms.TextBox filterContentTextBox;
        private System.Windows.Forms.Button resetContentKey;
        private System.Windows.Forms.Button resetTypeKey;
        private System.Windows.Forms.Button clearKey;
        private System.Windows.Forms.DataGridViewTextBoxColumn timestampColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn typeColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn contentColumn;
        private System.Windows.Forms.Button previousWeekKey;
        private System.Windows.Forms.Button nextWeekKey;
        private System.Windows.Forms.Button currentWeekKey;
    }
}
