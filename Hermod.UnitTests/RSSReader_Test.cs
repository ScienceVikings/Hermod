using System;
using System.Linq;
using System.Threading.Tasks;
using Hermod.Core.Services;
using NUnit.Framework;
using Shouldly;

namespace Hermod.UnitTests
{
    public class RSSReader_Test
    {

        private IRssReaderService _srv = new RssReaderService();
        private Uri _realFeed = new Uri("file://TestFixtures/real_feed.xml");
        
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
            foreach (var item in res)
            {
                item.Description.ShouldNotBeNullOrWhiteSpace();
                item.Title.ShouldNotBeNullOrWhiteSpace();
                item.Link.ShouldNotBeNullOrWhiteSpace();
            }
        }
    }
}