using System;
using System.IO;
using System.Net.Http;
using System.Threading.Tasks;

namespace Hermod.Core.Services
{
    public class FeedProvider:IFeedProvider
    {

        private readonly HttpClient _http;
        
        public FeedProvider(IHttpClientFactory httpClientFactory)
        {
            _http = httpClientFactory.CreateClient();
        }
        
        public async Task<Stream> GetFeed(Uri uri)
        {
            return await _http.GetStreamAsync(uri);
        }
    }
}