using System;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using TvDatabase.Classes;
using TvDatabase.VisualComponents;

namespace TvDatabase.Forms
{
    public partial class FHelp : Form
    {
        private FMain mainForm;

        public FHelp(FMain mainForm)
        {
            this.mainForm = mainForm;
            InitializeComponent();
        }

        private void FHelp_Resize(object sender, EventArgs e)
        {
            const int padd = 20;
            browseP.SetBounds(padd, padd, 200, this.Height - 50 - 3 * padd);
            webBrowserP.SetBounds(browseP.Right + padd, padd, this.Width - browseP.Right - 2 * padd, browseP.Height);
            menuP.SetBounds(this.Width / 2 - 100, this.Height - 50 - padd, 200, 50);
        }

        private void FHelp_Load(object sender, EventArgs e)
        {
            new TvDatabase.VisualComponents.FancyButtonCollection(menuP, new string[] { "Close" }, MenuButton_Click, true, 0);
        }

        private void FHelp_Shown(object sender, EventArgs e)
        {
            gettingStartedL_Click(null, null);
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            e.Graphics.Clear(Colors.Backgrounds[false]);
            e.Graphics.DrawRectangle(Pens.Gray, 1, 1, this.Width - 2, this.Height - 2);
            base.OnPaint(e);
        }

        private void MenuButton_Click(object sender, EventArgs e)
        {
            this.mainForm.ShowOnlyFormAndHideAllOthers(null);
        }

        private void gettingStartedL_Click(object sender, EventArgs e)
        {
            webBrowser.Navigate(new FileInfo(Paths.HelpFolder + @"index.html").FullName);
        }

        private void languagesL_Click(object sender, EventArgs e)
        {
            webBrowser.Navigate(new FileInfo(Paths.HelpFolder + @"languages.html").FullName);
        }

        private void networksL_Click(object sender, EventArgs e)
        {
            webBrowser.Navigate(new FileInfo(Paths.HelpFolder + @"networks.html").FullName);
        }

        private void genresL_Click(object sender, EventArgs e)
        {
            webBrowser.Navigate(new FileInfo(Paths.HelpFolder + @"genres.html").FullName);
        }

        private void peopleL_Click(object sender, EventArgs e)
        {
            webBrowser.Navigate(new FileInfo(Paths.HelpFolder + @"people.html").FullName);
        }

        private void seriesL_Click(object sender, EventArgs e)
        {
            webBrowser.Navigate(new FileInfo(Paths.HelpFolder + @"series.html").FullName);
        }

        private void interfaceL_Click(object sender, EventArgs e)
        {
            webBrowser.Navigate(new FileInfo(Paths.HelpFolder + @"interface.html").FullName);
        }

        private void seriesEditorL_Click(object sender, EventArgs e)
        {
            webBrowser.Navigate(new FileInfo(Paths.HelpFolder + @"seriesEditor.html").FullName);
        }

        private void settingsEditorL_Click(object sender, EventArgs e)
        {
            webBrowser.Navigate(new FileInfo(Paths.HelpFolder + @"settingsEditor.html").FullName);
        }

        private void updaterL_Click(object sender, EventArgs e)
        {
            webBrowser.Navigate(new FileInfo(Paths.HelpFolder + @"updater.html").FullName);
        }

        private void databaseL_Click(object sender, EventArgs e)
        {
            webBrowser.Navigate(new FileInfo(Paths.HelpFolder + @"database.html").FullName);
        }

        //
        private void label3_Click(object sender, EventArgs e)
        {
            webBrowser.Navigate(new FileInfo(Paths.HelpFolder + @"dataTypes.html").FullName);
        }
    }
}
