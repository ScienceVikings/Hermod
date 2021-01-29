using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Hosting;

namespace Hermod.Core.Services
{
    public class HermodService : BackgroundService
    {
        private readonly IRssReaderService _rssReaderService;

        public HermodService(IRssReaderService rssReaderService)
        {
            _rssReaderService = rssReaderService;
        }
        
        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            var rssItems = await _rssReaderService.GetRssFeedItemsAsync(new Uri(""));
        }
    }
}