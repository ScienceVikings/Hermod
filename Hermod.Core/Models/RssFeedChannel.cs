using System.Collections.Generic;
using System.Xml.Serialization;

namespace Hermod.Core.Models
{
    public class RssFeedChannel
    {
        [XmlElement("item")]
        public List<RssFeedItem> Items { get; set; }
    }
}