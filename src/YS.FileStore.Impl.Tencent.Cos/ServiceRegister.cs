using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using YS.Knife;
using Tencent.QCloud.Cos;
using Tencent.QCloud.Cos.Sdk;
using COSXML;
using COSXML.Auth;
namespace YS.FileStore.Impl.Tencent.Cos
{
    public class ServiceRegister : YS.Knife.IServiceRegister
    {
        public void RegisteServices(IServiceCollection services, IRegisteContext context)
        {
            services.AddSingleton<CosXmlServer>();
            services.AddSingleton<CosXmlConfig>(sp =>
            {
                return new CosXmlConfig.Builder()
                .SetAppid("1259347065")
                .SetRegion("ap-nanjing")
                .SetDebugLog(true)
                .Build();
            });
            services.AddSingleton<QCloudCredentialProvider>(sp =>
            {
                var hostEnvironment = sp.GetRequiredService<Microsoft.Extensions.Hosting.IHostEnvironment>();
                string envName = hostEnvironment.EnvironmentName;
                bool isDevelopment = hostEnvironment.IsDevelopment();
                var qCloud = sp.GetRequiredService<QCloudOptions>();
                return new DefaultQCloudCredentialProvider(qCloud.SecretId, qCloud.SecretKey, qCloud.KeyDurationSecond);
            });
        }
    }
    
    [OptionsClass("QCloud:Cos")]
    public class CosOptions
    {

    }

    [OptionsClass()]
    public class QCloudOptions
    {
        public string SecretId { get; set; }
        public string SecretKey { get; set; }
        public int KeyDurationSecond { get; set; } = 60 * 10;
    }
}