using System.IO;

namespace CifFile.Lib
{
    public interface IInputStreamFactory
    {
         Stream GetInputStream(string filename);
    }
}