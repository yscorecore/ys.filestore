using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace YS.FileStore
{
    public interface IFileStoreService
    {
        Task<string> Put(Stream content, string fileKey, IDictionary<string, string> tags = default);
        Task<string> GetUrl(string fileKey);
        Task<bool> Update(string fileKey, IDictionary<string, string> tags);
        Task<bool> Exists(string fileKey);
        Task<bool> DeleteByKey(string fileKey);
        Task<int> Count(IDictionary<string, string> tags);
        Task<List<string>> ListKeys(Dictionary<string, string> tags, string startFileKey = default);
        Task<IList<string>> DeleteBytags(Dictionary<string, string> tags, string startFileKey = default);
    }

}
