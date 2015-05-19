using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using TvDatabase.Classes;
using TvDatabase.VisualComponents;

namespace TvDatabase.Forms
{
    public partial class FSeries : Form
    {
        public static readonly FontWithColor GenreFont = new FontWithColor("Ubuntu", 16, true, true, false, Color.Purple);

        internal FMain mainForm { get; private set; }
        private FMenu seriesMenuForm;
        private Series series;

        private FancyCheckBoxCollection statusChBs;
        private List<Label> genreLs;
        private FancyPanel actingRolesFP;
        private List<ActingRoleView> actingRoleVs;
        private FancyPanel episodesFP;
        private List<SeasonEpisodeView> episodeVs;

        public FSeries(FMain mainForm)
        {
            this.mainForm = mainForm;
            InitializeComponent();
        }

        private void FSeries_Resize(object sender, EventArgs e)
        {
            menuP.Location = new Point(this.Width - menuP.Width - Sizes.ControlPadding, Sizes.ControlPadding);
            statusP.Location = new Point(menuP.Left - statusP.Width - Sizes.ControlPadding, menuP.Top);
            titleP.SetBounds(Sizes.ControlPadding, statusP.Bottom + Sizes.ControlPadding, this.Width - 2 * Sizes.ControlPadding, 100);
            infoP.SetBounds(Sizes.ControlPadding, titleP.Bottom + Sizes.ControlPadding, 280, this.Height - infoP.Top - Sizes.ControlPadding);
            Size posterSize = Utils.ScaleRectangle(Sizes.DefaultPosterSize.Width, Sizes.DefaultPosterSize.Height, this.Width, infoP.Height);
            posterImg.SetBounds(infoP.Right + Sizes.ControlPadding, infoP.Top, posterSize.Width, posterSize.Height);
            actingRolesP.SetBounds(posterImg.Right + Sizes.ControlPadding, infoP.Top, this.Width - posterImg.Right - 2 * Sizes.ControlPadding, infoP.Height / 3);
            actingRolesFPContainerP.SetBounds(0, actingRolesL.Bottom, actingRolesP.Width, actingRolesP.Height - actingRolesL.Bottom);
            episodesP.SetBounds(actingRolesP.Left, actingRolesP.Bottom + Sizes.ControlPadding, actingRolesP.Width, infoP.Height - actingRolesP.Height - Sizes.ControlPadding);
            episodesFPContainerP.SetBounds(0, episodesL.Bottom, episodesP.Width, episodesP.Height - episodesL.Bottom);
            //
            label18.Top = infoP.Height - label18.Height;
            label17.Top = label18.Top;
            label16.Top = label18.Top - 22;
            label15.Top = label16.Top;
            overviewL.SetBounds(0, label14.Bottom + Sizes.ControlPadding, infoP.Width, label16.Top - label14.Bottom - 2 * Sizes.ControlPadding);
        }

        private void FSeries_Load(object sender, EventArgs e)
        {
            this.seriesMenuForm = new FMenu(this);
            new FancyButtonCollection(menuP, new string[] { "Close" }, MenuButton_Click, true, 0);
            this.statusChBs = new FancyCheckBoxCollection(statusP, new Rectangle(Point.Empty, statusP.Size),
                new string[] { "Active", "Followed" }, SeriesStatus_Click, true);
            this.genreLs = new List<Label>();
            this.actingRolesFP = new FancyPanel(actingRolesFPContainerP, FancyScrollBar.FancyScrollBarPosition.Bottom, 5, true);
            this.actingRoleVs = new List<ActingRoleView>();
            this.episodesFP = new FancyPanel(episodesFPContainerP, FancyScrollBar.FancyScrollBarPosition.Right, 5, true);
            this.episodeVs = new List<SeasonEpisodeView>();
        }

        private void ActingRoleView_MouseEnter(object sender, EventArgs e)
        {
            this.MouseWheel -= this.episodesFP.MouseWheelScroll_EvtHandler;
            this.MouseWheel -= this.actingRolesFP.MouseWheelScroll_EvtHandler;
            this.MouseWheel += this.actingRolesFP.MouseWheelScroll_EvtHandler;
        }

        private void EpisodeView_MouseEnter(object sender, EventArgs e)
        {
            this.MouseWheel -= this.actingRolesFP.MouseWheelScroll_EvtHandler;
            this.MouseWheel -= this.episodesFP.MouseWheelScroll_EvtHandler;
            this.MouseWheel += this.episodesFP.MouseWheelScroll_EvtHandler;
        }

