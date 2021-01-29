using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using Hermod.Core.Models;

namespace Hermod.Core.Services
{
    public interface IRssReaderService
    {
        Task<IEnumerable<RssFeedItem>> GetRssFeedItemsAsync(Uri uri);
    }
}