using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Amazon.S3;
using Amazon.S3.Model;
using Hermod.Core.Models;
using Hermod.Core.Settings;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;

namespace Hermod.Core.Services
{
    public class NotifiedPostsPersister:INotifiedPostsPersister
    {
        private readonly IAmazonS3 _s3Client;
        private readonly NotifiedPostSettings _settings;

        public NotifiedPostsPersister(IAmazonS3 s3Client, IOptions<NotifiedPostSettings> settings)
        {
            _s3Client = s3Client;
            _settings = settings.Value;
        }
        
        public async Task SaveNotifiedPosts(IEnumerable<RssFeedItem> posts)
        {
            var feed = new RssFeed();
            feed.Channel= new RssFeedChannel();
            feed.Channel.Items = posts.ToList();

            var json = JsonConvert.SerializeObject(feed);
            
            var putRequest = new PutObjectRequest()
            {
                BucketName = _settings.BucketName,
                Key = _settings.Key,
                ContentType = "application/json",
                ContentBody = json
            };

            await _s3Client.PutObjectAsync(putRequest);
        }
    }
}