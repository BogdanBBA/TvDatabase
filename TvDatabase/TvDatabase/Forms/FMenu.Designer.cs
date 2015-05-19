﻿namespace TvDatabase.Forms
{
	partial class FMenu
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
            this.SFD = new System.Windows.Forms.SaveFileDialog();
            this.OFD = new System.Windows.Forms.OpenFileDialog();
            this.SuspendLayout();
            // 
            // SFD
            // 
            this.SFD.Filter = "TV Database file|*.xml";
            this.SFD.Title = "Save Database";
            // 
            // OFD
            // 
            this.OFD.Filter = "TV Database file|*.xml";
            this.OFD.Title = "Open Database";
            // 
            // FMenu
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ControlDark;
            this.ClientSize = new System.Drawing.Size(284, 262);
            this.Cursor = System.Windows.Forms.Cursors.Hand;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "FMenu";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.Text = "FMenu";
            this.TopMost = true;
            this.Deactivate += new System.EventHandler(this.FMenu_Deactivate);
            this.Load += new System.EventHandler(this.FMenu_Load);
            this.ResumeLayout(false);

		}

		#endregion

        private System.Windows.Forms.SaveFileDialog SFD;
        private System.Windows.Forms.OpenFileDialog OFD;
	}
}