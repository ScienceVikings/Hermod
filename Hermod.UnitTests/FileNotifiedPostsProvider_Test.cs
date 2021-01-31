using System.Linq;
using System.Threading.Tasks;
using Hermod.Core.Services;
using Microsoft.Extensions.DependencyInjection;
using NUnit.Framework;
using Shouldly;

namespace Hermod.UnitTests
{
    public class FileNotifiedPostsProvider_Test
    {
        [Test]
        public async Task ShouldProvideNotifiedPostsFromFile()
        {
            var hermod = TestHost.GetHost();
            var services = hermod.HostBuilder.Build().Services;
            var prov =services.GetService<INotifiedPostsProvider>();
            (await prov.GetNotifiedPosts()).Count().ShouldBe(4);
        }
    }
}