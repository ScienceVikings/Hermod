using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using System.Xml.Serialization;
using Hermod.Core.Models;
using Hermod.Core.Settings;
using Microsoft.Extensions.Options;

namespace Hermod.Core.Services
{
    public class RssReaderService : IRssReaderService
    {
        private readonly IFeedProvider _feedProvider;
        private readonly RssReaderSettings _settings;
        private RssFeed _feed = null;
        
        public RssReaderService(IFeedProvider feedProvider, IOptions<RssReaderSettings> settings)
        {
            _feedProvider = feedProvider;
            _settings = settings.Value;
        }
        
        public async Task<IEnumerable<RssFeedItem>> GetRssFeedItemsAsync()
        {
            if (_feed != null)
                return _feed.Channel.Items;
                    
            var xmlStream = await _feedProvider.GetFeed(new Uri(_settings.FeedUrl));
            var serializer = new XmlSerializer(typeof(RssFeed));
            _feed = (RssFeed)serializer.Deserialize(xmlStream);

            return _feed == null ? new List<RssFeedItem>() : _feed.Channel.Items;
        }
    }
}