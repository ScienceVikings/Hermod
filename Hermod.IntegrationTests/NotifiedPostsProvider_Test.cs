using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Amazon.S3;
using Amazon.S3.Model;
using Amazon.S3.Util;
using Hermod.Core;
using Hermod.Core.Services;
using Microsoft.Extensions.DependencyInjection;
using NUnit.Framework;
using Shouldly;

namespace Hermod.IntegrationTests
{
    [TestFixture]
    public class NotifiedPostsProvider_Test
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
            
            var json = await File.ReadAllTextAsync("TestFixtures/notified-posts.json");

            var putRequest = new PutObjectRequest()
            {
                BucketName = TestBucket,
                Key = "notified-posts.json",
                ContentType = "application/json",
                ContentBody = json
            };

            await s3.PutObjectAsync(putRequest);

        }

        [OneTimeTearDown]
        public async Task Cleanup()
        {
            var s3 = _services.GetService<IAmazonS3>();
            await s3.DeleteObjectAsync(TestBucket, "notified-posts.json");
        }

        [Test]
        public async Task ShouldGetNotifiedPostsFromS3()
        {
            var postsProvider = _services.GetService<INotifiedPostsProvider>();
            var posts = await postsProvider.GetNotifiedPosts();
            posts.Count().ShouldBe(4);
        }
    }
}