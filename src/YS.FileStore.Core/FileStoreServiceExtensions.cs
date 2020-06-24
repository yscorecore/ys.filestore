using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
namespace YS.FileStore
{
    public static class FileStoreServiceExtensions
    {
        public static async Task<string> PutLocalFile(this IFileStoreService fileStoreService, string bucketName, string fileKey, string localFilePath, IDictionary<string, string> tags = default)
        {
            using (var fileStream = new FileStream(localFilePath, FileMode.Open))
            {
                return await fileStoreService.Put(bucketName, fileKey, fileStream, tags);
            }
        }
        public static async Task<string> PutJsonObject<T>(this IFileStoreService fileStoreService, string bucketName, string fileKey, T objectToStore, IDictionary<string, string> tags = default)
        {
            using (var stream = new MemoryStream())
            {
                await JsonSerializer.SerializeAsync(stream, objectToStore);
                stream.Seek(0, SeekOrigin.Begin);
                return await fileStoreService.Put(bucketName, fileKey, stream, tags);
            }
        }
        public static async Task<string> PutContent(this IFileStoreService fileStoreService, string bucketName, string fileKey, string content, IDictionary<string, string> tags = default)
        {
            using (var stream = new MemoryStream(Encoding.UTF8.GetBytes(content ?? string.Empty)))
            {
                stream.Seek(0, SeekOrigin.Begin);
                return await fileStoreService.Put(bucketName, fileKey, stream, tags);
            }
        }
    }
}
