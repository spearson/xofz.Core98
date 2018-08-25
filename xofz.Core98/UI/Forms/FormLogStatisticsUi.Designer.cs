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
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.overallKey = new System.Windows.Forms.Button();
            this.rangeKey = new System.Windows.Forms.Button();
            this.groupBox = new System.Windows.Forms.GroupBox();
            this.totalEntryCountLabel = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.latestTimestampLabel = new System.Windows.Forms.Label();
            this.earliestTimestampLabel = new System.Windows.Forms.Label();
            this.newestTimestampLabel = new System.Windows.Forms.Label();
            this.oldestTimestampLabel = new System.Windows.Forms.Label();
            this.avgEntriesPerDayLabel = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.hideKey = new System.Windows.Forms.Button();
            this.label8 = new System.Windows.Forms.Label();
            this.filterContentTextBox = new System.Windows.Forms.TextBox();
            this.filterTypeTextBox = new System.Windows.Forms.TextBox();
            this.label10 = new System.Windows.Forms.Label();
            this.resetTypeKey = new System.Windows.Forms.Button();
            this.resetContentKey = new System.Windows.Forms.Button();
            this.groupBox.SuspendLayout();
            this.SuspendLayout();
            // 
            // endDatePicker
            // 
            this.endDatePicker.Location = new System.Drawing.Point(254, 47);
            this.endDatePicker.Margin = new System.Windows.Forms.Padding(0);
            this.endDatePicker.Name = "endDatePicker";
            this.endDatePicker.TabIndex = 3;
            // 
            // startDatePicker
            // 
            this.startDatePicker.Location = new System.Drawing.Point(9, 47);
            this.startDatePicker.Margin = new System.Windows.Forms.Padding(0);
            this.startDatePicker.Name = "startDatePicker";
            this.startDatePicker.TabIndex = 2;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(250, 27);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(46, 20);
            this.label2.TabIndex = 5;
            this.label2.Text = "End:";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(12, 27);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(54, 20);
            this.label1.TabIndex = 4;
            this.label1.Text = "Start:";
            // 
            // overallKey
            // 
            this.overallKey.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.overallKey.AutoSize = true;
            this.overallKey.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.overallKey.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Lime;
            this.overallKey.FlatAppearance.MouseOverBackColor = System.Drawing.Color.White;
            this.overallKey.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.overallKey.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.overallKey.Location = new System.Drawing.Point(9, 209);
            this.overallKey.Margin = new System.Windows.Forms.Padding(0);
            this.overallKey.Name = "overallKey";
            this.overallKey.Size = new System.Drawing.Size(153, 32);
            this.overallKey.TabIndex = 9;
            this.overallKey.Text = "Compute Overall";
            this.overallKey.UseVisualStyleBackColor = true;
            this.overallKey.Click += new System.EventHandler(this.overallKey_Click);
            // 
            // rangeKey
            // 
            this.rangeKey.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.rangeKey.AutoSize = true;
            this.rangeKey.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.rangeKey.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Lime;
            this.rangeKey.FlatAppearance.MouseOverBackColor = System.Drawing.Color.White;
            this.rangeKey.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.rangeKey.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rangeKey.Location = new System.Drawing.Point(330, 209);
            this.rangeKey.Margin = new System.Windows.Forms.Padding(0);
            this.rangeKey.Name = "rangeKey";
            this.rangeKey.Size = new System.Drawing.Size(151, 32);
            this.rangeKey.TabIndex = 10;
            this.rangeKey.Text = "Compute Range";
            this.rangeKey.UseVisualStyleBackColor = true;
            this.rangeKey.Click += new System.EventHandler(this.rangeKey_Click);
            // 
            // groupBox
            // 
            this.groupBox.Controls.Add(this.totalEntryCountLabel);
            this.groupBox.Controls.Add(this.label9);
            this.groupBox.Controls.Add(this.latestTimestampLabel);
            this.groupBox.Controls.Add(this.earliestTimestampLabel);
            this.groupBox.Controls.Add(this.newestTimestampLabel);
            this.groupBox.Controls.Add(this.oldestTimestampLabel);
            this.groupBox.Controls.Add(this.avgEntriesPerDayLabel);
            this.groupBox.Controls.Add(this.label7);
            this.groupBox.Controls.Add(this.label6);
            this.groupBox.Controls.Add(this.label5);
            this.groupBox.Controls.Add(this.label4);
            this.groupBox.Controls.Add(this.label3);
            this.groupBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox.Location = new System.Drawing.Point(12, 327);
            this.groupBox.Name = "groupBox";
            this.groupBox.Size = new System.Drawing.Size(466, 122);
            this.groupBox.TabIndex = 11;
            this.groupBox.TabStop = false;
            this.groupBox.Text = "Statistics";
            // 
            // totalEntryCountLabel
            // 
            this.totalEntryCountLabel.AutoSize = true;
            this.totalEntryCountLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.totalEntryCountLabel.Location = new System.Drawing.Point(166, 21);
            this.totalEntryCountLabel.Name = "totalEntryCountLabel";
            this.totalEntryCountLabel.Size = new System.Drawing.Size(92, 16);
            this.totalEntryCountLabel.TabIndex = 11;
            this.totalEntryCountLabel.Text = "Placeholder";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label9.Location = new System.Drawing.Point(51, 21);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(109, 16);
            this.label9.TabIndex = 10;
            this.label9.Text = "Total entry count:";
            // 
            // latestTimestampLabel
            // 
            this.latestTimestampLabel.AutoSize = true;
            this.latestTimestampLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.latestTimestampLabel.Location = new System.Drawing.Point(166, 101);
            this.latestTimestampLabel.Name = "latestTimestampLabel";
            this.latestTimestampLabel.Size = new System.Drawing.Size(92, 16);
            this.latestTimestampLabel.TabIndex = 9;
            this.latestTimestampLabel.Text = "Placeholder";
            // 
            // earliestTimestampLabel
            // 
            this.earliestTimestampLabel.AutoSize = true;
            this.earliestTimestampLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.earliestTimestampLabel.Location = new System.Drawing.Point(166, 85);
            this.earliestTimestampLabel.Name = "earliestTimestampLabel";
            this.earliestTimestampLabel.Size = new System.Drawing.Size(92, 16);
            this.earliestTimestampLabel.TabIndex = 8;
            this.earliestTimestampLabel.Text = "Placeholder";
            // 
            // newestTimestampLabel
            // 
            this.newestTimestampLabel.AutoSize = true;
            this.newestTimestampLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.newestTimestampLabel.Location = new System.Drawing.Point(166, 69);
            this.newestTimestampLabel.Name = "newestTimestampLabel";
            this.newestTimestampLabel.Size = new System.Drawing.Size(92, 16);
            this.newestTimestampLabel.TabIndex = 7;
            this.newestTimestampLabel.Text = "Placeholder";
            // 
            // oldestTimestampLabel
            // 
            this.oldestTimestampLabel.AutoSize = true;
            this.oldestTimestampLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.oldestTimestampLabel.Location = new System.Drawing.Point(166, 53);
            this.oldestTimestampLabel.Name = "oldestTimestampLabel";
            this.oldestTimestampLabel.Size = new System.Drawing.Size(92, 16);
            this.oldestTimestampLabel.TabIndex = 6;
            this.oldestTimestampLabel.Text = "Placeholder";
            // 
            // avgEntriesPerDayLabel
            // 
            this.avgEntriesPerDayLabel.AutoSize = true;
            this.avgEntriesPerDayLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.avgEntriesPerDayLabel.Location = new System.Drawing.Point(166, 37);
            this.avgEntriesPerDayLabel.Name = "avgEntriesPerDayLabel";
            this.avgEntriesPerDayLabel.Size = new System.Drawing.Size(92, 16);
            this.avgEntriesPerDayLabel.TabIndex = 5;
            this.avgEntriesPerDayLabel.Text = "Placeholder";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.Location = new System.Drawing.Point(48, 101);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(112, 16);
            this.label7.TabIndex = 4;
            this.label7.Text = "Latest timestamp:";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.Location = new System.Drawing.Point(39, 85);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(121, 16);
            this.label6.TabIndex = 3;
            this.label6.Text = "Earliest timestamp:";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(7, 69);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(153, 16);
            this.label5.TabIndex = 2;
            this.label5.Text = "Newest entry timestamp:";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(13, 53);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(147, 16);
            this.label4.TabIndex = 1;
            this.label4.Text = "Oldest entry timestamp:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(6, 37);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(154, 16);
            this.label3.TabIndex = 0;
            this.label3.Text = "Avg. # of entries per day:";
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
            this.hideKey.TabIndex = 12;
            this.hideKey.Text = "Hide";
            this.hideKey.UseVisualStyleBackColor = true;
            this.hideKey.Click += new System.EventHandler(this.hideKey_Click);
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label8.Location = new System.Drawing.Point(12, 260);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(149, 20);
            this.label8.TabIndex = 13;
            this.label8.Text = "Filter on Content:";
            // 
            // filterContentTextBox
            // 
            this.filterContentTextBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.filterContentTextBox.Location = new System.Drawing.Point(167, 257);
            this.filterContentTextBox.Name = "filterContentTextBox";
            this.filterContentTextBox.Size = new System.Drawing.Size(242, 26);
            this.filterContentTextBox.TabIndex = 0;
            // 
            // filterTypeTextBox
            // 
            this.filterTypeTextBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.filterTypeTextBox.Location = new System.Drawing.Point(167, 295);
            this.filterTypeTextBox.Name = "filterTypeTextBox";
            this.filterTypeTextBox.Size = new System.Drawing.Size(242, 26);
            this.filterTypeTextBox.TabIndex = 1;
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label10.Location = new System.Drawing.Point(39, 298);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(123, 20);
            this.label10.TabIndex = 15;
            this.label10.Text = "Filter on Type:";
            // 
            // resetTypeKey
            // 
            this.resetTypeKey.AutoSize = true;
            this.resetTypeKey.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.resetTypeKey.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Lime;
            this.resetTypeKey.FlatAppearance.MouseOverBackColor = System.Drawing.Color.White;
            this.resetTypeKey.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.resetTypeKey.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.resetTypeKey.Location = new System.Drawing.Point(412, 292);
            this.resetTypeKey.Margin = new System.Windows.Forms.Padding(0);
            this.resetTypeKey.Name = "resetTypeKey";
            this.resetTypeKey.Size = new System.Drawing.Size(69, 32);
            this.resetTypeKey.TabIndex = 17;
            this.resetTypeKey.Text = "Reset";
            this.resetTypeKey.UseVisualStyleBackColor = true;
            this.resetTypeKey.Click += new System.EventHandler(this.resetTypeKey_Click);
            // 
            // resetContentKey
            // 
            this.resetContentKey.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.resetContentKey.AutoSize = true;
            this.resetContentKey.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.resetContentKey.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Lime;
            this.resetContentKey.FlatAppearance.MouseOverBackColor = System.Drawing.Color.White;
            this.resetContentKey.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.resetContentKey.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.resetContentKey.Location = new System.Drawing.Point(412, 254);
            this.resetContentKey.Margin = new System.Windows.Forms.Padding(0);
            this.resetContentKey.Name = "resetContentKey";
            this.resetContentKey.Size = new System.Drawing.Size(69, 32);
            this.resetContentKey.TabIndex = 18;
            this.resetContentKey.Text = "Reset";
            this.resetContentKey.UseVisualStyleBackColor = true;
            this.resetContentKey.Click += new System.EventHandler(this.resetContentKey_Click);
            // 
            // FormLogStatisticsUi
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.ClientSize = new System.Drawing.Size(490, 461);
            this.Controls.Add(this.resetContentKey);
            this.Controls.Add(this.resetTypeKey);
            this.Controls.Add(this.filterTypeTextBox);
            this.Controls.Add(this.label10);
            this.Controls.Add(this.filterContentTextBox);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.hideKey);
            this.Controls.Add(this.groupBox);
            this.Controls.Add(this.rangeKey);
            this.Controls.Add(this.overallKey);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.endDatePicker);
            this.Controls.Add(this.startDatePicker);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FormLogStatisticsUi";
            this.Text = "Log Statistics";
            this.TopMost = true;
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.this_FormClosing);
            this.groupBox.ResumeLayout(false);
            this.groupBox.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MonthCalendar endDatePicker;
        private System.Windows.Forms.MonthCalendar startDatePicker;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button overallKey;
        private System.Windows.Forms.Button rangeKey;
        private System.Windows.Forms.GroupBox groupBox;
        private System.Windows.Forms.Label latestTimestampLabel;
        private System.Windows.Forms.Label earliestTimestampLabel;
        private System.Windows.Forms.Label newestTimestampLabel;
        private System.Windows.Forms.Label oldestTimestampLabel;
        private System.Windows.Forms.Label avgEntriesPerDayLabel;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button hideKey;
        private System.Windows.Forms.Label totalEntryCountLabel;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TextBox filterContentTextBox;
        private System.Windows.Forms.TextBox filterTypeTextBox;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Button resetTypeKey;
        private System.Windows.Forms.Button resetContentKey;
    }
}