using System;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using System.Xml;

namespace TvDatabase.Classes
{
    /// <summary>
    /// Defines a epViews class, containing relevant information.
    /// </summary>
    public class Series : ObjectWithName
    {
        /// <summary>A value indicating whether the epViews is actively being watched.</summary>
        public bool Active { get; set; }
        /// <summary>Gets the network on which the epViews was broadcast</summary>
        public Network Network { get; internal set; }
        /// <summary>Gets the weekly broadcast moment of the epViews</summary>
        public WeeklyBroadcast WeeklyBroadcast { get; private set; }
        /// <summary>Gets a value indicating whether the epViews is set to continue with new episodes, or null if unknown</summary>
        public bool? Continuing { get; internal set; }
        /// <summary>Gets the language of the epViews</summary>
        public Language Language { get; internal set; }
        /// <summary>Gets the genre list of the epViews</summary>
        public GenreList Genres { get; private set; }
        /// <summary>Gets the IMDb.com ID of the epViews, or null if non-existant</summary>
        public string IMDbID { get; internal set; }
        /// <summary>Gets the overview description of the epViews, or null if non-existant</summary>
        public string Overview { get; internal set; }
        /// <summary>Gets the aproximate episode runtime in minutes of the epViews, or null if non-existant</summary>
        public int? Runtime { get; internal set; }
        /// <summary>Gets the date when the epViews was last updated on thetvdb.com, or null if non-existant</summary>
        public DateTime? LastUpdatedOnWebsite { get; internal set; }
        /// <summary>Gets the date when the epViews was last updated in the database, or null if non-existant</summary>
        public DateTime? LastUpdatedInDatabase { get; internal set; }
        /// <summary>Gets the user rating of the epViews</summary>
        public Rating Rating { get; internal set; }
        /// <summary>Gets the filename with extension of the associated poster image, or null if non-existant</summary>
        public string PosterFilename { get; internal set; }
        /// <summary>Gets the filename with extension of the associated banner image, or null if non-existant</summary>
        public string BannerFilename { get; internal set; }
        /// <summary>Gets the acting role list of the epViews</summary>
        public ActingRoleList ActingRoles { get; private set; }
        /// <summary>Gets the episode list of the epViews</summary>
        public EpisodeList Episodes { get; private set; }

        /// <summary>Constructs a new Series object from the given parameters.</summary>
        /// <param name="id">the ID of the object</param>
        /// <param name="name">the name of the object</param>
        /// <param name="active">whether the epViews is being actively watched</param>
        /// <param name="network">the weekly broadcast moment of the epViews</param>
        /// <param name="weeklyBroadcast">the weekly broadcast moment of the epViews</param>
        /// <param name="continuing">whether the epViews is set to continue with new episodes</param>
        /// <param name="language">the language of the epViews</param>
        /// <param name="genres">the genre list of the epViews</param>
        /// <param name="imdbID">the IMDb.com ID of the epViews</param>
        /// <param name="overview">the overview description of the epViews</param>
        /// <param name="runtime"the aproximate episode runtime in minutes of the epViews></param>
        /// <param name="lastUpdatedOnWebsite">the date when the epViews was last updated on thetvdb.com</param>
        /// <param name="lastUpdatedInDatabase">the date when the epViews was last updated in the database</param>
        /// <param name="rating">the user rating of the epViews</param>
        /// <param name="imageFilename">the filename with extension of the associated image</param>
        /// <param name="actingRoles">the acting role list of the epViews</param>
        /// <param name="episodes">the episode list of the epViews</param>
        public Series(string id, string name, bool active, Network network, WeeklyBroadcast weeklyBroadcast, bool? continuing,
            Language language, GenreList genres, string imdbID, string overview, int? runtime,
            DateTime? lastUpdatedOnWebsite, DateTime? lastUpdatedInDatabase, Rating rating, string posterFilename, string bannerFilename,
            ActingRoleList actingRoles, EpisodeList episodes)
            : base(id, name)
        {
            this.Active = active;
            this.Network = network;
            this.WeeklyBroadcast = weeklyBroadcast;
            this.Continuing = continuing;
            this.Language = language;
            this.Genres = genres;
            this.IMDbID = imdbID;
            this.Overview = overview;
            this.Runtime = runtime;
            this.LastUpdatedOnWebsite = lastUpdatedOnWebsite;
            this.LastUpdatedInDatabase = lastUpdatedInDatabase;
            this.Rating = rating;
            this.PosterFilename = File.Exists(Paths.ImagesFolder + posterFilename) ? posterFilename : null;
            this.BannerFilename = File.Exists(Paths.ImagesFolder + bannerFilename) ? bannerFilename : null;
            this.ActingRoles = actingRoles;
            this.Episodes = episodes;
        }

