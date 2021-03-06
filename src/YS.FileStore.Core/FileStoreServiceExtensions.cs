﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
namespace YS.FileStore
{
    public static class FileStoreServiceExtensions
    {
        public static async Task PutLocalFile(this IFileStoreService fileStoreService, string bucketName, string fileKey, string localFilePath)
        {
            using (var fileStream = new FileStream(localFilePath, FileMode.Open))
            {
                await fileStoreService.PutStream(bucketName, fileKey, fileStream);
            }
        }
        public static async Task PutJsonObject<T>(this IFileStoreService fileStoreService, string bucketName, string fileKey, T objectToStore)
        {
            using (var stream = new MemoryStream())
            {
                await JsonSerializer.SerializeAsync(stream, objectToStore);
                stream.Seek(0, SeekOrigin.Begin);
                await fileStoreService.PutStream(bucketName, fileKey, stream);
            }
        }
        public static async Task PutContent(this IFileStoreService fileStoreService, string bucketName, string fileKey, string content)
        {
            using (var stream = new MemoryStream(Encoding.UTF8.GetBytes(content ?? string.Empty)))
            {
                stream.Seek(0, SeekOrigin.Begin);
                await fileStoreService.PutStream(bucketName, fileKey, stream);
            }
        }

        public static async Task<T> GetJsonObject<T>(this IFileStoreService fileStoreService, string bucketName, string fileKey)
        {
            using (var stream = await fileStoreService.GetStream(bucketName, fileKey))
            {
                return await JsonSerializer.DeserializeAsync<T>(stream);
            }
        }
        public static async Task<string> GetContent(this IFileStoreService fileStoreService, string bucketName, string fileKey)
        {
            using (var stream = await fileStoreService.GetStream(bucketName, fileKey))
            {
                using (var reader = new StreamReader(stream, Encoding.UTF8))
                {
                    return await reader.ReadToEndAsync();
                }
            }
        }
        public static async Task SaveToLocalFile(this IFileStoreService fileStoreService, string bucketName, string fileKey, string localFilePath)
        {
            using (var fileStream = new FileStream(localFilePath, FileMode.CreateNew))
            {
                using (var stream = await fileStoreService.GetStream(bucketName, fileKey))
                {
                    await stream.CopyToAsync(fileStream);
                }
            }

        }
    }
}
