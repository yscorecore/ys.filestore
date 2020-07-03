using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Microsoft.Extensions.DependencyInjection;
using YS.Knife.Hosting;

namespace YS.FileStore.Core.UnitTest
{
    public abstract class FileStoreServiceTestBase : KnifeHost
    {
        public virtual string BucketName
        {
            get { return "fs-bucket"; }
        }
        public virtual string FileName
        {
            get { return "test/hello.txt"; }
        }

        [TestCleanup]
        public async Task ClearStoredFile()
        {
            var fileStoreService = this.GetRequiredService<IFileStoreService>();
            await fileStoreService.Delete(this.BucketName, this.FileName);
        }

        [TestMethod]
        public async Task ShouldPutAndGetContentSuccessfully()
        {
            var fileStoreService = this.GetRequiredService<IFileStoreService>();
            await fileStoreService.PutContent(this.BucketName, this.FileName, "Hello,World!");
            var content = await fileStoreService.GetContent(this.BucketName, this.FileName);
            Assert.AreEqual("Hello,World!", content);
        }

        [TestMethod]
        public async Task ShouldExistsAfterPut()
        {
            var fileStoreService = this.GetRequiredService<IFileStoreService>();
            Assert.IsFalse(await fileStoreService.Exists(this.BucketName,this.FileName));
            await fileStoreService.PutContent(this.BucketName, this.FileName, "Hello,World!");
            Assert.IsTrue(await fileStoreService.Exists(this.BucketName,this.FileName));
        }

        [TestMethod]
        public async Task ShouldNotExistsAfterDelete()
        {
            var fileStoreService = this.GetRequiredService<IFileStoreService>();
            Assert.IsFalse(await fileStoreService.Exists(this.BucketName,this.FileName));
            await fileStoreService.PutContent(this.BucketName, this.FileName, "Hello,World!");
            await fileStoreService.Delete(this.BucketName, this.FileName);
            Assert.IsFalse(await fileStoreService.Exists(this.BucketName,this.FileName));
        }
    }
}