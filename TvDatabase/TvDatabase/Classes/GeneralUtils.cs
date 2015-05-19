using System;
using System.Collections;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Net;
using System.Text;
using System.Xml;

namespace TvDatabase.Classes
{
    /// <summary>
    /// Offers utility methods to be used throughout the application.
    /// </summary>
    public static class Utils
    {
        #region constants
        public const string NullString = "null";
        public const string StandardListSeparator = ";";
        public const string StandardDateTimeFormat = "yyyy-MM-dd HH:mm";
        public const string StandardDateFormat = "yyyy-MM-dd";
        public const string StandardTimeFormat = "HH:mm";
        public const string TheTvDbTimeFormat = "h:mm tt";
        public enum DurationFormatting { TotalSeconds, MinutesAndSeconds };
        #endregion

        #region encoders, decoders and formatters
        public static XmlAttribute GetXmlAttribute(XmlDocument doc, string name, string value)
        {
            XmlAttribute attr = doc.CreateAttribute(name);
            attr.Value = value;
            return attr;
        }

        public static XmlAttribute GetXmlAttribute(XmlDocument doc, string name, long value)
        {
            return GetXmlAttribute(doc, name, value.ToString());
        }

        public static string FormatNumber(long number)
        {
            return number.ToString("#,##0");
        }

        public static string FormatNumber(double number)
        {
            return number.ToString("#,##0.00");
        }

        public static string FormatNumberOrder(long number)
        {
            if (number == 0)
                return "0";
            number = Math.Abs(number);
            char[] ordChr = { ' ', 'K', 'M', 'B' };
            int powOrd = 3;
            while (powOrd >= 0)
            {
                long x = (long) Math.Pow(1000, powOrd);
                if (number >= x)
                    return ((double) number / x).ToString("n" + powOrd) + ordChr[powOrd];
                powOrd--;
            }
            return "???";
        }

        public static string EncodeNullableInteger(int? value)
        {
            return value == null ? NullString : value.ToString();
        }

        public static int? DecodeNullableInteger(string stringValue)
        {
            int value;
            if (stringValue == null || stringValue.Equals(NullString) || !Int32.TryParse(stringValue, out value))
                return null;
            return value;
        }

        public static string EncodeSoCalledNullableString(string value)
        {
            return value == null ? NullString : value.ToString();
        }

        public static string DecodeSoCalledNullableString(string stringValue)
        {
            if (stringValue == null || stringValue.Equals(NullString))
                return null;
            return stringValue;
        }

        public static string FormatDateTime(DateTime date, string format)
        {
            return date.ToString(format, CultureInfo.InvariantCulture);
        }

        public static string EncodeDateTime(DateTime date, string format)
        {
            return FormatDateTime(date, format);
        }

        public static DateTime DecodeDateTime(string stringValue, string format)
        {
            try { return DateTime.ParseExact(stringValue, format, CultureInfo.InvariantCulture); }
            catch (Exception E) { return new DateTime(2000, 1, 1, 1, 1, 1, 1); }
        }

        public static string EncodeNullableDateTime(DateTime? date, string format)
        {
            return date == null ? NullString : EncodeDateTime((DateTime) date, format);
        }

        public static DateTime? DecodeNullableDateTime(string stringValue, string format)
        {
            if (stringValue == null || stringValue.Equals(NullString))
                return null;
            return DecodeDateTime(stringValue, format);
        }

        public static string EncodeNullableBoolean(bool? boolean)
        {
            return boolean == null ? NullString : ((bool) boolean).ToString();
        }

        public static bool? DecodeNullableBoolean(string stringValue)
        {
            if (stringValue == null || stringValue.Equals(NullString))
                return null;
            return Boolean.Parse(stringValue);
        }

        public static string EncodeStringList(IEnumerable list, string separator)
        {
            StringBuilder sb = new StringBuilder();
            foreach (object item in list)
                sb.Append(item as string).Append(separator);
            return sb.Length > 0 ? sb.ToString().Substring(0, sb.Length - separator.Length) : "";
        }

        public static string[] DecodeStringList(string stringValue, string separator)
        {
            stringValue = stringValue.Trim();
            return stringValue.Length > 0 ? stringValue.Split(separator.ToCharArray()) : null;
        }

        public static DateTime UnixTimeStampToDateTime(double unixTimeStamp)
        {
            DateTime result = new DateTime(1970, 1, 1, 0, 0, 0, 0);
            result = result.AddSeconds(unixTimeStamp);
            return result;
        }

