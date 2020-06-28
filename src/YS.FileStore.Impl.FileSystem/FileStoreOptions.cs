namespace YS.FileStore.Impl.FileSystem
{
    [YS.Knife.OptionsClass]
    public class FileStoreOptions
    {
        public string RootDir { get; set; } = "__FileStore__";
    }
}