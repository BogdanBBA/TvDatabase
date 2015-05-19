namespace TvDatabase.Forms
{
    partial class FUpdater
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FUpdater));
            this.titleL = new System.Windows.Forms.Label();
            this.statusPercentageL = new System.Windows.Forms.Label();
            this.statusL = new System.Windows.Forms.Label();
            this.menuP = new System.Windows.Forms.Panel();
            this.errorL = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // titleL
            // 
            this.titleL.AutoSize = true;
            this.titleL.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(16)))), ((int)(((byte)(16)))), ((int)(((byte)(16)))));
            this.titleL.Font = new System.Drawing.Font("Odessa LET", 36F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.titleL.Location = new System.Drawing.Point(47, 29);
            this.titleL.Name = "titleL";
            this.titleL.Size = new System.Drawing.Size(226, 56);
            this.titleL.TabIndex = 0;
            this.titleL.Text = "Updating...";
            // 
            // statusPercentageL
            // 
            this.statusPercentageL.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(16)))), ((int)(((byte)(16)))), ((int)(((byte)(16)))));
            this.statusPercentageL.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.statusPercentageL.ForeColor = System.Drawing.Color.White;
            this.statusPercentageL.Location = new System.Drawing.Point(53, 100);
            this.statusPercentageL.Name = "statusPercentageL";
            this.statusPercentageL.Size = new System.Drawing.Size(67, 21);
            this.statusPercentageL.TabIndex = 11;
            this.statusPercentageL.Text = "StatusP";
            this.statusPercentageL.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // statusL
            // 
            this.statusL.AutoSize = true;
            this.statusL.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(16)))), ((int)(((byte)(16)))), ((int)(((byte)(16)))));
            this.statusL.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.statusL.ForeColor = System.Drawing.Color.White;
            this.statusL.Location = new System.Drawing.Point(126, 100);
            this.statusL.Name = "statusL";
            this.statusL.Size = new System.Drawing.Size(53, 21);
            this.statusL.TabIndex = 14;
            this.statusL.Text = "Status";
            // 
            // menuP
            // 
            this.menuP.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(16)))), ((int)(((byte)(16)))), ((int)(((byte)(16)))));
            this.menuP.Location = new System.Drawing.Point(249, 279);
            this.menuP.Name = "menuP";
            this.menuP.Size = new System.Drawing.Size(200, 50);
            this.menuP.TabIndex = 15;
            // 
            // errorL
            // 
            this.errorL.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(16)))), ((int)(((byte)(16)))), ((int)(((byte)(16)))));
            this.errorL.Font = new System.Drawing.Font("Ubuntu Mono", 8.25F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.errorL.ForeColor = System.Drawing.Color.White;
            this.errorL.Location = new System.Drawing.Point(54, 157);
            this.errorL.Name = "errorL";
            this.errorL.Size = new System.Drawing.Size(587, 119);
            this.errorL.TabIndex = 16;
            this.errorL.Text = "Status";
            this.errorL.Click += new System.EventHandler(this.errorL_Click);
            // 
            // FUpdater
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(16)))), ((int)(((byte)(16)))), ((int)(((byte)(16)))));
            this.ClientSize = new System.Drawing.Size(700, 350);
            this.ControlBox = false;
            this.Controls.Add(this.errorL);
            this.Controls.Add(this.menuP);
            this.Controls.Add(this.statusL);
            this.Controls.Add(this.statusPercentageL);
            this.Controls.Add(this.titleL);
            this.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ForeColor = System.Drawing.Color.White;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "FUpdater";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Update series";
            this.Load += new System.EventHandler(this.FUpdater_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label titleL;
        private System.Windows.Forms.Label statusPercentageL;
        private System.Windows.Forms.Label statusL;
        private System.Windows.Forms.Panel menuP;
        private System.Windows.Forms.Label errorL;
    }
}