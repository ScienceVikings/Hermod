using System.Collections.Generic;
using System.Threading.Tasks;
using Hermod.Core.Models;

namespace Hermod.Core.Services
{
    public class SocialMediaService:ISocialMediaService
    {
        private List<ISocialMediaSite> Sites { get; }

        public SocialMediaService()
        {
            Sites = new List<ISocialMediaSite>();
        }
        
        public async Task PostToSocialMedia(IEnumerable<RssFeedItem> items)
        {
            foreach (var site in Sites)
            {
                foreach (var item in items)
                {
                    await site.Post(item);
                }
            }
        }
    }
}