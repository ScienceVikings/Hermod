using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Hermod.Core.Models;
using Hermod.Core.Services;
using Newtonsoft.Json;

namespace Hermod.UnitTests.Mocks
{
    public class FileNotifiedPostsPersister:INotifiedPostsPersister
    {
        public async Task SaveNotifiedPosts(IEnumerable<RssFeedItem> posts)
        {
            var feed = new RssFeed {Channel = new RssFeedChannel {Items = posts.ToList()}};

            var json = JsonConvert.SerializeObject(feed);
            await File.WriteAllTextAsync("TestFixtures/notified-posts.json", json);

        }
    }
}