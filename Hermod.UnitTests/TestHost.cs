using System.Linq;
using Hermod.Core;
using Hermod.Core.Services;
using Hermod.UnitTests.Mocks;
using Microsoft.Extensions.DependencyInjection;

namespace Hermod.UnitTests
{
    public class TestHost
    {
        public static HermodHost GetHost()
        {
            var hermod = new HermodHost(null);
            hermod.ConfigureServices = (context, serviceCollection) =>
            {
                var feedProvider = serviceCollection.FirstOrDefault(d => d.ServiceType == typeof(IFeedProvider));
                if (feedProvider != null)
                {
                    serviceCollection.Remove(feedProvider);
                }
                
                var postsProvider = serviceCollection.FirstOrDefault(d=>d.ServiceType == typeof(INotifiedPostsProvider));
                if (postsProvider != null)
                {
                    serviceCollection.Remove(postsProvider);
                }

                var postsPersister = serviceCollection.FirstOrDefault(d=>d.ServiceType == typeof(INotifiedPostsPersister));
                if (postsPersister != null)
                {
                    serviceCollection.Remove(postsPersister);
                }
                
                serviceCollection.AddTransient<IFeedProvider, FileFeedProvider>();
                serviceCollection.AddTransient<INotifiedPostsProvider, FileNotifiedPostsProvider>();
                serviceCollection.AddTransient<INotifiedPostsPersister, FileNotifiedPostsPersister>();
                
            };
            return hermod;
        }
    }
}