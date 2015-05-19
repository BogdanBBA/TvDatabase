using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using TvDatabase.Classes;
using TvDatabase.VisualComponents;

namespace TvDatabase.Forms
{
    public partial class FSettings : Form
    {
        private FMain mainForm;

        private FancyTabView tabView;

        public FSettings(FMain mainForm)
        {
            InitializeComponent();
            this.mainForm = mainForm;
            this.Size = new Size(tabViewP.Width + 2 * tabViewP.Left, tabViewP.Height + menuP.Height + 3 * tabViewP.Top);
        }

        private void FSettings_Load(object sender, EventArgs e)
        {
            string[] captions = new string[] { "General", "Update", "View modes" };
            Control[] controls = new Control[] { generalSettP, updateSettP, viewModesP };
            this.tabView = new FancyTabView(tabViewP, captions, controls);
            new FancyButtonCollection(menuP, new string[] { "Save and close" }, MenuButton_Click, true, 0);

            //

            showViewModesCB.Items.AddRange(Enum.GetNames(new Settings.EShowViewMode().GetType()));
            scrollDirectionCB.Items.AddRange(Enum.GetNames(new FancyScrollBar.FancyScrollBarPosition().GetType()));
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            e.Graphics.Clear(Colors.Backgrounds[false]);
            e.Graphics.DrawRectangle(Pens.Gray, 1, 1, this.Width - 2, this.Height - 2);
            base.OnPaint(e);
        }

        private void CheckBox_CheckedChanged(object sender, EventArgs e)
        {
            (sender as CheckBox).Text = (sender as CheckBox).CheckState == CheckState.Checked ? "Yes" : "No";
        }

        private void ViewMode_Changed(object sender, EventArgs e)
        {
            if (showViewModesCB.SelectedIndex == -1 || scrollDirectionCB.SelectedIndex == -1)
                return;
            Settings.EShowViewMode view = (Settings.EShowViewMode) Enum.Parse(typeof(Settings.EShowViewMode), showViewModesCB.Items[showViewModesCB.SelectedIndex] as string);
            FancyScrollBar.FancyScrollBarPosition scrollPos = (FancyScrollBar.FancyScrollBarPosition) Enum.Parse(typeof(FancyScrollBar.FancyScrollBarPosition), scrollDirectionCB.Items[scrollDirectionCB.SelectedIndex] as string);
            switch (view)
            {
                case Settings.EShowViewMode.Weekdays:
                    rowsOfSeriesBoxesNUD.Enabled = scrollPos == FancyScrollBar.FancyScrollBarPosition.Right;
                    if (!rowsOfSeriesBoxesNUD.Enabled)
                        rowsOfSeriesBoxesNUD.Value = 7;
                    columnsOfSeriesBoxesNUD.Enabled = scrollPos == FancyScrollBar.FancyScrollBarPosition.Bottom;
                    if (!columnsOfSeriesBoxesNUD.Enabled)
                        columnsOfSeriesBoxesNUD.Value = 7;
                    break;
                case Settings.EShowViewMode.List:
                    rowsOfSeriesBoxesNUD.Enabled = true;
                    columnsOfSeriesBoxesNUD.Enabled = true;
                    break;
            }
        }

        private void FSettings_VisibleChanged(object sender, EventArgs e)
        {
            if (!this.Visible)
                return;
            Settings sett = this.mainForm.Database.Settings;
            mirrorUrlTB.Text = sett.MirrorURL;
            apiKeyTB.Text = sett.ApiKey;
            downloadBannersForSeriesChB.CheckState = sett.DownloadImagesForSeries ? CheckState.Checked : CheckState.Unchecked;
            downloadBannersForActorsChB.CheckState = sett.DownloadImagesForActors ? CheckState.Checked : CheckState.Unchecked;
            showViewModesCB.SelectedIndex = showViewModesCB.Items.IndexOf(sett.ShowViewMode.ToString());
            scrollDirectionCB.SelectedIndex = scrollDirectionCB.Items.IndexOf(sett.ScrollBarPosition.ToString());
            rowsOfSeriesBoxesNUD.Value = sett.NumberOfSeriesBoxes.Height;
            columnsOfSeriesBoxesNUD.Value = sett.NumberOfSeriesBoxes.Width;
            showTitleChB.CheckState = sett.ShowSeriesTitle ? CheckState.Checked : CheckState.Unchecked;
            showRatingChB.CheckState = sett.ShowSeriesRating ? CheckState.Checked : CheckState.Unchecked;
            showLWEChB.CheckState = sett.ShowSeriesLWE ? CheckState.Checked : CheckState.Unchecked;
        }

        private void MenuButton_Click(object sender, EventArgs e)
        {
            // save
            Settings sett = this.mainForm.Database.Settings;

            sett.MirrorURL = mirrorUrlTB.Text;
            sett.ApiKey = apiKeyTB.Text;
            sett.DownloadImagesForSeries = downloadBannersForSeriesChB.CheckState == CheckState.Checked;
            sett.DownloadImagesForActors = downloadBannersForActorsChB.CheckState == CheckState.Checked;
            sett.ShowViewMode = (Settings.EShowViewMode) Enum.Parse(typeof(Settings.EShowViewMode), showViewModesCB.Items[showViewModesCB.SelectedIndex] as string);
            sett.ScrollBarPosition = (FancyScrollBar.FancyScrollBarPosition) Enum.Parse(typeof(FancyScrollBar.FancyScrollBarPosition), scrollDirectionCB.Items[scrollDirectionCB.SelectedIndex] as string);
            sett.NumberOfSeriesBoxes = new Size((int) columnsOfSeriesBoxesNUD.Value, (int) rowsOfSeriesBoxesNUD.Value);
            sett.ShowSeriesTitle = showTitleChB.CheckState == CheckState.Checked;
            sett.ShowSeriesRating = showRatingChB.CheckState == CheckState.Checked;
            sett.ShowSeriesLWE = showLWEChB.CheckState == CheckState.Checked;

            this.mainForm.Database.Settings.SaveToFile();

            // close
            this.mainForm.ShowOnlyFormAndHideAllOthers(null);
            this.mainForm.RefreshSeriesListWithNecessaryCreations(this.mainForm.LastSeriesList);
        }
    }
}
