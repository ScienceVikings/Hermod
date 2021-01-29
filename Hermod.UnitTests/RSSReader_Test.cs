using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Hermod.Core;
using Hermod.Core.Services;
using Microsoft.Extensions.DependencyInjection;
using NUnit.Framework;
using Shouldly;

namespace Hermod.UnitTests
{
    public class RSSReader_Test
    {

        private IRssReaderService _srv;
        private Uri _realFeed;

        [SetUp]
        public void SetUp()
        {
            var hermod = TestHost.GetHost();
            var services = hermod.HostBuilder.Build().Services;
            _srv = services.GetService<IRssReaderService>();
            _realFeed = new Uri(Path.Join(Directory.GetCurrentDirectory(), "TestFixtures/real_feed.xml"));
        }
        
        [Test]
        public async Task ShouldReturnCorrectNumberOfItems()
        {
            var res = await _srv.GetRssFeedItemsAsync(_realFeed);
            res.Count().ShouldBe(10);
        }

        [Test]
        public async Task ShouldFillDataOfItems()
        {
            var res = await _srv.GetRssFeedItemsAsync(_realFeed);
            res.Count().ShouldBeGreaterThan(0);
            foreach (var item in res)
            {
                item.Description.ShouldNotBeNullOrWhiteSpace();
                item.Title.ShouldNotBeNullOrWhiteSpace();
                item.Link.ShouldNotBeNullOrWhiteSpace();
            }
        }
    }
}