using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using TvDatabase.VisualComponents;

namespace TvDatabase.Classes
{
    /// <summary>
    /// Contains settings values for the application.
    /// </summary>
    public class Settings
    {
        /// <summary>Defines the view modes in which the shows can be displayed on the main form.</summary>
        public enum EShowViewMode { Weekdays = 1, List = 2 };

        /// <summary>Gets or sets the mirror URL to be used to connect to thetvdb.com database</summary>
        public string MirrorURL { get; set; }
        /// <summary>Gets or sets the API key used to connect to thetvdb.com database</summary>
        public string ApiKey { get; set; }
        /// <summary>Gets or sets a value indicating whether the app should download images for epViews when updating the database.</summary>
        public bool DownloadImagesForSeries { get; set; }
        /// <summary>Gets or sets a value indicating whether the app should download images for actors when updating the database.</summary>
        public bool DownloadImagesForActors { get; set; }
        /// <summary>Gets or sets the view mode in which the shows will be displayed on the main form</summary>
        public EShowViewMode ShowViewMode { get; set; }
        /// <summary>Gets or sets the scroll bar position of the container panel of the main form</summary>
        public FancyScrollBar.FancyScrollBarPosition ScrollBarPosition { get; set; }
        /// <summary>Gets or sets the number of epViews boxes which will be displayed on the main form</summary>
        public Size NumberOfSeriesBoxes { get; set; }
        /// <summary>gets or sets a value indicating whether the epViews title will be displayed on the epViews box.</summary>
        public bool ShowSeriesTitle { get; set; }
        /// <summary>gets or sets a value indicating whether the epViews rating will be displayed on the epViews box.</summary>
        public bool ShowSeriesRating { get; set; }
        /// <summary>gets or sets a value indicating whether the epViews last-watched episode information will be displayed on the epViews box.</summary>
        public bool ShowSeriesLWE { get; set; }

        /// <summary>Reads the settings from file.</summary>
        /// <returns>a string representing the error that was encountered if an error did occur, or an empty string otherwise</returns>
        public string ReadFromFile()
        {
            try
            {
                XmlDocument doc = new XmlDocument();
                doc.Load(Paths.SettingsFile);
                XmlNode node = doc.SelectSingleNode("/SETTINGS");

                try { MirrorURL = node["MirrorURL"].Attributes["value"].Value; }
                catch (Exception E) { MirrorURL = @"http://thetvdb.com/"; }

                try { ApiKey = node["ApiKey"].Attributes["value"].Value; }
                catch (Exception E) { ApiKey = "DF6BA15D91EEF8B7"; }

                try { DownloadImagesForSeries = Boolean.Parse(node["DownloadImagesForSeries"].Attributes["value"].Value); }
                catch (Exception E) { DownloadImagesForSeries = true; }

                try { DownloadImagesForActors = Boolean.Parse(node["DownloadImagesForActors"].Attributes["value"].Value); }
                catch (Exception E) { DownloadImagesForActors = true; }

                try { ShowViewMode = (EShowViewMode) Enum.Parse(typeof(EShowViewMode), node["ShowViewMode"].Attributes["value"].Value); }
                catch (Exception E) { ShowViewMode = EShowViewMode.List; }

                try { ScrollBarPosition = (FancyScrollBar.FancyScrollBarPosition) Enum.Parse(typeof(FancyScrollBar.FancyScrollBarPosition), node["ScrollBarPosition"].Attributes["value"].Value); }
                catch (Exception E) { ScrollBarPosition = FancyScrollBar.FancyScrollBarPosition.Bottom; }

                try
                {
                    int r = Int32.Parse(node["NumberOfSeriesBoxes"].Attributes["rows"].Value), c = Int32.Parse(node["NumberOfSeriesBoxes"].Attributes["columns"].Value);
                    this.NumberOfSeriesBoxes = new Size(c, r);
                }
                catch (Exception E) { NumberOfSeriesBoxes = new Size(5, 2); }

                try { ShowSeriesTitle = Boolean.Parse(node["ShowSeriesTitle"].Attributes["value"].Value); }
                catch (Exception E) { ShowSeriesTitle = true; }

                try { ShowSeriesRating = Boolean.Parse(node["ShowSeriesRating"].Attributes["value"].Value); }
                catch (Exception E) { ShowSeriesRating = true; }

                try { ShowSeriesLWE = Boolean.Parse(node["ShowSeriesLWE"].Attributes["value"].Value); }
                catch (Exception E) { ShowSeriesLWE = true; }

                return "";
            }
            catch (Exception E)
            {
                return E.Message;
            }
        }

        /// <summary>Saves the settings to file.</summary>
        /// <returns>a string representing the error that was encountered if an error did occur, or an empty string otherwise</returns>
        public string SaveToFile()
        {
            try
            {
                XmlDocument doc = new XmlDocument();
                XmlNode root, node;

                root = doc.AppendChild(doc.CreateElement("SETTINGS"));
                root.Attributes.Append(Utils.GetXmlAttribute(doc, "lastSavedCirca", DateTime.Now.ToString("ddd, d MMMM yyyy, HH:mm")));

                node = root.AppendChild(doc.CreateElement("MirrorURL"));
                node.Attributes.Append(Utils.GetXmlAttribute(doc, "value", MirrorURL));

                node = root.AppendChild(doc.CreateElement("ApiKey"));
                node.Attributes.Append(Utils.GetXmlAttribute(doc, "value", ApiKey));

                node = root.AppendChild(doc.CreateElement("DownloadImagesForSeries"));
                node.Attributes.Append(Utils.GetXmlAttribute(doc, "value", DownloadImagesForSeries.ToString()));

                node = root.AppendChild(doc.CreateElement("DownloadImagesForActors"));
                node.Attributes.Append(Utils.GetXmlAttribute(doc, "value", DownloadImagesForActors.ToString()));

                node = root.AppendChild(doc.CreateElement("ShowViewMode"));
                node.Attributes.Append(Utils.GetXmlAttribute(doc, "value", ShowViewMode.ToString()));

                node = root.AppendChild(doc.CreateElement("ScrollBarPosition"));
                node.Attributes.Append(Utils.GetXmlAttribute(doc, "value", ScrollBarPosition.ToString()));

                node = root.AppendChild(doc.CreateElement("NumberOfSeriesBoxes"));
                node.Attributes.Append(Utils.GetXmlAttribute(doc, "rows", NumberOfSeriesBoxes.Height));
                node.Attributes.Append(Utils.GetXmlAttribute(doc, "columns", NumberOfSeriesBoxes.Width));

                node = root.AppendChild(doc.CreateElement("ShowSeriesTitle"));
                node.Attributes.Append(Utils.GetXmlAttribute(doc, "value", ShowSeriesTitle.ToString()));

                node = root.AppendChild(doc.CreateElement("ShowSeriesRating"));
                node.Attributes.Append(Utils.GetXmlAttribute(doc, "value", ShowSeriesRating.ToString()));

                node = root.AppendChild(doc.CreateElement("ShowSeriesLWE"));
                node.Attributes.Append(Utils.GetXmlAttribute(doc, "value", ShowSeriesLWE.ToString()));

                doc.Save(Paths.SettingsFile);
                return "";
            }
            catch (Exception E)
            {
                return E.Message;
            }
        }
    }
}
