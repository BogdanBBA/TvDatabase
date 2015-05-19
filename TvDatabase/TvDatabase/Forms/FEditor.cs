using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;
using TvDatabase.Classes;
using TvDatabase.VisualComponents;

namespace TvDatabase.Forms
{
    public partial class FEditor : Form
    {
        #region declarations and initialization methods
        private FMain mainForm;

        private FancyTabView tabView;
        private FancyCheckBox followedCheckBox;

        private MyThread searchBgW;
        private List<SeriesSummary> seriesSummaries;
        private SeriesList newlyFollowedSeries;

        public FEditor(FMain mainForm)
        {
            this.mainForm = mainForm;
            InitializeComponent();
            this.Size = new Size(tabViewP.Width + 2 * tabViewP.Left, tabViewP.Height + menuP.Height + 3 * tabViewP.Top);
            this.searchBgW = new MyThread(true, false, SearchBgW_Work, SearchBgW_Update, SearchBgW_Done);
            this.seriesSummaries = new List<SeriesSummary>();
            this.newlyFollowedSeries = new SeriesList();
        }

        private void FEditor_Load(object sender, EventArgs e)
        {
            string[] captions = new string[] { "Search online", "Edit existing" };
            Control[] contentControls = new Control[] { searchP, editP };
            this.tabView = new FancyTabView(tabViewP, captions, contentControls);
            new FancyButtonCollection(menuP, new string[] { "Close" }, MenuButton_Click, true, 0);
            new FancyButtonCollection(searchButtonP, new string[] { "Search!" }, SearchButton_Click, true, 0);
            this.followedCheckBox = new FancyCheckBox(followedP, "Followed", FollowedCheckBox_Click, new Rectangle(new Point(0, 0), followedP.Size));
        }

        private void FEditor_VisibleChanged(object sender, EventArgs e)
        {
            if (this.Visible)
            {
                searchTB.Text = "";
                searchTB.Text = "";
                searchStatusL.Text = "";
                summaryLB.Items.Clear();
                summaryLB_SelectedIndexChanged(null, null);
                this.newlyFollowedSeries.Clear();
            }
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            e.Graphics.Clear(Colors.Backgrounds[false]);
            e.Graphics.DrawRectangle(Pens.Gray, 1, 1, this.Width - 2, this.Height - 2);
            base.OnPaint(e);
        }
        #endregion

