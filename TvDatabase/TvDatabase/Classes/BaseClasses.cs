using System.Drawing;
using System.Xml;

namespace TvDatabase.Classes
{
    /// <summary>
    /// Defines methods for objects that should generate an XmlNode object containing their information.
    /// </summary>
    public interface IGeneratesXmlNode
    {
        XmlNode ToXML(XmlDocument doc, string nodeName);
    }

    /// <summary>
    /// An object that contains an ID field. The class is abstract.
    /// </summary>
    public abstract class ObjectWithID : IGeneratesXmlNode
    {
        /// <summary>Gets the ID of the current object.</summary>
        public string ID { get; private set; }

        /// <summary>Constructs a new ObjectWithID object from the given ID.</summary>
        /// <param name="id">the ID of the object</param>
        public ObjectWithID(string id)
        {
            this.ID = id;
        }
        
        /// <summary>Generates an XmlNode object containing the current object's information.</summary>
        /// <param name="doc">the XmlDocument object from which the node is to be created</param>
        /// <param name="nodeName">the name of the node</param>
        /// <returns>an XmlNode object</returns>
        public XmlNode ToXML(XmlDocument doc, string nodeName)
        {
            XmlNode resultNode = doc.CreateElement(nodeName);
            resultNode.Attributes.Append(Utils.GetXmlAttribute(doc, "ID", this.ID));
            return resultNode;
        }
    }

    /// <summary>
    /// An object that contains an ID field and a name field. The class is abstract.
    /// </summary>
    public abstract class ObjectWithName : ObjectWithID, IGeneratesXmlNode
    {
        /// <summary>Gets the name of the current object.</summary>
        public string Name { get; internal set; }
        
        /// <summary>Constructs a new ObjectWithName object from the given ID and name.</summary>
        /// <param name="id">the ID of the object</param>
        /// <param name="name">the name of the object</param>
        public ObjectWithName(string id, string name)
            : base(id)
        {
            this.Name = name;
        }
        
        /// <summary>Generates an XmlNode object containing the current object's information.</summary>
        /// <param name="doc">the XmlDocument object from which the node is to be created</param>
        /// <param name="nodeName">the name of the node</param>
        /// <returns>an XmlNode object</returns>
        public new XmlNode ToXML(XmlDocument doc, string nodeName)
        {
            XmlNode resultNode = base.ToXML(doc, nodeName);
            resultNode.Attributes.Append(Utils.GetXmlAttribute(doc, "name", this.Name));
            return resultNode;
        }
    }
}
