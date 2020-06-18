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
        public static async Task<string> Put(this IFileStoreService fileStoreService, string filePath, string fileKey, IDictionary<string, string> tags = default)
        {
            using (var fileStream = new System.IO.FileStream(filePath, FileMode.Open))
            {
                return await fileStoreService.Put(fileStream, fileKey, tags);
            }
        }
        public static async Task<string> PutJsonObject<T>(this IFileStoreService fileStoreService, T objectToStore, string fileKey, IDictionary<string, string> tags = default)
        {
            using (var stream = new MemoryStream())
            {
                await JsonSerializer.SerializeAsync(stream, objectToStore);
                stream.Seek(0, SeekOrigin.Begin);
                return await fileStoreService.Put(stream, fileKey, tags);
            }
        }
        public static async Task<string> PutContent(this IFileStoreService fileStoreService, string content, string fileKey, IDictionary<string, string> tags = default)
        {
            using (var stream = new MemoryStream(Encoding.UTF8.GetBytes(content ?? string.Empty)))
            {
                stream.Seek(0, SeekOrigin.Begin);
                return await fileStoreService.Put(stream, fileKey, tags);
            }
        }
    }
}
