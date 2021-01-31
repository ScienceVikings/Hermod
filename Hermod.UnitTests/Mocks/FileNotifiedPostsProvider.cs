using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using Hermod.Core.Models;
using Hermod.Core.Services;
using Newtonsoft.Json;

namespace Hermod.UnitTests.Mocks
{
    public class FileNotifiedPostsProvider : INotifiedPostsProvider
    {
        public async Task<IEnumerable<RssFeedItem>> GetNotifiedPosts()
        {
            var json = await File.ReadAllTextAsync("TestFixtures/notified-posts.json");
            var file = JsonConvert.DeserializeObject<NotifiedPostFile>(json);
            return file.Posts;
        }
    }
}