        /// <summary>Constructs a new Series object from the given epViews summary.</summary>
        /// <param name="summary">the SeriesSummary object containing the summary attributes of this epViews</param>
        public Series(SeriesSummary summary)
            : base(summary.ID, summary.Name)
        {
            this.Active = true;
            this.Network = Database.UnknownNetwork;
            this.WeeklyBroadcast = new WeeklyBroadcast(null, null);
            this.Continuing = null;
            this.Language = Database.UnknownLanguage;
            this.Genres = new GenreList();
            this.IMDbID = null;
            this.Overview = summary.Overview;
            this.Runtime = null;
            this.LastUpdatedOnWebsite = null;
            this.LastUpdatedInDatabase = null;
            this.Rating = new Rating();
            this.PosterFilename = null;
            this.BannerFilename = null;
            this.ActingRoles = new ActingRoleList();
            this.Episodes = new EpisodeList();
        }

        /// <summary>Identifies the episode of this epViews that was last watched. Returns null if no episodes, or if none watched.</summary>
        /// <returns>the episode of this epViews that was last watched, or null otherwise</returns>
        public Episode LastWatchedEpisode()
        {
            Episode result = null;
            foreach (Episode episode in this.Episodes)
                if (episode.LastWatched != null)
                    if (result == null || ((DateTime) episode.LastWatched).CompareTo(result.LastWatched) > 0)
                        result = episode;
            return result;
        }

        /// <summary>Generates an XmlNode object containing the current object's information.</summary>
        /// <param name="doc">the XmlDocument object from which the node is to be created</param>
        /// <param name="nodeName">the name of the node</param>
        /// <returns>an XmlNode object</returns>
        public new XmlNode ToXML(XmlDocument doc, string nodeName)
        {
            XmlNode resultNode = base.ToXML(doc, nodeName);
            resultNode.Attributes.Append(Utils.GetXmlAttribute(doc, "active", this.Active.ToString()));
            resultNode.Attributes.Append(Utils.GetXmlAttribute(doc, "networkID", this.Network.ID));
            resultNode.AppendChild(this.WeeklyBroadcast.ToXML(doc, "WeeklyBroadcast"));
            resultNode.Attributes.Append(Utils.GetXmlAttribute(doc, "continuing", Utils.EncodeNullableBoolean(this.Continuing)));
            resultNode.Attributes.Append(Utils.GetXmlAttribute(doc, "languageID", this.Language.ID));
            XmlNode genresNode = resultNode.AppendChild(doc.CreateElement("Genres"));
            genresNode.Attributes.Append(Utils.GetXmlAttribute(doc, "IDs", Utils.EncodeStringList(this.Genres.GetIDs(), Utils.StandardListSeparator)));
            resultNode.Attributes.Append(Utils.GetXmlAttribute(doc, "imdbID", Utils.EncodeSoCalledNullableString(this.IMDbID)));
            resultNode.Attributes.Append(Utils.GetXmlAttribute(doc, "overview", Utils.EncodeSoCalledNullableString(this.Overview)));
            resultNode.Attributes.Append(Utils.GetXmlAttribute(doc, "runtime", Utils.EncodeNullableInteger(this.Runtime)));
            resultNode.Attributes.Append(Utils.GetXmlAttribute(doc, "lastUpdatedOnWebsite",
                Utils.EncodeNullableDateTime(this.LastUpdatedOnWebsite, Utils.StandardDateTimeFormat)));
            resultNode.Attributes.Append(Utils.GetXmlAttribute(doc, "lastUpdatedInDatabase",
                Utils.EncodeNullableDateTime(this.LastUpdatedInDatabase, Utils.StandardDateTimeFormat)));
            resultNode.Attributes.Append(Utils.GetXmlAttribute(doc, "rating", this.Rating.Value));
            resultNode.Attributes.Append(Utils.GetXmlAttribute(doc, "posterFilename",
                this.PosterFilename != null && !File.Exists(Paths.ImagesFolder + this.PosterFilename) ? null : Utils.EncodeSoCalledNullableString(this.PosterFilename)));
            resultNode.Attributes.Append(Utils.GetXmlAttribute(doc, "bannerFilename",
                this.BannerFilename != null && !File.Exists(Paths.ImagesFolder + this.BannerFilename) ? null : Utils.EncodeSoCalledNullableString(this.BannerFilename)));
            XmlNode rolesNode = resultNode.AppendChild(doc.CreateElement("ActingRoles"));
            foreach (ActingRole role in this.ActingRoles)
                rolesNode.AppendChild(role.ToXML(doc, "ActingRole"));
            XmlNode episodesNode = resultNode.AppendChild(doc.CreateElement("Episodes"));
            foreach (Episode episode in this.Episodes)
                episodesNode.AppendChild(episode.ToXML(doc, "Episode"));
            return resultNode;
        }
    }

