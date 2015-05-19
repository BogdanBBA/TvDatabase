namespace TvDatabase.Forms
{
    partial class FSettings
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FSettings));
            this.tabViewP = new System.Windows.Forms.Panel();
            this.generalSettP = new System.Windows.Forms.Panel();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.updateSettP = new System.Windows.Forms.Panel();
            this.downloadBannersForActorsChB = new System.Windows.Forms.CheckBox();
            this.label13 = new System.Windows.Forms.Label();
            this.downloadBannersForSeriesChB = new System.Windows.Forms.CheckBox();
            this.label11 = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.apiKeyTB = new System.Windows.Forms.TextBox();
            this.label10 = new System.Windows.Forms.Label();
            this.mirrorUrlTB = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.viewModesP = new System.Windows.Forms.Panel();
            this.showLWEChB = new System.Windows.Forms.CheckBox();
            this.label20 = new System.Windows.Forms.Label();
            this.showRatingChB = new System.Windows.Forms.CheckBox();
            this.label19 = new System.Windows.Forms.Label();
            this.showTitleChB = new System.Windows.Forms.CheckBox();
            this.label18 = new System.Windows.Forms.Label();
            this.scrollDirectionCB = new System.Windows.Forms.ComboBox();
            this.label17 = new System.Windows.Forms.Label();
            this.columnsOfSeriesBoxesNUD = new System.Windows.Forms.NumericUpDown();
            this.rowsOfSeriesBoxesNUD = new System.Windows.Forms.NumericUpDown();
            this.label16 = new System.Windows.Forms.Label();
            this.label15 = new System.Windows.Forms.Label();
            this.label14 = new System.Windows.Forms.Label();
            this.showViewModesCB = new System.Windows.Forms.ComboBox();
            this.label7 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.menuP = new System.Windows.Forms.Panel();
            this.generalSettP.SuspendLayout();
            this.updateSettP.SuspendLayout();
            this.viewModesP.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.columnsOfSeriesBoxesNUD)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.rowsOfSeriesBoxesNUD)).BeginInit();
            this.SuspendLayout();
            // 
            // tabViewP
            // 
            this.tabViewP.Location = new System.Drawing.Point(20, 20);
            this.tabViewP.Name = "tabViewP";
            this.tabViewP.Size = new System.Drawing.Size(660, 310);
            this.tabViewP.TabIndex = 0;
            // 
            // generalSettP
            // 
            this.generalSettP.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(16)))), ((int)(((byte)(16)))), ((int)(((byte)(16)))));
            this.generalSettP.Controls.Add(this.label3);
            this.generalSettP.Controls.Add(this.label2);
            this.generalSettP.Controls.Add(this.label1);
            this.generalSettP.Location = new System.Drawing.Point(117, 392);
            this.generalSettP.Name = "generalSettP";
            this.generalSettP.Size = new System.Drawing.Size(660, 246);
            this.generalSettP.TabIndex = 1;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.ForeColor = System.Drawing.Color.White;
            this.label3.Location = new System.Drawing.Point(-4, 65);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(39, 19);
            this.label3.TabIndex = 5;
            this.label3.Text = "xxxxx";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Segoe UI Semibold", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.Color.Gray;
            this.label2.Location = new System.Drawing.Point(-4, 34);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(50, 21);
            this.label2.TabIndex = 4;
            this.label2.Text = "xxxxx";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Segoe UI", 14.25F, System.Drawing.FontStyle.Bold);
            this.label1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(1)))), ((int)(((byte)(96)))), ((int)(((byte)(183)))));
            this.label1.Location = new System.Drawing.Point(-5, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(309, 25);
            this.label1.TabIndex = 3;
            this.label1.Text = "General application-wide settings";
            // 
            // updateSettP
            // 
            this.updateSettP.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(16)))), ((int)(((byte)(16)))), ((int)(((byte)(16)))));
            this.updateSettP.Controls.Add(this.downloadBannersForActorsChB);
            this.updateSettP.Controls.Add(this.label13);
            this.updateSettP.Controls.Add(this.downloadBannersForSeriesChB);
            this.updateSettP.Controls.Add(this.label11);
            this.updateSettP.Controls.Add(this.label12);
            this.updateSettP.Controls.Add(this.apiKeyTB);
            this.updateSettP.Controls.Add(this.label10);
            this.updateSettP.Controls.Add(this.mirrorUrlTB);
            this.updateSettP.Controls.Add(this.label4);
            this.updateSettP.Controls.Add(this.label5);
            this.updateSettP.Controls.Add(this.label6);
            this.updateSettP.Location = new System.Drawing.Point(686, 20);
            this.updateSettP.Name = "updateSettP";
            this.updateSettP.Size = new System.Drawing.Size(660, 246);
            this.updateSettP.TabIndex = 2;
            // 
            // downloadBannersForActorsChB
            // 
            this.downloadBannersForActorsChB.AutoSize = true;
            this.downloadBannersForActorsChB.Cursor = System.Windows.Forms.Cursors.Hand;
            this.downloadBannersForActorsChB.Location = new System.Drawing.Point(337, 149);
            this.downloadBannersForActorsChB.Name = "downloadBannersForActorsChB";
            this.downloadBannersForActorsChB.Size = new System.Drawing.Size(102, 25);
            this.downloadBannersForActorsChB.TabIndex = 18;
            this.downloadBannersForActorsChB.Text = "checkBox2";
            this.downloadBannersForActorsChB.UseVisualStyleBackColor = true;
            this.downloadBannersForActorsChB.CheckedChanged += new System.EventHandler(this.CheckBox_CheckedChanged);
            this.downloadBannersForActorsChB.CheckStateChanged += new System.EventHandler(this.CheckBox_CheckedChanged);
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label13.ForeColor = System.Drawing.Color.White;
            this.label13.Location = new System.Drawing.Point(333, 126);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(213, 19);
            this.label13.TabIndex = 17;
            this.label13.Text = "Download images for acting roles";
            // 
            // downloadBannersForSeriesChB
            // 
            this.downloadBannersForSeriesChB.AutoSize = true;
            this.downloadBannersForSeriesChB.Cursor = System.Windows.Forms.Cursors.Hand;
            this.downloadBannersForSeriesChB.Location = new System.Drawing.Point(337, 88);
            this.downloadBannersForSeriesChB.Name = "downloadBannersForSeriesChB";
            this.downloadBannersForSeriesChB.Size = new System.Drawing.Size(102, 25);
            this.downloadBannersForSeriesChB.TabIndex = 16;
            this.downloadBannersForSeriesChB.Text = "checkBox1";
            this.downloadBannersForSeriesChB.UseVisualStyleBackColor = true;
            this.downloadBannersForSeriesChB.CheckedChanged += new System.EventHandler(this.CheckBox_CheckedChanged);
            this.downloadBannersForSeriesChB.CheckStateChanged += new System.EventHandler(this.CheckBox_CheckedChanged);
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label11.ForeColor = System.Drawing.Color.White;
            this.label11.Location = new System.Drawing.Point(333, 65);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(259, 19);
            this.label11.TabIndex = 15;
            this.label11.Text = "Download posters and banners for series";
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Font = new System.Drawing.Font("Segoe UI Semibold", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label12.ForeColor = System.Drawing.Color.Gray;
            this.label12.Location = new System.Drawing.Point(333, 34);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(83, 21);
            this.label12.TabIndex = 14;
            this.label12.Text = "Behaviour";
            // 
            // apiKeyTB
            // 
            this.apiKeyTB.Font = new System.Drawing.Font("Segoe UI", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.apiKeyTB.Location = new System.Drawing.Point(0, 148);
            this.apiKeyTB.Name = "apiKeyTB";
            this.apiKeyTB.Size = new System.Drawing.Size(320, 27);
            this.apiKeyTB.TabIndex = 13;
            this.apiKeyTB.Text = "xxx";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label10.ForeColor = System.Drawing.Color.White;
            this.label10.Location = new System.Drawing.Point(-4, 126);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(124, 19);
            this.label10.TabIndex = 12;
            this.label10.Text = "Authentication key";
            // 
            // mirrorUrlTB
            // 
            this.mirrorUrlTB.Font = new System.Drawing.Font("Segoe UI", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.mirrorUrlTB.Location = new System.Drawing.Point(0, 87);
            this.mirrorUrlTB.Name = "mirrorUrlTB";
            this.mirrorUrlTB.Size = new System.Drawing.Size(320, 27);
            this.mirrorUrlTB.TabIndex = 11;
            this.mirrorUrlTB.Text = "xxx";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.ForeColor = System.Drawing.Color.White;
            this.label4.Location = new System.Drawing.Point(-4, 65);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(80, 19);
            this.label4.TabIndex = 9;
            this.label4.Text = "Mirror path";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Segoe UI Semibold", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.ForeColor = System.Drawing.Color.Gray;
            this.label5.Location = new System.Drawing.Point(-4, 34);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(212, 21);
            this.label5.TabIndex = 8;
            this.label5.Text = "Connection to thetvdb.com";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Segoe UI", 14.25F, System.Drawing.FontStyle.Bold);
            this.label6.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(1)))), ((int)(((byte)(96)))), ((int)(((byte)(183)))));
            this.label6.Location = new System.Drawing.Point(-5, 0);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(151, 25);
            this.label6.TabIndex = 7;
            this.label6.Text = "Update settings";
            // 
            // viewModesP
            // 
            this.viewModesP.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(16)))), ((int)(((byte)(16)))), ((int)(((byte)(16)))));
            this.viewModesP.Controls.Add(this.showLWEChB);
            this.viewModesP.Controls.Add(this.label20);
            this.viewModesP.Controls.Add(this.showRatingChB);
            this.viewModesP.Controls.Add(this.label19);
            this.viewModesP.Controls.Add(this.showTitleChB);
            this.viewModesP.Controls.Add(this.label18);
            this.viewModesP.Controls.Add(this.scrollDirectionCB);
            this.viewModesP.Controls.Add(this.label17);
            this.viewModesP.Controls.Add(this.columnsOfSeriesBoxesNUD);
            this.viewModesP.Controls.Add(this.rowsOfSeriesBoxesNUD);
            this.viewModesP.Controls.Add(this.label16);
            this.viewModesP.Controls.Add(this.label15);
            this.viewModesP.Controls.Add(this.label14);
            this.viewModesP.Controls.Add(this.showViewModesCB);
            this.viewModesP.Controls.Add(this.label7);
            this.viewModesP.Controls.Add(this.label8);
            this.viewModesP.Controls.Add(this.label9);
            this.viewModesP.Location = new System.Drawing.Point(686, 272);
            this.viewModesP.Name = "viewModesP";
            this.viewModesP.Size = new System.Drawing.Size(660, 246);
            this.viewModesP.TabIndex = 3;
            // 
            // showLWEChB
            // 
            this.showLWEChB.AutoSize = true;
            this.showLWEChB.Cursor = System.Windows.Forms.Cursors.Hand;
            this.showLWEChB.Location = new System.Drawing.Point(337, 208);
            this.showLWEChB.Name = "showLWEChB";
            this.showLWEChB.Size = new System.Drawing.Size(102, 25);
            this.showLWEChB.TabIndex = 29;
            this.showLWEChB.Text = "checkBox2";
            this.showLWEChB.UseVisualStyleBackColor = true;
            this.showLWEChB.CheckedChanged += new System.EventHandler(this.CheckBox_CheckedChanged);
            this.showLWEChB.CheckStateChanged += new System.EventHandler(this.CheckBox_CheckedChanged);
            // 
            // label20
            // 
            this.label20.AutoSize = true;
            this.label20.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label20.ForeColor = System.Drawing.Color.White;
            this.label20.Location = new System.Drawing.Point(333, 185);
            this.label20.Name = "label20";
            this.label20.Size = new System.Drawing.Size(259, 19);
            this.label20.TabIndex = 28;
            this.label20.Text = "Show last episode watched info for series";
            // 
            // showRatingChB
            // 
            this.showRatingChB.AutoSize = true;
            this.showRatingChB.Cursor = System.Windows.Forms.Cursors.Hand;
            this.showRatingChB.Location = new System.Drawing.Point(337, 149);
            this.showRatingChB.Name = "showRatingChB";
            this.showRatingChB.Size = new System.Drawing.Size(102, 25);
            this.showRatingChB.TabIndex = 27;
            this.showRatingChB.Text = "checkBox2";
            this.showRatingChB.UseVisualStyleBackColor = true;
            this.showRatingChB.CheckedChanged += new System.EventHandler(this.CheckBox_CheckedChanged);
            this.showRatingChB.CheckStateChanged += new System.EventHandler(this.CheckBox_CheckedChanged);
            // 
            // label19
            // 
            this.label19.AutoSize = true;
            this.label19.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label19.ForeColor = System.Drawing.Color.White;
            this.label19.Location = new System.Drawing.Point(333, 126);
            this.label19.Name = "label19";
            this.label19.Size = new System.Drawing.Size(141, 19);
            this.label19.TabIndex = 26;
            this.label19.Text = "Show rating for series";
            // 
            // showTitleChB
            // 
            this.showTitleChB.AutoSize = true;
            this.showTitleChB.Cursor = System.Windows.Forms.Cursors.Hand;
            this.showTitleChB.Location = new System.Drawing.Point(337, 88);
            this.showTitleChB.Name = "showTitleChB";
            this.showTitleChB.Size = new System.Drawing.Size(102, 25);
            this.showTitleChB.TabIndex = 25;
            this.showTitleChB.Text = "checkBox2";
            this.showTitleChB.UseVisualStyleBackColor = true;
            this.showTitleChB.CheckedChanged += new System.EventHandler(this.CheckBox_CheckedChanged);
            this.showTitleChB.CheckStateChanged += new System.EventHandler(this.CheckBox_CheckedChanged);
            // 
            // label18
            // 
            this.label18.AutoSize = true;
            this.label18.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label18.ForeColor = System.Drawing.Color.White;
            this.label18.Location = new System.Drawing.Point(333, 65);
            this.label18.Name = "label18";
            this.label18.Size = new System.Drawing.Size(128, 19);
            this.label18.TabIndex = 24;
            this.label18.Text = "Show title for series";
            // 
            // scrollDirectionCB
            // 
            this.scrollDirectionCB.Cursor = System.Windows.Forms.Cursors.PanSouth;
            this.scrollDirectionCB.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.scrollDirectionCB.Font = new System.Drawing.Font("Segoe UI", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.scrollDirectionCB.FormattingEnabled = true;
            this.scrollDirectionCB.Location = new System.Drawing.Point(0, 148);
            this.scrollDirectionCB.Name = "scrollDirectionCB";
            this.scrollDirectionCB.Size = new System.Drawing.Size(320, 28);
            this.scrollDirectionCB.TabIndex = 23;
            this.scrollDirectionCB.SelectedIndexChanged += new System.EventHandler(this.ViewMode_Changed);
            // 
            // label17
            // 
            this.label17.AutoSize = true;
            this.label17.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label17.ForeColor = System.Drawing.Color.White;
            this.label17.Location = new System.Drawing.Point(-4, 126);
            this.label17.Name = "label17";
            this.label17.Size = new System.Drawing.Size(118, 19);
            this.label17.TabIndex = 22;
            this.label17.Text = "Scroll bar position";
            // 
            // columnsOfSeriesBoxesNUD
            // 
            this.columnsOfSeriesBoxesNUD.Font = new System.Drawing.Font("Segoe UI", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.columnsOfSeriesBoxesNUD.Location = new System.Drawing.Point(240, 208);
            this.columnsOfSeriesBoxesNUD.Maximum = new decimal(new int[] {
            12,
            0,
            0,
            0});
            this.columnsOfSeriesBoxesNUD.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.columnsOfSeriesBoxesNUD.Name = "columnsOfSeriesBoxesNUD";
            this.columnsOfSeriesBoxesNUD.Size = new System.Drawing.Size(80, 27);
            this.columnsOfSeriesBoxesNUD.TabIndex = 21;
            this.columnsOfSeriesBoxesNUD.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.columnsOfSeriesBoxesNUD.UpDownAlign = System.Windows.Forms.LeftRightAlignment.Left;
            this.columnsOfSeriesBoxesNUD.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // rowsOfSeriesBoxesNUD
            // 
            this.rowsOfSeriesBoxesNUD.Font = new System.Drawing.Font("Segoe UI", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rowsOfSeriesBoxesNUD.Location = new System.Drawing.Point(66, 208);
            this.rowsOfSeriesBoxesNUD.Maximum = new decimal(new int[] {
            12,
            0,
            0,
            0});
            this.rowsOfSeriesBoxesNUD.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.rowsOfSeriesBoxesNUD.Name = "rowsOfSeriesBoxesNUD";
            this.rowsOfSeriesBoxesNUD.Size = new System.Drawing.Size(80, 27);
            this.rowsOfSeriesBoxesNUD.TabIndex = 20;
            this.rowsOfSeriesBoxesNUD.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.rowsOfSeriesBoxesNUD.UpDownAlign = System.Windows.Forms.LeftRightAlignment.Left;
            this.rowsOfSeriesBoxesNUD.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // label16
            // 
            this.label16.AutoSize = true;
            this.label16.Location = new System.Drawing.Point(159, 209);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(75, 21);
            this.label16.TabIndex = 18;
            this.label16.Text = "Columns:";
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.Location = new System.Drawing.Point(3, 209);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(51, 21);
            this.label15.TabIndex = 16;
            this.label15.Text = "Rows:";
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label14.ForeColor = System.Drawing.Color.White;
            this.label14.Location = new System.Drawing.Point(-4, 185);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(156, 19);
            this.label14.TabIndex = 14;
            this.label14.Text = "Number of series boxes ";
            // 
            // showViewModesCB
            // 
            this.showViewModesCB.Cursor = System.Windows.Forms.Cursors.PanSouth;
            this.showViewModesCB.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.showViewModesCB.Font = new System.Drawing.Font("Segoe UI", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.showViewModesCB.FormattingEnabled = true;
            this.showViewModesCB.Location = new System.Drawing.Point(0, 87);
            this.showViewModesCB.Name = "showViewModesCB";
            this.showViewModesCB.Size = new System.Drawing.Size(320, 28);
            this.showViewModesCB.TabIndex = 10;
            this.showViewModesCB.SelectedIndexChanged += new System.EventHandler(this.ViewMode_Changed);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.ForeColor = System.Drawing.Color.White;
            this.label7.Location = new System.Drawing.Point(-4, 65);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(114, 19);
            this.label7.TabIndex = 9;
            this.label7.Text = "Series view mode";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("Segoe UI Semibold", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label8.ForeColor = System.Drawing.Color.Gray;
            this.label8.Location = new System.Drawing.Point(-4, 34);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(62, 21);
            this.label8.TabIndex = 8;
            this.label8.Text = "Display";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Font = new System.Drawing.Font("Segoe UI", 14.25F, System.Drawing.FontStyle.Bold);
            this.label9.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(1)))), ((int)(((byte)(96)))), ((int)(((byte)(183)))));
            this.label9.Location = new System.Drawing.Point(-5, 0);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(314, 25);
            this.label9.TabIndex = 7;
            this.label9.Text = "Series view modes (on main form)";
            // 
            // menuP
            // 
            this.menuP.Location = new System.Drawing.Point(246, 336);
            this.menuP.Name = "menuP";
            this.menuP.Size = new System.Drawing.Size(200, 50);
            this.menuP.TabIndex = 5;
            // 
            // FSettings
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(16)))), ((int)(((byte)(16)))), ((int)(((byte)(16)))));
            this.ClientSize = new System.Drawing.Size(1362, 523);
            this.ControlBox = false;
            this.Controls.Add(this.menuP);
            this.Controls.Add(this.viewModesP);
            this.Controls.Add(this.updateSettP);
            this.Controls.Add(this.generalSettP);
            this.Controls.Add(this.tabViewP);
            this.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ForeColor = System.Drawing.Color.White;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "FSettings";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Settings";
            this.Load += new System.EventHandler(this.FSettings_Load);
            this.VisibleChanged += new System.EventHandler(this.FSettings_VisibleChanged);
            this.generalSettP.ResumeLayout(false);
            this.generalSettP.PerformLayout();
            this.updateSettP.ResumeLayout(false);
            this.updateSettP.PerformLayout();
            this.viewModesP.ResumeLayout(false);
            this.viewModesP.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.columnsOfSeriesBoxesNUD)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.rowsOfSeriesBoxesNUD)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel tabViewP;
        private System.Windows.Forms.Panel generalSettP;
        private System.Windows.Forms.Panel updateSettP;
        private System.Windows.Forms.Panel viewModesP;
        private System.Windows.Forms.Panel menuP;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.CheckBox downloadBannersForSeriesChB;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.TextBox apiKeyTB;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.TextBox mirrorUrlTB;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.ComboBox showViewModesCB;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.CheckBox downloadBannersForActorsChB;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.Label label16;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.NumericUpDown columnsOfSeriesBoxesNUD;
        private System.Windows.Forms.NumericUpDown rowsOfSeriesBoxesNUD;
        private System.Windows.Forms.ComboBox scrollDirectionCB;
        private System.Windows.Forms.Label label17;
        private System.Windows.Forms.CheckBox showLWEChB;
        private System.Windows.Forms.Label label20;
        private System.Windows.Forms.CheckBox showRatingChB;
        private System.Windows.Forms.Label label19;
        private System.Windows.Forms.CheckBox showTitleChB;
        private System.Windows.Forms.Label label18;
    }
}