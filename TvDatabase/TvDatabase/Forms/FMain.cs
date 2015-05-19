using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using TvDatabase.Classes;
using TvDatabase.VisualComponents;

namespace TvDatabase.Forms
{
    public partial class FMain : Form
    {
        private FancyButtonCollection mainMenuButtons;
        private SeriesHighlightView highlightView;
        private FancyPanel contentP;
        private List<SeriesView> seriesViews;

        public FAbout AboutForm { get; private set; }
        public FMenu MenuForm { get; private set; }
        public FSeries SeriesForm { get; private set; }
        public FSettings SettingsForm { get; private set; }
        public FEditor EditorForm { get; private set; }
        public FUpdater UpdaterForm { get; private set; }
        public FHelp HelpForm { get; private set; }

        public Database Database { get; internal set; }
        public SeriesList LastSeriesList { get; private set; }

        public FMain(Database db)
        {
            InitializeComponent();
            this.Database = db;
        }

        private void FMain_Resize(object sender, EventArgs e)
        {
            mainMenuP.SetBounds(380, 40, this.Width - 420, 60);
            Size highlightPanelSize = Utils.ScaleRectangle(2 * 758, 140, this.Width - 80, this.Height / 2);
            highlightP.Bounds = new Rectangle(new Point(40, mainMenuP.Bottom + 10), highlightPanelSize);
            contentContainerP.SetBounds(highlightP.Left, highlightP.Bottom + 10, highlightP.Width, this.Height - contentContainerP.Top - 40);
            this.AboutForm = new FAbout(this);
            this.MenuForm = new FMenu(this);
            this.SeriesForm = new FSeries(this);
            this.SettingsForm = new FSettings(this);
            this.EditorForm = new FEditor(this);
            this.UpdaterForm = new FUpdater(this);
            this.HelpForm = new FHelp(this);
        }

        private void FMain_Load(object sender, EventArgs e)
        {
            this.mainMenuButtons = new FancyButtonCollection(mainMenuP, MenuButtonCaptions.MainForm_MainMenu, MainMenuButton_Click, true, 0);
            this.highlightView = new SeriesHighlightView(this.highlightP, new Rectangle(Point.Empty, this.highlightP.Size), HighlightView_Click);
            this.contentP = new FancyPanel(contentContainerP, FancyScrollBar.FancyScrollBarPosition.Right, FancyScrollBar.DefaultScrollBarWidth, true);
            this.MouseWheel += this.contentP.MouseWheelScroll_EvtHandler;
            this.seriesViews = new List<SeriesView>();
        }

        private void FMain_Shown(object sender, EventArgs e)
        {
            SeriesFilter filter = new SeriesFilter(CheckState.Indeterminate, new List<int?> { 1, 2, 3, 4, 5, 6, 7 }, SeriesList.SortingCriteria[1]);
            SeriesList filteredList = this.Database.Series.Filter(filter);
            RefreshSeriesListWithNecessaryCreations(filteredList);
            RefreshHighlightView(filteredList.Count > 0 ? filteredList[0] : null);
        }

        public void ShowOnlyFormAndHideAllOthers(Form form)
        {
            if (form != this.AboutForm)
                this.AboutForm.Hide();
            if (form != this.MenuForm)
                this.MenuForm.Hide();
            if (form != this.SeriesForm)
                this.SeriesForm.Hide();
            if (form != this.SettingsForm)
                this.SettingsForm.Hide();
            if (form != this.EditorForm)
                this.EditorForm.Hide();
            if (form != this.UpdaterForm)
                this.UpdaterForm.Hide();
            if (form != this.HelpForm)
                this.HelpForm.Hide();

            this.Enabled = form == null;
            if (this.Enabled)
                this.Focus();
            else
            {
                form.Show();
                form.Focus();
            }
        }

        private void MainMenuButton_Click(object sender, EventArgs e)
        {
            switch (mainMenuButtons.GetIndexOfFancyButton(sender as FancyButton))
            {
                case 0: // update db
                    this.ShowOnlyFormAndHideAllOthers(this.UpdaterForm);
                    this.UpdaterForm.StartSeriesUpdate(new UpdateArgument(this.Database, this.Database.Series));
                    break;
                case 1: // more
                    Point location = new Point(mainMenuButtons[1].Left, mainMenuButtons[1].Bottom);
                    location.Offset(mainMenuP.Location);
                    MenuForm.RefreshMenuForm(MenuButtonCaptions.MainForm_MainMenu_MoreActions, MenuForm.MainFormMenu_More_Click,
                        location, mainMenuP.Width / MenuButtonCaptions.MainForm_MainMenu.Length);
                    break;
                case 2: // hide
                    trayIcon.Visible = true;
                    trayIcon.ShowBalloonTip(3000);
                    this.Hide();
                    break;
                case 3: // exit
                    exitMI_Click(null, null);
                    break;
            }
        }

        private void exitMI_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void showAppMI_Click(object sender, EventArgs e)
        {
            this.Show();
            trayIcon.Visible = false;
        }

