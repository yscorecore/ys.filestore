using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
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
            var fileKey = "key";
            var tags = new Dictionary<string, string>();
            var content = default(string);
            File.WriteAllText(tempFile, "fake content.");
            Mock.Get(fileStoreService).Setup(p => p.Put(It.IsAny<Stream>(), fileKey, tags))
                .Callback<Stream, string, IDictionary<string, string>>((a, b, c) =>
                {
                    using (var reader = new StreamReader(a))
                    {
                        content = reader.ReadToEnd();
                    }
                });

            await fileStoreService.PutLocalFile(tempFile, fileKey, tags);
            Assert.AreEqual("fake content.", content);
        }

        [TestMethod]
        public async Task ShouldCallPutMethodWhenPutJsonObject()
        {
            var fileStoreService = Mock.Of<IFileStoreService>();
            var fileKey = "key";
            var tags = new Dictionary<string, string>();
            var content = default(string);
            Mock.Get(fileStoreService).Setup(p => p.Put(It.IsAny<Stream>(), fileKey, tags))
                .Callback<Stream, string, IDictionary<string, string>>((a, b, c) =>
                {
                    using (var reader = new StreamReader(a))
                    {
                        content = reader.ReadToEnd();
                    }
                });

            await fileStoreService.PutJsonObject(new { NM = "fake name." }, fileKey, tags);
            Assert.AreEqual("{\"NM\":\"fake name.\"}", content);
        }

        [TestMethod]
        public async Task ShouldCallPutMethodWhenPutContent()
        {
            var fileStoreService = Mock.Of<IFileStoreService>();
            var fileKey = "key";
            var tags = new Dictionary<string, string>();
            var content = default(string);
            Mock.Get(fileStoreService).Setup(p => p.Put(It.IsAny<Stream>(), fileKey, tags))
                .Callback<Stream, string, IDictionary<string, string>>((a, b, c) =>
                {
                    using (var reader = new StreamReader(a))
                    {
                        content = reader.ReadToEnd();
                    }
                });
            await fileStoreService.PutContent("fake content.", fileKey, tags);
            Assert.AreEqual("fake content.", content);
        }
    }
}
