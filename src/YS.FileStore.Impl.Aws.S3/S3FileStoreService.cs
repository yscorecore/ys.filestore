using System.Collections.Generic;
using System.IO;
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

        public Task<int> Count(string bucket, IDictionary<string, string> properties)
        {
            throw new System.NotImplementedException();
        }

        public Task<IList<string>> DeleteByCondition(string bucket, Dictionary<string, string> properties, string startFileKey = null)
        {
            throw new System.NotImplementedException();
        }

        public async Task<bool> DeleteByKey(string bucket, string fileKey)
        {
            var request = new DeleteObjectRequest
            {
                BucketName = bucket,
                Key = fileKey,
            };
            var response = await amazonS3.DeleteObjectAsync(request);
            return true;
        }

        public async Task<bool> Exists(string bucket, string fileKey)
        {
            
        
        }

        public Task<Stream> GetStream([BucketRule] string bucket, [FileKeyRule] string fileKey)
        {
            throw new System.NotImplementedException();
        }

        public Task<string> GetUrl(string bucket, string fileKey)
        {
            throw new System.NotImplementedException();
        }

        public Task<List<string>> ListKeys(string bucket, Dictionary<string, string> properties, [FileKeyRule(false)] string startFileKey = null)
        {
            throw new System.NotImplementedException();
        }

        public async Task<string> PutStream(string bucket, string fileKey, Stream content, IDictionary<string, string> properties = null)
        {
           AmazonS3Client s3Client;
           s3Client.url
            var request = new PutObjectRequest
            {
                BucketName = bucket,
                FilePath = fileKey,
                Key = fileKey,
                InputStream = content,
                ContentType="...",
            };
            var response =  await amazonS3.PutObjectAsync(request);

        }

        public Task<bool> Update(string bucket, string fileKey, IDictionary<string, string> properties)
        {
            throw new System.NotImplementedException();
        }

        private void AppendMetadataCollection(MetadataCollection metadata, IDictionary<string, string> dict)
        {
            if (dict != null)
            {
                foreach (var kv in dict)
                {
                    metadata.Add(kv.Key, kv.Value);
                }
            }
        }
        private string GetContentTypeByName()
        {
            return "";
        }
    }
}