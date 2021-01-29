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
            hermod.ConfigureServices = serviceCollection =>
            {
                var feedProvider = serviceCollection.FirstOrDefault(d => d.ServiceType == typeof(IFeedProvider));
                if (feedProvider != null)
                {
                    serviceCollection.Remove(feedProvider);
                }

                serviceCollection.AddTransient<IFeedProvider, FileFeedProvider>();
            };
            return hermod;
        }
    }
}