        public static string FormatDuration(TimeSpan when, DurationFormatting formatting)
        {
            switch (formatting)
            {
                case DurationFormatting.MinutesAndSeconds:
                    return string.Format("{0}:{1}", (int) when.TotalMinutes, when.Seconds.ToString("D2"));
                case DurationFormatting.TotalSeconds:
                default:
                    return ((int) when.TotalSeconds).ToString();
            }
        }
        #endregion

        #region general utility methods
        /// <summary>Downloads a file from the internet and returns a text containing the error message if encountered, or an empty string otherwise.</summary>
        /// <param name="url">the full address to download from</param>
        /// <param name="destFile">the local path where to save to file</param>
        /// <returns>a text containing the error message if encountered, or an empty string otherwise</returns>
        public static string DownloadFromTheHolyInternet(string url, string destFile)
        {
            try
            {
                new WebClient().DownloadFile(url, destFile);
                return "";
            }
            catch (Exception E)
            { return E.ToString(); }
        }

        public static double DurationRatio(TimeSpan duration, TimeSpan total)
        {
            return duration.Ticks / (double) total.Ticks;
        }

        public static string RegularPlural(string singularForm, long quantity, bool includeQuantity)
        {
            string result = includeQuantity ? FormatNumber(quantity) + " " + singularForm : singularForm;
            return quantity == 1 ? result : result + "s";
        }

        public static Size ScaleRectangle(int width, int height, int maxWidth, int maxHeight)
        {
            var ratioX = (double) maxWidth / width;
            var ratioY = (double) maxHeight / height;
            var ratio = Math.Min(ratioX, ratioY);

            var newWidth = (int) (width * ratio);
            var newHeight = (int) (height * ratio);

            return new Size(newWidth, newHeight);
        }

        public static Image ScaleImage(Image image, Size maxSize, bool disposeOldImage)
        {
            return ScaleImage(image, maxSize.Width, maxSize.Height, disposeOldImage);
        }

        public static Image ScaleImage(Image image, int maxWidth, int maxHeight, bool disposeOldImage)
        {
            Size newSize = ScaleRectangle(image.Width, image.Height, maxWidth, maxHeight);
            Image newImage = new Bitmap(newSize.Width, newSize.Height);
            Graphics.FromImage(newImage).DrawImage(image, 0, 0, newSize.Width, newSize.Height);
            if (disposeOldImage)
                image.Dispose();
            return newImage;
        }

        /// <summary>Loads and scales an image from a filepath. Disposes (pre-scale version of) loaded file if successful, 
        /// otherwise returns default value untouched and unchecked.</summary>
        /// <param name="imagePath">the full path to the image to load and scale</param>
        /// <param name="maxSize">the size for the loaded image to fit in</param>
        /// <param name="defaultImg">the image reference that should be returned if the file to be loaded does not exist or any other error occurs</param>
        public static Image GetScaledImage(string imagePath, Size maxSize, Image defaultImg)
        {
            try { return File.Exists(imagePath) ? ScaleImage(new Bitmap(imagePath), maxSize.Width, maxSize.Height, true) : defaultImg; }
            catch (Exception E) { return defaultImg; }
        }
        #endregion

        #region extension methods
        /// <summary>An extension method that implements a comparison function between two nullable integers.</summary>
        /// <param name="otherValue">the nullable integer to compare to</param>
        /// <returns>-1 if this value is smaller, 0 if they equate (null or in value), 1 if this value is larger</returns>
        public static int CompareTo(this int? value, int? otherValue)
        {
            if (value == null)
                return otherValue == null ? 0 : -1;
            return otherValue == null ? 1 : (int) value - (int) otherValue;
        }

        /// <summary>An extension method that implements a comparison function between two nullable DateTime values.</summary>
        /// <param name="otherValue">the nullable DateTime to compare to</param>
        /// <returns>-1 if this value is smaller, 0 if they equate (null or in value), 1 if this value is larger</returns>
        public static int CompareTimeTo(this DateTime? value, DateTime? otherValue)
        {
            if (value == null)
                return otherValue == null ? 0 : -1;
            return otherValue == null ? 1 : ((DateTime) value).TimeOfDay.CompareTo(((DateTime) otherValue).TimeOfDay);
        }

        /// <summary>Prepends a text to a StringBuilder object. Equivalent to ".Insert(0, text)".</summary>
        /// <param name="text">the text to append</param>
        /// <returns>the resulting StringBuilder instance</returns>
        public static StringBuilder Prepend(this StringBuilder sb, string text)
        {
            return sb.Insert(0, text);
        }
        #endregion
    }

