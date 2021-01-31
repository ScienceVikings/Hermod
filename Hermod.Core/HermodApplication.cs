using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Hosting;

namespace Hermod.Core.Services
{
    public class HermodApplication
    {
        private readonly IRssReaderService _rssReaderService;

        public HermodApplication(IRssReaderService rssReaderService)
        {
            _rssReaderService = rssReaderService;
        }
        
        public async Task RunAsync()
        {
            var rssItems = await _rssReaderService.GetRssFeedItemsAsync();
            foreach (var item in rssItems)
            {
                Console.WriteLine($"----- {item.Title} -----");
                Console.WriteLine(item.Link);
                Console.WriteLine("----------");
            }
        }
    }
}