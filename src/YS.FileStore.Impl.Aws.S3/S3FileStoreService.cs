using System.Collections.Generic;
using System.IO;
using System;
using System.Threading.Tasks;
using YS.Knife.Aws.S3;
using Amazon.S3;
using Amazon.S3.Model;

namespace YS.FileStore.Impl.Aws.S3
{
    public class S3FileStoreService : IFileStoreService
    {
        readonly IAmazonS3 amazonS3;
        public S3FileStoreService(IAmazonS3 s3)
        {
            amazonS3 = s3;
        }

        public async Task<bool> Delete([BucketRule] string bucket, [FileKeyRule] string fileKey)
        {
            try
            {
                _ = await amazonS3.DeleteObjectAsync(bucket, fileKey);
                return true;
            }
            catch (AmazonS3Exception e) when (e.ErrorCode == "NoSuchBucket" || e.ErrorCode == "NotFound")
            {
                return false;
            }
        }

        public async Task<bool> Exists([BucketRule] string bucket, [FileKeyRule] string fileKey)
        {
            try
            {
                _ = await amazonS3.GetObjectMetadataAsync(bucket, fileKey);
                return true;
            }
            catch (AmazonS3Exception e) when (e.ErrorCode == "NoSuchBucket" || e.ErrorCode == "NotFound")
            {
                return false;
            }
        }

        public async Task<Stream> GetStream([BucketRule] string bucket, [FileKeyRule] string fileKey)
        {
            var getObjectRequest = new GetObjectRequest
            {
                BucketName = bucket,
                Key = fileKey
            };
            var response = await this.amazonS3.GetObjectAsync(getObjectRequest);
            return response.ResponseStream;
        }

        public async Task PutStream([BucketRule] string bucket, [FileKeyRule] string fileKey, Stream content, bool forceCreateNew = false)
        {
            if (!forceCreateNew && await this.Exists(bucket, fileKey))
            {
                throw new Exception($"This file '{fileKey}' already exists in bucket '{bucket}'.");
            }
            var pubObjectRequest = new PutObjectRequest
            {
                BucketName = bucket,
                FilePath = fileKey,
                Key = fileKey,
            };
            _ = await this.amazonS3.PutObjectAsync(pubObjectRequest);
        }
    }
}