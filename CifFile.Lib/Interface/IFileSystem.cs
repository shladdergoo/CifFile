using System.IO;

namespace CifFile.Lib
{
    public interface IFileSystem
    {
         bool DirectoryExists(string path);
         void CreateDirectory(string path);
         Stream FileOpen(string path, FileMode fileMode);
    }
}