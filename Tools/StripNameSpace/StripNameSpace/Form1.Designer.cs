namespace StripNameSpace {
    partial class Form1 {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing) {
            if (disposing && (components != null)) {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent() {
            this.ofd = new System.Windows.Forms.OpenFileDialog();
            this.btnOpen = new System.Windows.Forms.Button();
            this.btnStrip = new System.Windows.Forms.Button();
            this.lstFiles = new System.Windows.Forms.ListBox();
            this.txtNS = new System.Windows.Forms.TextBox();
            this.lblNS = new System.Windows.Forms.Label();
            this.bgw = new System.ComponentModel.BackgroundWorker();
            this.SuspendLayout();
            // 
            // ofd
            // 
            this.ofd.Multiselect = true;
            this.ofd.Title = "Files to Strip";
            this.ofd.FileOk += new System.ComponentModel.CancelEventHandler(this.ofd_FileOk);
            // 
            // btnOpen
            // 
            this.btnOpen.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnOpen.Location = new System.Drawing.Point(487, 51);
            this.btnOpen.Name = "btnOpen";
            this.btnOpen.Size = new System.Drawing.Size(75, 23);
            this.btnOpen.TabIndex = 1;
            this.btnOpen.Text = "Open";
            this.btnOpen.UseVisualStyleBackColor = true;
            this.btnOpen.Click += new System.EventHandler(this.btnOpen_Click);
            // 
            // btnStrip
            // 
            this.btnStrip.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnStrip.Location = new System.Drawing.Point(487, 80);
            this.btnStrip.Name = "btnStrip";
            this.btnStrip.Size = new System.Drawing.Size(75, 23);
            this.btnStrip.TabIndex = 2;
            this.btnStrip.Text = "Strip";
            this.btnStrip.UseVisualStyleBackColor = true;
            this.btnStrip.Click += new System.EventHandler(this.btnStrip_Click);
            // 
            // lstFiles
            // 
            this.lstFiles.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lstFiles.FormattingEnabled = true;
            this.lstFiles.Location = new System.Drawing.Point(12, 12);
            this.lstFiles.Name = "lstFiles";
            this.lstFiles.Size = new System.Drawing.Size(469, 238);
            this.lstFiles.TabIndex = 2;
            this.lstFiles.TabStop = false;
            // 
            // txtNS
            // 
            this.txtNS.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.txtNS.Location = new System.Drawing.Point(487, 25);
            this.txtNS.Name = "txtNS";
            this.txtNS.Size = new System.Drawing.Size(75, 20);
            this.txtNS.TabIndex = 0;
            // 
            // lblNS
            // 
            this.lblNS.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lblNS.AutoSize = true;
            this.lblNS.Location = new System.Drawing.Point(484, 9);
            this.lblNS.Name = "lblNS";
            this.lblNS.Size = new System.Drawing.Size(53, 13);
            this.lblNS.TabIndex = 4;
            this.lblNS.Text = "NS Name";
            // 
            // bgw
            // 
            this.bgw.DoWork += new System.ComponentModel.DoWorkEventHandler(this.bgw_DoWork);
            this.bgw.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.bgw_RunWorkerCompleted);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(574, 261);
            this.Controls.Add(this.lblNS);
            this.Controls.Add(this.txtNS);
            this.Controls.Add(this.lstFiles);
            this.Controls.Add(this.btnStrip);
            this.Controls.Add(this.btnOpen);
            this.Name = "Form1";
            this.Text = "Strip NS";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.OpenFileDialog ofd;
        private System.Windows.Forms.Button btnOpen;
        private System.Windows.Forms.Button btnStrip;
        private System.Windows.Forms.ListBox lstFiles;
        private System.Windows.Forms.TextBox txtNS;
        private System.Windows.Forms.Label lblNS;
        private System.ComponentModel.BackgroundWorker bgw;
    }
}

