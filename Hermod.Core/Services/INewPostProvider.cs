﻿using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using Hermod.Core.Models;

namespace Hermod.Core.Services
{
    public interface INewPostProvider
    {
        Task<IEnumerable<RssFeedItem>> GetNewPosts();
    }
}