        #region form-wide importance methods
        private void MenuButton_Click(object sender, EventArgs e)
        {
            bool goUpdate = false;
            if (this.newlyFollowedSeries.Count > 0)
                goUpdate = MessageBox.Show("It seems you have followed some new series.\nWould you like to update them now?\n\nNote: the newly followed series won't appear in your list until they get updated.", "Update confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes;
            if (goUpdate)
            {
                this.mainForm.ShowOnlyFormAndHideAllOthers(this.mainForm.UpdaterForm);
                this.mainForm.UpdaterForm.StartSeriesUpdate(new UpdateArgument(this.mainForm.Database, this.newlyFollowedSeries));
            }
            else
            {
                this.mainForm.ShowOnlyFormAndHideAllOthers(null);
                this.mainForm.RefreshSeriesListWithNecessaryCreations(this.mainForm.Database.Series);
            }
        }
        #endregion

        #region search thread, epViews summary list and related actions
        private void searchTB_KeyDown(object sender, KeyEventArgs e)
        {
            if (this.Enabled && e.KeyCode == Keys.Enter)
                SearchButton_Click(null, null);
        }

        private void SearchButton_Click(object sender, EventArgs e)
        {
            if (!this.searchBgW.IsBusy)
            {
                if (searchTB.Text.Trim().Equals(""))
                {
                    MessageBox.Show("Please type a part of the name of the show you're looking for.", "Invalid search terms", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }
                this.Enabled = false;
                this.searchBgW.RunWorkerAsync();
            }
        }

        private void SearchBgW_Work(object sender, DoWorkEventArgs args)
        {
            searchBgW.ReportProgress(33, "Connecting to thetvdb.com");
            string url = @"http://thetvdb.com/api/GetSeries.php?seriesname=" + searchTB.Text, dest = Paths.TemporaryStorageFolder + "tempDld.xml";
            string dldResult = Utils.DownloadFromTheHolyInternet(url, dest);
            if (!dldResult.Equals(""))
                throw new ApplicationException("Downloading xml from \"" + url + "\" to \"" + dest + "\" failed!\n\n" + dldResult);

            searchBgW.ReportProgress(66, "Decoding XML");
            XmlDocument doc = new XmlDocument();
            doc.Load(dest);
            this.seriesSummaries.Clear();
            XmlNodeList nodes = doc.SelectSingleNode("Data").SelectNodes("Series");
            foreach (XmlNode node in nodes)
            {
                string id = node.SelectSingleNode("seriesid").InnerText;
                string name = node.SelectSingleNode("SeriesName").InnerText;
                string overview = node.SelectSingleNode("Overview") != null ? node.SelectSingleNode("Overview").InnerText : null;
                DateTime? firstAired = node.SelectSingleNode("FirstAired") != null
                    ? Utils.DecodeNullableDateTime(node.SelectSingleNode("FirstAired").InnerText, Utils.StandardDateFormat) : null;
                this.seriesSummaries.Add(new SeriesSummary(id, name, overview, firstAired));
            }

            searchBgW.ReportProgress(99, "Finishing up search");
        }

        private void SearchBgW_Update(object sender, ProgressChangedEventArgs args)
        {
            searchStatusL.Text = string.Format("{0} ({1}%)...", args.UserState as string, args.ProgressPercentage);
        }

        private void SearchBgW_Done(object sender, RunWorkerCompletedEventArgs args)
        {
            this.Enabled = true;
            searchStatusL.Text = "Search finished!";
            hideSearchStatusT.Enabled = true;

            if (args.Error != null)
                MessageBox.Show("An error has occured during the search:\n\n" + args.Error.ToString(), "Update ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);

            summaryLB.Items.Clear();
            foreach (SeriesSummary summ in this.seriesSummaries)
                summaryLB.Items.Add(summ.Name);
            summaryLB.SelectedIndex = summaryLB.Items.Count == 0 ? -1 : 0;
            summaryLB_SelectedIndexChanged(null, null);
        }
        #endregion

        #region search results
        private void summaryLB_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (summaryLB.SelectedIndex == -1)
            {
                nameL.Text = "No selection";
                firstAiredL.Text = "";
                overviewL.Text = "";
                this.followedCheckBox.Checked = false;
            }
            else
            {
                SeriesSummary summary = this.seriesSummaries[summaryLB.SelectedIndex];
                nameL.Text = summary.Name;
                firstAiredL.Text = summary.FirstAired == null ? "unknown broadcast date"
                    : "first broadcast " + Utils.EncodeDateTime((DateTime) summary.FirstAired, "MMMM dd, yyyy");
                overviewL.Text = summary.Overview == null ? "no description" : summary.Overview;
                this.followedCheckBox.Checked = this.mainForm.Database.Series.GetByID(summary.ID) != null;
            }
        }

        private void hideSearchStatusT_Tick(object sender, EventArgs e)
        {
            hideSearchStatusT.Enabled = false;
            searchStatusL.Text = "";
        }

        private void FollowedCheckBox_Click(object sender, EventArgs e)
        {
            if (summaryLB.SelectedIndex == -1)
            {
                MessageBox.Show("Search for a series and select it from the list in order to follow it.", "No series selected", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            SeriesSummary summary = this.seriesSummaries[summaryLB.SelectedIndex];
            if (!this.followedCheckBox.Checked) // add new
            {
                Series newSeries = new Series(summary);
                this.mainForm.Database.Series.Add(newSeries);
                this.newlyFollowedSeries.Add(newSeries);
            }
            else // remove existing
            {
                if (MessageBox.Show("Are you sure you want to unfollow " + summary.Name + "?\n\nNote: you may follow it again later.", "Unfollow confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
                    return;
                Series seriesToDelete = this.mainForm.Database.Series[this.mainForm.Database.Series.GetIndexOfID(summary.ID)];
                this.newlyFollowedSeries.Remove(seriesToDelete);
                this.mainForm.Database.Series.RemoveSeriesAndDeleteLooseConnexions(seriesToDelete, this.mainForm.Database);
            }
            string saveResult = this.mainForm.Database.SaveDatabase();
            if (!saveResult.Equals(""))
                MessageBox.Show("An ERROR occured while saving the database:\n\n" + saveResult, "Database save ERROR", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            summaryLB_SelectedIndexChanged(null, null);
        }
        #endregion
    }
}
