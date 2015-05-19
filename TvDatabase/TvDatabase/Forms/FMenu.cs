using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using TvDatabase.Classes;
using TvDatabase.VisualComponents;

namespace TvDatabase.Forms
{
    public partial class FMenu : Form
    {
        private Form parentForm;

        /// <summary>An argument to be used by various sources in various situations. Keep in mind that it belongs to this Form instance.</summary>
        public object[] Argument { get; set; }

        private List<string> menuButtonCaptions;
        private List<FancyButton> menuButtons;
        public BorderPictureBox BorderPB { get; private set; }

        public FMenu(Form parentForm)
        {
            InitializeComponent();
            this.parentForm = parentForm;
            //
            menuButtonCaptions = new List<string>();
            menuButtons = new List<FancyButton>();
            BorderPB = new BorderPictureBox(this);
        }

        private void FMenu_Load(object sender, EventArgs e)
        {
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            e.Graphics.Clear(Colors.Backgrounds[false]);
            e.Graphics.DrawRectangle(Pens.Gray, 1, 1, this.Width - 2, this.Height - 2);
            base.OnPaint(e);
        }

        public void RefreshMenuForm(ICollection<string> captions, EventHandler menuButton_Click_Event, Point location, int? width)
        {
            menuButtonCaptions.Clear();
            menuButtonCaptions.AddRange(captions);
            menuButtonCaptions.Add("[CLOSE]");
            //
            this.Location = location;
            int realWidth = width != null ? (int) width : Sizes.MenuButton.Width;
            this.Size = new Size(realWidth + 2 * BorderPB.BorderWidth, menuButtonCaptions.Count * Sizes.MenuButton.Height + 2 * BorderPB.BorderWidth);
            BorderPB.SetBounds(0, 0, this.Width, this.Height);
            BorderPB.RedrawBorder();
            //
            for (int i = menuButtonCaptions.Count; i < menuButtons.Count; i++)
                menuButtons[i].Hide();
            //
            for (int i = 0; i < menuButtonCaptions.Count; i++)
            {
                if (i >= menuButtons.Count)
                {
                    FancyButton butt = new FancyButton(this, "null", null, FancyButton.DefaultAccentColors, FancyButton.DefaultAnyFocusBackColors,
                        new BinaryVariants<FontWithColor>(new FontWithColor("Segoe UI", 18, Color.White), new FontWithColor("Segoe UI", 18, Colors.Accents[true])),
                        FancyButton.DefaultMousePressedFonts, false);
                    menuButtons.Add(butt);
                }
                menuButtons[i].SetBounds(BorderPB.BorderWidth, BorderPB.BorderWidth + i * Sizes.MenuButton.Height, realWidth, Sizes.MenuButton.Height);
                menuButtons[i].ClickEH = menuButton_Click_Event;
                menuButtons[i].Text = menuButtonCaptions[i];
                menuButtons[i].Invalidate();
                menuButtons[i].Show();
            }
            BorderPB.SendToBack();
            //
            this.Show();
        }

        private void FMenu_Deactivate(object sender, EventArgs e) // lose focus
        {
            if (this.parentForm is FMain)
                MainFormMenu_More_Click(this.menuButtons.Last(), null);
            else if (this.parentForm is FSeries)
                this.Hide();
        }

        //
        //
        //

