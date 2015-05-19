using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TvDatabase.Classes
{
    /// <summary>
    /// Defines an object that should be passed as argument to the update function. 
    /// Contains a reference to the database and a list of only the epViews to update.
    /// </summary>
    public class UpdateArgument
    {
        /// <summary>Gets the database object</summary>
        public Database Database { get; private set; }
        /// <summary>Gets the list of epViews to update</summary>
        public SeriesList SeriesToUpdate { get; private set; }

        /// <summary>Creates a new UpdateArgument object from the given parameters</summary>
        /// <param name="database">the database object</param>
        /// <param name="seriesToUpdate">the list of epViews to update</param>
        public UpdateArgument(Database database, SeriesList seriesToUpdate)
        {
            this.Database = database;
            this.SeriesToUpdate = seriesToUpdate;
        }
    }
}
