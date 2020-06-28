using System.IO;
using System.Threading.Tasks;
using COSXML;
using COSXML.Model.Object;
using Microsoft.Extensions.DependencyInjection;
using Tencent.QCloud.Cos.Sdk;

namespace YS.FileStore.Impl.Tencent.Cos
{
    [YS.Knife.ServiceClass(typeof(IFileStoreService), Lifetime = ServiceLifetime.Singleton))]
    public class CosFileStoreService : IFileStoreService
    {
        readonly CosXmlServer server;
        public CosFileStoreService(CosXmlServer cosXmlServer)
        {
            this.server = cosXmlServer;
        }
        public Task<bool> Delete(string bucket, string fileKey)
        {
            var result = server.DeleteObject(new DeleteObjectRequest(bucket, fileKey));
            return Task.FromResult(true);
        }

        public Task<bool> Exists(string bucket, string fileKey)
        {
            var head = server.HeadObject(new HeadObjectRequest(bucket, fileKey));
            return Task.FromResult(true);
        }

        public Task<Stream> GetStream(string bucket, string fileKey)
        {
            var tempFile = Path.GetTempFileName();
            var obj = server.GetObject(new GetObjectRequest(bucket, fileKey, Path.GetDirectoryName(tempFile), Path.GetFileName(tempFile)));
            return Task.FromResult<Stream>(File.OpenRead(tempFile));
        }

        public async Task PutStream(string bucket, string fileKey, Stream content, bool forceCreateNew = false)
        {
            var tempFile = Path.GetTempFileName();
            using (var fileStream = File.Create(tempFile))
            {
                await content.CopyToAsync(fileStream);
            }
            server.PutObject(new PutObjectRequest(bucket, fileKey, tempFile));
        }
    }
}