        #region highlight
        public void RefreshHighlightView(Series series)
        {
            if (this.Database.Series.Count > 0 && series != null)
            {
                Image bmp = Utils.GetScaledImage(Paths.UnknownSeriesBannerImageFile, new Size(this.highlightView.Width / 2, this.highlightView.Height), Paths.UnknownSeriesBannerImage);
                this.highlightView.SeriesImage = Utils.GetScaledImage(Paths.ImagesFolder + series.BannerFilename, new Size(this.highlightView.Width / 2, this.highlightView.Height), bmp);
            }
            else
            {
                this.highlightView.SeriesImage = null;
            }
            this.highlightView.Series = series;
        }

        private void HighlightView_Click(object sender, EventArgs e)
        {
            if ((sender as SeriesHighlightView).Series != null)
            {
                this.ShowOnlyFormAndHideAllOthers(this.SeriesForm);
                this.SeriesForm.Series = (sender as SeriesHighlightView).Series;
            }
        }
        #endregion highlight

        #region show list
        public void RefreshSeriesListWithNecessaryCreations(SeriesList seriesList)
        {
            this.RefreshHighlightView(this.Database.Series.Count > 0 ? this.Database.Series[0] : null);

            if (seriesList == null)
                return;
            Settings sett = this.Database.Settings;
            Size imgMaxSize;

            #region format static SeriesView attributes
            switch (sett.ShowViewMode)
            {
                case Settings.EShowViewMode.Weekdays:
                    SeriesView.SeriesViewSize = new Size(contentP.VisiblePanelSize.Width / sett.NumberOfSeriesBoxes.Width,
                        contentP.VisiblePanelSize.Height / sett.NumberOfSeriesBoxes.Height);
                    imgMaxSize = new Size(SeriesView.SeriesViewSize.Width - SeriesView.SVPadding, SeriesView.SeriesViewSize.Height / 2);
                    SeriesView.SeriesViewImageSize = Utils.ScaleRectangle(758, 140, SeriesView.SeriesViewSize.Width, SeriesView.SeriesViewSize.Height / 2);
                    SeriesView.Titles[false].FontSize = 8 + SeriesView.SeriesViewSize.Height / 20;
                    SeriesView.Titles[true].FontSize = 8 + SeriesView.SeriesViewSize.Height / 20;
                    SeriesView.LastWatcheds[false].FontSize = 3 + SeriesView.SeriesViewSize.Height / 30;
                    SeriesView.LastWatcheds[true].FontSize = 3 + SeriesView.SeriesViewSize.Height / 30;
                    Paths.Star = Utils.GetScaledImage(Paths.StarImageFile, new Size(SeriesView.SeriesViewSize.Height / 10, SeriesView.SeriesViewSize.Height / 10), Paths.Star);
                    Paths.UnknownSeriesBannerImage = Utils.GetScaledImage(Paths.UnknownSeriesBannerImageFile, SeriesView.SeriesViewImageSize, Paths.UnknownSeriesBannerImage);
                    this.contentP.ScrollAmount = sett.ScrollBarPosition == FancyScrollBar.FancyScrollBarPosition.Bottom ? SeriesView.SeriesViewSize.Width / 2 : SeriesView.SeriesViewSize.Height;
                    break;
                case Settings.EShowViewMode.List:
                    SeriesView.SeriesViewSize = new Size(contentP.VisiblePanelSize.Width / sett.NumberOfSeriesBoxes.Width,
                        contentP.VisiblePanelSize.Height / sett.NumberOfSeriesBoxes.Height);
                    imgMaxSize = new Size(SeriesView.SeriesViewSize.Width, SeriesView.SeriesViewSize.Height - SeriesView.SVPadding);
                    SeriesView.SeriesViewImageSize = Utils.ScaleRectangle(680, 1000, SeriesView.SeriesViewSize.Width, SeriesView.SeriesViewSize.Height);
                    SeriesView.Titles[false].FontSize = 8 + SeriesView.SeriesViewSize.Height / 20;
                    SeriesView.Titles[true].FontSize = 8 + SeriesView.SeriesViewSize.Height / 20;
                    SeriesView.LastWatcheds[false].FontSize = 3 + SeriesView.SeriesViewSize.Height / 30;
                    SeriesView.LastWatcheds[true].FontSize = 3 + SeriesView.SeriesViewSize.Height / 30;
                    Paths.Star = Utils.GetScaledImage(Paths.StarImageFile, new Size(SeriesView.SeriesViewSize.Height / 10, SeriesView.SeriesViewSize.Height / 10), Paths.Star);
                    Paths.UnknownSeriesPosterImage = Utils.GetScaledImage(Paths.UnknownSeriesPosterImageFile, SeriesView.SeriesViewImageSize, Paths.UnknownSeriesPosterImage);
                    this.contentP.ScrollAmount = sett.ScrollBarPosition == FancyScrollBar.FancyScrollBarPosition.Bottom ? SeriesView.SeriesViewSize.Width / 2 : SeriesView.SeriesViewSize.Height / 2;
                    break;
                default:
                    return;
            }
            #endregion format static SeriesView attributes

            for (int iSurplus = seriesList.Count; iSurplus < seriesViews.Count; iSurplus++)
                seriesViews[iSurplus].Hide();

            #region create and position views
            // to count the number of weekday boxes for the current day -> to calculate the coordinates; only on Settings.EShowViewMode.Weekdays
            int[] weekDayBoxes = new int[7];
            // iterate
            for (int iView = 0, lastX = 0, lastY = 0; iView < seriesList.Count; iView++)
            {
                if (iView >= seriesViews.Count)
                {
                    SeriesView newView = new SeriesView(null, Settings.EShowViewMode.List, SeriesView_Click);
                    seriesViews.Add(newView);
                    this.contentP.AddControlToFancyPanel(newView, Point.Empty, false);
                }
                SeriesView view = seriesViews[iView];
                view.Series = seriesList[iView];
                view.ViewMode = sett.ShowViewMode;

                #region set view location (using lastX and lastY)
                switch (view.ViewMode)
                {
                    // view mode weekdays
                    case Settings.EShowViewMode.Weekdays:
                        view.Visible = view.Series.WeeklyBroadcast.DayOfWeek != null;
                        if (!view.Visible)
                            view.Location = Point.Empty;
                        else
                        {
                            switch (sett.ScrollBarPosition)
                            {
                                // view mode weekdays, scroll horizontally
                                case FancyScrollBar.FancyScrollBarPosition.Bottom:
                                    lastY = SeriesView.SeriesViewSize.Height * ((int) view.Series.WeeklyBroadcast.DayOfWeek - 1);
                                    lastX = SeriesView.SeriesViewSize.Width * weekDayBoxes[(int) view.Series.WeeklyBroadcast.DayOfWeek - 1];
                                    break;
                                // view mode weekdays, scroll vertically
                                case FancyScrollBar.FancyScrollBarPosition.Right:
                                    lastY = SeriesView.SeriesViewSize.Height * weekDayBoxes[(int) view.Series.WeeklyBroadcast.DayOfWeek - 1];
                                    lastX = SeriesView.SeriesViewSize.Width * ((int) view.Series.WeeklyBroadcast.DayOfWeek - 1);
                                    break;
                            }
                            weekDayBoxes[(int) view.Series.WeeklyBroadcast.DayOfWeek - 1]++;
                            view.Bounds = new Rectangle(new Point(lastX, lastY), SeriesView.SeriesViewSize);
                        }
                        view.SeriesImage = Utils.GetScaledImage(Paths.ImagesFolder + view.Series.BannerFilename, imgMaxSize, Paths.UnknownSeriesBannerImage);
                        break;
                    // view mode list
                    case Settings.EShowViewMode.List:
                        view.Visible = true;
                        view.Bounds = new Rectangle(new Point(lastX, lastY), SeriesView.SeriesViewSize);
                        switch (sett.ScrollBarPosition)
                        {
                            // view mode list, scroll horizontally
                            case FancyScrollBar.FancyScrollBarPosition.Bottom:
                                lastY += SeriesView.SeriesViewSize.Height;
                                if (lastY >= contentP.VisiblePanelSize.Height - (contentP.VisiblePanelSize.Height % SeriesView.SeriesViewSize.Height))
                                {
                                    lastX += SeriesView.SeriesViewSize.Width;
                                    lastY = 0;
                                }
                                break;
                            // view mode list, scroll vertically
                            case FancyScrollBar.FancyScrollBarPosition.Right:
                                lastX += SeriesView.SeriesViewSize.Width;
                                if (lastX >= contentP.VisiblePanelSize.Width - (contentP.VisiblePanelSize.Width % SeriesView.SeriesViewSize.Width))
                                {
                                    lastY += SeriesView.SeriesViewSize.Height;
                                    lastX = 0;
                                }
                                break;
                        }
                        view.SeriesImage = Utils.GetScaledImage(Paths.ImagesFolder + view.Series.PosterFilename, imgMaxSize, Paths.UnknownSeriesPosterImage);
                        break;
                }
                #endregion set view location (using lastX and lastY)
            }
            #endregion create and position views
            this.contentP.ScrollBarPosition = sett.ScrollBarPosition;
            this.contentP.ResizeInnerPanelToControls();

            this.RefreshSeriesListContent(seriesList);
        }

        public void RefreshSeriesListContent(SeriesList seriesList)
        {
            for (int iView = 0; iView < seriesList.Count; iView++)
                seriesViews[iView].Invalidate();
            this.LastSeriesList = seriesList;
        }

        private void SeriesView_Click(object sender, EventArgs e)
        {
            this.ShowOnlyFormAndHideAllOthers(this.SeriesForm);
            this.SeriesForm.Series = (sender as SeriesView).Series;
        }
        #endregion
    }
}
