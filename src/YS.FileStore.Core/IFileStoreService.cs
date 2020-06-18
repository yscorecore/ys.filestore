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
        Task<string> Put(Stream content, [FileKeyRule] string fileKey, IDictionary<string, string> tags = default);
        Task<string> GetUrl([FileKeyRule] string fileKey);
        Task<bool> Update([FileKeyRule] string fileKey, IDictionary<string, string> tags);
        Task<bool> Exists([FileKeyRule] string fileKey);
        Task<bool> DeleteByKey([FileKeyRule] string fileKey);
        Task<int> Count(IDictionary<string, string> tags);
        Task<List<string>> ListKeys(Dictionary<string, string> tags, [FileKeyRule(false)] string startFileKey = default);
        Task<IList<string>> DeleteByCondition(Dictionary<string, string> tags, [FileKeyRule(false)] string startFileKey = default);
    }

}