    /// <summary>
    /// Defines a list of epViews, containing items with unique IDs and offering several related utility methods.
    /// </summary>
    public class SeriesList : BaseList<Series>
    {
        /// <summary>Represents the list of string sorting criteria relevant to the list of this data type.</summary>
        private static readonly string[] sortingCriteria = new string[] { "ID", "Name" };
        /// <summary>Gets the list of string sorting criteria relevant to the list of this data type.</summary>
        public static new string[] SortingCriteria { get { return SeriesList.sortingCriteria; } }

        /// <summary>Constructs a new, empty SeriesList object.</summary>
        public SeriesList()
            : base()
        {
        }

        /// <summary>Sorts the current list by the specified criteria.</summary>
        /// <param name="criteria">the criteria by which to sort the list</param>
        public override void SortBy(string criteria)
        {
            for (int i = 0; i < this.Count - 1; i++)
                for (int j = i + 1; j < this.Count; j++)
                {
                    bool mustSwap = false;
                    switch (criteria)
                    {
                        case "ID":
                            mustSwap = this[i].ID.CompareTo(this[j].ID) > 0;
                            break;
                        case "Name":
                            mustSwap = this[i].Name.CompareTo(this[j].Name) > 0;
                            break;
                    }
                    if (mustSwap)
                    {
                        Series aux = this[i];
                        this[i] = this[j];
                        this[j] = aux;
                    }
                }
        }

        /// <summary>Filters the items of the current list using the specified filter and returns the selected items in a new list.</summary>
        /// <param name="filter">the filter by which to select items</param>
        /// <returns>a new list containing the selected items</returns>
        public SeriesList Filter(SeriesFilter filter)
        {
            SeriesList result = new SeriesList();
            foreach (Series S in this)
            {
                bool activeOK = filter.ActiveSeries == CheckState.Indeterminate ? true : (filter.ActiveSeries == CheckState.Checked) == S.Active;
                bool dowOK = filter.DaysOfTheWeek.IndexOf(S.WeeklyBroadcast.DayOfWeek) != -1;

                if (activeOK && dowOK)
                    result.Add(S);
            }
            result.SortBy(filter.SortingCriteria);
            return result;
        }

        public SeriesList GetSeriesByNetwork(Network network)
        {
            SeriesList result = new SeriesList();
            foreach (Series series in this)
                if (series.Network.Equals(network))
                    result.Add(series);
            return result;
        }

        public SeriesList GetSeriesByLanguage(Language language)
        {
            SeriesList result = new SeriesList();
            foreach (Series series in this)
                if (series.Language.Equals(language))
                    result.Add(series);
            return result;
        }

        public SeriesList GetSeriesByGenre(Genre genre)
        {
            SeriesList result = new SeriesList();
            foreach (Series series in this)
                foreach (Genre iGenre in series.Genres)
                    if (iGenre.Equals(genre))
                        result.Add(series);
            return result;
        }

        public SeriesList GetSeriesByActor(Person actor)
        {
            SeriesList result = new SeriesList();
            foreach (Series series in this)
                foreach (ActingRole actingRole in series.ActingRoles)
                    if (actingRole.Actor.Equals(actor))
                    {
                        result.Add(series);
                        break;
                    }
            return result;
        }

        /// <summary></summary>
        /// <param name="series"></param>
        /// <param name="database"></param>
        public void RemoveSeriesAndDeleteLooseConnexions(Series series, Database database)
        {
            if (this.GetSeriesByNetwork(series.Network).Count <= 1)
                database.Networks.Remove(series.Network);
            /*if (this.GetSeriesByLanguage(series.Language).Count <= 1)
                database.Languages.Remove(series.Language);*/
            foreach (Genre genre in series.Genres)
                if (this.GetSeriesByGenre(genre).Count <= 1)
                    database.Genres.Remove(genre);
            foreach (ActingRole actingRole in series.ActingRoles)
            {
                if (actingRole.ImageFilename != null && File.Exists(Paths.ImagesFolder + actingRole.ImageFilename))
                    File.Delete(Paths.ImagesFolder + actingRole.ImageFilename);
                if (this.GetSeriesByActor(actingRole.Actor).Count <= 1)
                    database.People.Remove(actingRole.Actor);
            }
            if (series.BannerFilename != null && File.Exists(Paths.ImagesFolder + series.BannerFilename))
                File.Delete(Paths.ImagesFolder + series.BannerFilename);
            if (series.PosterFilename != null && File.Exists(Paths.ImagesFolder + series.PosterFilename))
                File.Delete(Paths.ImagesFolder + series.PosterFilename);
            this.Remove(series);
        }
    }
}