    /// <summary>
    /// Contains the centralized paths of folders and files used by the application.
    /// </summary>
    public static class Paths
    {
        public const string ProgramFilesFolder = @"..\..\..\program-files\";
        public const string GraphicsFolder = ProgramFilesFolder + @"graphics\";
        public const string ImagesFolder = ProgramFilesFolder + @"images\";
        public const string ExportsFolder = ProgramFilesFolder + @"exports\";
        public const string HelpFolder = ProgramFilesFolder + @"help\";
        public const string TemporaryStorageFolder = ProgramFilesFolder + @"temp\";

        public const string DatabaseFile = ProgramFilesFolder + @"database.xml";
        public const string SettingsFile = ProgramFilesFolder + @"settings.xml";

        internal const string StarImageFile = GraphicsFolder + @"star.png";
        internal const string UnknownSeriesPosterImageFile = GraphicsFolder + @"unknownSeriesPoster.png";
        internal const string UnknownSeriesBannerImageFile = GraphicsFolder + @"unknownSeriesBanner.png";
        internal const string UnknownActingRoleImageFile = GraphicsFolder + @"unknownActingRole.png";

        public static Image Star { get; set; }
        public static Image UnknownSeriesPosterImage { get; set; }
        public static Image UnknownSeriesBannerImage { get; set; }
        public static Image UnknownActingRoleImage { get; set; }

        private static readonly string[] FoldersToCheck = { ProgramFilesFolder, GraphicsFolder, ImagesFolder, ExportsFolder, HelpFolder, TemporaryStorageFolder };
        private static readonly string[] FilesToCheck = { DatabaseFile, SettingsFile, StarImageFile, UnknownSeriesPosterImageFile, UnknownSeriesBannerImageFile, UnknownActingRoleImageFile };

        /// <summary>Checks the drive for whether the folders and files used by the application exist.</summary>
        /// <param name="tryToCreateMissingFoldersOnce">whether to try once to create the folders that do not exist</param>
        /// <returns>a text message describing the problem if one did arise, or an empty string otherwise</returns>
        public static string CheckFoldersAndFiles(bool tryToCreateMissingFoldersOnce)
        {
            foreach (string folder in FoldersToCheck)
            {
                string checkResult = CheckFolder(folder, tryToCreateMissingFoldersOnce);
                if (!checkResult.Equals(""))
                    return checkResult;
            }
            foreach (string file in FilesToCheck)
            {
                string checkResult = CheckFile(file);
                if (!checkResult.Equals(""))
                    return checkResult;
            }
            return "";
        }

        private static string CheckFolder(string folder, bool tryToCreateMissingFolderOnce)
        {
            try
            {
                if (Directory.Exists(folder))
                    return "";
                if (tryToCreateMissingFolderOnce)
                {
                    Directory.CreateDirectory(folder);
                    return Directory.Exists(folder) ? "" : "The folder \"" + folder + "\" does not exist and could not be created!";
                }
                else
                    return "The folder \"" + folder + "\" does not exist!";
            }
            catch (Exception E)
            { return E.ToString(); }
        }

        private static string CheckFile(string file)
        {
            try
            {
                if (File.Exists(file))
                    return "";
                return "The file \"" + file + "\" does not exist!";
            }
            catch (Exception E)
            { return E.ToString(); }
        }

        /// <summary>Attempts to load the static images used throughout the application.</summary>
        /// <returns>a text message describing the problem if one did arise, or an empty string otherwise</returns>
        public static string LoadStaticImages()
        {
            try
            {
                Star = new Bitmap(StarImageFile);
                UnknownSeriesPosterImage = new Bitmap(UnknownSeriesPosterImageFile);
                UnknownSeriesBannerImage = new Bitmap(UnknownSeriesBannerImageFile);
                UnknownActingRoleImage = new Bitmap(UnknownActingRoleImageFile);
                return "";
            }
            catch (Exception E)
            { return E.ToString(); }
        }

        /// <summary>Generates a unique image filename.</summary>
        /// <param name="idLength">the length of the unique ID</param>
        /// <returns>a string containing the full path of the file</returns>
        public static string GetUniqueImagePath(int idLength)
        {
            StringBuilder sb = new StringBuilder();
            Random random = new Random();
            do
            {
                sb.Clear();
                for (int i = 0; i < idLength; i++)
                    sb.Append((char) random.Next(65, 91));
            }
            while (File.Exists(sb.Prepend(Paths.ImagesFolder).Append(".jpg").ToString()));
            return sb.ToString();
        }
    }
}
