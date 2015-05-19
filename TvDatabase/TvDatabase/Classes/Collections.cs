using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;

namespace TvDatabase.Classes
{
    /// <summary>
    /// Defines methods that will be contained by a list of objects with unique IDs.
    /// </summary>
    /// <typeparam name="DATA_TYPE">the data type of the objects of the list</typeparam>
    public interface IContainsUniqueIDs<DATA_TYPE>
    {
        int GetIndexOfID(string id);
        DATA_TYPE GetByID(string id);
        DATA_TYPE GetFirstByName(string name);
        string GetUniqueID(int length);
        string[] GetIDs();
        void SortBy(string criteria);
    }

    /// <summary>
    /// Defines a list of objects (of the specified data type) that implements the methods of the IContainsUniqueIDs interface.
    /// That is, the list contains items with unique IDs and offers several related utility methods.
    /// </summary>
    /// <typeparam name="DATA_TYPE">the data type of the objects of the list, which must be derived from ObjectWithName</typeparam>
    public abstract class BaseList<DATA_TYPE> : List<DATA_TYPE>, IContainsUniqueIDs<DATA_TYPE> where DATA_TYPE : ObjectWithName
    {
        /// <summary>Gets the list of string sorting criteria relevant to the list of this data type.</summary>
        public static string[] SortingCriteria { get { return null; } }

        /// <summary>Constructs a new, empty BaseList object.</summary>
        public BaseList()
            : base()
        {
        }

        /// <summary>Adds an item to the end of the current list, but only if it does not already contain an item with the same ID.</summary>
        /// <param name="item">the item to add to the list</param>
        public new void Add(DATA_TYPE item)
        {
            if (this.GetByID(item.ID) == null)
                base.Add(item);
        }

        /// <summary>Searches the current list for the item with the given ID.</summary>
        /// <param name="id">the ID of the item to search for</param>
        /// <returns>the zero-based index of the item with the given ID if found, -1 otherwise</returns>
        public int GetIndexOfID(string id)
        {
            for (int index = 0; index < this.Count; index++)
                if (this[index].ID.Equals(id))
                    return index;
            return -1;
        }

        /// <summary>Searches the current list for the item with the given ID.</summary>
        /// <param name="id">the ID of the item to search for</param>
        /// <returns>the item with the given ID if found, null otherwise</returns>
        public DATA_TYPE GetByID(string id)
        {
            foreach (DATA_TYPE item in this)
                if (item.ID.Equals(id))
                    return item;
            return null;
        }

        /// <summary>Searches the current list for the first item with the given name.</summary>
        /// <param name="id">the name of the item to search for</param>
        /// <returns>the first item with the given name if found, null otherwise</returns>
        public DATA_TYPE GetFirstByName(string name)
        {
            foreach (DATA_TYPE item in this)
                if (item.Name.Equals(name))
                    return item;
            return null;
        }

        /// <summary>Generates a new, unique string ID of the given length for this list.</summary>
        /// <param name="length">the character length of the new ID</param>
        /// <returns>a string object containing the new ID</returns>
        public string GetUniqueID(int length)
        {
            StringBuilder sb = new StringBuilder();
            Random random = new Random();
            do
            {
                sb.Clear();
                for (int i = 0; i < length; i++)
                    sb.Append((char) random.Next(65, 91));
            }
            while (this.GetByID(sb.ToString()) != null);
            return sb.ToString();
        }

        /// <summary>Generates a string array containing only the IDs of the items in the current list.</summary>
        /// <returns>a string array</returns>
        public string[] GetIDs()
        {
            string[] result = new string[this.Count];
            for (int i = 0; i < this.Count; i++)
                result[i] = this[i].ID;
            return result;
        }

        /// <summary>Sorts the current list by the specified criteria.</summary>
        /// <param name="criteria">the criteria by which to sort the list</param>
        public abstract void SortBy(string criteria);
    }
}
