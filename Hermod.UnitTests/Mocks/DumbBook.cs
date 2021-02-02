using System.IO;
using System.Threading.Tasks;
using Hermod.Core.Models;
using Hermod.Core.Services;

namespace Hermod.UnitTests.Mocks
{
    public class DumbBook : ISocialMediaSite
    {
        public async Task Post(RssFeedItem item)
        {
            await File.WriteAllTextAsync("dumb_book_post", item.ToString());
        }
    }
}