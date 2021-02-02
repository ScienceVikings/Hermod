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
        
        public override bool Equals(object obj)
        {
            if (obj == null)
                return false;

            if (obj == this)
                return true;

            return obj switch
            {
                string _ => Link.Equals(obj),
                RssFeedItem item => Link.Equals(item.Link),
                _ => false
            };
        }

        public override int GetHashCode()
        {
            return Link.GetHashCode();
        }

        public override string ToString()
        {
            return $"{Description} ${Link}";
        }
    }
}