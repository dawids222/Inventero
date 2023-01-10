using LibLite.Inventero.Core.Contracts.Tools;
using LibLite.Inventero.Core.Models.Tools;
using System.Text;

namespace LibLite.Inventero.Service.Tools
{
    public class FileSystem : IFileSystem
    {
        public async Task<FileModel> ReadFileAsync(string fullPath)
        {
            var file = new FileInfo(fullPath);
            var path = file.Directory.FullName;
            var fullName = file.Name;
            var splited = fullName.Split('.');
            var name = splited[0];
            var extension = splited[1];
            var content = await File.ReadAllTextAsync(fullPath);
            return new FileModel(path, name, extension, content);
        }

        public Task SaveFileAsync(FileModel file)
        {
            Directory.CreateDirectory(file.Path);
            var content = Encoding.UTF8.GetBytes(file.Content);
            return File.WriteAllBytesAsync(file.FullPath, content);
        }
    }
}
