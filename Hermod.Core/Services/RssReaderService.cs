using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using System.Xml.Serialization;
using Hermod.Core.Models;

namespace Hermod.Core.Services
{
    public class RssReaderService : IRssReaderService
    {
        private readonly IFeedProvider _feedProvider;

        public RssReaderService(IFeedProvider feedProvider)
        {
            _feedProvider = feedProvider;
        }
        
        public async Task<IEnumerable<RssFeedItem>> GetRssFeedItemsAsync(Uri uri)
        {
            var xmlStream = await _feedProvider.GetFeed(uri);
            
            var serializer = new XmlSerializer(typeof(RssFeed));
            
            var feed = (RssFeed)serializer.Deserialize(xmlStream);

            return feed == null ? new List<RssFeedItem>() : feed.Channel.Items;
        }
    }
}