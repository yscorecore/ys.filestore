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
        Task<string> Put([BucketRule] string bucket, [FileKeyRule] string fileKey, Stream content, IDictionary<string, string> tags = default);
        Task<string> GetUrl([BucketRule] string bucket, [FileKeyRule] string fileKey);
        Task<bool> Update([BucketRule] string bucket, [FileKeyRule] string fileKey, IDictionary<string, string> tags);
        Task<bool> Exists([BucketRule] string bucket, [FileKeyRule] string fileKey);
        Task<bool> DeleteByKey([BucketRule] string bucket, [FileKeyRule] string fileKey);
        Task<int> Count([BucketRule] string bucket, IDictionary<string, string> tags);
        Task<List<string>> ListKeys([BucketRule] string bucket, Dictionary<string, string> tags, [FileKeyRule(false)] string startFileKey = default);
        Task<IList<string>> DeleteByCondition([BucketRule] string bucket, Dictionary<string, string> tags, [FileKeyRule(false)] string startFileKey = default);
    }

}
