using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Hermod.Core.Models;

namespace Hermod.Core.Services
{
    public class NewPostProvider:INewPostProvider
    {
        private readonly IRssReaderService _rssReaderService;
        private readonly INotifiedPostsProvider _notifiedPostsProvider;

        public NewPostProvider(IRssReaderService rssReaderService, INotifiedPostsProvider notifiedPostsProvider)
        {
            _rssReaderService = rssReaderService;
            _notifiedPostsProvider = notifiedPostsProvider;
        }
        
        public async Task<IEnumerable<RssFeedItem>> GetNewPosts()
        {
            var feedItems = await _rssReaderService.GetRssFeedItemsAsync();
            var postedItems = await _notifiedPostsProvider.GetNotifiedPosts();

            var newPosts = feedItems.Except(postedItems);
            return newPosts;
        }
    }
}