using System;
using System.IO;

namespace CifFile.Lib
{
    public class InputStreamFactory : IInputStreamFactory
    {
        public Stream GetInputStream(string filename)
        {
            if(filename == null) throw new ArgumentNullException(nameof(filename));

            return new FileStream(filename, FileMode.Open, FileAccess.Read);
        }
    }
}