        public void MainFormMenu_More_Click(object sender, EventArgs e)
        {
            FMain mainForm = this.parentForm as FMain;
            mainForm.ShowOnlyFormAndHideAllOthers(null);
            int index = menuButtonCaptions.IndexOf((sender as FancyButton).Text);
            if (index != menuButtonCaptions.Count - 1)
                switch (index)
                {
                    case 0: // about
                        mainForm.ShowOnlyFormAndHideAllOthers(mainForm.AboutForm);
                        break;
                    case 1: // help
                        mainForm.ShowOnlyFormAndHideAllOthers(mainForm.HelpForm);
                        break;
                    case 2: // add/edit shows
                        mainForm.ShowOnlyFormAndHideAllOthers(mainForm.EditorForm);
                        break;
                    case 3: // settings
                        mainForm.ShowOnlyFormAndHideAllOthers(mainForm.SettingsForm);
                        break;
                    case 4: // special functions
                        Size size = new Size(320, MenuButtonCaptions.MainForm_MainMenu_MoreActions_SpecialFunctions.Length * Sizes.MenuButton.Height);
                        this.RefreshMenuForm(
                            MenuButtonCaptions.MainForm_MainMenu_MoreActions_SpecialFunctions,
                            this.MainFormMenu_More_SpecialFunctions_Click,
                            new Point(this.parentForm.Width / 2 - size.Width / 2, this.parentForm.Height / 2 - size.Height / 2),
                            size.Width);
                        break;
                    default:
                        MessageBox.Show("Invalid menu button caption :\\", "Weird", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        break;
                }
        }

        public void MainFormMenu_More_SpecialFunctions_Click(object sender, EventArgs e)
        {
            FMain mainForm = this.parentForm as FMain;
            mainForm.ShowOnlyFormAndHideAllOthers(null);
            int index = menuButtonCaptions.IndexOf((sender as FancyButton).Text);
            if (index != menuButtonCaptions.Count - 1)
                switch (index)
                {
                    case 0: // open workspace
                        System.Diagnostics.Process.Start(Paths.ProgramFilesFolder);
                        break;
                    case 1: // export db
                        SFD.InitialDirectory = Path.GetFullPath(Path.Combine(Application.ExecutablePath, Paths.ExportsFolder).Replace("\\", "/"));
                        SFD.RestoreDirectory = true;
                        if (SFD.ShowDialog() == DialogResult.OK)
                        {
                            string saveResult = mainForm.Database.SaveDatabase(SFD.FileName);
                            if (!saveResult.Equals(""))
                                MessageBox.Show(saveResult, "Database export ERROR", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            else
                                MessageBox.Show("Database exported successfully to \"" + SFD.FileName + "\".", "Export OK", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                        break;
                    case 2: // import db
                        OFD.InitialDirectory = Path.GetFullPath(Path.Combine(Application.ExecutablePath, Paths.ExportsFolder).Replace("\\", "/"));
                        OFD.RestoreDirectory = true;
                        if (OFD.ShowDialog() == DialogResult.OK)
                        {
                            Database newDB = new Database();
                            string openResult = newDB.OpenDatabase(OFD.FileName);
                            if (!openResult.Equals(""))
                            {
                                MessageBox.Show(openResult, "Database import ERROR", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                break;
                            }
                            bool merge = MessageBox.Show("Would you like to merge the database items?\n\nIf not, the current database will simply be replaced by the new one.", "Database merge confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes;
                            if (merge)
                            {
                                MessageBox.Show("Sorry, this is currently unavailable.", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                break;
                            }
                            else
                                mainForm.Database = newDB;

                            string saveResult = mainForm.Database.SaveDatabase();
                            if (!saveResult.Equals(""))
                                MessageBox.Show(saveResult, "Database save ERROR", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            else
                                MessageBox.Show("Database imported successfully from \"" + OFD.FileName + "\".", "Import OK", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            mainForm.RefreshSeriesListWithNecessaryCreations(mainForm.Database.Series);
                        }
                        break;
                    case 3: // import from wtvsh3 
                        MessageBox.Show("Sorry, this is currently unavailable.", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        break;
                    case 4: // clear database
                        if (MessageBox.Show("Are you sure you want to clear all items in the database?", "Clear database confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
                        {
                            while (mainForm.Database.Series.Count > 0)
                                mainForm.Database.Series.RemoveSeriesAndDeleteLooseConnexions(mainForm.Database.Series[0], mainForm.Database);

                            string saveResult = mainForm.Database.SaveDatabase();
                            if (!saveResult.Equals(""))
                                MessageBox.Show(saveResult, "Database save ERROR", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            else
                                MessageBox.Show("Database cleared successfully.", "Clear OK", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            mainForm.RefreshSeriesListWithNecessaryCreations(mainForm.Database.Series);
                        }
                        break;
                    default:
                        MessageBox.Show("Invalid menu button caption :\\", "Weird", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        break;
                }
        }

        public void SeriesForm_EpisodeViewOptions_Click(object sender, EventArgs e)
        {
            FSeries seriesForm = this.parentForm as FSeries;
            SeasonEpisodeView view = this.Argument[0] as SeasonEpisodeView;
            List<SeasonEpisodeView> epViews = this.Argument[1] as List<SeasonEpisodeView>;
            bool saveNeeded = false;

            this.Hide();
            int index = menuButtonCaptions.IndexOf((sender as FancyButton).Text);
            if (index != menuButtonCaptions.Count - 1)
                switch (index)
                {
                    case 0: // info
                        MessageBox.Show(view.Episode.Overview + "\n\nMore detailed info to come soon.");
                        break;
                    case 1: // not watched
                        if (view.Episode.LastWatched != null)
                        {
                            view.Episode.LastWatched = null;
                            view.Invalidate();
                            saveNeeded = true;
                        }
                        break;
                    case 2: // watched
                        bool shouldSet = view.Episode.LastWatched == null;
                        if (!shouldSet)
                            shouldSet =
                                MessageBox.Show("The episode is already marked as watched on\n" +
                                Utils.FormatDateTime((DateTime) view.Episode.LastWatched, "dddd, d MMMM yyyy, HH:mm") + ".\n\nOverride date and time?",
                                "Last watched date override confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question)
                                == DialogResult.Yes;
                        if (shouldSet)
                        {
                            view.Episode.LastWatched = DateTime.Now;
                            view.Invalidate();
                            saveNeeded = true;
                        }
                        break;
                    case 3: // watched up to here
                        foreach (SeasonEpisodeView iView in epViews)
                            if (iView.ViewMode == SeasonEpisodeView.ViewModes.Episode)
                            {
                                if (iView.Episode.LastWatched == null)
                                {
                                    iView.Episode.LastWatched = DateTime.Now;
                                    iView.Invalidate();
                                    saveNeeded = true;
                                }
                                if (iView.Equals(view))
                                    break;
                            }
                        break;
                    default:
                        MessageBox.Show("Invalid menu button caption :\\", "Weird", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        break;
                }
            if (saveNeeded)
            {
                string saveResult = seriesForm.mainForm.Database.SaveDatabase();
                if (!saveResult.Equals(""))
                    MessageBox.Show("An ERROR occured while saving the database:\n\n" + saveResult);
            }
        }
    }
}
