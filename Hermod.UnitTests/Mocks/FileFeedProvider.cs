using System;
using System.IO;
using System.Threading.Tasks;
using Hermod.Core.Services;

namespace Hermod.UnitTests.Mocks
{
    public class FileFeedProvider : IFeedProvider
    {
        public Task<Stream> GetFeed(Uri uri)
        {
            return Task.FromResult<Stream>(
                File.OpenRead(Path.Join(Path.Join(Directory.GetCurrentDirectory(), uri.LocalPath))));
        }
    }
}