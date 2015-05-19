namespace TvDatabase.Forms
{
    partial class FInitialize
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FInitialize));
            this.progressL = new System.Windows.Forms.Label();
            this.statusL = new System.Windows.Forms.Label();
            this.errorL = new System.Windows.Forms.Label();
            this.exitL = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // progressL
            // 
            this.progressL.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(16)))), ((int)(((byte)(16)))), ((int)(((byte)(16)))));
            this.progressL.Font = new System.Drawing.Font("Segoe UI Semibold", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.progressL.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(127)))), ((int)(((byte)(127)))), ((int)(((byte)(127)))));
            this.progressL.Location = new System.Drawing.Point(41, 126);
            this.progressL.Name = "progressL";
            this.progressL.Size = new System.Drawing.Size(70, 30);
            this.progressL.TabIndex = 0;
            this.progressL.Text = "label1";
            this.progressL.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // statusL
            // 
            this.statusL.AutoSize = true;
            this.statusL.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(16)))), ((int)(((byte)(16)))), ((int)(((byte)(16)))));
            this.statusL.Font = new System.Drawing.Font("Segoe UI Light", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.statusL.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(127)))), ((int)(((byte)(127)))), ((int)(((byte)(127)))));
            this.statusL.Location = new System.Drawing.Point(117, 126);
            this.statusL.Name = "statusL";
            this.statusL.Size = new System.Drawing.Size(65, 30);
            this.statusL.TabIndex = 1;
            this.statusL.Text = "label2";
            this.statusL.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // errorL
            // 
            this.errorL.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(16)))), ((int)(((byte)(16)))), ((int)(((byte)(16)))));
            this.errorL.Cursor = System.Windows.Forms.Cursors.Hand;
            this.errorL.Font = new System.Drawing.Font("Ubuntu Mono", 8.25F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.errorL.ForeColor = System.Drawing.Color.White;
            this.errorL.Location = new System.Drawing.Point(37, 156);
            this.errorL.Name = "errorL";
            this.errorL.Size = new System.Drawing.Size(621, 135);
            this.errorL.TabIndex = 2;
            this.errorL.Text = "label3 a x c s fd fd df";
            this.errorL.Visible = false;
            this.errorL.Click += new System.EventHandler(this.errorL_Click);
            // 
            // exitL
            // 
            this.exitL.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(16)))), ((int)(((byte)(16)))), ((int)(((byte)(16)))));
            this.exitL.Cursor = System.Windows.Forms.Cursors.Hand;
            this.exitL.Font = new System.Drawing.Font("Segoe UI", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.exitL.ForeColor = System.Drawing.Color.WhiteSmoke;
            this.exitL.Location = new System.Drawing.Point(301, 291);
            this.exitL.Name = "exitL";
            this.exitL.Size = new System.Drawing.Size(95, 30);
            this.exitL.TabIndex = 5;
            this.exitL.Text = "Exit";
            this.exitL.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.exitL.Visible = false;
            this.exitL.Click += new System.EventHandler(this.exitL_Click);
            // 
            // FInitialize
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.BackColor = System.Drawing.Color.CadetBlue;
            this.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("$this.BackgroundImage")));
            this.ClientSize = new System.Drawing.Size(700, 350);
            this.Controls.Add(this.exitL);
            this.Controls.Add(this.errorL);
            this.Controls.Add(this.statusL);
            this.Controls.Add(this.progressL);
            this.Font = new System.Drawing.Font("Ubuntu", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "FInitialize";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Initializing TV Database";
            this.Load += new System.EventHandler(this.FInitialize_Load);
            this.Shown += new System.EventHandler(this.FInitialize_Shown);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label progressL;
        private System.Windows.Forms.Label statusL;
        private System.Windows.Forms.Label errorL;
        private System.Windows.Forms.Label exitL;
    }
}

