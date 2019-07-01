namespace xofz.UI.Forms
{
    partial class FormLogEditorUi
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
            this.customTypeTextBox = new System.Windows.Forms.TextBox();
            this.contentTextBox = new System.Windows.Forms.TextBox();
            this.entryTypeComboBox = new System.Windows.Forms.ComboBox();
            this.addKey = new System.Windows.Forms.Button();
            this.entryContentLabel = new System.Windows.Forms.Label();
            this.typeLabel = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // customTypeTextBox
            // 
            this.customTypeTextBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.customTypeTextBox.Location = new System.Drawing.Point(319, 14);
            this.customTypeTextBox.Name = "customTypeTextBox";
            this.customTypeTextBox.Size = new System.Drawing.Size(207, 26);
            this.customTypeTextBox.TabIndex = 1;
            // 
            // contentTextBox
            // 
            this.contentTextBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.contentTextBox.Location = new System.Drawing.Point(108, 49);
            this.contentTextBox.Multiline = true;
            this.contentTextBox.Name = "contentTextBox";
            this.contentTextBox.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.contentTextBox.Size = new System.Drawing.Size(524, 150);
            this.contentTextBox.TabIndex = 2;
            // 
            // entryTypeComboBox
            // 
            this.entryTypeComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.entryTypeComboBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.entryTypeComboBox.FormattingEnabled = true;
            this.entryTypeComboBox.Items.AddRange(new object[] {
            "Information",
            "Warning",
            "Error"});
            this.entryTypeComboBox.Location = new System.Drawing.Point(108, 14);
            this.entryTypeComboBox.Name = "entryTypeComboBox";
            this.entryTypeComboBox.Size = new System.Drawing.Size(205, 28);
            this.entryTypeComboBox.TabIndex = 0;
            this.entryTypeComboBox.SelectedIndexChanged += new System.EventHandler(this.entryTypeComboBox_SelectedIndexChanged);
            // 
            // addKey
            // 
            this.addKey.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.addKey.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Lime;
            this.addKey.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.addKey.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.addKey.Location = new System.Drawing.Point(532, 11);
            this.addKey.Name = "addKey";
            this.addKey.Size = new System.Drawing.Size(100, 32);
            this.addKey.TabIndex = 3;
            this.addKey.Text = "Add Entry";
            this.addKey.UseVisualStyleBackColor = true;
            this.addKey.Click += new System.EventHandler(this.addKey_Click);
            // 
            // entryContentLabel
            // 
            this.entryContentLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.entryContentLabel.Location = new System.Drawing.Point(12, 49);
            this.entryContentLabel.Name = "entryContentLabel";
            this.entryContentLabel.Size = new System.Drawing.Size(90, 32);
            this.entryContentLabel.TabIndex = 6;
            this.entryContentLabel.Text = "Content:";
            this.entryContentLabel.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // typeLabel
            // 
            this.typeLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.typeLabel.Location = new System.Drawing.Point(41, 11);
            this.typeLabel.Name = "typeLabel";
            this.typeLabel.Size = new System.Drawing.Size(61, 32);
            this.typeLabel.TabIndex = 5;
            this.typeLabel.Text = "Type:";
            this.typeLabel.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // FormLogEditorUi
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.ClientSize = new System.Drawing.Size(644, 211);
            this.Controls.Add(this.customTypeTextBox);
            this.Controls.Add(this.contentTextBox);
            this.Controls.Add(this.entryTypeComboBox);
            this.Controls.Add(this.addKey);
            this.Controls.Add(this.entryContentLabel);
            this.Controls.Add(this.typeLabel);
            this.MaximizeBox = false;
            this.Name = "FormLogEditorUi";
            this.ShowIcon = false;
            this.Text = "Add Log Entry";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.this_FormClosing);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox contentTextBox;
        private System.Windows.Forms.ComboBox entryTypeComboBox;
        private System.Windows.Forms.Button addKey;
        private System.Windows.Forms.Label entryContentLabel;
        private System.Windows.Forms.Label typeLabel;
        private System.Windows.Forms.TextBox customTypeTextBox;
    }
}