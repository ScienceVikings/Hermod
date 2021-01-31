using System.Linq;
using System.Threading.Tasks;
using Hermod.Core.Services;
using Microsoft.Extensions.DependencyInjection;
using NUnit.Framework;
using Shouldly;

namespace Hermod.UnitTests
{
    public class NewPostProvider_Test
    {
        private INewPostProvider _srv;

        [SetUp]
        public void SetUp()
        {
            var hermod = TestHost.GetHost();
            var services = hermod.HostBuilder.Build().Services;
            _srv = services.GetService<INewPostProvider>();
            
        }

        [Test]
        public async Task ShouldReturnPostsThatHaveNotBeenPostedAlready()
        {
            (await _srv.GetNewPosts()).Count().ShouldBe(3);
        }
        
    }
}