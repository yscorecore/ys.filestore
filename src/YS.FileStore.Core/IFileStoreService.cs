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
        Task<string> PutStream([BucketRule] string bucket, [FileKeyRule] string fileKey, Stream content, IDictionary<string, string> properties = default);
        Task<Stream> GetStream([BucketRule] string bucket, [FileKeyRule] string fileKey);
        Task<IDictionary<string, string>> GetProperties([BucketRule] string bucket, [FileKeyRule] string fileKey);
        Task<int> RemoveProperties([BucketRule] string bucket, [FileKeyRule] string fileKey, params string[] propertyNames);
        Task<int> UpdateProperties([BucketRule] string bucket, [FileKeyRule] string fileKey, IDictionary<string, string> properties);


        Task<bool> Exists([BucketRule] string bucket, [FileKeyRule] string fileKey);
        Task<bool> DeleteByKey([BucketRule] string bucket, [FileKeyRule] string fileKey);
        Task<int> Count([BucketRule] string bucket, IDictionary<string, string> properties);
        Task<List<string>> ListKeys([BucketRule] string bucket, Dictionary<string, string> properties, [FileKeyRule(false)] string startFileKey = default);
        Task<IList<string>> DeleteByCondition([BucketRule] string bucket, Dictionary<string, string> properties, [FileKeyRule(false)] string startFileKey = default);
    }

}
