using System;
using System.Threading.Tasks;
using Amazon.Lambda.Core;
using Hermod.Core;
using Microsoft.Extensions.Hosting;

[assembly:LambdaSerializer(typeof(Amazon.Lambda.Serialization.Json.JsonSerializer))]
namespace Hermod
{
    public class LambdaFunction
    {
        public async Task Handler(string input, ILambdaContext lambdaContext)
        {
            var hermod = new HermodHost(null);
            await hermod.HostBuilder.Build().RunAsync();
        }
    }
}