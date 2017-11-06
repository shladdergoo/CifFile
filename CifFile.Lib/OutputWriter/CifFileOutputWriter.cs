using System;
using System.IO;
using System.Text;
using System.Collections.Generic;
using System.Linq;

namespace CifFile.Lib
{
    public class CifFileOutputWriter : IOutputWriter
    {
        private readonly IFileSystem _fileSystem;

        private StreamWriter _writer;

        public CifFileOutputWriter(IFileSystem fileSystem)
        {
            if (fileSystem == null) throw new ArgumentNullException(nameof(fileSystem));

            _fileSystem = fileSystem;
        }

        public void Open(string filename, ScheduleType scheduleType)
        {
            string outputDir = Path.GetDirectoryName(filename);

            if (!Directory.Exists(outputDir)) Directory.CreateDirectory(outputDir);

            FileStream stream = new FileStream(filename, FileMode.Create, FileAccess.Write);

            _writer = new StreamWriter(stream);
        }

        public void Write(IEnumerable<IEnumerable<string>> buffer)
        {
            if (buffer == null) throw new ArgumentNullException(nameof(buffer));
            if (_writer == null) throw new InvalidOperationException("Writer not open.");

            foreach (IEnumerable<string> lineStrings in buffer)
            {
                foreach (string line in lineStrings)
                {
                    _writer.WriteLine(line);
                }
            }

            _writer.Flush();
        }

        public void Close()
        {
            _writer.Flush();
            _writer.Dispose();
        }
    }
}