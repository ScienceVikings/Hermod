using System;
using Hermod.Core.Services;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace Hermod.Core
{
    public class HermodHost
    {
        public IHostBuilder HostBuilder => GetHostBuilder();
        
        private IHostBuilder _hostBuilder;
        private readonly string[] _hostArguments;

        public Action<IServiceCollection> ConfigureServices { get; set; }
        
        public HermodHost(string[] args)
        {
            _hostArguments = args;
        }

        private IHostBuilder GetHostBuilder()
        {
            if (_hostBuilder != null)
                return _hostBuilder;
            
            _hostBuilder = Host.CreateDefaultBuilder(_hostArguments)
                .ConfigureServices(ConfigureServicesInternal);

            if (ConfigureServices != null)
            {
                _hostBuilder.ConfigureServices(ConfigureServices);
            }

            return _hostBuilder;

        }
        
        private static void ConfigureServicesInternal(IServiceCollection services)
        {
            services.AddHostedService<HermodService>();
            services.AddHttpClient();

            services.AddTransient<IFeedProvider, FeedProvider>();
            services.AddTransient<IRssReaderService, RssReaderService>();
        }
    }
}