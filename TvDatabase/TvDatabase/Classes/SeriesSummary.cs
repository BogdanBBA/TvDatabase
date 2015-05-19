using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TvDatabase.Classes
{
    /// <summary>
    /// Defines a summary of a epViews class, containing only the information to be used in the search function of the application.
    /// </summary>
    public class SeriesSummary : ObjectWithName
    {
        /// <summary>Gets the overview description of the epViews, or null if non-existant</summary>
        public string Overview { get; private set; }
        /// <summary>Gets the date when the epViews was first broadcast, or null if non-existant</summary>
        public DateTime? FirstAired{ get; private set; }

        /// <summary>Constructs a new Series object from the given parameters.</summary>
        /// <param name="id">the ID of the object</param>
        /// <param name="name">the name of the object</param>
        /// <param name="overview">the overview description of the epViews</param>
        /// <param name="firstAired">the date when the epViews was first broadcast</param>
        public SeriesSummary(string id, string name, string overview, DateTime? firstAired)
            : base(id, name)
        {
            this.Overview = overview;
            this.FirstAired = firstAired;
        }
    }
}
