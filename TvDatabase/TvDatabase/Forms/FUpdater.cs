using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;
using Ionic.Zip;
using TvDatabase.Classes;
using TvDatabase.VisualComponents;

namespace TvDatabase.Forms
{
    public partial class FUpdater : Form
    {
        private FMain mainForm;

        private FancyButton menuButton;
        private FancyProgressBar progressBar;

        private MyThread updateBgW;

        public FUpdater(FMain mainForm)
        {
            this.mainForm = mainForm;
            InitializeComponent();
        }

        private void FUpdater_Load(object sender, EventArgs e)
        {
            this.menuButton = new FancyButton(menuP, "Close", MenuButton_Click, new Rectangle(Point.Empty, menuP.Size));
            this.progressBar = new FancyProgressBar(this, new Rectangle(statusPercentageL.Left, statusPercentageL.Bottom + 6, this.Width - 2 * statusPercentageL.Left, 20));
            this.updateBgW = new MyThread(true, true, UpdateBgW_Work, UpdateBgW_Update, UpdateBgW_Done);
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            e.Graphics.Clear(Colors.Backgrounds[false]);
            e.Graphics.DrawRectangle(Pens.Gray, 1, 1, this.Width - 2, this.Height - 2);
            base.OnPaint(e);
        }

        #region decoding utilities
        private string GetInnerText(XmlNode node)
        {
            return node != null ? node.InnerText : null;
        }
        #endregion

        public void StartSeriesUpdate(UpdateArgument arg)
        {
            if (!this.updateBgW.IsBusy)
            {
                this.menuButton.Text = MenuButtonCaptions.UpdateForm_MainMenu[0];
                this.errorL.Text = "";
                this.updateBgW.RunWorkerAsync(arg);
            }
        }

