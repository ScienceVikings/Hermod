﻿using System.Collections.Generic;
using System.Threading.Tasks;
using Hermod.Core.Models;

namespace Hermod.Core.Services
{
    public interface INotifiedPostsPersister
    {
        Task SaveNotifiedPosts(IEnumerable<RssFeedItem> posts);
    }
}