namespace TvDatabase.Forms
{
    partial class FMain
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FMain));
            this.mainMenuP = new System.Windows.Forms.Panel();
            this.highlightP = new System.Windows.Forms.Panel();
            this.contentContainerP = new System.Windows.Forms.Panel();
            this.trayIcon = new System.Windows.Forms.NotifyIcon(this.components);
            this.trayMS = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.showAppMI = new System.Windows.Forms.ToolStripMenuItem();
            this.exitMI = new System.Windows.Forms.ToolStripMenuItem();
            this.trayMS.SuspendLayout();
            this.SuspendLayout();
            // 
            // mainMenuP
            // 
            this.mainMenuP.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(16)))), ((int)(((byte)(16)))), ((int)(((byte)(16)))));
            this.mainMenuP.Location = new System.Drawing.Point(274, 57);
            this.mainMenuP.Name = "mainMenuP";
            this.mainMenuP.Size = new System.Drawing.Size(200, 100);
            this.mainMenuP.TabIndex = 1;
            // 
            // highlightP
            // 
            this.highlightP.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(16)))), ((int)(((byte)(16)))), ((int)(((byte)(16)))));
            this.highlightP.Location = new System.Drawing.Point(125, 180);
            this.highlightP.Name = "highlightP";
            this.highlightP.Size = new System.Drawing.Size(200, 100);
            this.highlightP.TabIndex = 2;
            // 
            // contentContainerP
            // 
            this.contentContainerP.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(16)))), ((int)(((byte)(16)))), ((int)(((byte)(16)))));
            this.contentContainerP.Location = new System.Drawing.Point(227, 286);
            this.contentContainerP.Name = "contentContainerP";
            this.contentContainerP.Size = new System.Drawing.Size(200, 100);
            this.contentContainerP.TabIndex = 3;
            // 
            // trayIcon
            // 
            this.trayIcon.BalloonTipIcon = System.Windows.Forms.ToolTipIcon.Info;
            this.trayIcon.BalloonTipText = "Click to open the application interface.\r\n";
            this.trayIcon.BalloonTipTitle = "TV Database";
            this.trayIcon.ContextMenuStrip = this.trayMS;
            this.trayIcon.Icon = ((System.Drawing.Icon)(resources.GetObject("trayIcon.Icon")));
            this.trayIcon.Text = "TvDb";
            // 
            // trayMS
            // 
            this.trayMS.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.showAppMI,
            this.exitMI});
            this.trayMS.Name = "trayMS";
            this.trayMS.Size = new System.Drawing.Size(230, 48);
            // 
            // showAppMI
            // 
            this.showAppMI.Name = "showAppMI";
            this.showAppMI.Size = new System.Drawing.Size(229, 22);
            this.showAppMI.Text = "Show application interface";
            this.showAppMI.Click += new System.EventHandler(this.showAppMI_Click);
            // 
            // exitMI
            // 
            this.exitMI.Name = "exitMI";
            this.exitMI.Size = new System.Drawing.Size(229, 22);
            this.exitMI.Text = "Exit application";
            this.exitMI.Click += new System.EventHandler(this.exitMI_Click);
            // 
            // FMain
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.BackColor = System.Drawing.SystemColors.ControlDark;
            this.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("$this.BackgroundImage")));
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ClientSize = new System.Drawing.Size(850, 467);
            this.ControlBox = false;
            this.Controls.Add(this.contentContainerP);
            this.Controls.Add(this.highlightP);
            this.Controls.Add(this.mainMenuP);
            this.DoubleBuffered = true;
            this.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "FMain";
            this.Text = "TV Database";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.FMain_Load);
            this.Shown += new System.EventHandler(this.FMain_Shown);
            this.Resize += new System.EventHandler(this.FMain_Resize);
            this.trayMS.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel mainMenuP;
        private System.Windows.Forms.Panel highlightP;
        private System.Windows.Forms.Panel contentContainerP;
        private System.Windows.Forms.NotifyIcon trayIcon;
        private System.Windows.Forms.ContextMenuStrip trayMS;
        private System.Windows.Forms.ToolStripMenuItem showAppMI;
        private System.Windows.Forms.ToolStripMenuItem exitMI;
    }
}