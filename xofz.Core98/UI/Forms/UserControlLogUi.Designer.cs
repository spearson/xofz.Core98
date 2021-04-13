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
            this.startLabel = new System.Windows.Forms.Label();
            this.endLabel = new System.Windows.Forms.Label();
            this.entriesGrid = new System.Windows.Forms.DataGridView();
            this.timestampColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.typeColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.contentColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.addKey = new System.Windows.Forms.Button();
            this.downKey = new System.Windows.Forms.Button();
            this.upKey = new System.Windows.Forms.Button();
            this.statisticsKey = new System.Windows.Forms.Button();
            this.filterContentLabel = new System.Windows.Forms.Label();
            this.filterTypeLabel = new System.Windows.Forms.Label();
            this.filterTypeTextBox = new System.Windows.Forms.TextBox();
            this.filterContentTextBox = new System.Windows.Forms.TextBox();
            this.resetContentKey = new System.Windows.Forms.Button();
            this.resetTypeKey = new System.Windows.Forms.Button();
            this.clearKey = new System.Windows.Forms.Button();
            this.previousWeekKey = new System.Windows.Forms.Button();
            this.nextWeekKey = new System.Windows.Forms.Button();
            this.currentWeekKey = new System.Windows.Forms.Button();
            this.oldestKey = new System.Windows.Forms.Button();
            this.newestKey = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.entriesGrid)).BeginInit();
            this.SuspendLayout();
            // 
            // startDatePicker
            // 
            this.startDatePicker.Location = new System.Drawing.Point(1, 20);
            this.startDatePicker.Margin = new System.Windows.Forms.Padding(0);
            this.startDatePicker.Name = "startDatePicker";
            this.startDatePicker.TabIndex = 0;
            this.startDatePicker.DateSelected += new System.Windows.Forms.DateRangeEventHandler(this.startDatePicker_DateSelected);
            // 
            // endDatePicker
            // 
            this.endDatePicker.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.endDatePicker.Location = new System.Drawing.Point(373, 20);
            this.endDatePicker.Margin = new System.Windows.Forms.Padding(0);
            this.endDatePicker.Name = "endDatePicker";
            this.endDatePicker.TabIndex = 1;
            this.endDatePicker.DateSelected += new System.Windows.Forms.DateRangeEventHandler(this.endDatePicker_DateSelected);
            // 
            // startLabel
            // 
            this.startLabel.AutoSize = true;
            this.startLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.startLabel.Location = new System.Drawing.Point(-3, 0);
            this.startLabel.Name = "startLabel";
            this.startLabel.Size = new System.Drawing.Size(54, 20);
            this.startLabel.TabIndex = 99;
            this.startLabel.Text = "Start:";
            // 
            // endLabel
            // 
            this.endLabel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.endLabel.AutoSize = true;
            this.endLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.endLabel.Location = new System.Drawing.Point(369, 0);
            this.endLabel.Name = "endLabel";
            this.endLabel.Size = new System.Drawing.Size(46, 20);
            this.endLabel.TabIndex = 99;
            this.endLabel.Text = "End:";
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
            this.entriesGrid.Location = new System.Drawing.Point(0, 320);
            this.entriesGrid.Margin = new System.Windows.Forms.Padding(0);
            this.entriesGrid.Name = "entriesGrid";
            this.entriesGrid.ReadOnly = true;
            this.entriesGrid.RowHeadersVisible = false;
            this.entriesGrid.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.entriesGrid.Size = new System.Drawing.Size(600, 180);
            this.entriesGrid.TabIndex = 14;
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
            this.addKey.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.addKey.AutoSize = true;
            this.addKey.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.addKey.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Lime;
            this.addKey.FlatAppearance.MouseOverBackColor = System.Drawing.Color.White;
            this.addKey.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.addKey.Font = new System.Drawing.Font("Consolas", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.addKey.Location = new System.Drawing.Point(246, 148);
            this.addKey.Margin = new System.Windows.Forms.Padding(0);
            this.addKey.Name = "addKey";
            this.addKey.Size = new System.Drawing.Size(102, 31);
            this.addKey.TabIndex = 9;
            this.addKey.Text = "Add Entry";
            this.addKey.UseVisualStyleBackColor = true;
            this.addKey.Click += new System.EventHandler(this.addKey_Click);
            // 
            // downKey
            // 
            this.downKey.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.downKey.AutoSize = true;
            this.downKey.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.downKey.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Lime;
            this.downKey.FlatAppearance.MouseOverBackColor = System.Drawing.Color.White;
            this.downKey.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.downKey.Font = new System.Drawing.Font("Consolas", 27.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.downKey.Location = new System.Drawing.Point(322, 20);
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
            this.upKey.Anchor = System.Windows.Forms.AnchorStyles.Top;
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
            this.upKey.TabIndex = 5;
            this.upKey.Text = "^";
            this.upKey.UseVisualStyleBackColor = true;
            this.upKey.Click += new System.EventHandler(this.upKey_Click);
            // 
            // statisticsKey
            // 
            this.statisticsKey.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.statisticsKey.AutoSize = true;
            this.statisticsKey.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.statisticsKey.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Lime;
            this.statisticsKey.FlatAppearance.MouseOverBackColor = System.Drawing.Color.White;
            this.statisticsKey.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.statisticsKey.Font = new System.Drawing.Font("Consolas", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.statisticsKey.Location = new System.Drawing.Point(242, 113);
            this.statisticsKey.Margin = new System.Windows.Forms.Padding(0);
            this.statisticsKey.Name = "statisticsKey";
            this.statisticsKey.Size = new System.Drawing.Size(111, 31);
            this.statisticsKey.TabIndex = 8;
            this.statisticsKey.Text = "Statistics";
            this.statisticsKey.UseVisualStyleBackColor = true;
            this.statisticsKey.Click += new System.EventHandler(this.statisticsKey_Click);
            // 
            // filterContentLabel
            // 
            this.filterContentLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.filterContentLabel.Location = new System.Drawing.Point(3, 257);
            this.filterContentLabel.Name = "filterContentLabel";
            this.filterContentLabel.Size = new System.Drawing.Size(149, 20);
            this.filterContentLabel.TabIndex = 9;
            this.filterContentLabel.Text = "Filter on Content:";
            this.filterContentLabel.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // filterTypeLabel
            // 
            this.filterTypeLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.filterTypeLabel.Location = new System.Drawing.Point(29, 289);
            this.filterTypeLabel.Name = "filterTypeLabel";
            this.filterTypeLabel.Size = new System.Drawing.Size(123, 20);
            this.filterTypeLabel.TabIndex = 10;
            this.filterTypeLabel.Text = "Filter on Type:";
            this.filterTypeLabel.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // filterTypeTextBox
            // 
            this.filterTypeTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.filterTypeTextBox.Font = new System.Drawing.Font("Consolas", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.filterTypeTextBox.Location = new System.Drawing.Point(158, 287);
            this.filterTypeTextBox.Name = "filterTypeTextBox";
            this.filterTypeTextBox.Size = new System.Drawing.Size(204, 26);
            this.filterTypeTextBox.TabIndex = 12;
            this.filterTypeTextBox.TextChanged += new System.EventHandler(this.filterTypeTextBox_TextChanged);
            // 
            // filterContentTextBox
            // 
            this.filterContentTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.filterContentTextBox.Font = new System.Drawing.Font("Consolas", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.filterContentTextBox.Location = new System.Drawing.Point(158, 255);
            this.filterContentTextBox.Name = "filterContentTextBox";
            this.filterContentTextBox.Size = new System.Drawing.Size(373, 26);
            this.filterContentTextBox.TabIndex = 10;
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
            this.resetContentKey.Location = new System.Drawing.Point(534, 252);
            this.resetContentKey.Margin = new System.Windows.Forms.Padding(0);
            this.resetContentKey.Name = "resetContentKey";
            this.resetContentKey.Size = new System.Drawing.Size(66, 31);
            this.resetContentKey.TabIndex = 11;
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
            this.resetTypeKey.Location = new System.Drawing.Point(365, 284);
            this.resetTypeKey.Margin = new System.Windows.Forms.Padding(0);
            this.resetTypeKey.Name = "resetTypeKey";
            this.resetTypeKey.Size = new System.Drawing.Size(66, 31);
            this.resetTypeKey.TabIndex = 13;
            this.resetTypeKey.Text = "Reset";
            this.resetTypeKey.UseVisualStyleBackColor = true;
            this.resetTypeKey.Click += new System.EventHandler(this.resetTypeKey_Click);
            // 
            // clearKey
            // 
            this.clearKey.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.clearKey.AutoSize = true;
            this.clearKey.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.clearKey.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Lime;
            this.clearKey.FlatAppearance.MouseOverBackColor = System.Drawing.Color.White;
            this.clearKey.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.clearKey.Font = new System.Drawing.Font("Consolas", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.clearKey.Location = new System.Drawing.Point(246, 78);
            this.clearKey.Margin = new System.Windows.Forms.Padding(0);
            this.clearKey.Name = "clearKey";
            this.clearKey.Size = new System.Drawing.Size(102, 31);
            this.clearKey.TabIndex = 7;
            this.clearKey.Text = "Clear Log";
            this.clearKey.UseVisualStyleBackColor = true;
            this.clearKey.Click += new System.EventHandler(this.clearKey_Click);
            // 
            // previousWeekKey
            // 
            this.previousWeekKey.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.previousWeekKey.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Lime;
            this.previousWeekKey.FlatAppearance.MouseOverBackColor = System.Drawing.Color.White;
            this.previousWeekKey.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.previousWeekKey.Font = new System.Drawing.Font("Consolas", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.previousWeekKey.Location = new System.Drawing.Point(63, 183);
            this.previousWeekKey.Margin = new System.Windows.Forms.Padding(0);
            this.previousWeekKey.Name = "previousWeekKey";
            this.previousWeekKey.Size = new System.Drawing.Size(165, 31);
            this.previousWeekKey.TabIndex = 2;
            this.previousWeekKey.Text = "<< Previous Week";
            this.previousWeekKey.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.previousWeekKey.UseVisualStyleBackColor = true;
            this.previousWeekKey.Click += new System.EventHandler(this.previousWeekKey_Click);
            // 
            // nextWeekKey
            // 
            this.nextWeekKey.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.nextWeekKey.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Lime;
            this.nextWeekKey.FlatAppearance.MouseOverBackColor = System.Drawing.Color.White;
            this.nextWeekKey.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.nextWeekKey.Font = new System.Drawing.Font("Consolas", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.nextWeekKey.Location = new System.Drawing.Point(373, 183);
            this.nextWeekKey.Margin = new System.Windows.Forms.Padding(0);
            this.nextWeekKey.Name = "nextWeekKey";
            this.nextWeekKey.Size = new System.Drawing.Size(165, 31);
            this.nextWeekKey.TabIndex = 3;
            this.nextWeekKey.Text = "Next Week >>";
            this.nextWeekKey.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.nextWeekKey.UseVisualStyleBackColor = true;
            this.nextWeekKey.Click += new System.EventHandler(this.nextWeekKey_Click);
            // 
            // currentWeekKey
            // 
            this.currentWeekKey.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.currentWeekKey.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Lime;
            this.currentWeekKey.FlatAppearance.MouseOverBackColor = System.Drawing.Color.White;
            this.currentWeekKey.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.currentWeekKey.Font = new System.Drawing.Font("Consolas", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.currentWeekKey.Location = new System.Drawing.Point(255, 183);
            this.currentWeekKey.Margin = new System.Windows.Forms.Padding(0);
            this.currentWeekKey.Name = "currentWeekKey";
            this.currentWeekKey.Size = new System.Drawing.Size(84, 31);
            this.currentWeekKey.TabIndex = 4;
            this.currentWeekKey.Text = "Current";
            this.currentWeekKey.UseVisualStyleBackColor = true;
            this.currentWeekKey.Click += new System.EventHandler(this.currentWeekKey_Click);
            // 
            // oldestKey
            // 
            this.oldestKey.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.oldestKey.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Lime;
            this.oldestKey.FlatAppearance.MouseOverBackColor = System.Drawing.Color.White;
            this.oldestKey.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.oldestKey.Font = new System.Drawing.Font("Consolas", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.oldestKey.Location = new System.Drawing.Point(153, 221);
            this.oldestKey.Margin = new System.Windows.Forms.Padding(0);
            this.oldestKey.Name = "oldestKey";
            this.oldestKey.Size = new System.Drawing.Size(75, 31);
            this.oldestKey.TabIndex = 100;
            this.oldestKey.Text = "Oldest";
            this.oldestKey.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.oldestKey.UseVisualStyleBackColor = true;
            this.oldestKey.Click += new System.EventHandler(this.oldestKey_Click);
            // 
            // newestKey
            // 
            this.newestKey.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.newestKey.AutoSize = true;
            this.newestKey.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.newestKey.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Lime;
            this.newestKey.FlatAppearance.MouseOverBackColor = System.Drawing.Color.White;
            this.newestKey.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.newestKey.Font = new System.Drawing.Font("Consolas", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.newestKey.Location = new System.Drawing.Point(373, 221);
            this.newestKey.Margin = new System.Windows.Forms.Padding(0);
            this.newestKey.Name = "newestKey";
            this.newestKey.Size = new System.Drawing.Size(75, 31);
            this.newestKey.TabIndex = 101;
            this.newestKey.Text = "Newest";
            this.newestKey.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.newestKey.UseVisualStyleBackColor = true;
            this.newestKey.Click += new System.EventHandler(this.newestKey_Click);
            // 
            // UserControlLogUi
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.Controls.Add(this.newestKey);
            this.Controls.Add(this.oldestKey);
            this.Controls.Add(this.currentWeekKey);
            this.Controls.Add(this.nextWeekKey);
            this.Controls.Add(this.previousWeekKey);
            this.Controls.Add(this.clearKey);
            this.Controls.Add(this.resetTypeKey);
            this.Controls.Add(this.resetContentKey);
            this.Controls.Add(this.filterContentTextBox);
            this.Controls.Add(this.filterTypeTextBox);
            this.Controls.Add(this.filterTypeLabel);
            this.Controls.Add(this.filterContentLabel);
            this.Controls.Add(this.statisticsKey);
            this.Controls.Add(this.upKey);
            this.Controls.Add(this.downKey);
            this.Controls.Add(this.addKey);
            this.Controls.Add(this.entriesGrid);
            this.Controls.Add(this.endLabel);
            this.Controls.Add(this.startLabel);
            this.Controls.Add(this.endDatePicker);
            this.Controls.Add(this.startDatePicker);
            this.MinimumSize = new System.Drawing.Size(600, 500);
            this.Name = "UserControlLogUi";
            this.Size = new System.Drawing.Size(600, 500);
            ((System.ComponentModel.ISupportInitialize)(this.entriesGrid)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        protected System.Windows.Forms.MonthCalendar startDatePicker;
        protected System.Windows.Forms.MonthCalendar endDatePicker;
        protected System.Windows.Forms.Label startLabel;
        protected System.Windows.Forms.Label endLabel;
        protected System.Windows.Forms.DataGridView entriesGrid;
        protected System.Windows.Forms.Button addKey;
        protected System.Windows.Forms.Button downKey;
        protected System.Windows.Forms.Button upKey;
        protected System.Windows.Forms.Button statisticsKey;
        protected System.Windows.Forms.Label filterContentLabel;
        protected System.Windows.Forms.Label filterTypeLabel;
        protected System.Windows.Forms.TextBox filterTypeTextBox;
        protected System.Windows.Forms.TextBox filterContentTextBox;
        protected System.Windows.Forms.Button resetContentKey;
        protected System.Windows.Forms.Button resetTypeKey;
        protected System.Windows.Forms.Button clearKey;
        protected System.Windows.Forms.DataGridViewTextBoxColumn timestampColumn;
        protected System.Windows.Forms.DataGridViewTextBoxColumn typeColumn;
        protected System.Windows.Forms.DataGridViewTextBoxColumn contentColumn;
        protected System.Windows.Forms.Button previousWeekKey;
        protected System.Windows.Forms.Button nextWeekKey;
        protected System.Windows.Forms.Button currentWeekKey;
        protected System.Windows.Forms.Button oldestKey;
        protected System.Windows.Forms.Button newestKey;
    }
}
