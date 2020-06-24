using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
namespace YS.FileStore.Core.UnitTest
{
    [TestClass]
    public class FileStoreServiceExtensionsTest
    {
        [TestMethod]
        public async Task ShouldCallPutMethodWhenPutLocalFile()
        {
            var fileStoreService = Mock.Of<IFileStoreService>();
            var tempFile = Path.GetTempFileName();
            var bucketName = "bucket";
            var fileKey = "key";
            var tags = new Dictionary<string, string>();
            var content = default(string);
            File.WriteAllText(tempFile, "fake content.");
            Mock.Get(fileStoreService).Setup(p => p.Put(bucketName, fileKey, It.IsAny<Stream>(), tags))
                .Callback<string, string, Stream, IDictionary<string, string>>((a, b, c, d) =>
                 {
                     using (var reader = new StreamReader(c))
                     {
                         content = reader.ReadToEnd();
                     }
                 });

            await fileStoreService.PutLocalFile(bucketName, fileKey, tempFile, tags);
            Assert.AreEqual("fake content.", content);
        }

        [TestMethod]
        public async Task ShouldCallPutMethodWhenPutJsonObject()
        {
            var fileStoreService = Mock.Of<IFileStoreService>();
            var bucketName = "bucket";
            var fileKey = "key";
            var tags = new Dictionary<string, string>();
            var content = default(string);
            Mock.Get(fileStoreService).Setup(p => p.Put(bucketName, fileKey, It.IsAny<Stream>(), tags))
                .Callback<string, string, Stream, IDictionary<string, string>>((a, b, c, d) =>
                {
                    using (var reader = new StreamReader(c))
                    {
                        content = reader.ReadToEnd();
                    }
                });

            await fileStoreService.PutJsonObject(bucketName, fileKey, new { NM = "fake name." }, tags);
            Assert.AreEqual("{\"NM\":\"fake name.\"}", content);
        }

        [TestMethod]
        public async Task ShouldCallPutMethodWhenPutContent()
        {
            var fileStoreService = Mock.Of<IFileStoreService>();
            var bucketName = "bucket";
            var fileKey = "key";
            var tags = new Dictionary<string, string>();
            var content = default(string);
            Mock.Get(fileStoreService).Setup(p => p.Put(bucketName, fileKey, It.IsAny<Stream>(), tags))
               .Callback<string, string, Stream, IDictionary<string, string>>((a, b, c, d) =>
               {
                   using (var reader = new StreamReader(c))
                   {
                       content = reader.ReadToEnd();
                   }
               });
            await fileStoreService.PutContent(bucketName, fileKey, "fake content.", tags);
            Assert.AreEqual("fake content.", content);
        }
    }
}
