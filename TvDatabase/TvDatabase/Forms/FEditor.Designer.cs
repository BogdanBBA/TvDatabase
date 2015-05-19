namespace TvDatabase.Forms
{
    partial class FEditor
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FEditor));
            this.tabViewP = new System.Windows.Forms.Panel();
            this.searchP = new System.Windows.Forms.Panel();
            this.followedP = new System.Windows.Forms.Panel();
            this.searchStatusL = new System.Windows.Forms.Label();
            this.overviewL = new System.Windows.Forms.Label();
            this.firstAiredL = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.nameL = new System.Windows.Forms.Label();
            this.searchButtonP = new System.Windows.Forms.Panel();
            this.label4 = new System.Windows.Forms.Label();
            this.summaryLB = new System.Windows.Forms.ListBox();
            this.label2 = new System.Windows.Forms.Label();
            this.searchTB = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.editP = new System.Windows.Forms.Panel();
            this.label7 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.menuP = new System.Windows.Forms.Panel();
            this.hideSearchStatusT = new System.Windows.Forms.Timer(this.components);
            this.label5 = new System.Windows.Forms.Label();
            this.searchP.SuspendLayout();
            this.editP.SuspendLayout();
            this.SuspendLayout();
            // 
            // tabViewP
            // 
            this.tabViewP.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(16)))), ((int)(((byte)(16)))), ((int)(((byte)(16)))));
            this.tabViewP.Location = new System.Drawing.Point(12, 12);
            this.tabViewP.Name = "tabViewP";
            this.tabViewP.Size = new System.Drawing.Size(500, 514);
            this.tabViewP.TabIndex = 0;
            // 
            // searchP
            // 
            this.searchP.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(16)))), ((int)(((byte)(16)))), ((int)(((byte)(16)))));
            this.searchP.Controls.Add(this.followedP);
            this.searchP.Controls.Add(this.searchStatusL);
            this.searchP.Controls.Add(this.overviewL);
            this.searchP.Controls.Add(this.firstAiredL);
            this.searchP.Controls.Add(this.label6);
            this.searchP.Controls.Add(this.nameL);
            this.searchP.Controls.Add(this.searchButtonP);
            this.searchP.Controls.Add(this.label4);
            this.searchP.Controls.Add(this.summaryLB);
            this.searchP.Controls.Add(this.label2);
            this.searchP.Controls.Add(this.searchTB);
            this.searchP.Controls.Add(this.label1);
            this.searchP.Location = new System.Drawing.Point(521, 12);
            this.searchP.Name = "searchP";
            this.searchP.Size = new System.Drawing.Size(500, 464);
            this.searchP.TabIndex = 1;
            // 
            // followedP
            // 
            this.followedP.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(16)))), ((int)(((byte)(16)))), ((int)(((byte)(16)))));
            this.followedP.Location = new System.Drawing.Point(257, 411);
            this.followedP.Name = "followedP";
            this.followedP.Size = new System.Drawing.Size(239, 39);
            this.followedP.TabIndex = 15;
            // 
            // searchStatusL
            // 
            this.searchStatusL.AutoSize = true;
            this.searchStatusL.Font = new System.Drawing.Font("Segoe UI", 7F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.searchStatusL.ForeColor = System.Drawing.Color.White;
            this.searchStatusL.Location = new System.Drawing.Point(8, 95);
            this.searchStatusL.Name = "searchStatusL";
            this.searchStatusL.Size = new System.Drawing.Size(64, 12);
            this.searchStatusL.TabIndex = 12;
            this.searchStatusL.Text = "searchStatusL";
            // 
            // overviewL
            // 
            this.overviewL.Font = new System.Drawing.Font("Segoe UI", 8F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.overviewL.Location = new System.Drawing.Point(258, 186);
            this.overviewL.Name = "overviewL";
            this.overviewL.Size = new System.Drawing.Size(227, 222);
            this.overviewL.TabIndex = 10;
            this.overviewL.Text = "Search result";
            // 
            // firstAiredL
            // 
            this.firstAiredL.AutoSize = true;
            this.firstAiredL.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.firstAiredL.Location = new System.Drawing.Point(254, 152);
            this.firstAiredL.Name = "firstAiredL";
            this.firstAiredL.Size = new System.Drawing.Size(90, 20);
            this.firstAiredL.TabIndex = 9;
            this.firstAiredL.Text = "Search result";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.ForeColor = System.Drawing.Color.Gray;
            this.label6.Location = new System.Drawing.Point(254, 107);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(117, 21);
            this.label6.TabIndex = 8;
            this.label6.Text = "Selected details";
            // 
            // nameL
            // 
            this.nameL.AutoSize = true;
            this.nameL.Font = new System.Drawing.Font("Segoe UI", 12F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.nameL.Location = new System.Drawing.Point(254, 131);
            this.nameL.Name = "nameL";
            this.nameL.Size = new System.Drawing.Size(105, 21);
            this.nameL.TabIndex = 7;
            this.nameL.Text = "Search result";
            // 
            // searchButtonP
            // 
            this.searchButtonP.Location = new System.Drawing.Point(396, 56);
            this.searchButtonP.Name = "searchButtonP";
            this.searchButtonP.Size = new System.Drawing.Size(100, 39);
            this.searchButtonP.TabIndex = 6;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.ForeColor = System.Drawing.Color.Gray;
            this.label4.Location = new System.Drawing.Point(4, 107);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(107, 21);
            this.label4.TabIndex = 5;
            this.label4.Text = "Search results";
            // 
            // summaryLB
            // 
            this.summaryLB.FormattingEnabled = true;
            this.summaryLB.ItemHeight = 21;
            this.summaryLB.Location = new System.Drawing.Point(4, 131);
            this.summaryLB.Name = "summaryLB";
            this.summaryLB.Size = new System.Drawing.Size(232, 319);
            this.summaryLB.TabIndex = 4;
            this.summaryLB.SelectedIndexChanged += new System.EventHandler(this.summaryLB_SelectedIndexChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.ForeColor = System.Drawing.Color.Gray;
            this.label2.Location = new System.Drawing.Point(4, 37);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(249, 21);
            this.label2.TabIndex = 2;
            this.label2.Text = "Type the name of the series below:";
            // 
            // searchTB
            // 
            this.searchTB.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.searchTB.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(48)))), ((int)(((byte)(48)))), ((int)(((byte)(48)))));
            this.searchTB.Location = new System.Drawing.Point(4, 61);
            this.searchTB.Name = "searchTB";
            this.searchTB.Size = new System.Drawing.Size(386, 29);
            this.searchTB.TabIndex = 1;
            this.searchTB.Text = "game";
            this.searchTB.KeyDown += new System.Windows.Forms.KeyEventHandler(this.searchTB_KeyDown);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Segoe UI", 14.25F, System.Drawing.FontStyle.Bold);
            this.label1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(1)))), ((int)(((byte)(96)))), ((int)(((byte)(183)))));
            this.label1.Location = new System.Drawing.Point(3, 12);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(218, 25);
            this.label1.TabIndex = 0;
            this.label1.Text = "Search for series online";
            // 
            // editP
            // 
            this.editP.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(16)))), ((int)(((byte)(16)))), ((int)(((byte)(16)))));
            this.editP.Controls.Add(this.label7);
            this.editP.Controls.Add(this.label3);
            this.editP.Location = new System.Drawing.Point(1024, 12);
            this.editP.Name = "editP";
            this.editP.Size = new System.Drawing.Size(500, 464);
            this.editP.TabIndex = 2;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.ForeColor = System.Drawing.Color.Gray;
            this.label7.Location = new System.Drawing.Point(4, 37);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(108, 21);
            this.label7.TabIndex = 3;
            this.label7.Text = "Coming soon!";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Segoe UI", 14.25F, System.Drawing.FontStyle.Bold);
            this.label3.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(1)))), ((int)(((byte)(96)))), ((int)(((byte)(183)))));
            this.label3.Location = new System.Drawing.Point(3, 12);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(123, 25);
            this.label3.TabIndex = 1;
            this.label3.Text = "Series editor";
            // 
            // menuP
            // 
            this.menuP.Location = new System.Drawing.Point(167, 538);
            this.menuP.Name = "menuP";
            this.menuP.Size = new System.Drawing.Size(200, 50);
            this.menuP.TabIndex = 3;
            // 
            // hideSearchStatusT
            // 
            this.hideSearchStatusT.Interval = 2000;
            this.hideSearchStatusT.Tick += new System.EventHandler(this.hideSearchStatusT_Tick);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.ForeColor = System.Drawing.Color.Yellow;
            this.label5.Location = new System.Drawing.Point(543, 479);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(752, 21);
            this.label5.TabIndex = 4;
            this.label5.Text = "Be careful about resizing anything except the form\'s width! The tab view uses a f" +
    "ixed header height for now.";
            this.label5.Visible = false;
            // 
            // FEditor
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(16)))), ((int)(((byte)(16)))), ((int)(((byte)(16)))));
            this.ClientSize = new System.Drawing.Size(1362, 600);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.menuP);
            this.Controls.Add(this.editP);
            this.Controls.Add(this.searchP);
            this.Controls.Add(this.tabViewP);
            this.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ForeColor = System.Drawing.Color.White;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "FEditor";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Data editor";
            this.Load += new System.EventHandler(this.FEditor_Load);
            this.VisibleChanged += new System.EventHandler(this.FEditor_VisibleChanged);
            this.searchP.ResumeLayout(false);
            this.searchP.PerformLayout();
            this.editP.ResumeLayout(false);
            this.editP.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel tabViewP;
        private System.Windows.Forms.Panel searchP;
        private System.Windows.Forms.Panel editP;
        private System.Windows.Forms.Panel menuP;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox searchTB;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Panel searchButtonP;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ListBox summaryLB;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label nameL;
        private System.Windows.Forms.Label overviewL;
        private System.Windows.Forms.Label firstAiredL;
        private System.Windows.Forms.Label searchStatusL;
        private System.Windows.Forms.Timer hideSearchStatusT;
        private System.Windows.Forms.Panel followedP;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label7;
    }
}