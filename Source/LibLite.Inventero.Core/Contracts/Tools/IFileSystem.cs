using LibLite.Inventero.Core.Models.Tools;

namespace LibLite.Inventero.Core.Contracts.Tools
{
    public interface IFileSystem
    {
        Task<FileModel> ReadFileAsync(string fullPath);
        Task SaveFileAsync(FileModel file);
    }
}
