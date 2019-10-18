namespace xofz.UI.Forms
{
    partial class FormLogStatisticsUi
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

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.endDatePicker = new System.Windows.Forms.MonthCalendar();
            this.startDatePicker = new System.Windows.Forms.MonthCalendar();
            this.endLabel = new System.Windows.Forms.Label();
            this.startLabel = new System.Windows.Forms.Label();
            this.overallKey = new System.Windows.Forms.Button();
            this.rangeKey = new System.Windows.Forms.Button();
            this.statsContainer = new System.Windows.Forms.GroupBox();
            this.totalEntryCountLabel = new System.Windows.Forms.Label();
            this.totalEntryCountLabelLabel = new System.Windows.Forms.Label();
            this.latestTimestampLabel = new System.Windows.Forms.Label();
            this.earliestTimestampLabel = new System.Windows.Forms.Label();
            this.newestTimestampLabel = new System.Windows.Forms.Label();
            this.oldestTimestampLabel = new System.Windows.Forms.Label();
            this.avgEntriesPerDayLabel = new System.Windows.Forms.Label();
            this.latestTimestampLabelLabel = new System.Windows.Forms.Label();
            this.earliestTimestampLabelLabel = new System.Windows.Forms.Label();
            this.newestTimestampLabelLabel = new System.Windows.Forms.Label();
            this.oldestTimestampLabelLabel = new System.Windows.Forms.Label();
            this.avgEntriesPerDayLabelLabel = new System.Windows.Forms.Label();
            this.hideKey = new System.Windows.Forms.Button();
            this.filterContentLabel = new System.Windows.Forms.Label();
            this.filterContentTextBox = new System.Windows.Forms.TextBox();
            this.filterTypeTextBox = new System.Windows.Forms.TextBox();
            this.filterTypeLabel = new System.Windows.Forms.Label();
            this.resetTypeKey = new System.Windows.Forms.Button();
            this.resetContentKey = new System.Windows.Forms.Button();
            this.statsContainer.SuspendLayout();
            this.SuspendLayout();
            // 
            // endDatePicker
            // 
            this.endDatePicker.Location = new System.Drawing.Point(254, 47);
            this.endDatePicker.Margin = new System.Windows.Forms.Padding(0);
            this.endDatePicker.Name = "endDatePicker";
            this.endDatePicker.TabIndex = 1;
            // 
            // startDatePicker
            // 
            this.startDatePicker.Location = new System.Drawing.Point(9, 47);
            this.startDatePicker.Margin = new System.Windows.Forms.Padding(0);
            this.startDatePicker.Name = "startDatePicker";
            this.startDatePicker.TabIndex = 0;
            // 
            // endLabel
            // 
            this.endLabel.AutoSize = true;
            this.endLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.endLabel.Location = new System.Drawing.Point(250, 27);
            this.endLabel.Name = "endLabel";
            this.endLabel.Size = new System.Drawing.Size(46, 20);
            this.endLabel.TabIndex = 5;
            this.endLabel.Text = "End:";
            // 
            // startLabel
            // 
            this.startLabel.AutoSize = true;
            this.startLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.startLabel.Location = new System.Drawing.Point(12, 27);
            this.startLabel.Name = "startLabel";
            this.startLabel.Size = new System.Drawing.Size(54, 20);
            this.startLabel.TabIndex = 4;
            this.startLabel.Text = "Start:";
            // 
            // overallKey
            // 
            this.overallKey.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.overallKey.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Lime;
            this.overallKey.FlatAppearance.MouseOverBackColor = System.Drawing.Color.White;
            this.overallKey.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.overallKey.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.overallKey.Location = new System.Drawing.Point(9, 209);
            this.overallKey.Margin = new System.Windows.Forms.Padding(0);
            this.overallKey.Name = "overallKey";
            this.overallKey.Size = new System.Drawing.Size(153, 32);
            this.overallKey.TabIndex = 2;
            this.overallKey.Text = "Compute Overall";
            this.overallKey.UseVisualStyleBackColor = true;
            this.overallKey.Click += new System.EventHandler(this.overallKey_Click);
            // 
            // rangeKey
            // 
            this.rangeKey.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.rangeKey.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Lime;
            this.rangeKey.FlatAppearance.MouseOverBackColor = System.Drawing.Color.White;
            this.rangeKey.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.rangeKey.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rangeKey.Location = new System.Drawing.Point(330, 209);
            this.rangeKey.Margin = new System.Windows.Forms.Padding(0);
            this.rangeKey.Name = "rangeKey";
            this.rangeKey.Size = new System.Drawing.Size(151, 32);
            this.rangeKey.TabIndex = 3;
            this.rangeKey.Text = "Compute Range";
            this.rangeKey.UseVisualStyleBackColor = true;
            this.rangeKey.Click += new System.EventHandler(this.rangeKey_Click);
            // 
            // statsContainer
            // 
            this.statsContainer.Controls.Add(this.totalEntryCountLabel);
            this.statsContainer.Controls.Add(this.totalEntryCountLabelLabel);
            this.statsContainer.Controls.Add(this.latestTimestampLabel);
            this.statsContainer.Controls.Add(this.earliestTimestampLabel);
            this.statsContainer.Controls.Add(this.newestTimestampLabel);
            this.statsContainer.Controls.Add(this.oldestTimestampLabel);
            this.statsContainer.Controls.Add(this.avgEntriesPerDayLabel);
            this.statsContainer.Controls.Add(this.latestTimestampLabelLabel);
            this.statsContainer.Controls.Add(this.earliestTimestampLabelLabel);
            this.statsContainer.Controls.Add(this.newestTimestampLabelLabel);
            this.statsContainer.Controls.Add(this.oldestTimestampLabelLabel);
            this.statsContainer.Controls.Add(this.avgEntriesPerDayLabelLabel);
            this.statsContainer.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.statsContainer.Location = new System.Drawing.Point(12, 327);
            this.statsContainer.Name = "statsContainer";
            this.statsContainer.Size = new System.Drawing.Size(466, 162);
            this.statsContainer.TabIndex = 99;
            this.statsContainer.TabStop = false;
            this.statsContainer.Text = "Statistics";
            // 
            // totalEntryCountLabel
            // 
            this.totalEntryCountLabel.AutoSize = true;
            this.totalEntryCountLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.totalEntryCountLabel.Location = new System.Drawing.Point(166, 21);
            this.totalEntryCountLabel.Name = "totalEntryCountLabel";
            this.totalEntryCountLabel.Size = new System.Drawing.Size(92, 16);
            this.totalEntryCountLabel.TabIndex = 99;
            this.totalEntryCountLabel.Text = "Placeholder";
            // 
            // totalEntryCountLabelLabel
            // 
            this.totalEntryCountLabelLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.totalEntryCountLabelLabel.Location = new System.Drawing.Point(51, 21);
            this.totalEntryCountLabelLabel.Name = "totalEntryCountLabelLabel";
            this.totalEntryCountLabelLabel.Size = new System.Drawing.Size(109, 16);
            this.totalEntryCountLabelLabel.TabIndex = 99;
            this.totalEntryCountLabelLabel.Text = "Total entry count:";
            this.totalEntryCountLabelLabel.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // latestTimestampLabel
            // 
            this.latestTimestampLabel.AutoSize = true;
            this.latestTimestampLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.latestTimestampLabel.Location = new System.Drawing.Point(166, 131);
            this.latestTimestampLabel.Name = "latestTimestampLabel";
            this.latestTimestampLabel.Size = new System.Drawing.Size(92, 16);
            this.latestTimestampLabel.TabIndex = 99;
            this.latestTimestampLabel.Text = "Placeholder";
            // 
            // earliestTimestampLabel
            // 
            this.earliestTimestampLabel.AutoSize = true;
            this.earliestTimestampLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.earliestTimestampLabel.Location = new System.Drawing.Point(166, 109);
            this.earliestTimestampLabel.Name = "earliestTimestampLabel";
            this.earliestTimestampLabel.Size = new System.Drawing.Size(92, 16);
            this.earliestTimestampLabel.TabIndex = 99;
            this.earliestTimestampLabel.Text = "Placeholder";
            // 
            // newestTimestampLabel
            // 
            this.newestTimestampLabel.AutoSize = true;
            this.newestTimestampLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.newestTimestampLabel.Location = new System.Drawing.Point(166, 87);
            this.newestTimestampLabel.Name = "newestTimestampLabel";
            this.newestTimestampLabel.Size = new System.Drawing.Size(92, 16);
            this.newestTimestampLabel.TabIndex = 99;
            this.newestTimestampLabel.Text = "Placeholder";
            // 
            // oldestTimestampLabel
            // 
            this.oldestTimestampLabel.AutoSize = true;
            this.oldestTimestampLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.oldestTimestampLabel.Location = new System.Drawing.Point(166, 65);
            this.oldestTimestampLabel.Name = "oldestTimestampLabel";
            this.oldestTimestampLabel.Size = new System.Drawing.Size(92, 16);
            this.oldestTimestampLabel.TabIndex = 99;
            this.oldestTimestampLabel.Text = "Placeholder";
            // 
            // avgEntriesPerDayLabel
            // 
            this.avgEntriesPerDayLabel.AutoSize = true;
            this.avgEntriesPerDayLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.avgEntriesPerDayLabel.Location = new System.Drawing.Point(166, 43);
            this.avgEntriesPerDayLabel.Name = "avgEntriesPerDayLabel";
            this.avgEntriesPerDayLabel.Size = new System.Drawing.Size(92, 16);
            this.avgEntriesPerDayLabel.TabIndex = 99;
            this.avgEntriesPerDayLabel.Text = "Placeholder";
            // 
            // latestTimestampLabelLabel
            // 
            this.latestTimestampLabelLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.latestTimestampLabelLabel.Location = new System.Drawing.Point(48, 131);
            this.latestTimestampLabelLabel.Name = "latestTimestampLabelLabel";
            this.latestTimestampLabelLabel.Size = new System.Drawing.Size(112, 16);
            this.latestTimestampLabelLabel.TabIndex = 99;
            this.latestTimestampLabelLabel.Text = "Latest timestamp:";
            this.latestTimestampLabelLabel.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // earliestTimestampLabelLabel
            // 
            this.earliestTimestampLabelLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.earliestTimestampLabelLabel.Location = new System.Drawing.Point(39, 109);
            this.earliestTimestampLabelLabel.Name = "earliestTimestampLabelLabel";
            this.earliestTimestampLabelLabel.Size = new System.Drawing.Size(121, 16);
            this.earliestTimestampLabelLabel.TabIndex = 99;
            this.earliestTimestampLabelLabel.Text = "Earliest timestamp:";
            this.earliestTimestampLabelLabel.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // newestTimestampLabelLabel
            // 
            this.newestTimestampLabelLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.newestTimestampLabelLabel.Location = new System.Drawing.Point(7, 87);
            this.newestTimestampLabelLabel.Name = "newestTimestampLabelLabel";
            this.newestTimestampLabelLabel.Size = new System.Drawing.Size(153, 16);
            this.newestTimestampLabelLabel.TabIndex = 99;
            this.newestTimestampLabelLabel.Text = "Newest entry timestamp:";
            this.newestTimestampLabelLabel.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // oldestTimestampLabelLabel
            // 
            this.oldestTimestampLabelLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.oldestTimestampLabelLabel.Location = new System.Drawing.Point(13, 65);
            this.oldestTimestampLabelLabel.Name = "oldestTimestampLabelLabel";
            this.oldestTimestampLabelLabel.Size = new System.Drawing.Size(147, 16);
            this.oldestTimestampLabelLabel.TabIndex = 99;
            this.oldestTimestampLabelLabel.Text = "Oldest entry timestamp:";
            this.oldestTimestampLabelLabel.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // avgEntriesPerDayLabelLabel
            // 
            this.avgEntriesPerDayLabelLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.avgEntriesPerDayLabelLabel.Location = new System.Drawing.Point(6, 43);
            this.avgEntriesPerDayLabelLabel.Name = "avgEntriesPerDayLabelLabel";
            this.avgEntriesPerDayLabelLabel.Size = new System.Drawing.Size(154, 16);
            this.avgEntriesPerDayLabelLabel.TabIndex = 99;
            this.avgEntriesPerDayLabelLabel.Text = "Avg. # of entries per day:";
            this.avgEntriesPerDayLabelLabel.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // hideKey
            // 
            this.hideKey.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.hideKey.AutoSize = true;
            this.hideKey.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.hideKey.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Lime;
            this.hideKey.FlatAppearance.MouseOverBackColor = System.Drawing.Color.White;
            this.hideKey.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.hideKey.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.hideKey.Location = new System.Drawing.Point(423, 9);
            this.hideKey.Margin = new System.Windows.Forms.Padding(0);
            this.hideKey.Name = "hideKey";
            this.hideKey.Size = new System.Drawing.Size(58, 32);
            this.hideKey.TabIndex = 8;
            this.hideKey.Text = "Hide";
            this.hideKey.UseVisualStyleBackColor = true;
            this.hideKey.Click += new System.EventHandler(this.hideKey_Click);
            // 
            // filterContentLabel
            // 
            this.filterContentLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.filterContentLabel.Location = new System.Drawing.Point(12, 260);
            this.filterContentLabel.Name = "filterContentLabel";
            this.filterContentLabel.Size = new System.Drawing.Size(149, 20);
            this.filterContentLabel.TabIndex = 13;
            this.filterContentLabel.Text = "Filter on Content:";
            this.filterContentLabel.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // filterContentTextBox
            // 
            this.filterContentTextBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.filterContentTextBox.Location = new System.Drawing.Point(167, 257);
            this.filterContentTextBox.Name = "filterContentTextBox";
            this.filterContentTextBox.Size = new System.Drawing.Size(242, 26);
            this.filterContentTextBox.TabIndex = 4;
            // 
            // filterTypeTextBox
            // 
            this.filterTypeTextBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.filterTypeTextBox.Location = new System.Drawing.Point(167, 295);
            this.filterTypeTextBox.Name = "filterTypeTextBox";
            this.filterTypeTextBox.Size = new System.Drawing.Size(242, 26);
            this.filterTypeTextBox.TabIndex = 6;
            // 
            // filterTypeLabel
            // 
            this.filterTypeLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.filterTypeLabel.Location = new System.Drawing.Point(39, 298);
            this.filterTypeLabel.Name = "filterTypeLabel";
            this.filterTypeLabel.Size = new System.Drawing.Size(123, 20);
            this.filterTypeLabel.TabIndex = 15;
            this.filterTypeLabel.Text = "Filter on Type:";
            this.filterTypeLabel.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // resetTypeKey
            // 
            this.resetTypeKey.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Lime;
            this.resetTypeKey.FlatAppearance.MouseOverBackColor = System.Drawing.Color.White;
            this.resetTypeKey.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.resetTypeKey.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.resetTypeKey.Location = new System.Drawing.Point(412, 292);
            this.resetTypeKey.Margin = new System.Windows.Forms.Padding(0);
            this.resetTypeKey.Name = "resetTypeKey";
            this.resetTypeKey.Size = new System.Drawing.Size(69, 32);
            this.resetTypeKey.TabIndex = 7;
            this.resetTypeKey.Text = "Reset";
            this.resetTypeKey.UseVisualStyleBackColor = true;
            this.resetTypeKey.Click += new System.EventHandler(this.resetTypeKey_Click);
            // 
            // resetContentKey
            // 
            this.resetContentKey.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.resetContentKey.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Lime;
            this.resetContentKey.FlatAppearance.MouseOverBackColor = System.Drawing.Color.White;
            this.resetContentKey.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.resetContentKey.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.resetContentKey.Location = new System.Drawing.Point(412, 254);
            this.resetContentKey.Margin = new System.Windows.Forms.Padding(0);
            this.resetContentKey.Name = "resetContentKey";
            this.resetContentKey.Size = new System.Drawing.Size(69, 32);
            this.resetContentKey.TabIndex = 5;
            this.resetContentKey.Text = "Reset";
            this.resetContentKey.UseVisualStyleBackColor = true;
            this.resetContentKey.Click += new System.EventHandler(this.resetContentKey_Click);
            // 
            // FormLogStatisticsUi
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.ClientSize = new System.Drawing.Size(490, 501);
            this.Controls.Add(this.resetContentKey);
            this.Controls.Add(this.resetTypeKey);
            this.Controls.Add(this.filterTypeTextBox);
            this.Controls.Add(this.filterTypeLabel);
            this.Controls.Add(this.filterContentTextBox);
            this.Controls.Add(this.filterContentLabel);
            this.Controls.Add(this.hideKey);
            this.Controls.Add(this.statsContainer);
            this.Controls.Add(this.rangeKey);
            this.Controls.Add(this.overallKey);
            this.Controls.Add(this.endLabel);
            this.Controls.Add(this.startLabel);
            this.Controls.Add(this.endDatePicker);
            this.Controls.Add(this.startDatePicker);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FormLogStatisticsUi";
            this.ShowIcon = false;
            this.Text = "Log Statistics";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.this_FormClosing);
            this.statsContainer.ResumeLayout(false);
            this.statsContainer.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        protected System.Windows.Forms.MonthCalendar endDatePicker;
        protected System.Windows.Forms.MonthCalendar startDatePicker;
        protected System.Windows.Forms.Label endLabel;
        protected System.Windows.Forms.Label startLabel;
        protected System.Windows.Forms.Button overallKey;
        protected System.Windows.Forms.Button rangeKey;
        protected System.Windows.Forms.GroupBox statsContainer;
        protected System.Windows.Forms.Label latestTimestampLabel;
        protected System.Windows.Forms.Label earliestTimestampLabel;
        protected System.Windows.Forms.Label newestTimestampLabel;
        protected System.Windows.Forms.Label oldestTimestampLabel;
        protected System.Windows.Forms.Label avgEntriesPerDayLabel;
        protected System.Windows.Forms.Label latestTimestampLabelLabel;
        protected System.Windows.Forms.Label earliestTimestampLabelLabel;
        protected System.Windows.Forms.Label newestTimestampLabelLabel;
        protected System.Windows.Forms.Label oldestTimestampLabelLabel;
        protected System.Windows.Forms.Label avgEntriesPerDayLabelLabel;
        protected System.Windows.Forms.Button hideKey;
        protected System.Windows.Forms.Label totalEntryCountLabel;
        protected System.Windows.Forms.Label totalEntryCountLabelLabel;
        protected System.Windows.Forms.Label filterContentLabel;
        protected System.Windows.Forms.TextBox filterContentTextBox;
        protected System.Windows.Forms.TextBox filterTypeTextBox;
        protected System.Windows.Forms.Label filterTypeLabel;
        protected System.Windows.Forms.Button resetTypeKey;
        protected System.Windows.Forms.Button resetContentKey;
    }
}