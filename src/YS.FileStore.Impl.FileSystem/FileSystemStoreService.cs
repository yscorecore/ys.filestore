using System.IO;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;

namespace YS.FileStore.Impl.FileSystem
{
    [YS.Knife.ServiceClass(typeof(IFileStoreService), Lifetime = ServiceLifetime.Singleton)]
    public class FileSystemStoreService : IFileStoreService
    {
        private readonly FileStoreOptions fileStoreOptions;
        public FileSystemStoreService(FileStoreOptions fileStoreOptions)
        {
            this.fileStoreOptions = fileStoreOptions;
        }
        public Task<bool> Delete(string bucket, string fileKey)
        {
            string filePath = GetFilePath(bucket, fileKey);
            if (File.Exists(filePath))
            {
                File.Delete(filePath);
                return Task.FromResult(true);
            }
            return Task.FromResult(false);
        }

        public Task<bool> Exists(string bucket, string fileKey)
        {
            string filePath = GetFilePath(bucket, fileKey);
            return Task.FromResult(File.Exists(filePath));
        }

        public Task<Stream> GetStream(string bucket, string fileKey)
        {
            string filePath = GetFilePath(bucket, fileKey);
            return Task.FromResult<Stream>(File.OpenRead(filePath));
        }

        public async Task PutStream(string bucket, string fileKey, Stream content, bool forceCreateNew = false)
        {
            string filePath = GetFilePath(bucket, fileKey);
            var directoryPath = Path.GetDirectoryName(filePath);
            Directory.CreateDirectory(directoryPath);
            using (var fileStream = File.Open(filePath, forceCreateNew ? FileMode.Create : FileMode.CreateNew))
            {
                await content.CopyToAsync(content);
            }
        }

        private string GetFilePath(string bucket, string fileKey)
        {
            return Path.GetFullPath(Path.Combine(fileStoreOptions.RootDir, bucket, fileKey));
        }
    }
}