using System.Xml.Serialization;

namespace Hermod.Core.Models
{
    public class RssFeedItem
    {
        [XmlElement("title")]
        public string Title { get; set; }
        
        [XmlElement("description")]
        public string Description { get; set; }
        
        [XmlElement("link")]
        public string Link { get; set; }
        
    }
}