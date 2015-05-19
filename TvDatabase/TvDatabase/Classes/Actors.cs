using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace TvDatabase.Classes
{
    /// <summary>
    /// Defines an actor class, containing relevant information.
    /// </summary>
    public class ActingRole : Person
    {
        /// <summary>Gets the person which is acting</summary>
        public Person Actor { get; internal set; }
        /// <summary>Gets the name of the role</summary>
        public int SortOrder { get; internal set; }
        /// <summary>Gets the filename with extension of the associated role image, or null if non-existant</summary>
        public string ImageFilename { get; internal set; }

        /// <summary>Constructs a new ActingRole object from the given parameters.</summary>
        /// <param name="id">the ID of the object</param>
        /// <param name="name">the name of the role</param>
        /// <param name="actor">the person which is acting</param>
        /// <param name="sortOrder">the sorting order to be used when sorting acting roles</param>
        /// <param name="imageFilename">the filename with extension of the associated role image</param>
        public ActingRole(string id, string name, Person actor, int sortOrder, string imageFilename)
            : base(id, name)
        {
            this.Actor = actor;
            this.SortOrder = sortOrder;
            this.ImageFilename = File.Exists(Paths.ImagesFolder + imageFilename) ? imageFilename : null;
        }

        /// <summary>Generates an XmlNode object containing the current object's information.</summary>
        /// <param name="doc">the XmlDocument object from which the node is to be created</param>
        /// <param name="nodeName">the name of the node</param>
        /// <returns>an XmlNode object</returns>
        public new XmlNode ToXML(XmlDocument doc, string nodeName)
        {
            XmlNode resultNode = base.ToXML(doc, nodeName);
            resultNode.Attributes.Append(Utils.GetXmlAttribute(doc, "actorID", this.Actor.ID));
            resultNode.Attributes.Append(Utils.GetXmlAttribute(doc, "sortOrder", this.SortOrder));
            resultNode.Attributes.Append(Utils.GetXmlAttribute(doc, "imageFilename", Utils.EncodeSoCalledNullableString(this.ImageFilename)));
            return resultNode;
        }
    }

    /// <summary>
    /// Defines a list of acting roles, containing items with unique IDs and offering several related utility methods.
    /// </summary>
    public class ActingRoleList : BaseList<ActingRole>
    {
        /// <summary>Represents the list of string sorting criteria relevant to the list of this data type.</summary>
        private static readonly string[] sortingCriteria = new string[] { "ID", "Name" };
        /// <summary>Gets the list of string sorting criteria relevant to the list of this data type.</summary>
        public static new string[] SortingCriteria { get { return ActingRoleList.sortingCriteria; } }

        /// <summary>Constructs a new, empty ActingRoleList object.</summary>
        public ActingRoleList()
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
                        ActingRole aux = this[i];
                        this[i] = this[j];
                        this[j] = aux;
                    }
                }
        }
    }
}
