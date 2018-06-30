namespace xofz.UI.Forms
{
    partial class UserControlToggleUi
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
            this.key = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // key
            // 
            this.key.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.key.FlatAppearance.MouseDownBackColor = System.Drawing.Color.GhostWhite;
            this.key.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.key.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.key.Location = new System.Drawing.Point(0, 0);
            this.key.Margin = new System.Windows.Forms.Padding(0);
            this.key.Name = "key";
            this.key.Size = new System.Drawing.Size(100, 94);
            this.key.TabIndex = 3;
            this.key.Text = "Sample Label";
            this.key.UseVisualStyleBackColor = true;
            this.key.Click += new System.EventHandler(this.key_Click);
            this.key.MouseDown += new System.Windows.Forms.MouseEventHandler(this.key_MouseDown);
            this.key.MouseUp += new System.Windows.Forms.MouseEventHandler(this.key_MouseUp);
            // 
            // UserControlToggleUi
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.Controls.Add(this.key);
            this.Name = "UserControlToggleUi";
            this.Size = new System.Drawing.Size(100, 94);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button key;
    }
}
