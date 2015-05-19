using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace TvDatabase.Classes
{
    /// <summary>
    /// Defines filter settings to pass to a BaseList list object for it to filter.
    /// </summary>
    public abstract class Filter
    {
    }

    /// <summary>
    /// Defines filter settings to pass to a SeriesList object for it to filter.
    /// </summary>
    public class SeriesFilter : Filter
    {
        /// <summary>Gets or sets a value indicating whether the filter should only select active epViews.</summary>
        public CheckState ActiveSeries { get; set; }
        /// <summary>Gets or sets a list of days of the week that filtered epViews should be broadcasted on. Include null to select epViews of which the broadcasting day is unknown.</summary>
        public List<int?> DaysOfTheWeek { get; set; }
        /// <summary>Gets or sets the string sorting criteria for the selected epViews of this filter.</summary>
        public string SortingCriteria { get; set; }

        /// <summary>Constructs a new filter with the given attributes.</summary>
        public SeriesFilter(CheckState activeSeries, List<int?> daysOfTheWeek, string sortingCriteria)
            : base()
        {
            this.ActiveSeries = activeSeries;
            this.DaysOfTheWeek = daysOfTheWeek;
            this.SortingCriteria = sortingCriteria;
        }
    }
}
