using System.IO;

namespace CifFile.Lib
{
    public class FileSystem : IFileSystem
    {
        public void CreateDirectory(string path)
        {
            if (path == null) { throw new System.ArgumentNullException(nameof(path)); }

            Directory.CreateDirectory(path);
        }

        public bool DirectoryExists(string path)
        {
            if (path == null) { throw new System.ArgumentNullException(nameof(path)); }

            return Directory.Exists(path);
        }

        public Stream FileOpen(string path, FileMode fileMode)
        {
            if (path == null) { throw new System.ArgumentNullException(nameof(path)); }

            return File.Open(path, fileMode);
        }
    }
}