        public Series Series
        {
            get { return this.series; }
            set
            {
                this.series = value;

                // title and status
                titleL.Text = this.series.Name;
                statusChBs[0].Text = this.series.Active ? "Active" : "Inactive";
                statusChBs[0].Checked = this.series.Active;
                statusChBs[1].Text = "Followed";
                statusChBs[1].Checked = true;

                // info
                RefreshGenreLabels(this.series.Genres.GetIDs());
                SetInfoAndPositionLabels(new BinaryVariants<Label>(label1, label2), new BinaryVariants<string>("Continuing:", this.series.Continuing == null ? "unknown" : (bool) this.series.Continuing ? "Yes" : "No"));
                SetInfoAndPositionLabels(new BinaryVariants<Label>(label3, label4), new BinaryVariants<string>("Broadcast:", this.series.WeeklyBroadcast.Format()));
                SetInfoAndPositionLabels(new BinaryVariants<Label>(label5, label6), new BinaryVariants<string>("Rating:", this.series.Rating.Value == 0 ? "unknown" : this.series.Rating.Value + "/" + Rating.HighestRating));
                SetInfoAndPositionLabels(new BinaryVariants<Label>(label7, label8), new BinaryVariants<string>("Language:", this.series.Language.Name));
                SetInfoAndPositionLabels(new BinaryVariants<Label>(label9, label10), new BinaryVariants<string>("Network:", this.series.Network.Name));
                SetInfoAndPositionLabels(new BinaryVariants<Label>(label11, label12), new BinaryVariants<string>("IMDB ID:", this.series.IMDbID == null ? "unknown" : this.series.IMDbID));
                SetInfoAndPositionLabels(new BinaryVariants<Label>(label13, label14), new BinaryVariants<string>("Runtime:", this.series.Runtime == null ? "unknown" : (int) this.series.Runtime + " minutes"));
                SetInfoAndPositionLabels(new BinaryVariants<Label>(label15, label16), new BinaryVariants<string>("Updated (website):", this.series.LastUpdatedOnWebsite == null ? "unknown" : Utils.FormatDateTime((DateTime) this.series.LastUpdatedOnWebsite, "d MMM yyyy")));
                SetInfoAndPositionLabels(new BinaryVariants<Label>(label17, label18), new BinaryVariants<string>("Updated (database):", this.series.LastUpdatedInDatabase == null ? "unknown" : Utils.FormatDateTime((DateTime) this.series.LastUpdatedInDatabase, "d MMM yyyy")));
                overviewL.Text = this.series.Overview;

                // poster
                posterImg.Image = Utils.GetScaledImage(Paths.ImagesFolder + this.series.PosterFilename, posterImg.Size,
                    Utils.ScaleImage(Paths.UnknownSeriesPosterImage, posterImg.Size, false));

                // acting roles
                actingRolesL.Text = "Acting roles (" + this.series.ActingRoles.Count + ")";
                Size viewSize = Utils.ScaleRectangle(Sizes.DefaultActingRoleSize.Width, Sizes.DefaultActingRoleSize.Height, this.actingRolesFP.VisiblePanelSize.Width, this.actingRolesFP.VisiblePanelSize.Height);
                this.actingRolesFP.ScrollAmount = (viewSize.Width + Sizes.ControlPadding / 3) / 2;
                for (int iARV = this.series.ActingRoles.Count; iARV < this.actingRoleVs.Count; iARV++)
                    this.actingRoleVs[iARV].Hide();
                for (int iAR = 0; iAR < this.series.ActingRoles.Count; iAR++)
                {
                    if (iAR >= this.actingRoleVs.Count)
                    {
                        ActingRoleView newView = new ActingRoleView(ActingRoleView_Click);
                        newView.Size = viewSize;
                        newView.MouseEnter += this.ActingRoleView_MouseEnter;
                        this.actingRoleVs.Add(newView);
                        this.actingRolesFP.AddControlToFancyPanel(newView, new Point(iAR * (viewSize.Width + Sizes.ControlPadding / 3), 0), false);
                    }
                    ActingRoleView view = this.actingRoleVs[iAR];
                    view.ActingRole = this.series.ActingRoles[iAR];
                    view.Show();
                }
                this.actingRolesFP.ResizeInnerPanelToControls();

                // episodes
                episodesL.Text = "Episodes (" + this.series.Episodes.Count + ")";
                List<int> seasons = this.series.Episodes.GetSeasons();
                List<string> viewStrs = new List<string>();
                for (int iS = 0, iE = 0; iS < seasons.Count; iS++)
                {
                    viewStrs.Add("Season:" + seasons[iS].ToString());
                    while (iE < this.series.Episodes.Count && seasons[iS] == this.series.Episodes[iE].SeasonNumber)
                    {
                        viewStrs.Add(this.series.Episodes[iE].ID);
                        iE++;
                    }
                }
                viewSize = new Size(this.episodesFP.VisiblePanelSize.Width, 24);
                this.episodesFP.ScrollAmount = 8 * viewSize.Height;
                for (int iEV = viewStrs.Count; iEV < this.episodeVs.Count; iEV++)
                    this.episodeVs[iEV].Hide();
                for (int iE = 0; iE < viewStrs.Count; iE++)
                {
                    if (iE >= this.episodeVs.Count)
                    {
                        SeasonEpisodeView newView = new SeasonEpisodeView(EpisodeView_Click);
                        newView.Size = viewSize;
                        newView.MouseEnter += this.EpisodeView_MouseEnter;
                        this.episodeVs.Add(newView);
                        this.episodesFP.AddControlToFancyPanel(newView, new Point(0, iE * viewSize.Height), false);
                    }
                    SeasonEpisodeView view = this.episodeVs[iE];
                    if (viewStrs[iE].Length > 7 && viewStrs[iE].Substring(0, 7).Equals("Season:"))
                    {
                        view.SeasonName = viewStrs[iE].Substring(7);
                        view.ViewMode = SeasonEpisodeView.ViewModes.Season;
                    }
                    else
                    {
                        view.Episode = this.series.Episodes.GetByID(viewStrs[iE]);
                        view.ViewMode = SeasonEpisodeView.ViewModes.Episode;
                    }
                }
                this.episodesFP.ResizeInnerPanelToControls();
            }
        }

