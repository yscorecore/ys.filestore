using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace YS.FileStore.Impl.Tencent.Cos.UnitTest
{
    [TestClass]
    public class TestEnvironment
    {
        [AssemblyInitialize]
        public static void InitEnv(TestContext context)
        {
            //System.Environment.SetEnvironmentVariable("DOTNET_ENVIRONMENT","Development");
        }
        
    }
}