using System;
using System.IO;
using System.Windows.Forms;
using System.Xml;

namespace TvDatabase.Classes
{
    /// <summary>
    /// Defines a database structure, containing all information related to the epViews selected by the user.
    /// </summary>
    public class Database
    {
        /// <summary>Static definition of an unknown language.</summary>
        public static readonly Language UnknownLanguage = new Language("UNK", "Unknown language");
        /// <summary>Static definition of an unknown network.</summary>
        public static readonly Network UnknownNetwork = new Network("UNK", "Unknown network");
        /// <summary>Static definition of an unknown genre.</summary>
        public static readonly Genre UnknownGenre = new Genre("UNK", "Unknown genre");

        /// <summary>Gets the full list of languages of the database.</summary>
        public LanguageList Languages { get; private set; }
        /// <summary>Gets the full list of networks of the database.</summary>
        public NetworkList Networks { get; private set; }
        /// <summary>Gets the full list of genres of the database.</summary>
        public GenreList Genres { get; private set; }
        /// <summary>Gets the full list of people of the database.</summary>
        public PersonList People { get; private set; }
        /// <summary>Gets the full list of epViews of the database.</summary>
        public SeriesList Series { get; private set; }
        /// <summary>Gets the settings object of the database.</summary>
        public Settings Settings { get; private set; }

        /// <summary>Constructs a new Database object, containing only the static "unknown [...]" members of class Database.</summary>
        public Database()
        {
            this.Languages = new LanguageList();
            this.Languages.Add(Database.UnknownLanguage);
            this.Networks = new NetworkList();
            this.Networks.Add(Database.UnknownNetwork);
            this.Genres = new GenreList();
            this.Genres.Add(Database.UnknownGenre);
            this.People = new PersonList();
            this.Series = new SeriesList();
            this.Settings = new Settings();
        }
        
        /// <summary>Reads the database from the default file.</summary>
        /// <returns>a string representing the error that was encountered if an error did occur, or an empty string otherwise</returns>
        public string OpenDatabase()
        {
            return this.OpenDatabase(Paths.DatabaseFile);
        }

