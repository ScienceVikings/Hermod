using System;
using System.Threading.Tasks;
using Amazon.Lambda.Core;
using Hermod.Core;
using Hermod.Core.Services;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

[assembly:LambdaSerializer(typeof(Amazon.Lambda.Serialization.Json.JsonSerializer))]
namespace Hermod
{
    public class LambdaFunction
    {
        public async Task Handler(string input, ILambdaContext lambdaContext)
        {
            var hermod = new HermodHost(null);
            var service = hermod.HostBuilder.Build().Services.GetService<HermodApplication>();
            await service.RunAsync();
        }
    }
}