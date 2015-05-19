using System;
using System.Xml;

namespace TvDatabase.Classes
{
    /// <summary>
    /// Defines a day of the week and hour of the day for when a epViews is broadcasted.
    /// </summary>
    public class WeeklyBroadcast : IGeneratesXmlNode, IComparable
    {
        /// <summary>An array of string representing the days of the week (0-based, with index 0 for unknown, and 1-7 for the days of the week), to be used with the WeeklyBroadcast class.</summary>
        public static readonly string[] DaysOfTheWeek = new string[] { "Unknown", "Monday", "Tuesday", "Wednesday", "Thursday", "Friday", "Saturday", "Sunday" };

        /// <summary>Gets the day of the week when the epViews is broadcasted, or null if unknown.</summary>
        public int? DayOfWeek { get; internal set; }
        /// <summary>Gets the hour of the day when the epViews is broadcasted, or null if unknown.</summary>
        public DateTime? Time { get; internal set; }

        /// <summary>Constructs a new WeeklyBroadcast object from the given values.</summary>
        /// <param name="dayOfWeek">the day of the week when the epViews is broadcasted</param>
        /// <param name="time">the hour of the day when the epViews is broadcasted</param>
        public WeeklyBroadcast(int? dayOfWeek, DateTime? time)
        {
            this.DayOfWeek = dayOfWeek;
            this.Time = time;
        }

        /// <summary>Compares thuis instance of WeeklyBroadcast with another, returning -1, 0 or 1.</summary>
        /// <param name="obj">the WeeklyBroadcast object to compare to</param>
        /// <returns>-1 if smaller, 0 if equal, or 1 if larger</returns>
        public int CompareTo(object obj)
        {
            WeeklyBroadcast wb = obj as WeeklyBroadcast;
            int dCT = this.DayOfWeek.CompareTo(wb.DayOfWeek);
            return dCT != 0 ? dCT : this.Time.CompareTimeTo(wb.Time);
        }

        /// <summary>Formats thr current WeeklyBroadcast information to be readable.</summary>
        /// <returns>a readable, formatted string</returns>
        public string Format()
        {
            if (this.DayOfWeek != null)
                return this.Time != null
                    ? string.Format("{0}s @ {1}", WeeklyBroadcast.DaysOfTheWeek[(int) this.DayOfWeek], Utils.FormatDateTime((DateTime) this.Time, Utils.StandardTimeFormat))
                    : WeeklyBroadcast.DaysOfTheWeek[(int) this.DayOfWeek] + "s";
            return this.Time != null 
                ? Utils.FormatDateTime((DateTime) this.Time, Utils.StandardTimeFormat) 
                : "unknown";
        }

        /// <summary>Generates an XmlNode object containing the current object's information.</summary>
        /// <param name="doc">the XmlDocument object from which the node is to be created</param>
        /// <param name="nodeName">the name of the node</param>
        /// <returns>an XmlNode object</returns>
        public XmlNode ToXML(XmlDocument doc, string nodeName)
        {
            XmlNode resultNode = doc.CreateElement(nodeName);
            resultNode.Attributes.Append(Utils.GetXmlAttribute(doc, "dayOfWeek", Utils.EncodeNullableInteger(this.DayOfWeek)));
            resultNode.Attributes.Append(Utils.GetXmlAttribute(doc, "time", Utils.EncodeNullableDateTime(this.Time, Utils.StandardTimeFormat)));
            return resultNode;
        }
    }

    /// <summary>
    /// Defines a simple rating system for an entity that may be rated by the user on a scale from 1 to 10.
    /// </summary>
    public class Rating
    {
        /// <summary>Offers text descriptions of the permitted rating values.</summary>
        public static readonly string[] Descriptions = new string[] { "unknown description", "rating 1", "rating 2", "rating 3", "rating 4", "rating 5", "rating 6", "rating 7", "rating 8", "rating 9", "rating 10" };
        /// <summary>Defines the constant value (0) of an unknown rating</summary>
        public const int UnknownRating = 0;
        /// <summary>Defines the constant value (1) of the lowest rating</summary>
        public const int LowestRating = 1;
        /// <summary>Defines the constant value (10) of the highest rating</summary>
        public const int HighestRating = 10;

        /// <summary>Contains the actual rating value</summary>
        private int value;

        /// <summary>Creates a new Rating object with an unknown rating value.</summary>
        public Rating()
            : this(0)
        {
        }

        /// <summary>Creates a new Rating object with the specified rating value.</summary>
        /// <param name="value">the rating value (will be constrained to a 0-10 range)</param>
        public Rating(int value)
        {
            this.Value = value;
        }

        /// <summary>Gets or sets the rating value of the current Rating object.
        /// Set to 0 for unknown, or from 1-10 for a known value.
        /// Any value not between 0-10 is set to the closest value numerically.</summary>
        public int Value
        {
            get { return this.value; }
            set { this.value = value < 0 ? 0 : (value > 10 ? 10 : value); }
        }
    }
}
