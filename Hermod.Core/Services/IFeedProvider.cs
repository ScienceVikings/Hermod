using System;
using System.IO;
using System.Threading.Tasks;

namespace Hermod.Core.Services
{
    public interface IFeedProvider
    {
        Task<Stream> GetFeed(Uri uri);
    }
}