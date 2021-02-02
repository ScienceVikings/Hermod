using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using Amazon.S3;
using Amazon.S3.Model;
using Amazon.S3.Util;
using Hermod.Core;
using Hermod.Core.Models;
using Hermod.Core.Services;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using NUnit.Framework;
using Shouldly;

namespace Hermod.IntegrationTests
{
    public class NotifiedPostsPersister_Test
    {
        private IServiceProvider _services;
        private const string TestBucket = "hermod-integration-tests";
        
        [OneTimeSetUp]
        public async Task Setup()
        {
            var hermod = new HermodHost(null);
            _services = hermod.HostBuilder.Build().Services;
            var s3 = _services.GetService<IAmazonS3>();
            
            if (!await AmazonS3Util.DoesS3BucketExistV2Async(s3, TestBucket))
            {
                await s3.PutBucketAsync(TestBucket);
            }

        }

        [OneTimeTearDown]
        public async Task Cleanup()
        {
            var s3 = _services.GetService<IAmazonS3>();
            await s3.DeleteObjectAsync(TestBucket, "notified-posts.json");
        }
        
        [Test]
        public async Task ShouldUpdateNotifiedPosts()
        {
            var srv = _services.GetService<INotifiedPostsPersister>();

            var posts = new List<RssFeedItem>();

            posts.Add(new RssFeedItem()
            {
                Description = "A post thing",
                Link= "https://sciencevikinglabs.com",
                Title = "Post Numero Uno"
            });
            
            posts.Add(new RssFeedItem()
            {
                Description = "A cooler post thing",
                Link= "https://sciencevikinglabs.com/anotherone",
                Title = "Second Post"
            });
            
            posts.Add(new RssFeedItem()
            {
                Description = "A final post thing",
                Link= "https://sciencevikinglabs.com/thefinalone",
                Title = "Da last one"
            });
            
            srv.SaveNotifiedPosts(posts);
            
            var s3 = _services.GetService<IAmazonS3>();
            var getRequest = new GetObjectRequest()
            {
                BucketName = TestBucket,
                Key = "notified-posts.json"
            };

            var getResp = await s3.GetObjectAsync(getRequest);
            var reader = new StreamReader(getResp.ResponseStream);
            var json = reader.ReadToEnd();
            var feed = JsonConvert.DeserializeObject<RssFeed>(json);
            feed.Channel.Items.Count().ShouldBe(3);
            

        }
    }
}