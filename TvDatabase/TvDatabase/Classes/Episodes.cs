using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;

namespace TvDatabase.Classes
{
    /// <summary>
    /// Defines an episode class, containing relevant information.
    /// </summary>
    public class Episode : ObjectWithName
    {
        /// <summary>Gets the season number of the episode</summary>
        public int SeasonNumber { get; internal set; }
        /// <summary>Gets the episode number of the episode</summary>
        public int EpisodeNumber { get; internal set; }
        /// <summary>Gets the overview description of the episode, or null if non-existant</summary>
        public string Overview { get; internal set; }
        /// <summary>Gets the date when the episode was first broadcast, or null if non-existant</summary>
        public DateTime? FirstBroadcasted { get; internal set; }
        /// <summary>Gets the date when the episode was first watched by the user, or null if non-existant</summary>
        public DateTime? LastWatched { get; set; }

        /// <summary>Constructs a new episode object from the given parameters.</summary>
        /// <param name="id">the ID of the object</param>
        /// <param name="name">the name of the object</param>
        /// <param name="season">the season number of the episode</param>
        /// <param name="episode">the episode number of the episode</param>
        /// <param name="overview">the overview description of the episode</param>
        /// <param name="firstBroadcasted">the date when the episode was first broadcast</param>
        /// <param name="lastWatched">the date when the episode was first watched by the user</param>
        public Episode(string id, string name, int season, int episode, string overview, DateTime? firstBroadcasted, DateTime? lastWatched)
            : base(id, name)
        {
            this.SeasonNumber = season;
            this.EpisodeNumber = episode;
            this.Overview = overview;
            this.FirstBroadcasted = firstBroadcasted;
            this.LastWatched = lastWatched;
        }

        /// <summary>Formats the episode information in the specified manner.</summary>
        /// <param name="format">the format string to format to</param>
        /// <returns>a string containing the required information</returns>
        public string FormatEpisode(string format)
        {
            StringBuilder sb = new StringBuilder(format);
            sb = sb.Replace("%e", this.EpisodeNumber.ToString()).Replace("%E", this.EpisodeNumber.ToString("D2"));
            sb = sb.Replace("%s", this.SeasonNumber.ToString()).Replace("%S", this.SeasonNumber.ToString("D2"));
            return sb.ToString();
        }

        /// <summary>Generates an XmlNode object containing the current object's information.</summary>
        /// <param name="doc">the XmlDocument object from which the node is to be created</param>
        /// <param name="nodeName">the name of the node</param>
        /// <returns>an XmlNode object</returns>
        public new XmlNode ToXML(XmlDocument doc, string nodeName)
        {
            XmlNode resultNode = base.ToXML(doc, nodeName);
            resultNode.Attributes.Append(Utils.GetXmlAttribute(doc, "seasonNumber", this.SeasonNumber));
            resultNode.Attributes.Append(Utils.GetXmlAttribute(doc, "episodeNumber", this.EpisodeNumber));
            resultNode.Attributes.Append(Utils.GetXmlAttribute(doc, "overview", Utils.EncodeSoCalledNullableString(this.Overview)));
            resultNode.Attributes.Append(Utils.GetXmlAttribute(doc, "firstBroadcasted",
                Utils.EncodeNullableDateTime(this.FirstBroadcasted, Utils.StandardDateFormat)));
            resultNode.Attributes.Append(Utils.GetXmlAttribute(doc, "lastWatched",
                Utils.EncodeNullableDateTime(this.LastWatched, Utils.StandardDateTimeFormat)));
            return resultNode;
        }
    }

    /// <summary>
    /// Defines a list of episodes, containing items with unique IDs and offering several related utility methods.
    /// </summary>
    public class EpisodeList : BaseList<Episode>
    {
        /// <summary>Represents the list of string sorting criteria relevant to the list of this data type.</summary>
        private static readonly string[] sortingCriteria = new string[] { "ID", "Name", "Season number", "Episode number", "Overview", "First broadcasted", "Last watched" };
        /// <summary>Gets the list of string sorting criteria relevant to the list of this data type.</summary>
        public static new string[] SortingCriteria { get { return EpisodeList.sortingCriteria; } }

        /// <summary>Constructs a new, empty EpisodeList object.</summary>
        public EpisodeList()
            : base()
        {
        }

        /// <summary>Generates a list of unique season numbers in this EpisodeList.</summary>
        /// <returns>an integer list</returns>
        public List<int> GetSeasons()
        {
            List<int> seasons = new List<int>();
            foreach (Episode episode in this)
                if (!seasons.Contains(episode.SeasonNumber))
                    seasons.Add(episode.SeasonNumber);
            return seasons;
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
                        Episode aux = this[i];
                        this[i] = this[j];
                        this[j] = aux;
                    }
                }
        }
    }
}
