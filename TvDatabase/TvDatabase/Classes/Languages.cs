using System;

namespace TvDatabase.Classes
{
    /// <summary>
    /// Defines a language class, containing relevant information.
    /// </summary>
    public class Language : ObjectWithName
    {
        /// <summary>Constructs a new Language object from the given parameters.</summary>
        /// <param name="id">the ID of the object</param>
        /// <param name="name">the name of the object</param>
        public Language(string id, string name)
            : base(id, name)
        {
        }
    }
    
    /// <summary>
    /// Defines a list of languages, containing items with unique IDs and offering several related utility methods.
    /// </summary>
    public class LanguageList : BaseList<Language>
    {
        /// <summary>Represents the list of string sorting criteria relevant to the list of this data type.</summary>
        private static readonly string[] sortingCriteria = new string[] { "ID", "Name" };
        /// <summary>Gets the list of string sorting criteria relevant to the list of this data type.</summary>
        public static new string[] SortingCriteria { get { return LanguageList.sortingCriteria; } }
        
        /// <summary>Constructs a new, empty LanguageList object.</summary>
        public LanguageList()
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
                        Language aux = this[i];
                        this[i] = this[j];
                        this[j] = aux;
                    }
                }
        }
    }
}