        private void RefreshGenreLabels(string[] genreIDs)
        {
            for (int iL = genreIDs.Length; iL < genreLs.Count; iL++)
                genreLs[iL].Hide();
            for (int iL = 0; iL < genreIDs.Length; iL++)
            {
                if (iL >= genreLs.Count)
                {
                    Label newLabel = new Label();
                    newLabel.Parent = this;
                    newLabel.Font = GenreFont.GetFont();
                    newLabel.ForeColor = GenreFont.FontColor;
                    newLabel.AutoSize = true;
                    newLabel.Cursor = Cursors.Hand;
                    newLabel.Click += GenreLabel_Click;
                    genreLs.Add(newLabel);
                }
                Label label = genreLs[iL];
                label.Text = this.mainForm.Database.Genres.GetByID(genreIDs[iL]).Name;
                label.Location = new Point(iL == 0 ? titleP.Left : genreLs[iL - 1].Right + Sizes.ControlPadding, titleP.Top - 28);
                label.Show();
            }
        }

        private void SetInfoAndPositionLabels(BinaryVariants<Label> labels, BinaryVariants<string> captions)
        {
            labels[false].Text = captions[false];
            labels[true].Text = captions[true];
            labels[true].Left = labels[true].Parent.Width - labels[true].Width;
            labels[false].Left = labels[true].Left - labels[false].Width;
        }

        private void MenuButton_Click(object sender, EventArgs e)
        {
            this.mainForm.ShowOnlyFormAndHideAllOthers(null);
        }

        private void SeriesStatus_Click(object sender, EventArgs e)
        {
            MessageBox.Show("SeriesStatus_Click: Not yet available.");
        }

        private void GenreLabel_Click(object sender, EventArgs e)
        {
            MessageBox.Show("GenreLabel_Click: " + this.mainForm.Database.Genres.GetFirstByName((sender as Label).Text).Name); //as test
        }

        private void ActingRoleView_Click(object sender, EventArgs e)
        {
            MessageBox.Show("ActingRoleView_Click: " + (sender as ActingRoleView).ActingRole.Name + " played by " + (sender as ActingRoleView).ActingRole.Actor.Name);
        }

        private void EpisodeView_Click(object sender, EventArgs e)
        {
            SeasonEpisodeView view = sender as SeasonEpisodeView;
            if (view.ViewMode == SeasonEpisodeView.ViewModes.Episode)
            {
                this.seriesMenuForm.Argument = new object[] { view, this.episodeVs };
                Size menuFormSize = new Size(320, Sizes.MenuButton.Height + 2 * this.seriesMenuForm.BorderPB.BorderWidth);
                Point location = view.Parent.PointToScreen(view.Location);
                location.Offset(this.episodesFPContainerP.Width / 2 - menuFormSize.Width / 2,
                    -MenuButtonCaptions.SeriesForm_EpisodeViewOptions.Length * menuFormSize.Height);
                this.seriesMenuForm.RefreshMenuForm(MenuButtonCaptions.SeriesForm_EpisodeViewOptions,
                    this.seriesMenuForm.SeriesForm_EpisodeViewOptions_Click, location, menuFormSize.Width);
            }
        }
    }
}
