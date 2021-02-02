using System.Xml.Serialization;

namespace Hermod.Core.Models
{
    [XmlType("rss")]
    
    public class RssFeed
    {
        [XmlElement("channel")]
        public RssFeedChannel Channel { get; set; }
    }
}