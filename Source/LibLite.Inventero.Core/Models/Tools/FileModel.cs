namespace LibLite.Inventero.Core.Models.Tools
{
    public class FileModel
    {
        public string Path { get; }
        public string Name { get; }
        public string Extension { get; }
        public string Content { get; }

        public string FullPath => $"{Path}\\{FullName}";
        public string FullName => $"{Name}.{Extension}";

        public FileModel(string path, string name, string extension, string content)
        {
            Path = path;
            Name = name;
            Extension = extension;
            Content = content;
        }
    }
}
