namespace xofz.UI.Forms
{
    partial class UserControlVncUi
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
            this.remoteDesktop = new VncSharp.RemoteDesktop();
            this.SuspendLayout();
            // 
            // remoteDesktop
            // 
            this.remoteDesktop.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.remoteDesktop.AutoScroll = true;
            this.remoteDesktop.AutoScrollMinSize = new System.Drawing.Size(608, 427);
            this.remoteDesktop.Location = new System.Drawing.Point(0, 0);
            this.remoteDesktop.Margin = new System.Windows.Forms.Padding(0);
            this.remoteDesktop.Name = "remoteDesktop";
            this.remoteDesktop.Size = new System.Drawing.Size(640, 480);
            this.remoteDesktop.TabIndex = 0;
            this.remoteDesktop.ConnectionLost += new System.EventHandler(this.remoteDesktop_ConnectionLost);
            // 
            // UserControlVncUi
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.Controls.Add(this.remoteDesktop);
            this.Name = "UserControlVncUi";
            this.Size = new System.Drawing.Size(640, 480);
            this.ResumeLayout(false);

        }

        #endregion

        private VncSharp.RemoteDesktop remoteDesktop;
    }
}
