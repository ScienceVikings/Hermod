﻿using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using Amazon.S3;
using Amazon.S3.Model;
using Hermod.Core.Models;
using Hermod.Core.Settings;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;

namespace Hermod.Core.Services
{
    public class NotifiedPostsProvider : INotifiedPostsProvider
    {
        private readonly IAmazonS3 _s3Client;
        private readonly NotifiedPostSettings _settings;

        private RssFeed _posts = null;

        public NotifiedPostsProvider(IAmazonS3 s3Client, IOptions<NotifiedPostSettings> settings)
        {
            _s3Client = s3Client;
            _settings = settings.Value;
        }
        
        public async Task<IEnumerable<RssFeedItem>> GetNotifiedPosts()
        {
            if (_posts != null)
                return _posts.Channel.Items;
            
            var getRequest = new GetObjectRequest()
            {
                BucketName = _settings.BucketName,
                Key = _settings.Key
            };

            var response = await _s3Client.GetObjectAsync(getRequest);
            
            using var reader = new StreamReader(response.ResponseStream);
            var json = await reader.ReadToEndAsync();

            _posts = JsonConvert.DeserializeObject<RssFeed>(json);
            return _posts.Channel.Items;
        }
    }
}