        /// <summary>Reads the database from the specified file.</summary>
        /// <returns>a string representing the error that was encountered if an error did occur, or an empty string otherwise</returns>
        public string OpenDatabase(string openFilePath)
        {
            string phase = "initializing";
            try
            {
                XmlDocument doc = new XmlDocument();
                doc.Load(openFilePath);

                phase = "decoding languages";
                XmlNodeList nodes = doc.SelectSingleNode("Database").SelectSingleNode("Languages").SelectNodes("Language");
                //this.Languages.Clear();
                foreach (XmlNode node in nodes)
                    this.Languages.Add(new Language(node.Attributes["ID"].Value, node.Attributes["name"].Value));

                phase = "decoding networks";
                nodes = doc.SelectSingleNode("Database").SelectSingleNode("Networks").SelectNodes("Network");
                //this.Networks.Clear();
                foreach (XmlNode node in nodes)
                    this.Networks.Add(new Network(node.Attributes["ID"].Value, node.Attributes["name"].Value));

                phase = "decoding genres";
                nodes = doc.SelectSingleNode("Database").SelectSingleNode("Genres").SelectNodes("Genre");
                //this.Genres.Clear();
                foreach (XmlNode node in nodes)
                    this.Genres.Add(new Genre(node.Attributes["ID"].Value, node.Attributes["name"].Value));

                phase = "decoding people";
                nodes = doc.SelectSingleNode("Database").SelectSingleNode("People").SelectNodes("Person");
                //this.People.Clear();
                foreach (XmlNode node in nodes)
                    this.People.Add(new Person(node.Attributes["ID"].Value, node.Attributes["name"].Value));

                phase = "decoding series";
                nodes = doc.SelectSingleNode("Database").SelectSingleNode("Series").SelectNodes("Series");
                //this.Series.Clear();
                foreach (XmlNode node in nodes)
                {
                    string id = node.Attributes["ID"].Value;
                    string name = node.Attributes["name"].Value;
                    bool active = Boolean.Parse(node.Attributes["active"].Value);
                    Network network = this.Networks.GetByID(node.Attributes["networkID"].Value);
                    bool? continuing = Utils.DecodeNullableBoolean(node.Attributes["continuing"].Value);
                    Language language = this.Languages.GetByID(node.Attributes["languageID"].Value);
                    string imdbID = Utils.DecodeSoCalledNullableString(node.Attributes["imdbID"].Value);
                    string overview = Utils.DecodeSoCalledNullableString(node.Attributes["overview"].Value);
                    int? runtime = Utils.DecodeNullableInteger(node.Attributes["runtime"].Value);
                    DateTime? updateWebsite = Utils.DecodeNullableDateTime(node.Attributes["lastUpdatedOnWebsite"].Value, Utils.StandardDateTimeFormat);
                    DateTime? updateDatabase = Utils.DecodeNullableDateTime(node.Attributes["lastUpdatedInDatabase"].Value, Utils.StandardDateTimeFormat);
                    int? ratingValue = Utils.DecodeNullableInteger(node.Attributes["rating"].Value);
                    Rating rating = ratingValue == null ? new Rating() : new Rating((int) ratingValue);
                    string posterFilename = Utils.DecodeSoCalledNullableString(node.Attributes["posterFilename"].Value);
                    posterFilename = posterFilename == null || !File.Exists(Paths.ImagesFolder + posterFilename) ? null : posterFilename;
                    string bannerFilename = Utils.DecodeSoCalledNullableString(node.Attributes["bannerFilename"].Value);
                    bannerFilename = bannerFilename == null || !File.Exists(Paths.ImagesFolder + bannerFilename) ? null : bannerFilename;

                    XmlNode subNode = node.SelectSingleNode("WeeklyBroadcast");
                    WeeklyBroadcast weeklyBroadcast = new WeeklyBroadcast(Utils.DecodeNullableInteger(subNode.Attributes["dayOfWeek"].Value),
                        Utils.DecodeNullableDateTime(subNode.Attributes["time"].Value, Utils.StandardTimeFormat));

                    subNode = node.SelectSingleNode("Genres");
                    GenreList genres = new GenreList();
                    string[] genreIDs = Utils.DecodeStringList(subNode.Attributes["IDs"].Value, Utils.StandardListSeparator);
                    if (genreIDs == null)
                        genreIDs = new string[] { "UNK" };
                    foreach (string genreID in genreIDs)
                        genres.Add(this.Genres.GetByID(genreID));

                    XmlNodeList subNodes = node.SelectSingleNode("ActingRoles").SelectNodes("ActingRole");
                    ActingRoleList actingRoles = new ActingRoleList();
                    foreach (XmlNode actingRoleNode in subNodes)
                    {
                        string actingRoleID = actingRoleNode.Attributes["ID"].Value;
                        string roleName = actingRoleNode.Attributes["name"].Value;
                        Person actor = this.People.GetByID(actingRoleNode.Attributes["actorID"].Value);
                        int sortOrder = Int32.Parse(actingRoleNode.Attributes["sortOrder"].Value);
                        string imageFilename = Utils.DecodeSoCalledNullableString(actingRoleNode.Attributes["imageFilename"].Value);
                        actingRoles.Add(new ActingRole(actingRoleID, roleName, actor, sortOrder, imageFilename));
                    }

                    subNodes = node.SelectSingleNode("Episodes").SelectNodes("Episode");
                    EpisodeList episodes = new EpisodeList();
                    foreach (XmlNode episodeNode in subNodes)
                    {
                        string episodeID = episodeNode.Attributes["ID"].Value;
                        string episodeName = episodeNode.Attributes["name"].Value;
                        int seasonNumber = Int32.Parse(episodeNode.Attributes["seasonNumber"].Value);
                        int episodeNumber = Int32.Parse(episodeNode.Attributes["episodeNumber"].Value);
                        string episodeOverview = Utils.DecodeSoCalledNullableString(episodeNode.Attributes["overview"].Value);
                        DateTime? firstBroadcasted = Utils.DecodeNullableDateTime(episodeNode.Attributes["firstBroadcasted"].Value, Utils.StandardDateFormat);
                        DateTime? lastWatched = Utils.DecodeNullableDateTime(episodeNode.Attributes["lastWatched"].Value, Utils.StandardDateTimeFormat);
                        episodes.Add(new Episode(episodeID, episodeName, seasonNumber, episodeNumber, episodeOverview, firstBroadcasted, lastWatched));
                    }

                    this.Series.Add(new Series(id, name, active, network, weeklyBroadcast, continuing, language, genres, imdbID, overview,
                        runtime, updateWebsite, updateDatabase, rating, posterFilename, bannerFilename, actingRoles, episodes));
                }

                phase = "reading settings";
                string settingsReadResult = this.Settings.ReadFromFile();
                if (!settingsReadResult.Equals(""))
                    throw new ApplicationException(settingsReadResult);

                return "";
            }
            catch (ApplicationException appE)
            { return "Preventable ERROR in Database.OpenDatabase(), in phase '" + phase + "';\n\n" + appE.ToString(); }
            catch (Exception e)
            { return "Unexpected ERROR in Database.OpenDatabase(), in phase '" + phase + "';\n\n" + e.ToString(); }
        }
        
        /// <summary>Saves the database to the default file.</summary>
        /// <returns>a string representing the error that was encountered if an error did occur, or an empty string otherwise</returns>
        public string SaveDatabase()
        {
            return this.SaveDatabase(Paths.DatabaseFile);
        }

        /// <summary>Saves the database to the specified file.</summary>
        /// <returns>a string representing the error that was encountered if an error did occur, or an empty string otherwise</returns>
        public string SaveDatabase(string saveFilePath)
        {
            string phase = "initializing";
            try
            {
                XmlDocument doc = new XmlDocument();
                XmlNode root = doc.AppendChild(doc.CreateElement("Database"));
                root.Attributes.Append(Utils.GetXmlAttribute(doc, "lastSaved", DateTime.Now.ToString("ddd, d MMMM yyyy, HH:mm")));

                phase = "saving languages";
                XmlNode node = root.AppendChild(doc.CreateElement("Languages"));
                foreach (Language language in this.Languages)
                    node.AppendChild(language.ToXML(doc, "Language"));

                phase = "saving networks";
                node = root.AppendChild(doc.CreateElement("Networks"));
                foreach (Network network in this.Networks)
                    node.AppendChild(network.ToXML(doc, "Network"));

                phase = "saving genres";
                node = root.AppendChild(doc.CreateElement("Genres"));
                foreach (Genre genre in this.Genres)
                    node.AppendChild(genre.ToXML(doc, "Genre"));

                phase = "saving people";
                node = root.AppendChild(doc.CreateElement("People"));
                foreach (Person person in this.People)
                    node.AppendChild(person.ToXML(doc, "Person"));

                phase = "saving series";
                node = root.AppendChild(doc.CreateElement("Series"));
                foreach (Series series in this.Series)
                    node.AppendChild(series.ToXML(doc, "Series"));

                phase = "saving to file and exiting";
                doc.Save(saveFilePath);

                return "";
            }
            catch (Exception e)
            { return "Unexpected ERROR in Database.SaveDatabase(), in phase '" + phase + "';\n\n" + e.ToString(); }
        }
    }
}