        private void UpdateBgW_Work(object sender, DoWorkEventArgs args)
        {
            // initialize
            updateBgW.ReportProgress(0, "Initializing update and checking connection");
            UpdateArgument arg = args.Argument as UpdateArgument;
            string urlAdress = arg.Database.Settings.MirrorURL;
            string destFile = Paths.TemporaryStorageFolder + "tempDld.xml";
            string unzipFolder = Paths.TemporaryStorageFolder + @"unzipped\";
            string dldResult = Utils.DownloadFromTheHolyInternet(urlAdress, destFile);
            if (!dldResult.Equals(""))
                throw new ApplicationException("Connection to " + urlAdress + " failed!\n\n" + dldResult);

            // download and decode languages.xml
            urlAdress = arg.Database.Settings.MirrorURL + @"api/" + arg.Database.Settings.ApiKey + @"/languages.xml";
            dldResult = Utils.DownloadFromTheHolyInternet(urlAdress, destFile);
            if (!dldResult.Equals(""))
            {
                MessageBox.Show("Failed to download language list from " + urlAdress + "!\n\n" + dldResult + "\n\n", "Language list ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            XmlDocument doc = new XmlDocument();
            doc.Load(destFile);
            XmlNodeList languageNodes = doc.SelectSingleNode("Languages").SelectNodes("Language");
            foreach (XmlNode node in languageNodes)
                if (arg.Database.Languages.GetByID(node.SelectSingleNode("abbreviation").InnerText) == null)
                    arg.Database.Languages.Add(new Language(node.SelectSingleNode("abbreviation").InnerText, node.SelectSingleNode("name").InnerText));

            if (updateBgW.CancellationPending)
                throw new ApplicationException("Update cancelled by user.\n\n");

            // prepare for epViews iteration
            destFile = Paths.TemporaryStorageFolder + "tempDld.zip";
            double progressStep = 100.0 / arg.SeriesToUpdate.Count, progress = -progressStep;

            // iterate
            for (int i = 0; i < arg.SeriesToUpdate.Count; i++)
            {
                Series S = arg.SeriesToUpdate[i];
                progress += progressStep;
                updateBgW.ReportProgress((int) progress, "Downloading and updating information for " + S.Name);

                // download archive
                urlAdress = arg.Database.Settings.MirrorURL + @"api/" + arg.Database.Settings.ApiKey + @"/series/" + S.ID + @"/all/en.zip";
                dldResult = Utils.DownloadFromTheHolyInternet(urlAdress, destFile);
                if (!dldResult.Equals(""))
                {
                    MessageBox.Show("Failed to download file from " + urlAdress + "!\n\n" + dldResult + "\n\n", S.ID + " ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    continue;
                }

                if (updateBgW.CancellationPending)
                    throw new ApplicationException("Update cancelled by user.\n\n");

                // unarchive
                ZipFile zip = new ZipFile(destFile);
                zip.ExtractAll(unzipFolder, ExtractExistingFileAction.OverwriteSilently);

                // decode actors.xml
                doc = new XmlDocument();
                doc.Load(unzipFolder + "actors.xml");
                XmlNodeList actorNodes = doc.SelectSingleNode("Actors").SelectNodes("Actor");
                foreach (XmlNode actorNode in actorNodes)
                {
                    // decode item
                    string id = GetInnerText(actorNode.SelectSingleNode("id"));
                    string actorName = GetInnerText(actorNode.SelectSingleNode("Name"));
                    string roleName = GetInnerText(actorNode.SelectSingleNode("Role"));
                    int sortOrder = Int32.Parse(GetInnerText(actorNode.SelectSingleNode("SortOrder")));
                    string imgUrlStr = GetInnerText(actorNode.SelectSingleNode("Image"));

                    // update
                    Person actor = arg.Database.People.GetFirstByName(actorName);
                    if (actor == null)
                    {
                        actor = new Person(arg.Database.People.GetUniqueID(4), actorName);
                        arg.Database.People.Add(actor);
                    }

                    ActingRole role = S.ActingRoles.GetByID(id);
                    if (role == null)
                    {
                        role = new ActingRole(id, roleName, actor, sortOrder, null);
                        S.ActingRoles.Add(role);
                    }
                    else
                    {
                        role.Name = roleName;
                        role.Actor = actor;
                        role.SortOrder = sortOrder;
                        // don't touch "role.ImageFilename"
                    }

                    // image
                    if (arg.Database.Settings.DownloadImagesForActors && imgUrlStr != null && !File.Exists(Paths.ImagesFolder + role.ImageFilename))
                    {
                        string imgFile = Paths.GetUniqueImagePath(6);
                        if (Utils.DownloadFromTheHolyInternet(arg.Database.Settings.MirrorURL + "/banners/" + imgUrlStr, imgFile).Equals(""))
                            role.ImageFilename = Path.GetFileName(imgFile);
                    }
                }

                if (updateBgW.CancellationPending)
                    throw new ApplicationException("Update cancelled by user.\n\n");

                // decode en.xml
                doc = new XmlDocument();
                doc.Load(unzipFolder + "en.xml");
                XmlNode seriesNode = doc.SelectSingleNode("Data").SelectSingleNode("Series");

                // name
                S.Name = GetInnerText(seriesNode.SelectSingleNode("SeriesName"));

                // language
                string langAbbr = GetInnerText(seriesNode.SelectSingleNode("Language"));
                Language language = langAbbr != null ? arg.Database.Languages.GetByID(langAbbr) : null;
                S.Language = language != null ? language : Database.UnknownLanguage;

                // weekly broadcast
                string dowStr = GetInnerText(seriesNode.SelectSingleNode("Airs_DayOfWeek"));
                DayOfWeek[] dows = new DayOfWeek[] { DayOfWeek.Monday, DayOfWeek.Tuesday, DayOfWeek.Wednesday, DayOfWeek.Thursday, DayOfWeek.Friday, DayOfWeek.Saturday, DayOfWeek.Sunday };
                S.WeeklyBroadcast.DayOfWeek = null;
                for (int iDow = 0; iDow < 7; iDow++)
                    if (dowStr.ToString().Equals(dows[iDow].ToString()))
                        S.WeeklyBroadcast.DayOfWeek = iDow + 1;
                string timeStr = GetInnerText(seriesNode.SelectSingleNode("Airs_Time"));
                S.WeeklyBroadcast.Time = timeStr != null ? (DateTime?) Utils.DecodeDateTime(timeStr, Utils.TheTvDbTimeFormat) : null;

                // genres
                S.Genres.Clear();
                string genreList = GetInnerText(seriesNode.SelectSingleNode("Genre"));
                if (genreList == null)
                    S.Genres.Add(Database.UnknownGenre);
                else
                {
                    string[] genreStrs = genreList.Split('|');
                    foreach (string genreStr in genreStrs)
                    {
                        if (genreStr.Trim().Equals(""))
                            continue;
                        Genre genre = arg.Database.Genres.GetFirstByName(genreStr);
                        if (genre == null)
                        {
                            genre = new Genre(arg.Database.Genres.GetUniqueID(3), genreStr);
                            arg.Database.Genres.Add(genre);
                        }
                        S.Genres.Add(genre);
                    }
                    if (S.Genres.Count == 0)
                        S.Genres.Add(Database.UnknownGenre);
                }

                // imdb id
                S.IMDbID = GetInnerText(seriesNode.SelectSingleNode("IMDB_ID"));

                // network
                string networkName = GetInnerText(seriesNode.SelectSingleNode("Network"));
                Network network = networkName != null ? arg.Database.Networks.GetFirstByName(networkName) : null;
                if (network == null && networkName != null)
                {
                    network = new Network(arg.Database.Networks.GetUniqueID(3), networkName);
                    arg.Database.Networks.Add(network);
                }
                S.Network = network != null ? network : Database.UnknownNetwork;

                // overview
                S.Overview = GetInnerText(seriesNode.SelectSingleNode("Overview"));

                // runtime
                string runtimeStr = GetInnerText(seriesNode.SelectSingleNode("Runtime"));
                S.Runtime = runtimeStr != null ? (int?) Int32.Parse(runtimeStr) : null;

                // continuing
                string contuingStr = GetInnerText(seriesNode.SelectSingleNode("Status"));
                S.Continuing = contuingStr != null ? (bool?) contuingStr.Equals("Continuing") : null;

                // last updated (website)
                string lastUpWeb = GetInnerText(seriesNode.SelectSingleNode("lastupdated"));
                S.LastUpdatedOnWebsite = lastUpWeb != null ? (DateTime?) Utils.UnixTimeStampToDateTime(Double.Parse(lastUpWeb)) : null;

                // episodes
                XmlNodeList episodeNodes = doc.SelectSingleNode("Data").SelectNodes("Episode");
                foreach (XmlNode epNode in episodeNodes)
                {
                    string epID = GetInnerText(epNode.SelectSingleNode("id"));
                    string epName = GetInnerText(epNode.SelectSingleNode("EpisodeName"));
                    int seasNo = Int32.Parse(GetInnerText(epNode.SelectSingleNode("SeasonNumber")));
                    int epNo = Int32.Parse(GetInnerText(epNode.SelectSingleNode("EpisodeNumber")));
                    string epOverview = GetInnerText(epNode.SelectSingleNode("Overview"));
                    string firstAiredStr = GetInnerText(epNode.SelectSingleNode("FirstAired"));
                    DateTime? firstAired = firstAiredStr != null ? Utils.DecodeNullableDateTime(firstAiredStr, Utils.StandardDateFormat) : null;

                    Episode episode = S.Episodes.GetByID(epID);
                    if (episode == null)
                        S.Episodes.Add(new Episode(epID, epName, seasNo, epNo, epOverview, firstAired, null));
                    else
                    {
                        episode.Name = epName;
                        episode.SeasonNumber = seasNo;
                        episode.EpisodeNumber = epNo;
                        episode.Overview = epOverview;
                        episode.FirstBroadcasted = firstAired;
                    }
                }

                // images
                string imageStr = GetInnerText(seriesNode.SelectSingleNode("poster"));
                if (arg.Database.Settings.DownloadImagesForSeries && imageStr != null && !File.Exists(Paths.ImagesFolder + S.PosterFilename))
                {
                    string imgFile = Paths.GetUniqueImagePath(6);
                    if (Utils.DownloadFromTheHolyInternet(arg.Database.Settings.MirrorURL + "/banners/" + imageStr, imgFile).Equals(""))
                        S.PosterFilename = Path.GetFileName(imgFile);
                }
                imageStr = GetInnerText(seriesNode.SelectSingleNode("banner"));
                if (arg.Database.Settings.DownloadImagesForSeries && imageStr != null && !File.Exists(Paths.ImagesFolder + S.BannerFilename))
                {
                    string imgFile = Paths.GetUniqueImagePath(6);
                    if (Utils.DownloadFromTheHolyInternet(arg.Database.Settings.MirrorURL + "/banners/" + imageStr, imgFile).Equals(""))
                        S.BannerFilename = Path.GetFileName(imgFile);
                }

                // last updated (database)
                S.LastUpdatedInDatabase = DateTime.Now;

                if (updateBgW.CancellationPending)
                    throw new ApplicationException("Update cancelled by user.\n\n");
            }
            args.Result = arg;
        }

        private void UpdateBgW_Update(object sender, ProgressChangedEventArgs args)
        {
            progressBar.Percentage = args.ProgressPercentage;
            statusPercentageL.Text = args.ProgressPercentage + "%";
            statusL.Text = args.UserState as string + "...";
        }

        private void UpdateBgW_Done(object sender, RunWorkerCompletedEventArgs args)
        {
            progressBar.Percentage = 100;
            statusPercentageL.Text = "100%";
            statusL.Text = "Update has finished.";
            this.menuButton.Text = MenuButtonCaptions.UpdateForm_MainMenu[1];
            this.menuButton.Invalidate();

            if (args.Error != null && !args.Error.Message.Contains("cancelled by user"))
            {
                errorL.Text += args.Error.ToString();
                MessageBox.Show("Some errors have occured. The database has not been saved.", "Info errors", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else
            {
                string saveResult = (args.Result as UpdateArgument).Database.SaveDatabase();
                if (!saveResult.Equals(""))
                    MessageBox.Show("An error occured while saving the newly updated database to file.\n\n" + saveResult, "Save ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void errorL_Click(object sender, EventArgs e)
        {
            MessageBox.Show(errorL.Text, "Update ERROR", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void MenuButton_Click(object sender, EventArgs e)
        {
            string text = (sender as FancyButton).Text;
            if (text.Equals(MenuButtonCaptions.UpdateForm_MainMenu[0]))
            {
                this.updateBgW.CancelAsync();
                (sender as FancyButton).Text = MenuButtonCaptions.UpdateForm_MainMenu[1];
            }
            else if (text.Equals(MenuButtonCaptions.UpdateForm_MainMenu[1]))
            {
                this.mainForm.ShowOnlyFormAndHideAllOthers(null);
                this.mainForm.RefreshSeriesListWithNecessaryCreations(this.mainForm.Database.Series);
            }
        }
    }
}
