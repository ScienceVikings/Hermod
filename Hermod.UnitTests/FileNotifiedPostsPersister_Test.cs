using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Hermod.Core.Models;
using Hermod.Core.Services;
using Microsoft.Extensions.DependencyInjection;
using NUnit.Framework;
using Shouldly;

namespace Hermod.UnitTests
{
    public class FileNotifiedPostsPersister_Test
    {
        [OneTimeSetUp]
        public void OneTimeSetup()
        {
            File.Delete("TestFixtures/notified-posts.json");
            File.Copy("TestFixtures/notified-posts-base.json", "TestFixtures/notified-posts.json");
        }

        [Test]
        public async Task ShouldSaveDifferenceInPosts()
        {
            var hermod = TestHost.GetHost();
            var services = hermod.HostBuilder.Build().Services;
            var persister = services.GetService<INotifiedPostsPersister>();
            var provider = services.GetService<INotifiedPostsProvider>();

            var posts = (await provider.GetNotifiedPosts()).ToList();
            
            posts.Add(new RssFeedItem()
            {
                Description = "A post thing",
                Link= "https://sciencevikinglabs.com",
                Title = "Post Numero Uno"
            });
            
            posts.Add(new RssFeedItem()
            {
                Description = "A cooler post thing",
                Link= "https://sciencevikinglabs.com/anotherone",
                Title = "Second Post"
            });
            
            posts.Add(new RssFeedItem()
            {
                Description = "A final post thing",
                Link= "https://sciencevikinglabs.com/thefinalone",
                Title = "Da last one"
            });
            
            await persister.SaveNotifiedPosts(posts);

            var updatedPosts = await provider.GetNotifiedPosts();
            updatedPosts.Count().ShouldBe(7);
            
        }
    }
}