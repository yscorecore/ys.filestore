using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using YS.Knife.Aop;

namespace YS.FileStore
{
    [ParameterValidation]
    public interface IFileStoreService
    {
        Task PutStream([BucketRule] string bucket, [FileKeyRule] string fileKey, Stream content, bool forceCreateNew = false);
        Task<Stream> GetStream([BucketRule] string bucket, [FileKeyRule] string fileKey);
        Task<bool> Exists([BucketRule] string bucket, [FileKeyRule] string fileKey);
        Task<bool> Delete([BucketRule] string bucket, [FileKeyRule] string fileKey);
    }

}
