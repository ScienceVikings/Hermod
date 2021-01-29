using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Hermod.Core.Models;

namespace Hermod.Core.Services
{
    public class RssReaderService : IRssReaderService
    {
        public Task<IEnumerable<RssFeedItem>> GetRssFeedItemsAsync(Uri uri)
        {
            throw new NotImplementedException();
        }
    }
}