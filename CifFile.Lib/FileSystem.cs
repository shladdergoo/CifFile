using System.IO;

namespace CifFile.Lib
{
    public class FileSystem : IFileSystem
    {
        public void CreateDirectory(string path)
        {
            Directory.CreateDirectory(path);
        }

        public bool DirectoryExists(string path)
        {
            return Directory.Exists(path);
        }

        public Stream FileOpen(string path, FileMode fileMode)
        {
            return File.Open(path, fileMode);
        }
    }
}