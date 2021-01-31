using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Xml.Linq;
using Hermod.Core;
using Hermod.Core.Services;
using Hermod.UnitTests.Mocks;
using Microsoft.Extensions.DependencyInjection;
using NUnit.Framework;
using Shouldly;

namespace Hermod.UnitTests
{
    public class FeedProvider_Test
    {

        [Test]
        public async Task ShouldReturnAStreamFromWeb()
        {
            var hermod = new HermodHost(null);
            var services = hermod.HostBuilder.Build().Services;
            
            var srv = services.GetService<IFeedProvider>();
            srv.ShouldNotBeNull();
            
            await srv.GetFeed(new Uri("https://sciencevikinglabs.com/feed.xml")).ShouldNotBeNull();
        }
        
        [Test]
        public async Task ShouldReturnAStreamFromFile()
        {
            var hermod = TestHost.GetHost();
            var services = hermod.HostBuilder.Build().Services;
            
            var srv = services.GetService<IFeedProvider>();
            srv.ShouldNotBeNull();
            
            await srv.GetFeed(new Uri("file://TestFixtures/real_feed.xml")).ShouldNotBeNull();
        }
    }
}