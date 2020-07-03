using Microsoft.VisualStudio.TestTools.UnitTesting;
using Microsoft.Extensions.DependencyInjection;
using System.Threading.Tasks;
using YS.FileStore.Core.UnitTest;

namespace YS.FileStore.Impl.Tencent.Cos.UnitTest
{
    [TestClass]
    public class CosFileStoreServiceTest:FileStoreServiceTestBase
    {
        [TestMethod]
        public async Task TestMethod1()
        {
            var fileStoreService = this.GetRequiredService<IFileStoreService>();
            var exists = await fileStoreService.Exists("abc", "file.json");
            Assert.IsFalse(exists);
        }
        [TestMethod]
        public async Task TestMethod2()
        {
            var fileStoreService = this.GetRequiredService<IFileStoreService>();
            string bucket = "ypb-test-1259347065";
            string fileName = "abc/hello.txt";
            Assert.IsFalse(await fileStoreService.Exists(bucket, fileName));
            await fileStoreService.PutContent(bucket, fileName, "Hello,World!");
            Assert.IsTrue(await fileStoreService.Exists(bucket, fileName));
            var content = await fileStoreService.GetContent(bucket, fileName);
            Assert.AreEqual("Hello,World!", content);
            await fileStoreService.Delete(bucket, fileName);
            Assert.IsFalse(await fileStoreService.Exists(bucket, fileName));
        }
    }
}
