using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Hermod.Core;
using Hermod.Core.Models;
using Hermod.Core.Services;
using Microsoft.Extensions.DependencyInjection;
using NUnit.Framework;
using Shouldly;

namespace Hermod.UnitTests
{
    public class RSSReader_Test
    {

        private IRssReaderService _srv;
        RssFeedItem _post = new RssFeedItem()
        {
            Link = "whatever"
        };
        
        [SetUp]
        public void SetUp()
        {
            var hermod = TestHost.GetHost();
            var services = hermod.HostBuilder.Build().Services;
            _srv = services.GetService<IRssReaderService>();
        }
        
        [Test]
        public async Task ShouldReturnCorrectNumberOfItems()
        {
            var res = await _srv.GetRssFeedItemsAsync();
            res.Count().ShouldBe(7);
        }

        [Test]
        public async Task ShouldFillDataOfItems()
        {
            var res = await _srv.GetRssFeedItemsAsync();
            res.Count().ShouldBeGreaterThan(0);
            foreach (var item in res)
            {
                item.Description.ShouldNotBeNullOrWhiteSpace();
                item.Title.ShouldNotBeNullOrWhiteSpace();
                item.Link.ShouldNotBeNullOrWhiteSpace();
            }
        }
        
        [Test]
        public void ShouldReturnFalseOnNull()
        {
            (_post == null).ShouldBeFalse();
        }
        
        [Test]
        public void ShouldReturnTrueOnSameObject()
        {
            _post.Equals(_post).ShouldBeTrue();
        }

        [Test]
        public void ShouldReturnFalseOnDifferentString()
        {
            _post.Equals("whatever").ShouldBeTrue();
            _post.Equals("not whatever").ShouldBeFalse();
        }
    }
}