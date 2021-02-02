using System;
using Amazon.S3;
using Hermod.Core.Services;
using Hermod.Core.Settings;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace Hermod.Core
{
    public class HermodHost
    {
        public IHostBuilder HostBuilder => GetHostBuilder();
        
        private IHostBuilder _hostBuilder;
        private readonly string[] _hostArguments;
        

        public Action<HostBuilderContext, IServiceCollection> ConfigureServices { get; set; }
        public Action<HostBuilderContext, IConfigurationBuilder> ConfigureApp { get; set; }

        public HermodHost(string[] args)
        {
            _hostArguments = args;
        }

        private IHostBuilder GetHostBuilder()
        {
            if (_hostBuilder != null)
                return _hostBuilder;
            
            _hostBuilder = Host.CreateDefaultBuilder(_hostArguments)
                .ConfigureAppConfiguration(ConfigureAppInternal)
                .ConfigureServices(ConfigureServicesInternal);

            if (ConfigureApp != null)
            {
                _hostBuilder.ConfigureAppConfiguration(ConfigureApp);
            }
            
            if (ConfigureServices != null)
            {
                _hostBuilder.ConfigureServices(ConfigureServices);
            }

            return _hostBuilder;

        }

        private void ConfigureAppInternal(HostBuilderContext context, IConfigurationBuilder config)
        {
            //config.Sources.Clear
            config.AddJsonFile("appsettings.json");
        }
        
        private static void ConfigureServicesInternal(HostBuilderContext context, IServiceCollection services)
        {

            var config = context.Configuration;
            
            services.AddTransient<HermodApplication>();
            services.AddHttpClient();
            services.Configure<NotifiedPostSettings>(config.GetSection("NotifiedPostSettings"));
            services.Configure<RssReaderSettings>(config.GetSection("RssReaderSettings"));

            services.AddAWSService<IAmazonS3>();
            
            services.AddSingleton<IFeedProvider, FeedProvider>();
            services.AddSingleton<INotifiedPostsProvider, NotifiedPostsProvider>();
            services.AddSingleton<IRssReaderService, RssReaderService>();
            services.AddSingleton<INewPostProvider, NewPostProvider>();
            services.AddSingleton<INotifiedPostsPersister, NotifiedPostsPersister>();
            services.AddSingleton<ISocialMediaService, SocialMediaService>();
        }
    }
}